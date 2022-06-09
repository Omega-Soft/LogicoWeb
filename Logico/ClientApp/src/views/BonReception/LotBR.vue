<template>
  <div>
    <h2 class="content-block">Les Lots Receptions</h2>
    <DxDataGrid
      class="content-block"
      :focused-row-enabled="true"
      :column-auto-width="true"
      :column-hiding-enabled="true"
      id="grid-container"
      key-expr="idBr"
      :ref="gridRefName"
      :selection="{ mode: 'single' }"
      :show-borders="false"
      :data-source="getBonReceptions"
      :auto-navigate-to-focused-row="true"
      v-model:focused-row-key="selectedRowId"
      v-model:focused-row-index="selectedRowIndex"
      @focused-row-changing="onFocusedRowChanging"
      @focused-row-changed="onFocusedRowChanged"
    >
      <DxFilterRow :visible="true" />
      <DxPaging :page-size="10" />
      <DxPager :show-page-size-selector="true" :show-info="true" />
      <DxEditing refresh-mode="repaint" />
      <DxMasterDetail :enabled="true" template="masterDetailTemplate" />
      <template #masterDetailTemplate="{ data: br }">
        <DetailTemplate :BrData="br" />
      </template>

      <DxToolbar>
        <DxItem location="before" template="codeLot" />
        <DxItem location="before" template="dateLot" />
        <DxItem location="before" template="btnAddLot" />
      </DxToolbar>

      <template #codeLot>
        <div class="dx-field">
          <div class="dx-field-value">
            <DxSelectBox
              label="Code Lot MP"
              label-mode="floating"
              width="225"
              :data-source="getLots"
              display-expr="codeLot"
              value-expr="idLot"
              v-model:value="selectedLot"
              @value-changed="onSelectLot"
              v-if="!insertMode"
            />
            <DxTextBox 
              label="Code Lot MP"
              label-mode="floating"
              width="225"
              v-model:value="codeLot"
              v-else/>
          </div>
        </div>
      </template>

      <template #dateLot>
        <!-- <div class="dx-field">
          <div class="dx-field-value"> -->
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
        <!-- </div>
        </div> -->
      </template>

      <template #btnAddLot>
        <DxButton v-if="!insertMode" icon="plus" @click="btnAddLotClick()" />
        <DxButton v-if="insertMode" icon="save" @click="btnSaveLotClick()" />
        <DxButton v-if="insertMode" icon="close" @click="btnCancelLotClick()" />
      </template>

      <DxColumn data-field="numBonPese" caption="Bon Pese" />

      <DxColumn data-field="codeBr" caption="N° BR" :width="100" />

      <DxColumn data-field="idFournisseur" caption="Fournisseur">
        <DxLookup
          :data-source="getFournisseurs"
          display-expr="raisonSociale"
          value-expr="idFournisseur"
        />
      </DxColumn>
      <DxColumn data-field="idCamion" caption="Camion">
        <DxLookup
          :data-source="getCamions"
          display-expr="matricule"
          value-expr="idCamion"
        />
      </DxColumn>

      <DxColumn data-field="idOrigine" caption="Origine">
        <DxLookup
          :data-source="getOrigines"
          display-expr="designation"
          value-expr="idOrigine"
        />
      </DxColumn>

      <DxColumn data-field="montant" caption="Montant" />

      <DxColumn data-field="prevalidee" caption="Prévalidée" />

      <DxColumn data-field="validee" caption="Validée" />

      <DxColumn data-field="dateBr" data-type="date" caption="Date BR" />

      <DxColumn data-field="idProvenance" caption="Provenance">
        <DxLookup
          :data-source="getProvenances"
          display-expr="designation"
          value-expr="idProvenance"
        />
      </DxColumn>
    </DxDataGrid>
    <DxSpeedDialAction
      :index="1"
      :on-click="exportGrid"
      icon="exportpdf"
      label="Exporter PDF"
    />
    <DxSpeedDialAction
      :index="2"
      :on-click="addBR"
      icon="add"
      label="Ajouter"
    />
    <DxSpeedDialAction
      :visible="selectedRowId !== -1"
      :on-click="onDeleteBR"
      :index="3"
      icon="trash"
      label="Supprimer"
    />
    <DxSpeedDialAction
      :visible="selectedRowId !== -1"
      :on-click="updateBR"
      :index="4"
      icon="edit"
      label="Modifier"
    />
    <DxSpeedDialAction
      :visible="selectedRowId !== -1"
      :on-click="printBL"
      :index="5"
      icon="print"
      label="Imprimer"
    />
  </div>
