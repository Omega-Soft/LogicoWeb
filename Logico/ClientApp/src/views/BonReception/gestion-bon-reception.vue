<template>
    <form id="form-container" class="content-block">
      <DxForm id="form" :col-count="3">
        <DxItem :col-span="2">
          <template #default>
            <div>
              <DxButton
                icon="arrowleft"
                type="default"
                styling-mode="text"
                @click="this.$router.go(-1)"
                style="
                  margin-bottom: 15px;
                  padding-right: 0px;
                  border-right-width: 5px;
                  margin-right: 5px;
                "
              />
              <h2 style="display: contents">
                {{ action }} Un Bon De Réception
              </h2>
            </div>
          </template>
        </DxItem>
        <DxButtonItem
          :button-options="buttonOptions"
          horizontal-alignment="right"
        />
      </DxForm>
    </form>


<div class="center">
  <div class="dx-field">
          <DxTextBox
            label="Code Lot MP"
            label-mode="floating"
            width="200"
            v-model:value="codeLot"
            :read-only="!insertMode"
          />
          <div class="margin"></div>

        <DxDateBox
        label="Date Lot MP"
        width="200"
        display-expr="journee"
        label-mode="floating"
        value-expr="idLot"
        v-model:value="dateLot"
        type="date"
        :read-only="!insertMode"
      />
      </div>
</div>
      

    
   
  <div>
    <form
      id="form-container"
      class="content-block"
      @submit.prevent="customBR()"
    >
      <DxForm :form-data="getCurrentBR" id="form" :col-count="4">
        <DxItem
          :editor-options="{ value: agenerateCodeBR, disabled: true }"
          data-field="acodeBr"
        />
        <DxItem
          :editor-options="{
            value: getCurrentBR.BR.idFournisseur,
            searchEnabled: true,
            items: get,
            displayExpr: 'raisonSociale',
            valueExpr: 'idFournisseur',
          }"
          data-field="idFournisseur"
          editor-type="dxSelectBox"
        >
          <DxRequiredRule message="Fournisseur est obligatoire" />
        </DxItem>

        <DxItem
          :editor-options="{
            value: getCurrentBR.BR.idOrigine,
            searchEnabled: true,
            items: get,
            displayExpr: 'designation',
            valueExpr: 'idOrigine',
          }"
          data-field="idOrigine"
          editor-type="dxSelectBox"
        >
          <DxRequiredRule message="L'origine est obligatoire" />
        </DxItem>

        <DxItem
          :editor-options="{
            value: getCurrentBR.BR.idCamion,
            searchEnabled: true,
            items: get,
            displayExpr: 'matricule',
            valueExpr: 'idCamion',
          }"
          data-field="idCamion"
          editor-type="dxSelectBox"
        >
          <DxRequiredRule message="Camion est obligatoire" />
        </DxItem>
        <DxItem
          :editor-options="{
            value: getCurrentBR.BR.idTransporteur,
            searchEnabled: true,
            items: get,
            displayExpr: 'raisonSociale',
            valueExpr: 'idTransporteur',
          }"
          data-field="idTransporteur"
          editor-type="dxSelectBox"
        >
          <DxRequiredRule message="Transporteur est obligatoire" />
        </DxItem>

        <DxItem
          :editor-options="{
            value: getCurrentBR.BR.idProvenance,
            searchEnabled: true,
            items: get,
            displayExpr: 'designation',
            valueExpr: 'idProvenance',
          }"
          data-field="idProvenance"
          editor-type="dxSelectBox"
        >
          <DxRequiredRule message="Provenance est obligatoire" />
        </DxItem>
        <DxItem data-field="tare" />
        <DxItem data-field="brute" />
        <DxItem data-field="nbPalette" />
        <DxItem data-field="nbCaisse" />
        <DxItem data-field="numBonPese" />
        <DxItem data-field="isNegos" />
      </DxForm>
    </form>

    <DxDataGrid
      class="content-block"
      id="grid-container"
      :column-auto-width="true"
      :column-hiding-enabled="true"
      :show-borders="false"
      :data-source="getCurrentBR.DetailsBR"
      :ref="gridRefName"
      :selection="{ mode: 'single' }"
      :auto-navigate-to-focused-row="true"
      v-model:focused-row-key="selectedRowId"
      v-model:focused-row-index="selectedRowIndex"
      @focused-row-changing="onFocusedRowChanging"
      @focused-row-changed="onFocusedRowChanged"
    >
      <DxEditing
        :allow-adding="true"
        :allow-updating="true"
        :allow-deleting="true"
        refresh-mode="repaint"
        mode="batch"
      />
      <DxColumn
        aria-required="true"
        data-field="idArticle"
        caption="Article"
        editor-type="dxSelectBox"
        :editor-options="{
          value: '',
          searchEnabled: true,
          items: getArticle,
          displayExpr: 'designation',
          valueExpr: 'idArticle',
        }"
      >
        <DxRequiredRule message="L'article est obligatoire" />
      </DxColumn>
      <DxColumn
        data-field="idMoule"
        caption="Moule"
        editor-type="dxSelectBox"
        :editor-options="{
          value: '',
          searchEnabled: true,
          items: getMoule,
          displayExpr: 'designation',
          valueExpr: 'idMoule',
        }"
      >
        <DxRequiredRule message="Le moule est obligatoire" />
      </DxColumn>

      <DxColumn
        data-field="idQualite"
        caption="Qualité"
        editor-type="dxSelectBox"
        :editor-options="{
          value: '',
          searchEnabled: true,
          items: getQualite,
          displayExpr: 'designation',
          valueExpr: 'idQualite',
        }"
      >
        <DxRequiredRule message="La Qualité est obligatoire" />
      </DxColumn>
      <DxColumn
        data-field="qteRecue"
        caption="Qte Reçue"
        editor-type="dxNumberBox"
        :editor-options="{ min: '1' }"
      >
        <DxRequiredRule message="La quantité reçue est obligatoire" />
      </DxColumn>
      <DxColumn
        data-field="qtePayee"
        caption="Qte Payée"
        editor-type="dxNumberBox"
        :editor-options="{ min: '1' }"
      >
        <DxRequiredRule message="La quantité payée est obligatoire" />
      </DxColumn>
      <DxColumn
        data-field="prixUnitaire"
        caption="Prix Unitaire"
        editor-type="dxNumberBox"
        :editor-options="{ min: '1' }"
      >
        <DxRequiredRule message="Le prix unitaire  est obligatoire" />
      </DxColumn>
      <DxColumn
        data-field="eventre"
        caption="Eventré(%)"
        editor-type="dxNumberBox"
      >
        <DxRequiredRule message="L'eventré  est obligatoire" />
      </DxColumn>
      <DxColumn
        data-field="histamine"
        caption="Histamine(%)"
        editor-type="dxNumberBox"
      >
        <DxRequiredRule message="L'Histamine  est obligatoire" />
      </DxColumn>
    </DxDataGrid>
  </div>
