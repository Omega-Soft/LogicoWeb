import Api from "../../api";
const MAIN_END_POINT = "Groups/";

const Groups = {
  namespaced: true,
  state() {
    return {
      Groups: [],
    };
  },
  getters: {
    getGroups: (state) => state.Groups,
  },
  mutations: {
    setGroups: (state, payLoad) => (state.Groups = payLoad),
  },
  actions: {
    // Initialize Groups (Get all Groups)
    initGroups: ({ commit }) => {
      return new Promise((resolve, reject) => {
        Api.get(MAIN_END_POINT)
          .then((response) => {
            commit("setGroups", response.data);
            resolve(response.data);
          })
          .catch((error) => {
            reject(error);
          });
      });
    },

    // Add Group
    addGroup: ({ commit },payload) => {
      console.log(!!commit);
      console.log("Store........................");
      console.log(payload);
      return new Promise((resolve, reject) => {
        Api.post(MAIN_END_POINT, payload)
          .then((response) => {
            console.log("then/......");
            console.log(response);
            resolve(response);
          })
          .catch((error) => {
            console.log("catch/......");
            console.log(error);
            reject(error);
          });
      });
    },

    // Update Group
    updateGroup: ({ commit }, payload) => {
      console.log("Update...");
      console.log(payload);
      return new Promise((resolve, reject) => {
        Api.put(MAIN_END_POINT + payload.idGroup, payload)
          .then((response) => {
            resolve(response);
          })
          .catch((error) => {
            reject(error);
          });
      });
    },

    // Delete Group
    deleteGroup: ({ commit }, id) => {
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
export default Groups;
