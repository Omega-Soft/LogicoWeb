import Api from "../../api";
const MAIN_END_POINT = "Pages/";

const Pages = {
  namespaced: true,
  state() {
    return {
      Pages: [],
    };
  },
  getters: {
    getPages: (state) => state.Pages,
  },
  mutations: {
    setPages: (state, payLoad) => (state.Pages = payLoad),
  },
  actions: {
    // Initialize Pages (Get all Pages)
    initPages: ({ commit }) => {
      return new Promise((resolve, reject) => {
        Api.get(MAIN_END_POINT)
          .then((response) => {
            commit("setPages", response.data);
            resolve(response.data);
          })
          .catch((error) => {
            reject(error);
          });
      });
    },

    // Add Page
    addPage: ({ commit },payload) => {
      console.log(!!commit);
      console.log(payload);
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

    // Update Page
    updatePage: ({ commit }, payload) => {
      console.log("Update...");
      console.log(payload);
      return new Promise((resolve, reject) => {
        Api.put(MAIN_END_POINT + payload.idPage, payload)
          .then((response) => {
            resolve(response);
          })
          .catch((error) => {
            reject(error);
          });
      });
    },

    // Delete Page
    deletePage: ({ commit }, id) => {
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
export default Pages;