</template>
<script>
import {
  DxDataGrid,
  DxFilterRow,
  DxPager,
  DxPaging,
  DxToolbar,
  DxItem,
  DxEditing,
  DxLookup,
  DxColumn,
  DxMasterDetail,
} from "devextreme-vue/data-grid";
import DxDateBox from "devextreme-vue/date-box";
import { DxSelectBox } from "devextreme-vue/select-box";
import CustomStore from "devextreme/data/custom_store";
import DxSpeedDialAction from "devextreme-vue/speed-dial-action";
import notify from "devextreme/ui/notify";
import { mapGetters, mapActions } from "vuex";

import DetailTemplate from "../../components/detail-bon-reception.vue";

import { jsPDF } from "jspdf";
import "jspdf-autotable";
import { exportDataGrid as exportDataGridToPdf } from "devextreme/pdf_exporter";
import DxButton from "devextreme-vue/button";
import DxTextBox from 'devextreme-vue/text-box';
export default {
  data() {
    return {
      gridRefName: "grid",
      selectedLot: -1,
      selectedRowId: -1,
      selectedRowIndex: -1,
      codeLot: null,
      dateLot: null,
      insertMode: false,
    };
  },
  mounted: async function () {
    await this.initBonReceptionLot(-1);
    await this.initLots();
    await this.initCamions();
    await this.initFournisseurs();
    await this.initOrigines();
    await this.initProvenances();
  },
  computed: {
    ...mapGetters({
      getBonReceptions: "bonReception/getBonReceptions",
      getLots: "bonReception/getLots",
      getCamions: "camion/getCamions",
      getFournisseurs: "fournisseur/getFournisseurs",
      getOrigines: "origine/getOrigines",
      getProvenances: "provenance/getProvenances",
    }),
    grid: function () {
      return this.$refs[this.gridRefName].instance;
    },
  },
  methods: {
    ...mapActions({
      initBonReceptionLot: "bonReception/initBonReceptionLot",
      initLots: "bonReception/initLots",
      generateCodeLot: "bonReception/generateCodeLot",
      addLot: "bonReception/addLot",
      initCamions: "camion/initCamions",
      initFournisseurs: "fournisseur/initFournisseurs",
      initOrigines: "origine/initOrigines",
      initProvenances: "provenance/initProvenances",
    }),

    addBR: function () {
      this.$router.push({
        name: "gestion-bon-reception",
        params: { action: "Ajouter", id: "new" },
      });
    },
    updateBR: function () {
      this.$router.push({
        name: "gestion-bon-reception",
        params: { action: "Modifier", id: this.selectedRowId },
      });
    },
    deleteBR: function () {
      console.log("Deleting..." + this.selectedRowIndex);
      this.grid.deleteRow(2);
    },
    onDeleteBR: function () {
      console.log("Deleting..." + this.selectedRowId);
      this.$store.dispatch(
        "bonReception/deleteBonReception",
        this.selectedRowId
      );
      this.grid.Reload();
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
      this.initBonReceptionLot(e.value);
    },

    btnAddLotClick: function(){
      this.insertMode = true;
      this.dateLot = null;
    },
    btnSaveLotClick: async function(){
      this.insertMode = false;
      console.log("codeLot => " + this.codeLot);
      console.log("dateLot => " + new Date(this.dateLot));
      await this.addLot({codeLot: this.codeLot, journee: new Date(this.dateLot)})
      .then((response) => {
        this.initLots().then(() => {
          this.selectedLot = response.data.idLot;
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
    }

  },
  components: {
    DxDataGrid,
    DxFilterRow,
    DxLookup,
    DxPager,
    DxDateBox,
    DxPaging,
    DxEditing,
    DxColumn,
    DxMasterDetail,
    DxToolbar,
    DxItem,
    DxSelectBox,
    DxSpeedDialAction,
    DetailTemplate,
    DxButton,
    DxTextBox,
  },
};
</script>
<style>
#grid-container {
  height: 440px;
}
</style>
