import { createRouter, createWebHashHistory, createWebHistory } from "vue-router";
import routes from "./routes"
import {store} from "./store"




const router = new createRouter({
  routes,
  history: createWebHistory()
});

// router.beforeEach((to, from, next) => {
//   const logged = !!store.state.login.signedUser;

//   if (to.name === "login-form" && logged) {
//     next({ name: "home" });
//   }

//   if (to.matched.some(record => record.meta.requiresAuth)) {
//     if (!logged) {
//       next({
//         name: "login-form",
//         query: { redirect: to.fullPath }
//       });
//     } else {
//       next();
//     }
//   }
//    else {
//     next();
//   }
// });

export default router;
