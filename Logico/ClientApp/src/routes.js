import defaultLayout from "./layouts/side-nav-inner-toolbar";
import simpleLayout from "./layouts/single-card";
function loadView(view) {
  return () => import(`./views/${view}.vue`);
}

export default [
  {
    path: "/home",
    name: "home",
    meta: {
      requiresAuth: true,
      layout: defaultLayout,
    },
    component: loadView("home"),
  },
  {
    path: "/parametrage/article",
    name: "article",
    meta: {
      requiresAuth: true,
      layout: defaultLayout,
    },
    component: loadView("Parametrage/Article"),
  },
  {
    name: "form",
    path: "/form/:action/:id",
    meta: {
      requiresAuth: true,
      layout: defaultLayout,
    },
    component: loadView("Test/form"),
  },
  {
    path: "/tickets",
    name: "ticket",
    meta: {
      requiresAuth: true,
      layout: defaultLayout,
    },
    component: loadView("Test/Ticket"),
  },
  {
    path: "/ticketStore",
    name: "ticketStore",
    meta: {
      requiresAuth: true,
      layout: defaultLayout,
    },
    component: loadView("Test/TicketStore"),
  },
  {
    path: "/login",
    name: "login-form",
    meta: {
      requiresAuth: false,
      layout: simpleLayout,
      title: "Sign In",
    },
    component: loadView("login-form"),
  },
  {
    path: "/",
    redirect: "/home",
  },
  {
    path: "/recovery",
    redirect: "/home",
  },
  {
    path: "/:pathMatch(.*)*",
    redirect: "/home",
  },
];
