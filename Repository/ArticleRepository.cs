﻿using Contracts;
using Entities.Models;

namespace Repository
{
    public class ArticleRepository : RepositoryBase<_0400Article>, IArticleRepository
    {
        public ArticleRepository(LogicoWebContext repositoryContext)
            : base(repositoryContext)
        {
        }

        public IEnumerable<Object> GetAllArticles()
        {
            return GetAll()
                .Select(x => new
                {
                    x.IdArticle,
                    x.CodeArticle,
                    x.Designation
                })
                .ToList();
        }

        public _0400Article GetArticleById(int Id)
        {
            return GetByCondition(x => x.IdArticle.Equals(Id)).FirstOrDefault();
        }



    }
}