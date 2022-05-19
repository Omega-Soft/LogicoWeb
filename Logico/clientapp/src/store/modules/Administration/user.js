import Api from "../../api";
const MAIN_END_POINT = "Users/";

const Users = {
  namespaced: true,
  state() {
    return {
      Users: [],
    };
  },
  getters: {
    getUsers: (state) => state.Users,
  },
  mutations: {
    setUsers: (state, payLoad) => (state.Users = payLoad),
  },
  actions: {
    // Initialize Users (Get all Users)
    initUsers: ({ commit }) => {
      return new Promise((resolve, reject) => {
        Api.get(MAIN_END_POINT)
          .then((response) => {
            commit("setUsers", response.data);
            resolve(response.data);
          })
          .catch((error) => {
            reject(error);
          });
      });
    },

    // Add User
    addUser: ({ commit },payload) => {
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

    // Update User
    updateUser: ({ commit }, payload) => {
      console.log("Update...");
      console.log(payload);
      return new Promise((resolve, reject) => {
        Api.put(MAIN_END_POINT + payload.idUser, payload)
          .then((response) => {
            resolve(response);
          })
          .catch((error) => {
            reject(error);
          });
      });
    },

    // Delete User
    deleteUser: ({ commit }, id) => {
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
export default Users;
