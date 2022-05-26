import Api from "../../api";
const MAIN_END_POINT = "Roles/";

const Roles = {
  namespaced: true,
  state() {
    return {
      Roles: [],
    };
  },
  getters: {
    getRoles: (state) => state.Roles,
  },
  mutations: {
    setRoles: (state, payLoad) => (state.Roles = payLoad),
  },
  actions: {
    // Initialize Roles (Get all Roles)
    initRoles: ({ commit }) => {
      return new Promise((resolve, reject) => {
        Api.get(MAIN_END_POINT)
          .then((response) => {
            commit("setRoles", response.data);
            resolve(response.data);
          })
          .catch((error) => {
            reject(error);
          });
      });
    },

    // Add Role
    addRole: ({ commit },payload) => {
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

    // Update Role
    updateRole: ({ commit }, payload) => {
      console.log("Update...");
      console.log(payload);
      return new Promise((resolve, reject) => {
        Api.put(MAIN_END_POINT + payload.idRole, payload)
          .then((response) => {
            resolve(response);
          })
          .catch((error) => {
            reject(error);
          });
      });
    },

    // Delete Role
    deleteRole: ({ commit }, id) => {
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
export default Roles;
