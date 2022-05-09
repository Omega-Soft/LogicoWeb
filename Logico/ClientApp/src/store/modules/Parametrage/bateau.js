import Api from "../../api";
const MAIN_END_POINT = "Bateaux/";

const Bateaux = {
  namespaced: true,
  state() {
    return {
        Bateaux: [],
    };
  },
  getters: {
    getBateaux: (state) => state.Bateaux,
  },
  mutations: {
    setBateaux: (state, payLoad) => (state.Bateaux = payLoad),
  },
  actions: {
    // Initialize Bateaux (Get all Bateaux)
    initBateaux: ({ commit }) => {
      return new Promise((resolve, reject) => {
        Api.get(MAIN_END_POINT)
          .then((response) => {
            commit("setBateaux", response.data);
            resolve(response.data);
          })
          .catch((error) => {
            reject(error);
          });
      });
    },

    // Add Bateau
    addBateau: ({ commit },payload) => {
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

    // Update Bateau
    updateBateau: ({ commit }, payload) => {
      return new Promise((resolve, reject) => {
        Api.put(MAIN_END_POINT + payload.idBateau, paylad)
          .then((response) => {
            resolve(response);
          })
          .catch((error) => {
            reject(error);
          });
      });
    },

    // Delete Bateau
    deleteBateau: ({ commit }, id) => {
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
export default Bateaux;
