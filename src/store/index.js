import Vue from "vue";
import Vuex from "vuex";
import userService from "../Services/UserService";


Vue.use(Vuex);

export default new Vuex.Store({
  state: {
    token: localStorage.getItem("token") || "",
    status: '',
  },
  getters: {
    isAuth: state => !!state.token,
    authStatus: state => state.status
  },
  actions: {
    AUTH_REQ: ({ commit }, userCred) => {
      return new Promise((resolve, reject) => {
        commit("AUTH_REQ");
        userService.login(userCred).then(({ data }) => {
          localStorage.setItem("token", data);
          commit("AUTH_SUCCESS", data);
          resolve(data);
        }).catch(err => {
          localStorage.removeItem("token");
          reject(err)
        })
      })

    },
    AUTH_LOGOUT({ commit }) {
      commit("AUTH_LOGOUT");
      localStorage.removeItem("token");
    }
  },
  mutations: {
    AUTH_SUCCESS(state, token) {
      state.status = "success"
      state.token = token
    },
    AUTH_LOGOUT(state) {
      state.token = "";
    },
    AUTH_REQ(state) {
      state.status = "loading"
    }
  },

});
