import Api from "../../api";
const MAIN_END_POINT = "Camions/";

const Camions = {
  namespaced: true,
  state() {
    return {
        Camions: [],
    };
  },
  getters: {
    getCamions: (state) => state.Camions,
  },
  mutations: {
    setCamions: (state, payLoad) => (state.Camions = payLoad),
  },
  actions: {
    // Initialize Camions (Get all Camions)
    initCamions: ({ commit }) => {
      return new Promise((resolve, reject) => {
        Api.get(MAIN_END_POINT)
          .then((response) => {
            commit("setCamions", response.data);
            resolve(response.data);
          })
          .catch((error) => {
            reject(error);
          });
      });
    },

    // Add Camion
    addCamion: ({ commit },payload) => {
      console.log(payload);
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

    // Update Camion
    updateCamion: ({ commit }, payload) => {
      return new Promise((resolve, reject) => {
        Api.put(MAIN_END_POINT + payload.idCamion, payload)
          .then((response) => {
            resolve(response);
          })
          .catch((error) => {
            reject(error);
          });
      });
    },

    // Delete Camion
    deleteCamion: ({ commit }, id) => {
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
export default Camions;
