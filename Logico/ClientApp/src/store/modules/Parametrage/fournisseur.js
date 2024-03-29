import Api from "../../api";
const MAIN_END_POINT = "Fournisseurs/";

const Fournisseurs = {
  namespaced: true,
  state() {
    return {
        Fournisseurs: [],
    };
  },
  getters: {
    getFournisseurs: (state) => state.Fournisseurs,
  },
  mutations: {
    setFournisseurs: (state, payLoad) => (state.Fournisseurs = payLoad),
  },
  actions: {
    // Initialize Fournisseurs (Get all Fournisseurs)
    initFournisseurs: ({ commit }) => {
      return new Promise((resolve, reject) => {
        Api.get(MAIN_END_POINT)
          .then((response) => {
            commit("setFournisseurs", response.data);
            resolve(response.data);
          })
          .catch((error) => {
            reject(error);
          });
      });
    },

    // Add Fournisseur
    addFournisseur: ({ commit },payload) => {
      console.log(!!commit);
      return new Promise((resolve, reject) => {
        Api.post(MAIN_END_POINT, payload)
          .then((response) => {
            resolve(response);
          })
          .catch((error) => {
            reject(error);
          });
      });
    },

    // Update Fournisseur
    updateFournisseur: ({ commit }, payload) => {
      return new Promise((resolve, reject) => {
        Api.put(MAIN_END_POINT + payload.idFournisseur, payload)
          .then((response) => {
            resolve(response);
          })
          .catch((error) => {
            reject(error);
          });
      });
    },

    // Delete Fournisseur
    deleteFournisseur: ({ commit }, id) => {
      console.log(!!commit);
      return new Promise((resolve, reject) => {
        Api.delete(MAIN_END_POINT + id)
          .then((response) => {
            resolve(response);
          })
          .catch((error) => {
            reject(error);
          });
      });
    },
  },
};
export default Fournisseurs;
