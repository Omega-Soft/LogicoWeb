<template>
  <div>
    <h2 class="content-block">Transporteurs</h2>
    <DxDataGrid
      :show-borders="true"
      :data-source="getTransporteurs"
      :ref="gridRef"
      :column-auto-width="true"
      key-expr="idTransporteur"
      @row-inserting="(e) => Insert(e)"
      @row-updated="(e) => Update(e)"
      @row-removing="(e) => Delete(e)"
      :focused-row-enabled="true"
      :selection="{ mode: 'single' }"
      class="content-block"
      :column-hiding-enabled="true"
      :allow-column-resizing="false"
      column-resizing-mode="widget"
      :repaint-changes-only="true"
      @selection-changed="selectedChanged"
    >
      <DxLoadPanel :enabled="true" />
      <DxPaging :page-size="10" />
      <DxPager :show-page-size-selector="true" :show-info="true" />
      <DxFilterRow :visible="true" />
      <DxEditing
        :allow-updating="userRoles.update"
        :allow-deleting="userRoles.delete"
        :allow-adding="userRoles.add"
        refresh-mode="reshape"
        mode="row"
      />

      <DxSpeedDialAction
        :index="1"
        :visible="Array.from(getTransporteurs).length > 0"
        :on-click="exportGrid"
        icon="exportpdf"
        label=""
      />

      <DxColumn caption="Code Transporteurs" data-field="code">
        <DxRequiredRule />
      </DxColumn>

      <DxColumn caption="Raison Sociale" data-field="raisonSociale">
        <DxRequiredRule />
      </DxColumn>

      <DxColumn caption="Adresse" data-field="adresse"> </DxColumn>

      <DxColumn caption="Téléphone" data-field="tel"> </DxColumn>
    </DxDataGrid>
  </div>
</template>
<script>
import {
  DxDataGrid,
  DxColumn,
  DxEditing,
  DxRequiredRule,
  DxLookup,
  DxPager,
  DxEmailRule,
  DxPaging,
  DxFilterRow,
  DxLoadPanel,
} from "devextreme-vue/data-grid";
import notify from "devextreme/ui/notify";
import DxSpeedDialAction from "devextreme-vue/speed-dial-action";
import { jsPDF } from "jspdf";
import "jspdf-autotable";
import { exportDataGrid as exportDataGridToPdf } from "devextreme/pdf_exporter";
import { mapGetters, mapActions } from "vuex";
const gridRef = "grid";

export default {
  components: {
    DxDataGrid,
    DxColumn,
    DxEditing,
    DxRequiredRule,
    DxFilterRow,
    DxLookup,
    DxSpeedDialAction,
    DxEmailRule,
    DxPager,
    DxPaging,
    DxLoadPanel,
  },

  data() {
    return {
      gridRef,
    };
  },

  mounted: async function () {
    await this.initTransporteurs();
  },

  computed: {
    ...mapGetters({
      getTransporteurs: "transporteur/getTransporteurs",
      getUserPages: "login/getUserPages",
    }),
    userRoles() {
      return this.getUserPages.find((e) => e.page === "PRMT");
    },
    grid() {
      return this.$refs[gridRef].instance;
    },
  },

  methods: {
    ...mapActions({
      initTransporteurs: "transporteur/initTransporteurs",
      addTransporteur: "transporteur/addTransporteur",
      updateTransporteur: "transporteur/updateTransporteur",
      deleteTransporteur: "transporteur/deleteTransporteur",
    }),
    saveGridInstance: function (e) {
      this.dataGridInstance = e.component;
    },
    refresh: function () {
      this.dataGridInstance.refresh();
    },

    async Insert(e) {
      await this.addTransporteur(e.data)
        .then((response) => {
          console.log(response);
          notify("Le Transporteur a été ajouté!", "success", 2000);
        })
        .catch((error) => {
          console.log(error);
          notify("Echec d'ajout!", "error", 2000);
        });
    },

    async Update(e) {
      await this.updateTransporteur(e.data)
        .then((response) => {
          console.log(response);
          notify("Le Transporteur a bien été modifié!", "success", 2000);
        })
        .catch((error) => {
          console.log(error);
          notify("Echec de modification!", "error", 2000);
        });
    },

    async Delete(e) {
      await this.deleteTransporteur(e.data.idTransporteur)
        .then((response) => {
          console.log(response);
          notify("Le Transporteur a bien été supprimé!", "success", 2000);
        })
        .catch((error) => {
          console.log(error);
          notify("Echec de suppression!", "error", 2000);
        });
    },
    exportGrid() {
      let transporteur = this.getTransporteurs;
      if (!transporteur) {
        notify("Aucun données a exporter", "error", 2000);
        return;
      }
      const pdfDoc = new jsPDF();
      exportDataGridToPdf({
        jsPDFDocument: pdfDoc,
        component: this.grid,
        customizeCell: function (options) {
          const { gridCell, pdfCell } = options;
          if (gridCell.rowType === "data") {
            pdfCell.styles = {};
          }
        },
      })
        .then(() => {
          pdfDoc.setFontSize(12);
          const pageCount = pdfDoc.internal.getNumberOfPages();
          for (let i = 1; i <= pageCount; i++) {
            pdfDoc.setPage(i);
            const pageSize = pdfDoc.internal.pageSize;
            const pageWidth = pageSize.width
              ? pageSize.width
              : pageSize.getWidth();
            const pageHeight = pageSize.height
              ? pageSize.height
              : pageSize.getHeight();
            const header = "Liste des Transporteurs";
            const footer = `Page ${i} sur ${pageCount}`;

            // Header
            pdfDoc.setTextColor(20, 143, 119);
            pdfDoc.setFontSize(18);
            pdfDoc.text(
              header,
              pageWidth / 2 - pdfDoc.getTextWidth(header) / 2,
              4,
              { baseline: "top" }
            );

            // Footer
            pdfDoc.setTextColor(0, 0, 0);
            pdfDoc.setFontSize(12);
            pdfDoc.text(
              footer,
              pageWidth / 2 - pdfDoc.getTextWidth(footer) / 2,
              pageHeight - 7,
              { baseline: "bottom" }
            );
          }
        })
        .then(() => {
          pdfDoc.save("Liste_des_Transporteurs.pdf");
        });
    },
  },
};
</script>
