<template>
  <form class="login-form" @submit.prevent="onSubmit" >
            

    <dx-form :form-data="formData"  :disabled="loading">
      <dx-item
        data-field="username"
        editor-type="dxTextBox"
        :editor-options="{
          stylingMode: 'filled',
          placeholder: 'Identifiant',
          mode: 'text', 
        }"
      >
        <dx-required-rule message="Username requis!" />
        <dx-label :visible="false" />
      </dx-item>

      <dx-item
        data-field="password"
        editor-type="dxTextBox"
        :editor-options="{
          stylingMode: 'filled',
          placeholder: 'Mot de passe',
          mode: 'password',
        }"
      >
        <dx-required-rule message="Mot de passe requis!" />
        <dx-label :visible="false" />
      </dx-item>

      <dx-button-item>
        <dx-button-options
          width="100%"
          type="default"
          template="signInTemplate"
          :use-submit-behavior="true"
        >
        </dx-button-options>
      </dx-button-item>       
      <template #signInTemplate>
        <div>
          <span class="dx-button-text">
            <dx-load-indicator
              v-if="loading"
              width="24px"
              height="24px"
              :visible="true"
            />
            <span v-if="!loading">Se connecter</span>
          </span>
        </div>
      </template>
    </dx-form>
  </form>
</template>

<script>
import DxLoadIndicator from "devextreme-vue/load-indicator";
import DxForm, {
  DxItem,
  DxRequiredRule,
  DxLabel,
  DxButtonItem,
  DxButtonOptions,
} from "devextreme-vue/form";
import notify from "devextreme/ui/notify";

import { reactive } from "vue";

import { mapActions } from "vuex";

export default {
  data() {
    return {
      formData: reactive({
        username: "",
        password: "",
      }),
      loading: false,
    };
  },
  methods: {
    ...mapActions({
      loginAction: "login/Login",
    }),
    onSubmit: function () {
      //  this.loading = true;
      console.log(this.formData);
      this.loginAction(this.formData)
        .then(() => {
          this.$router.push("/home");
        })
        .catch(() => {
          this.loading = false;
          notify("Nom d'utilisateur ou mot de passe incorrect", "error", 2000);
        });
    },
  },
  components: {
    DxLoadIndicator,
    DxForm,
    DxRequiredRule,
    DxItem,
    DxLabel,
    DxButtonItem,
    DxButtonOptions,
  },
};
</script>

<style lang="scss">
@import "../themes/generated/variables.base.scss";

.login-form {
  .link {
    text-align: center;
    font-size: 16px;
    font-style: normal;

    a {
      text-decoration: none;
    }
  }

  .form-text {
    margin: 10px 0;
    color: rgba($base-text-color, alpha($base-text-color) * 0.7);
  }

}
</style>
