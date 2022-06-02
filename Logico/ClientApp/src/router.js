import {
  createRouter,
  createWebHashHistory,
  createWebHistory,
} from "vue-router";
import routes from "./routes";
import { store } from "./store";

const router = new createRouter({
  routes,
  history: createWebHistory(),
});

router.beforeEach((to, from, next) => {
  const logged = !!store.state.login.signedUser;
  const pages = JSON.parse(localStorage.getItem("UserPages")) || [];
  var mapObj = {
    ADMIN: "administration",
    PRMT: "parametrage",
    RCMP: "reception",
    TKT: "ticket",
  };
  let names = pages.map((p) => {
    return p.page.replace(/ADMIN|PRMT|RCMP|TKT/gi, function (matched) {
      return mapObj[matched];
    });
  });

  let hasAccess = false;
  names.forEach((e) => {
    if (to.path.includes(e)) {
      hasAccess = true;
    }
  });

  if (to.name === "login-form" && logged) {
    next({ name: "home" });
  }

  if (to.matched.some((record) => record.meta.requiresAuth)) {
    if (!logged) {
      next({
        name: "login-form",
        query: { redirect: to.fullPath },
      });
    } else if (hasAccess || to.name == "home") {
      next();
    } else {
      next({ name: "home" });
    }
  } else {
    next();
  }
});

export default router;
