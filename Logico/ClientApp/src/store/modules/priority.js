import Api from "../api"
const MAIN_END_POINT = 'Priorites';

const priority = {
    namespaced: true,
    state () {
        return {
            Priorites: []
        }
    },
    getters: {
        getPriorites: (state) => state.Priorites,
    },
    mutations:{
        setPriorites: (state, payLoad) => state.Priorites = payLoad,
    },
    actions:{
        initPriorites: ({commit}) => {
            return new Promise((resolve, reject) => {
                Api.get(MAIN_END_POINT)
                .then((response) => {
                    commit("setPriorites", response.data);
                    resolve(response.data);
                })
                .catch((error) => {
                    reject(error);
                });
            });
        },
    }
};
export default priority