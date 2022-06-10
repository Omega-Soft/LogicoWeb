<template>
  <div>
    <h2 class="content-block">List Bon Receptions</h2>
    <DxDataGrid
      class="content-block"
      :focused-row-enabled="true"
      :column-auto-width="true"
      :column-hiding-enabled="true"
      id="grid-container"
      :ref="gridRefName"
      :selection="{ mode: 'single' }"
      :show-borders="false"
      :data-source="dataSource"
      :auto-navigate-to-focused-row="true"
      v-model:focused-row-key="selectedRowId"
      v-model:focused-row-index="selectedRowIndex"
      @focused-row-changing="onFocusedRowChanging"
      @focused-row-changed="onFocusedRowChanged"
    >
      <DxGroupPanel :visible="true" />
      <DxGrouping :auto-expand-all="autoExpandAll" />
      <DxFilterRow :visible="true" />
      <DxPaging :page-size="10" />
      <DxPager :show-page-size-selector="true" :show-info="true" />
      <DxEditing refresh-mode="repaint" />
      <DxMasterDetail :enabled="true" template="masterDetailTemplate" />
      <template #masterDetailTemplate="{ data: br }">
        <DetailTemplate :BrData="br" />
      </template>

      <DxColumn data-field="idLot" caption="Code Lot" :group-index="0">
        <DxLookup
          :data-source="getLots"
          display-expr="codeLot"
          value-expr="idLot"
        />
      </DxColumn>

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

      <!-- <DxColumn :width="100" data-field="codeLot" :group-index="0" /> -->
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
      :visible="selectedRowId > -1 && typeof selectedRowId == 'number'"
      :on-click="deleteBR"
      :index="3"
      icon="trash"
      label="Supprimer"
    />
    <DxSpeedDialAction
      :visible="selectedRowId > -1 && typeof selectedRowId == 'number'"
      :on-click="updateBR"
      :index="4"
      icon="edit"
      label="Modifier"
    />
    <DxSpeedDialAction
      :visible="selectedRowId > -1 && typeof selectedRowId == 'number'"
      :on-click="printBR"
      :index="5"
      icon="print"
      label="Imprimer"
    />
  </div>
</template>
<script>
import {
  DxDataGrid,
  DxGrouping,
  DxGroupPanel,
  DxFilterRow,
  DxPager,
  DxPaging,
  DxEditing,
  DxLookup,
  DxColumn,
  DxMasterDetail,
} from "devextreme-vue/data-grid";
import CustomStore from "devextreme/data/custom_store";
import DxSpeedDialAction from "devextreme-vue/speed-dial-action";
import notify from "devextreme/ui/notify";
import { mapGetters, mapActions } from "vuex";

import DetailTemplate from "../../components/detail-bon-reception.vue";

import { jsPDF } from "jspdf";
import "jspdf-autotable";
import { exportDataGrid as exportDataGridToPdf } from "devextreme/pdf_exporter";

export default {
  data() {
    return {
      gridRefName: "grid",
      dataSource: new CustomStore({
        key: "idBr",
        load: async () => {
          await this.initBonReceptions();
          return this.getBonReceptions;
        },
        remove: () => {
          this.onDeleteBR();
        },
      }),
      selectedRowId: -1,
      selectedRowIndex: -1,
    };
  },
  mounted: async function () {
    await this.initBonReceptions();
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
      initBonReceptions: "bonReception/initBonReceptions",
      deleteBonReception: "bonReception/deleteBonReception",
      initLots: "bonReception/initLots",
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
      this.grid.deleteRow(this.selectedRowIndex);
    },
    onDeleteBR: async function () {
      await this.deleteBonReception(this.selectedRowId)
        .then((response) => {
          notify("Le bon reception a bien été supprimé!", "success", 2000);
        })
        .catch((error) => {
          console.log(error);
          notify("Echec de suppression!", "error", 2000);
        });
    },
  },
  components: {
    DxDataGrid,
    DxGrouping,
    DxGroupPanel,
    DxFilterRow,
    DxLookup,
    DxPager,
    DxPaging,
    DxEditing,
    DxColumn,
    DxMasterDetail,
    DxSpeedDialAction,
    DetailTemplate,
  },
};
</script>
<style>
#grid-container {
  height: 440px;
}
</style>
