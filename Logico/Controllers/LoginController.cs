using Contracts;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Logico.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
         private ILoggerManager _logger;
        private IRepositoryWrapper _repository;
        private IConfiguration _config;

        public LoginController(ILoggerManager logger, IRepositoryWrapper repository, IConfiguration config)
        {
            _logger = logger;
            _repository = repository;
            _config = config;
        }


        // Cryptage de mot de passe
        public static string CalculateMD5Hash(string input)
        {
            // step 1, calculate MD5 hash from input
            MD5 md5 = System.Security.Cryptography.MD5.Create();
            byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
            byte[] hash = md5.ComputeHash(inputBytes);

            // step 2, convert byte array to hex string
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("x2"));
            }
            return sb.ToString();
        }


        // login
        [HttpPost]
        public IActionResult Login(LoginCreds creds)
        {
            IActionResult response;

            // l'utilisateur connecté, null si le login incorrect 
            WebUser currentUser = Authenticate(creds); // cette methods est pour verifier le login

            if (currentUser != null)
            {
                var tokenString = GenerateJWT(currentUser);
                response = Ok(new { token = tokenString, currentUser });
            }
            else response = Unauthorized("Username or password incorrect !!!");

            return response;
        }

        // authentication
        private WebUser Authenticate(LoginCreds creds)
        {
            var signedUser = _repository.User.GetByCondition(user =>
                user.Username == creds.username
                && user.Password == creds.password// CalculateMD5Hash(creds.password) // crypter le mot de passe
            ).FirstOrDefault();

            return signedUser;
        }

        // generate json web token
        private string GenerateJWT(WebUser user)
        {
            // recuperer la clé de génération dans appsetting.json
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));

            // SigningCredential Représente la clé cryptographique
            // et les algorithmes de sécurité utilisés pour générer une signature numérique.
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            // claims sont utilisées pour accéder aux ressources
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Username)
            };

            // generation de JWT token
            var token = new JwtSecurityToken(
                    issuer: _config["Jwt:Issuer"],
                    audience: _config["Jwt:Audience"],
                    claims,
                    expires: DateTime.Now.AddMonths(1),
                    signingCredentials: credentials
                );
            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);
            return tokenString;
        }

    }
}
