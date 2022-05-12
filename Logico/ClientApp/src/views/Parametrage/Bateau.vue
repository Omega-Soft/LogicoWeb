<template>
  <div>
    <h2 class="content-block">Bateaux</h2>
    <DxDataGrid
      :ref="gridRef"
      :show-borders="true"
      :data-source="getBateaux"
      :column-auto-width="true"
      key-expr="idBateau"
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
        :allow-updating="true"
        :allow-deleting="true"
        :allow-adding="true"
        refresh-mode="reshape"
        mode="popup"
      />
       <DxSpeedDialAction
        :index="1"
        :on-click="exportGrid"
        icon="exportpdf"
        label=""
      />

      <DxColumn caption="Code Bateau" data-field="code">
        <DxRequiredRule />
      </DxColumn>

      <DxColumn caption="Nom" data-field="nom">
        <DxRequiredRule />
      </DxColumn>

      <DxColumn caption="Type Bateau" data-field="type">
        <DxRequiredRule />
      </DxColumn>
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
    DxSpeedDialAction,
    DxEditing,
    DxRequiredRule,
    DxFilterRow,
    DxLookup,
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
    await this.initBateaux();
  },

  computed: {
    ...mapGetters({
      getBateaux: "bateau/getBateaux",
    }),
    grid() {
      return this.$refs[gridRef].instance;
    },
  },

  methods: {
    ...mapActions({
      initBateaux: "bateau/initBateaux",
      addBateau: "bateau/addBateau",
      updateBateau: "bateau/updateBateau",
      deleteBateau: "bateau/deleteBateau",
    }),
    saveGridInstance: function (e) {
      this.dataGridInstance = e.component;
    },
    refresh: function () {
      this.dataGridInstance.refresh();
    },

    async Insert(e) {
      await this.addBateau(e.data)
        .then((response) => {
          console.log(response);
          notify("Le Bateau a été ajouté!", "success", 2000);
        })
        .catch((error) => {
          console.log(error);
          notify("Echec d'ajout!", "error", 2000);
        });
    },

    async Update(e) {
      await this.updateBateau(e.data)
        .then((response) => {
          console.log(response);
          notify("Le Bateau a bien été modifié!", "success", 2000);
        })
        .catch((error) => {
          console.log(error);
          notify("Echec de modification!", "error", 2000);
        });
    },

    async Delete(e) {
      await this.deleteBateau(e.data.idBateau)
        .then((response) => {
          console.log(response);
          notify("Le Bateau a bien été supprimé!", "success", 2000);
        })
        .catch((error) => {
          console.log(error);
          notify("Echec de suppression!", "error", 2000);
        });
    },
    exportGrid() {
      let bateau = this.getBateaux;
      if (!bateau) {
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
            const header = "Liste des Bateaux";
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
          pdfDoc.save("Liste_des_Bateaux.pdf");
        });
    },
  },
};
</script>
