<template>

  <DxLoadPanel
    :position="{ of: '#form' }"
    v-model:visible="loading"
    :show-indicator="true"
    :show-pane="true"
    :shading="true"
    :close-on-outside-click="false"
    shading-color="rgba(242,242,242,0.75)"
  />

  <div>
    <form
      id="form-container"
      class="content-block"
      @submit.prevent="SubmitBR()"
    >
      <DxForm :form-data="getCurrentBR" id="form" :col-count="4">
        <!-- Header -->
        <DxItem :col-span="3">
          <template #default>
            <div>
              <DxButton
                icon="arrowleft"
                type="default"
                styling-mode="text"
                @click="dispose()"
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
        <!-- ---------------- Lot --------------------------  -->
        <DxItem :col-span="1">
          <template #default>
            <DxSelectBox
              label="Code Lot MP"
              label-mode="floating"
              :data-source="getLots"
              display-expr="codeLot"
              value-expr="idLot"
              @value-changed="onSelectLot"
              :visible="!insertMode"
              v-model:value="getCurrentBR.idLot"
              :read-only="readOnlyLot"
            >
            </DxSelectBox>
            <DxTextBox
              label="Code Lot MP"
              label-mode="floating"
              v-model:value="codeLot"
              :visible="insertMode"
            />
          </template>
        </DxItem>
        <DxItem :col-span="1">
          <template #default>
            <DxDateBox
              label="Date Lot MP"
              display-expr="journee"
              label-mode="floating"
              value-expr="idLot"
              v-model:value="dateLot"
              type="date"
              :read-only="!insertMode"
              @value-changed="onChangeDate"
            />
          </template>
        </DxItem>
        <DxItem :col-span="2">
          <template #default>
            <DxButton v-if="!insertMode && !readOnlyLot" icon="plus" @click="btnAddLotClick()" style="background-color:transparent; margin-top:5px;margin-left:-30px"/>
            <DxButton v-if="insertMode" icon="save" @click="btnSaveLotClick()" style="background-color:transparent; margin-top:5px;margin-left:-30px"/>
            <DxButton v-if="insertMode" icon="close" @click="btnCancelLotClick()" style="background-color:transparent; margin-top:5px"/>
          </template>
        </DxItem>
        <!-- ----------------- BR ------------------------- -->
        <DxItem
          :editor-options="{ value: getCurrentBR.numBonReception, readOnly: true }"
          data-field="numBonReception"
        />
        <DxItem
          :editor-options="{
            vModelValue: getCurrentBR.idFournisseur,
            searchEnabled: true,
            items: getFournisseurs,
            displayExpr: 'raisonSociale',
            valueExpr: 'idFournisseur',
          }"
          data-field="Fournisseur"
          editor-type="dxSelectBox"
        >
          <DxRequiredRule message="Fournisseur est obligatoire" />
        </DxItem>
        <DxItem
          :editor-options="{
            vModelValue: getCurrentBR.idOrigine,
            searchEnabled: true,
            items: getOrigines,
            displayExpr: 'designation',
            valueExpr: 'idOrigine',
          }"
          data-field="Origine"
          editor-type="dxSelectBox"
        >
          <DxRequiredRule message="L'origine est obligatoire" />
        </DxItem>
        <DxItem
          :editor-options="{
            vModelValue: getCurrentBR.idCamion,
            searchEnabled: true,
            items: getCamions,
            displayExpr: 'matricule',
            valueExpr: 'idCamion',
          }"
          data-field="Camion"
          editor-type="dxSelectBox"
        >
          <DxRequiredRule message="Camion est obligatoire" />
        </DxItem>
        <DxItem
          :editor-options="{
            vModelValue: getCurrentBR.idTransporteur,
            searchEnabled: true,
            items: getTransporteurs,
            displayExpr: 'raisonSociale',
            valueExpr: 'idTransporteur',
          }"
          data-field="Transporteur"
          editor-type="dxSelectBox"
        >
          <DxRequiredRule message="Transporteur est obligatoire" />
        </DxItem>
        <DxItem
          :editor-options="{
            vModelValue: getCurrentBR.idProvenance,
            searchEnabled: true,
            items: getProvenances,
            displayExpr: 'designation',
            valueExpr: 'idProvenance',
          }"
          data-field="Provenance"
          editor-type="dxSelectBox"
        >
          <DxRequiredRule message="Provenance est obligatoire" />
        </DxItem>
        <DxItem data-field="brute" />
        <DxItem data-field="tare" />
        <DxItem data-field="nombreDePalettes" />
        <DxItem data-field="nombreDeCaisses" />
        <DxItem data-field="numeroBonPese" />
        <DxItem 
          data-field="isNegos"
          editor-type="dxCheckBox"
          :editor-options="{
            value: getCurrentBR.isNegos,
          }"
        />
      </DxForm>
    </form>
    <!-- ----------------- Details BR ------------------------- -->
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
          items: getArticles,
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
          items: getMoules,
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
          items: getQualites,
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
      />
      <DxColumn
        data-field="histamine"
        caption="Histamine(%)"
        editor-type="dxNumberBox"
      />
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
import { DxLoadPanel } from 'devextreme-vue/load-panel';
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
    DxLoadPanel
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
      insertMode: false,
      codeLot: null,
      dateLot: null,
      readOnlyLot: false,
      loading: false,
    };
  },
  mounted: async function () {
    this.loading = true;
    await this.initData();
    this.loading = false;
  },
  computed: {
    ...mapGetters({
      getCurrentBR: "bonReception/getCurrentBR",
      getTransporteurs: "transporteur/getTransporteurs",
      getArticles: "article/getArticles",
      getMoules: "moule/getMoules",
      getQualites: "qualite/getQualites",
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
      prepareCurrentBR: "bonReception/prepareCurrentBR",
      generateCodeLot: "bonReception/generateCodeLot",
      addLot: "bonReception/addLot",
      addBonReception: "bonReception/addBonReception",
      updateBonReception: "bonReception/updateBonReception",
      initLots: "bonReception/initLots",
      initCamions: "camion/initCamions",
      initFournisseurs: "fournisseur/initFournisseurs",
      initTransporteurs: "transporteur/initTransporteurs",
      initOrigines: "origine/initOrigines",
      initProvenances: "provenance/initProvenances",
      initArticles: "article/initArticles",
      initMoules: "moule/initMoules",
      initQualites: "qualite/initQualites",
    }),
    
    dataValidation() {
      if (this.getCurrentBR.DetailsBR.length < 1) {
        return "Le Bon Réception doit contenir au moins un article...!!?";
      }
      else if (this.getCurrentBR.idLot == null || this.getCurrentBR.idLot < 1){
        return "Le Lot est obligatoire...!!?";
      } 
      else return true;
    },
    
    SubmitBR: async function () {
      if (this.dataValidation() === true) {
        if (this.action == "Ajouter") {
          await this.addBonReception()
            .then(() => {
              notify("Le Bon de réception est enegistrer", "success", 2000);
              this.$router.go(-1);
            })
            .catch(() => {
              notify("Echec d'ajout..!!", "error", 1500);
            });
        } else if (this.action == "Modifier" && this.id > 0) {
          await this.updateBonReception()
            .then(() => {
              notify(
                "Le bon réception a etes modifié avec success...!!",
                "success",
                2000
              );
              this.dispose();
            })
            .catch(() => {
              notify("Echec de modification..!!", "error", 1500);
            });
        }
      } else {
        notify(this.dataValidation(), "error", 2000);
      }
    },
    
    getdate: function () {
      return this.dateLot;
    },
    onSelectLot: function (e) {
      let lots = Array.from(
        this.getLots.map((x) => {
          return {
            idLot: x.idLot,
            codeLot: x.codeLot,
            journee: x.journee,
          };
        })
      );
      this.dateLot = lots.find((x) => x.idLot == e.value).journee;
    },
    btnAddLotClick: function(){
      this.insertMode = true;
      this.codeLot = null;
      this.dateLot = null;
      this.selectedLot = 0;
    },
    btnSaveLotClick: async function(){
      this.insertMode = false;
      await this.addLot({codeLot: this.codeLot, journee: new Date(this.dateLot)})
      .then((response) => {
        this.initLots().then(() => {
          this.selectedLot = response.data.idLot;
          this.getCurrentBR.idLot = response.data.idLot;
        })
      })
    },
    btnCancelLotClick: function(){
      this.insertMode = false;
      this.codeLot = null;
      this.dateLot = null;
      this.selectedLot = -1;
    },

    onChangeDate: function(){
      if (this.insertMode && this.dateLot != null) {
        let date = this.dateLot.toISOString();
        this.generateCodeLot(date.toString().substring(0, 10))
        .then((response) => {
            this.codeLot = response;
        });
      }
    },

    initData: async function(){
      await this.initLots();
      await this.initCamions();
      await this.initFournisseurs();
      await this.initTransporteurs();
      await this.initOrigines();
      await this.initProvenances();
      await this.initArticles();
      await this.initMoules();
      await this.initQualites();
      await this.prepareCurrentBR(this.id);
      if (this.id != "nouveau"){
        this.readOnlyLot = true;
      }
    },

    dispose(){
      this.$router.go(-1);
      this.getCurrentBR = {};
    }
  },
};
</script>


<style>
.dx-checkbox .dx-checkbox-container{
  margin: 15px !important;
  margin-left: 30px !important
}
</style>
<style scoped>
.espace {
  margin-top: 25px;
}
.margin {
  margin-right: 40px;
  margin-left: 40px;
}
.center {
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