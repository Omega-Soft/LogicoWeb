import Api from "../../api";
const MAIN_END_POINT = "Tickets/";

const ticket = {
  namespaced: true,
  state() {
    return {
      Tickets: [],
      currentTicket: {
        titre: "",
        priorite: 1,
        statut: 1,
        user: 1,
        message: "",
      }
    };
  },
  getters: {
    getTickets: (state) => state.Tickets,
    getCurrentTicket: (state) => state.currentTicket,
  },
  mutations: {
    setTickets: (state, payLoad) => (state.Tickets = payLoad),
    setCurrentTicket: (state, payLoad) => (state.currentTicket = payLoad)
  },
  actions: {
    initTickets: ({ commit }) => {
      return new Promise((resolve, reject) => {
        Api.get(MAIN_END_POINT)
          .then((response) => {
            commit("setTickets", response.data);
            resolve(response.data);
          })
          .catch((error) => {
            reject(error);
          });
      });
    },
    addTicket: ({ getters }) => {
      var Ticket = {
        titre: getters.getCurrentTicket.titre,
        priorite: getters.getCurrentTicket.priorite,
        statut: getters.getCurrentTicket.statut,
        user: getters.getCurrentTicket.user,
        message:getters.getCurrentTicket.message
      };
      return new Promise((resolve, reject) => {
        Api.post(MAIN_END_POINT, Ticket)
          .then((response) => {
            resolve(response);
          })
          .catch((error) => {
            reject(error);
          });
      });
    },
    initDataForUpdate: ({commit}, payload) => {
      return new Promise((resolve, reject) => {
          // get next numBl
          Api.get(MAIN_END_POINT + payload)
          .then((response) => {
              commit("setCurrentTicket", response.data);
              resolve(response.data);
          })
          .catch((error) => {
              reject(error);
          });
      });
  },

    updateTicket: ({ getters }, id) => {
      var Ticket = {
        id,
        titre: getters.getCurrentTicket.titre,
        priorite: getters.getCurrentTicket.priorite,
        statut: getters.getCurrentTicket.statut,
        user: getters.getCurrentTicket.user,
        message:getters.getCurrentTicket.message
        // id: payLoad.data.id,
        // titre: payLoad.data.titre,
        // priorite: payLoad.data.priorite,
        // statut: payLoad.data.etat,
        // user: payLoad.data.utilisateur,
      };
      return new Promise((resolve, reject) => {

        Api.put(MAIN_END_POINT + id, Ticket)
          .then((response) => {
            resolve(response);
          })
          .catch((error) => {
            reject(error);
          });
      });
    },
    deleteTicket: ({ commit }, payLoad) => {
      console.log(!!commit);
      return new Promise((resolve, reject) => {
        Api.delete(MAIN_END_POINT + payLoad.data.id)
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
export default ticket;
