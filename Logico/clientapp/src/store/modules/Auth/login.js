import Api from "../../api";
const END_POINT = 'login/';

const login = {
    namespaced: true,
    state () {
        return {
            signedUser: JSON.parse(localStorage.getItem("User")) || null,
            userPages: JSON.parse(localStorage.getItem("UserPages")) || null
        }
    },
    getters: {
        getSignedUser: (state) => state.signedUser,
        getUserPages: (state) => state.userPages
    },
    mutations: {
        setSignedUser: (state, payLoad) => state.signedUser = payLoad,
        setUserPages: (state, payLoad) => state.userPages = payLoad
    },
    actions: {
        Login: ({ commit }, creds) => {
            return new Promise((resolve, reject) => {            
                Api.post(END_POINT, creds)
                .then((response) => {
                    localStorage.setItem("UserToken", response.data.token);
                    localStorage.setItem("User", JSON.stringify(response.data.currentUser));
                    localStorage.setItem("UserPages", JSON.stringify(response.data.pages));
                    commit("setSignedUser", response.data.currentUser);
                    commit("setUserPages", response.data.pages);
                    Api.defaults.headers.common['Authorization'] = `Bearer ${response.data.token}`;
                    resolve(response.data);
                })
                .catch((error) => reject(error));
            })
        },
        Logout: ({ commit }) => {
            localStorage.removeItem('User');
            localStorage.removeItem('UserToken');
            localStorage.removeItem('UserPages');
            commit("setSignedUser", null);
            commit("setUserPages", null);
        }
    }
};
export default login