import Api from "../../api";
const MAIN_END_POINT = "Qualites/";

const Qualites = {
  namespaced: true,
  state() {
    return {
        Qualites: [],
    };
  },
  getters: {
    getQualites: (state) => state.Qualites,
  },
  mutations: {
    setQualites: (state, payLoad) => (state.Qualites = payLoad),
  },
  actions: {
    // Initialize Qualites (Get all Qualites)
    initQualites: ({ commit }) => {
      return new Promise((resolve, reject) => {
        Api.get(MAIN_END_POINT)
          .then((response) => {
            commit("setQualites", response.data);
            resolve(response.data);
          })
          .catch((error) => {
            reject(error);
          });
      });
    },

    // Add Qualite
    addQualite: ({ commit },payload) => {
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

    // Update Qualite
    updateQualite: ({ commit }, payload) => {
      return new Promise((resolve, reject) => {
        Api.put(MAIN_END_POINT + payload.idQualite, payload)
          .then((response) => {
            resolve(response);
          })
          .catch((error) => {
            reject(error);
          });
      });
    },

    // Delete Qualite
    deleteQualite: ({ commit }, id) => {
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
export default Qualites;
