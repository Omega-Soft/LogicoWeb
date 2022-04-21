import Api from "../api"
const MAIN_END_POINT = 'Statuts';

const status= {
    namespaced: true,
    state () {
        return {
            Status: []
        }
    },
    getters: {
        getStatuts: (state) => state.Statuts,
    },
    mutations:{
        setStatuts: (state, payLoad) => state.Statuts= payLoad,
    },
    actions:{
        initStatuts: ({commit}) => {
            return new Promise((resolve, reject) => {
                Api.get(MAIN_END_POINT)
                .then((response) => {
                    commit("setStatuts", response.data);
                    resolve(response.data);
                })
                .catch((error) => {
                    reject(error);
                });
            });
        },
    }
};
export default status