import Vue from "vue";
import VueRouter from "vue-router";
import Home from "../views/Home.vue"
import store from "../store";

Vue.use(VueRouter);
const isAuthenticated = (to, from, next) => {
  if (store.getters.isAuth) {
    next();
    return;
  }
  next("/login")
}

const isNotAuthenticated = (to, from, next) => {
  if (!store.getters.isAuth) {
    next();
    return;
  }
  next("/");
}



const routes = [
  {
    path: "/",
    component: Home,
    beforeEnter: isAuthenticated
  },
  {
    path: "/login",
    component: () => import("../views/Login.vue"),
    beforeEnter: isNotAuthenticated
  }
];

const router = new VueRouter({
  mode: "history",
  base: process.env.BASE_URL,
  routes,
});

export default router;
