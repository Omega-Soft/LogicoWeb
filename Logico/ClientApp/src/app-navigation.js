export default [
  {
    name: "DSB",
    text: "Dashboard",
    path: "/dashboard",
    icon: "home",
  },

  {
    name: "ADMIN",
    text: "Administration",
    icon: "key",
    items: [
      {
        text: "Utilisateurs",
        icon: "",
        path: "/administration/users",
      },
      {
        text: "Groupes Utilisateurs",
        icon: "",
        path: "/administration/groupes",
      },
      {
        text: "Droits d'Accéss",
        icon: "",
        path: "/administration/droits-access",
      },
      {
        text: "Interfaces d'Application",
        icon: "",
        path: "/administration/pages",
      },
    ],
  },

  {
    name: "PRMT",
    text: "Paramétrage",
    icon: "preferences",
    items: [
      {
        text: "Article",
        path: "/parametrage/article",
      },
      {
        text: "Moule",
        path: "/parametrage/Moule",
      },
      {
        text: "Qualité",
        path: "/parametrage/qualite",
      },
      {
        text: "Fournisseur",
        path: "/parametrage/Fournisseur",
      },
      {
        text: "Bateau",
        path: "/parametrage/Bateau",
      },
      {
        text: "Origine",
        path: "/parametrage/Origine",
      },
      {
        text: "Provenance",
        path: "/parametrage/Provenance",
      },
      {
        text: "Camion",
        path: "/parametrage/Camion",
      },
      {
        text: "Transporteur",
        path: "/parametrage/Transporteur",
      },
    ],
  },

  {
    name: "RCMP",
    text: "Réception MP",
    icon: "columnchooser",
    items: [
      {
        text: "Bon Réception",
        path: "/list-bon-receptions"
      },
      {
        text:"Lot Réception",
        path: "/lot-receptions"
      },
    ],
  },

  {
    name: "FCTR",
    text: "Facturation",
    icon: "money",
    path: "/facturation"
  },
  
  {
    name: "TKT",
    text: "Gestion Test",
    icon: "folder",
    items: [
      {
        text: "Test",
        path: "/tickets",
      },
      {
        text: "TestStore",
        path: "/ticketStore",
      },
    ],
  },
];
