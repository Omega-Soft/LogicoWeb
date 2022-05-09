import Api from "../../api";
const MAIN_END_POINT = "Provenances/";

const Provenances = {
  namespaced: true,
  state() {
    return {
        Provenances: [],
    };
  },
  getters: {
    getProvenances: (state) => state.Provenances,
  },
  mutations: {
    setProvenances: (state, payLoad) => (state.Provenances = payLoad),
  },
  actions: {
    // Initialize Provenances (Get all Provenances)
    initProvenances: ({ commit }) => {
      return new Promise((resolve, reject) => {
        Api.get(MAIN_END_POINT)
          .then((response) => {
            commit("setProvenances", response.data);
            resolve(response.data);
          })
          .catch((error) => {
            reject(error);
          });
      });
    },

    // Add Provenance
    addProvenance: ({ commit },payload) => {
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

    // Update Provenance
    updateProvenance: ({ commit }, payload) => {
      return new Promise((resolve, reject) => {
        Api.put(MAIN_END_POINT + payload.idProvenance, payload)
          .then((response) => {
            resolve(response);
          })
          .catch((error) => {
            reject(error);
          });
      });
    },

    // Delete Provenance
    deleteProvenance: ({ commit }, id) => {
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
export default Provenances;
