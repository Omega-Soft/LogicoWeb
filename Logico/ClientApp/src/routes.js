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