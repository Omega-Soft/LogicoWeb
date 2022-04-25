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
    initArticles: ({ commit }) => {
      return new Promise((resolve, reject) => {
        Api.get(MAIN_END_POINT + "all")
          .then((response) => {
            commit("setArticles", response.data);
            resolve(response.data);
          })
          .catch((error) => {
            reject(error);
          });
      });
    },
    addArticle: ({ commit }, payload) => {
      console.log(!!commit);

      var article = {
        codeArticle: payload.code,
        designation: payload.article
      };

      return new Promise((resolve, reject) => {
        Api.post(MAIN_END_POINT, article)
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
