import Api from "../../api";
const MAIN_END_POINT = "Transporteurs/";

const Transporteurs = {
  namespaced: true,
  state() {
    return {
        Transporteurs: [],
    };
  },
  getters: {
    getTransporteurs: (state) => state.Transporteurs,
  },
  mutations: {
    setTransporteurs: (state, payLoad) => (state.Transporteurs = payLoad),
  },
  actions: {
    // Initialize Transporteurs (Get all Transporteurs)
    initTransporteurs: ({ commit }) => {
      return new Promise((resolve, reject) => {
        Api.get(MAIN_END_POINT)
          .then((response) => {
            commit("setTransporteurs", response.data);
            resolve(response.data);
          })
          .catch((error) => {
            reject(error);
          });
      });
    },

    // Add Transporteur
    addFTransporteur: ({ commit },payload) => {
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

    // Update Transporteur
    updateFTransporteur: ({ commit }, payload) => {
      return new Promise((resolve, reject) => {
        Api.put(MAIN_END_POINT + payload.idTransporteur, payload)
          .then((response) => {
            resolve(response);
          })
          .catch((error) => {
            reject(error);
          });
      });
    },

    // Delete Transporteur
    deleteTransporteur: ({ commit }, id) => {
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
export default Transporteurs;
