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

    // Initialize BonReceptions LOT (Get BRs by Lot)
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

    // Generate Code BR
    generateCodeBR: ({ commit }) => {
      return new Promise((resolve, reject) => {
        Api.get(MAIN_END_POINT + "GenerateCodeBR")
          .then((response) => {
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
    
        let BR = {
          codeBr: getters.getCurrentBR.numBonReception || null,
          idCamion: getters.getCurrentBR.Camion || null,
          idFournisseur: getters.getCurrentBR.Fournisseur || null,
          idOrigine: getters.getCurrentBR.Origine || null,
          idProvenance: getters.getCurrentBR.Provenance || null,
          idTransporteur: getters.getCurrentBR.Transporteur || null,
          brute: getters.getCurrentBR.brute || null,
          idLot: getters.getCurrentBR.idLot || null,
          isNegos: getters.getCurrentBR.isNegos || null,
          nbCaisse: getters.getCurrentBR.nombreDeCaisses || null,
          nbPalette: getters.getCurrentBR.nombreDePalettes || null,
          nbonPese: getters.getCurrentBR.numeroBonPese || null,
          tare: getters.getCurrentBR.tare || null,
          dateBr: new Date(),
          _0301DetailsReceptionMps: getters.getCurrentBR.DetailsBR,
        };
        
        Api.post(MAIN_END_POINT, BR)
          .then((response) => {
            console.log("Store => Added...!!!");
            resolve(response);
          })
          .catch((error) => {
            console.log("Store => Catch...!!!");
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

    // Generate Code Lot
    generateCodeLot: ({ commit }, date) => {
      return new Promise((resolve, reject) => {
        Api.get(LOT_END_POINT + "GenerateCodeLot/" + date)
          .then((response) => {
            resolve(response.data);
          })
          .catch((error) => {
            reject(error);
          });
      });
    },

    // Add BonReception
    addLot: ({ commit }, payload) => {
      return new Promise((resolve, reject) => {
        Api.post(LOT_END_POINT, payload)
          .then((response) => {
            resolve(response);
          })
          .catch((error) => {
            reject(error);
          });
      });
    },

    // Prepare CurrentBR for Add/Edit
    prepareCurrentBR: ({ commit,dispatch }, payload) => {
      if (payload === "nouveau"){
        dispatch('generateCodeBR')
          .then((response) => {
            let BR = {
              numBonReception: response,
              DetailsBR:[]
            }
            commit("setCurrentBR", BR);
          });
      }
      else if (payload > 0){
        console.log(payload);
      }
    }

  },
};
export default BonReceptions;
