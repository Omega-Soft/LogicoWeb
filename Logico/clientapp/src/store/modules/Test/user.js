// import Api from "../../api"
// const MAIN_END_POINT = 'Users';

// const user= {
//     namespaced: true,
//     state () {
//         return {
//             Users: []
//         }
//     },
//     getters: {
//         getUsers: (state) => state.Users,
//     },
//     mutations:{
//         setUsers: (state, payLoad) => state.Users= payLoad,
//     },
//     actions:{
//         initUsers: ({commit}) => {
//             return new Promise((resolve, reject) => {
//                 Api.get(MAIN_END_POINT)
//                 .then((response) => {
//                     commit("setUsers", response.data);
//                     resolve(response.data);
//                 })
//                 .catch((error) => {
//                     reject(error);
//                 });
//             });
//         },
//     }
// };
// export default user