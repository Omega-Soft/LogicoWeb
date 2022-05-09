import Api from "../../api";
const MAIN_END_POINT = "Moules/";

const Moules = {
  namespaced: true,
  state() {
    return {
        Moules: [],
    };
  },
  getters: {
    getMoules: (state) => state.Moules,
  },
  mutations: {
    setMoules: (state, payLoad) => (state.Moules = payLoad),
  },
  actions: {
    // Initialize Moules (Get all Moules)
    initMoules: ({ commit }) => {
      return new Promise((resolve, reject) => {
        Api.get(MAIN_END_POINT)
          .then((response) => {
            commit("setMoules", response.data);
            resolve(response.data);
          })
          .catch((error) => {
            reject(error);
          });
      });
    },

    // Add Moule
    addMoule: ({ commit },payload) => {
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

    // Update Moules
    updateMoule: ({ commit }, payload) => {
      return new Promise((resolve, reject) => {
        Api.put(MAIN_END_POINT + payload.idMoule, payload)
          .then((response) => {
            resolve(response);
          })
          .catch((error) => {
            reject(error);
          });
      });
    },

    // Delete Moule
    deleteMoule: ({ commit }, id) => {
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
export default Moules;
