<template>
  <div>
    <h1>Hello {{ user.name }}!</h1>
    <div v-if="isAuth">
      <b-button @click="signOut">Sign out</b-button>
    </div>
  </div>
</template>

<script>
import userService from "../Services/UserService";
import { mapGetters, mapActions } from "vuex";
import { BButton } from "bootstrap-vue";
export default {
  created() {
    userService.get().then(({ data }) => (this.user = data));
  },
  components: {
    BButton,
  },
  data() {
    return {
      user: {
        username: "",
        name: "",
      },
    };
  },
  computed: {
    ...mapGetters(["isAuth"]),
  },
  methods: {
    ...mapActions(["AUTH_LOGOUT"]),
    signOut() {
      this.AUTH_LOGOUT().then(() => {
        this.$router.push("/login");
      });
    },
  },
};
</script>
