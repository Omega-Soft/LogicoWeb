import defaultLayout from "./layouts/side-nav-inner-toolbar";
import simpleLayout from "./layouts/single-card";
function loadView(view) {
    return () => import (`./views/${view}.vue`)
}
  
  export default [
    {
        path: "/home",
        name: "home",
        meta: {
          requiresAuth: true,
          layout: defaultLayout
        },
        component: loadView("home")
      },
      {
        name: "form",
        path: "/form/:action/:id",
        meta: { 
          requiresAuth: true,
          layout: defaultLayout
        },
        component: loadView("form")
      },
      {
        path: "/tickets",
        name: "ticket",
        meta: { 
          requiresAuth: true,
          layout: defaultLayout
        },
        component: loadView("Ticket")
      },
      {
        path: "/ticketStore",
        name: "ticketStore",
        meta: { 
          requiresAuth: true,
          layout: defaultLayout
        },
        component: loadView("TicketStore")
      },
      {
        path: "/login",
        name: "login-form",
        meta: {
          requiresAuth: false,
          layout: simpleLayout,
          title: "Sign In"
        },
        component: loadView("login-form")
      },
      {
        path: "/reset-password",
        name: "reset-password",
        meta: {
          requiresAuth: false,
          layout: simpleLayout,
          title: "Reset Password",
          description: "Please enter the email address that you used to register, and we will send you a link to reset your password via Email."
        },
        component: loadView("reset-password-form")
      },
      {
        path: "/create-account",
        name: "create-account",
        meta: {
          requiresAuth: false,
          layout: simpleLayout,
          title: "Sign Up"
        },
        component: loadView("create-account-form"),
      },
      {
        path: "/change-password/:recoveryCode",
        name: "change-password",
        meta: {
          requiresAuth: false,
          layout: simpleLayout,
          title: "Change Password"
        },
        component: loadView("change-password-form")
      },
      {
        path: "/",
        redirect: "/home"
      },
      {
        path: "/recovery",
        redirect: "/home"
      },
      {
        path: "/:pathMatch(.*)*",
        redirect: "/home"
      }
  ];