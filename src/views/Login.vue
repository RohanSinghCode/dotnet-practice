<template>
  <div>
    <b-container>
      <div v-if="authStatus == 'loading'">
        <b-spinner />
      </div>
      <b-form @submit.prevent="handleSubmit">
        <b-row align-h="center">
          <b-col cols="8">
            <b-form-group label="Username" label-for="username">
              <b-form-input
                id="username"
                type="text"
                placeholder="Enter username"
                v-model="user.username"
                required
              />
            </b-form-group>
          </b-col>
          <b-col cols="8">
            <b-form-group label="Password" label-for="password">
              <b-form-input
                id="password"
                type="password"
                placeholder="Enter password"
                v-model="user.password"
                required
              />
            </b-form-group>
          </b-col>
          <b-col cols="8">
            <b-button type="submit" variant="primary">Login</b-button>
          </b-col>
        </b-row>
      </b-form>
    </b-container>
  </div>
</template>
<script>
import {
  BForm,
  BFormGroup,
  BFormInput,
  BContainer,
  BRow,
  BCol,
  BButton,
  BSpinner,
} from "bootstrap-vue";
import { mapActions, mapGetters } from "vuex";
export default {
  components: {
    BForm,
    BFormGroup,
    BFormInput,
    BContainer,
    BRow,
    BCol,
    BButton,
    BSpinner,
  },
  data() {
    return {
      user: {
        username: "",
        password: "",
      },
    };
  },
  methods: {
    ...mapActions(["AUTH_REQ"]),
    handleSubmit() {
      this.AUTH_REQ(this.user).then(() => {
        this.$router.push("/").catch(() => {});
      });
    },
  },
  computed: {
    ...mapGetters(["authStatus"]),
  },
};
</script>