</template>

<script>
import {
  DxForm,
  DxItem,
  DxRequiredRule,
  DxToolbar,
  DxButtonItem,
} from "devextreme-vue/form";
import { DxSelectBox } from "devextreme-vue/select-box";
import { DxDataGrid, DxEditing, DxColumn } from "devextreme-vue/data-grid";
import DxButton from "devextreme-vue/button";
import { DxBox } from "devextreme-vue/box";
import DxTextBox from "devextreme-vue/text-box";
import DxDateBox from "devextreme-vue/date-box";
import notify from "devextreme/ui/notify";
import { mapActions, mapGetters } from "vuex";

export default {
  components: {
    DxForm,
    DxBox,
    DxSelectBox,
    DxDateBox,
    DxItem,
    DxToolbar,
    DxTextBox,
    DxRequiredRule,
    DxButtonItem,
    DxButton,
    DxDataGrid,
    DxEditing,
    DxColumn,
  },
  data() {
    return {
      action: this.$route.params.action,
      id: this.$route.params.id,
      buttonOptions: {
        text: "Sauvegarder",
        type: "default",
        stylingMode: "text",
        useSubmitBehavior: true,
      },
    };
  },
  mounted: async function () {},
  computed: {
    ...mapGetters({
      getCurrentBR: "bonReception/getCurrentBR",
      getTransporteur: "transporteur/getTransporteurs",
      getArticle: "article/getArticles",
      getMoule: "moule/getMoules",
      getQualite: "qualite/getQualites",
      getLots: "bonReception/getLots",
      getCamions: "camion/getCamions",
      getFournisseurs: "fournisseur/getFournisseurs",
      getOrigines: "origine/getOrigines",
      getProvenances: "provenance/getProvenances",
    }),
  },
  methods: {
    ...mapActions({
      generateCodeBR: "bonReception/generateCodeBR",
    }),
    dataValidation() {
      if (this.getDetailsBR.length < 1) {
        return "Le Bon Réception doit contenir au moins un article...!!?";
      } else return true;
    },
    customBR: function () {
      if (this.dataValidation() === true) {
        if (this.action == "Ajouter") {
          this.addBR()
            .then(() => {
              notify("Le Bon de réception est enegistrer", "success", 2000);
              this.$router.go(-1);
            })
            .catch(() => {
              notify("Echec..!!", "error", 1500);
            });
        } else if (this.action == "Modifier" && this.id > 0) {
          this.$store.dispatch("bonReception/updateBonReception", this.id);
          notify(
            "Le bon réception a etes modifié avec success...!!",
            "success",
            2000
          );
          this.$router.go(-1);
        }
      } else {
        notify(this.dataValidation(), "error", 2000);
      }
    },
    beforeMount() {},
    beforeUnmount() {},
  },
};
</script>

<style scoped>
.espace {
  margin-top: 25px;
}
.margin{
  margin-right: 40px;
  margin-left: 40px;
}
.center{
 display: grid;
    place-items: center; 
}

.prt {
  width: 45%;
  float: left;
}
.sts {
  width: 45%;
  float: right;
}
.btn {
  margin-top: 20px;
  float: right;
}
</style>
