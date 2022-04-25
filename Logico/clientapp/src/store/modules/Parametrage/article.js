import Api from "../../api";
const MAIN_END_POINT = "Articles/";

const Articles = {
  namespaced: true,
  state() {
    return {
      Articles: [],
    };
  },
  getters: {
    getArticles: (state) => state.Articles,
  },
  mutations: {
    setArticles: (state, payLoad) => (state.Articles = payLoad),
  },
  actions: {
    // Initialize Articles (Get all articles)
    initArticles: ({ commit }) => {
      return new Promise((resolve, reject) => {
        Api.get(MAIN_END_POINT)
          .then((response) => {
            commit("setArticles", response.data);
            resolve(response.data);
          })
          .catch((error) => {
            reject(error);
          });
      });
    },

    // Add Article
    addArticle: ({ commit },payload) => {
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

    // Update Article
    updateArticle: ({ commit }, payload) => {
      return new Promise((resolve, reject) => {
        Api.put(MAIN_END_POINT + payload.idArticle, payload)
          .then((response) => {
            resolve(response);
          })
          .catch((error) => {
            reject(error);
          });
      });
    },

    // Delete Article
    deleteArticle: ({ commit }, id) => {
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
export default Articles;
