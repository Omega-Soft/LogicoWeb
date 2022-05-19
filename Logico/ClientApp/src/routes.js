import defaultLayout from "./layouts/side-nav-inner-toolbar";
import simpleLayout from "./layouts/single-card";
function loadView(view) {
  return () => import(`./views/${view}.vue`);
}

export default [
// Dashboard ----------------------------------------------------------------------
  {
    path: "/home",
    name: "home",
    meta: {
      requiresAuth: true,
      layout: defaultLayout,
    },
    component: loadView("home"),
  },

// Administration ----------------------------------------------------------------
  {
    path: "/administration/users",
    name: "users",
    meta: {
      requiresAuth: true,
      layout: defaultLayout,
    },
    component: loadView("Administration/User"),
  },
  {
    path: "/administration/groupes",
    name: "groupes",
    meta: {
      requiresAuth: true,
      layout: defaultLayout,
    },
    component: loadView("Administration/GroupUsers"),
  },
  {
    path: "/administration/droits-access",
    name: "droits-access",
    meta: {
      requiresAuth: true,
      layout: defaultLayout,
    },
    component: loadView("Administration/DroitsAccess"),
  },

// Parametrage ----------------------------------------------------------------
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
    path: "/parametrage/fournisseur",
    name: "fournisseur",
    meta: {
      requiresAuth: true,
      layout: defaultLayout,
    },
    component: loadView("Parametrage/Fournisseur"),
  },
  {
    path: "/parametrage/moule",
    name: "moule",
    meta: {
      requiresAuth: true,
      layout: defaultLayout,
    },
    component: loadView("Parametrage/Moule"),
  },
  {
    path: "/parametrage/qualite",
    name: "qualite",
    meta: {
      requiresAuth: true,
      layout: defaultLayout,
    },
    component: loadView("Parametrage/Qualite"),
  },
  {
    path: "/parametrage/Bateau",
    name: "bateau",
    meta: {
      requiresAuth: true,
      layout: defaultLayout,
    },
    component: loadView("Parametrage/Bateau"),
  },
  {
    path: "/parametrage/Camion",
    name: "camion",
    meta: {
      requiresAuth: true,
      layout: defaultLayout,
    },
    component: loadView("Parametrage/Camion"),
  },
  {
    path: "/parametrage/Transporteur",
    name: "transporteur",
    meta: {
      requiresAuth: true,
      layout: defaultLayout,
    },
    component: loadView("Parametrage/Transporteur"),
  },
  {
    path: "/parametrage/Origine",
    name: "origine",
    meta: {
      requiresAuth: true,
      layout: defaultLayout,
    },
    component: loadView("Parametrage/Origine"),
  },
  {
    path: "/parametrage/Provenance",
    name: "provenance",
    meta: {
      requiresAuth: true,
      layout: defaultLayout,
    },
    component: loadView("Parametrage/Provenance"),
  },

// Bon Reception MP ------------------------------------------------------------------------------------
  {
    path: "/list-bon-receptions",
    name: "list-bon-receptions",
    meta: {
      requiresAuth: true,
      layout: defaultLayout,
    },
    component: loadView("BonReception/ListBR"),
  },

// Test ------------------------------------------------------------------------------------------------
  {
    path: "/lot-receptions",
    name: "lot-receptions",
    meta: {
      requiresAuth: true,
      layout: defaultLayout,
    },
    component: loadView("BonReception/LotBR"),
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

// Login -----------------------------------------------------------------------------------------------
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

// Matching --------------------------------------------------------------------------------------------
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
