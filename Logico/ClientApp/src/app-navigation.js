export default [
  {
    text: "Home",
    path: "/home",
    icon: "home",
  },

  {
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
    ],
  },

  {
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
