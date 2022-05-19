import Api from "../../api";
const MAIN_END_POINT = "BonReceptions/";
const LOT_END_POINT = "LotMPs/";

const BonReceptions = {
  namespaced: true,
  state() {
    return {
      BonReceptions: [],
      CurrentBR: {
        BR:{},
        DetailsBR:[]
      },
      Lots: []
    };
  },
  getters: {
    getBonReceptions: (state) => state.BonReceptions,
    getCurrentBR: (state) => state.CurrentBR,
    getLots: (state) => state.Lots,
  },
  mutations: {
    setBonReceptions: (state, payLoad) => (state.BonReceptions = payLoad),
    setCurrentBR: (state, payLoad) => (state.CurrentBR = payLoad),
    setLots: (state, payLoad) => (state.Lots = payLoad),
  },
  actions: {
    // Initialize BonReceptions (Get all BonReceptions)
    initBonReceptions: ({ commit }) => {
      return new Promise((resolve, reject) => {
        Api.get(MAIN_END_POINT)
          .then((response) => {
            commit("setBonReceptions", response.data);
            resolve(response.data);
          })
          .catch((error) => {
            reject(error);
          });
      });
    },


    initBonReceptionLot: ({ commit },idLot) => {
      return new Promise((resolve, reject) => {
        Api.get(MAIN_END_POINT + "Lot/" + idLot)
          .then((response) => {
            commit("setBonReceptions", response.data);
            resolve(response.data);
          })
          .catch((error) => {
            reject(error);
          });
      });
    },
    // Initialize LOTs (Get all LOTs)
    initLots: ({ commit }) => {
      return new Promise((resolve, reject) => {
        Api.get(LOT_END_POINT)
          .then((response) => {
            commit("setLots", response.data);
            resolve(response.data);
          })
          .catch((error) => {
            reject(error);
          });
      });
    },

    // Add BonReception
    addBonReception: ({ getters }) => {
      return new Promise((resolve, reject) => {
        Api.post(MAIN_END_POINT, getters.getCurrentBR)
          .then((response) => {
            resolve(response);
          })
          .catch((error) => {
            reject(error);
          });
      });
    },

    // Update BonReception
    updateBonReception: ({ getters }) => {
      return new Promise((resolve, reject) => {
        Api.put(MAIN_END_POINT + getters.getCurrentBR.idBonReception, getters.getCurrentBR)
          .then((response) => {
            resolve(response);
          })
          .catch((error) => {
            reject(error);
          });
      });
    },

    // Delete BonReception
    deleteBonReception: ({ commit }, idBR) => {
      console.log(!!commit);
      return new Promise((resolve, reject) => {
        Api.delete(MAIN_END_POINT + idBR)
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
export default BonReceptions;
