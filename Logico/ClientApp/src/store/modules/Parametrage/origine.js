import Api from "../../api";
const MAIN_END_POINT = "Origines/";

const Origines = {
  namespaced: true,
  state() {
    return {
        Origines: [],
    };
  },
  getters: {
    getOrigines: (state) => state.Origines,
  },
  mutations: {
    setOrigines: (state, payLoad) => (state.Origines = payLoad),
  },
  actions: {
    // Initialize Origines (Get all Origines)
    initOrigines: ({ commit }) => {
      return new Promise((resolve, reject) => {
        Api.get(MAIN_END_POINT)
          .then((response) => {
            commit("setOrigines", response.data);
            resolve(response.data);
          })
          .catch((error) => {
            reject(error);
          });
      });
    },

    // Add Origine
    addOrigine: ({ commit },payload) => {
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

    // Update Origine
    updateOrigine: ({ commit }, payload) => {
      return new Promise((resolve, reject) => {
        Api.put(MAIN_END_POINT + payload.idOrigine, payload)
          .then((response) => {
            resolve(response);
          })
          .catch((error) => {
            reject(error);
          });
      });
    },

    // Delete Origine
    deleteOrigine: ({ commit }, id) => {
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
export default Origines;
