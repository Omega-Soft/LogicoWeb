<template>
  <div>
    <h2 class="content-block">Articles</h2>
    <DxDataGrid
      :ref="gridRef"
      :show-borders="true"
      :data-source="getArticles"
      :column-auto-width="true"
      key-expr="idArticle"
      @row-inserting="(e) => Insert(e)"
      @row-updated="(e) => Update(e)"
      @row-removing="(e) => Delete(e)"
      :focused-row-enabled="true"
      :selection="{ mode: 'single' }"
      class="content-block"
      @selection-changed="selectedChanged"
    >
      <DxLoadPanel :enabled="true" />
      <DxPaging :page-size="5" />
      <DxPager :show-page-size-selector="true" :show-info="true" />
      <DxFilterRow :visible="true" />

      <!-- <DxToolbar>
        <DxItem name="groupPanel" />
        <DxItem location="before">
          <DxButton icon="exportpdf" text="" @click="exportGrid()" />
        </DxItem>
        <DxItem name="searchPanel" />
      </DxToolbar> -->
      <DxEditing
        :allow-updating="true"
        :allow-deleting="true"
        :allow-adding="true"
        mode="row"
      />

      <DxSpeedDialAction
        :index="1"
        :visible="(Array.from(getArticles).length > 0)"
        :on-click="exportGrid"
        icon="exportpdf"
        label=""
      />
      <DxColumn caption="Code Article" data-field="codeArticle">
        <DxRequiredRule />
      </DxColumn>
      <DxColumn caption="Designation" data-field="designation">
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
  DxToolbar,
  DxItem,
} from "devextreme-vue/data-grid";
import DxButton from "devextreme-vue/button";
import DxSpeedDialAction from "devextreme-vue/speed-dial-action";
import notify from "devextreme/ui/notify";
import { jsPDF } from "jspdf";
import "jspdf-autotable";
import { exportDataGrid as exportDataGridToPdf } from "devextreme/pdf_exporter";
import { mapGetters, mapActions } from "vuex";
const gridRef = "grid";
export default {
  components: {
    DxDataGrid,
    DxButton,
    DxColumn,
    DxEditing,
    DxRequiredRule,
    DxSpeedDialAction,
    DxFilterRow,
    DxLookup,
    DxPager,
    DxPaging,
    DxLoadPanel,
    DxToolbar,
    DxItem,
  },

  data() {
    return {
      gridRef,
    };
  },

  mounted: async function () {
    await this.initArticles();
  },

  computed: {
    ...mapGetters({
      getArticles: "article/getArticles",
    }),
    grid() {
      return this.$refs[gridRef].instance;
    },
  },

  methods: {
    ...mapActions({
      initArticles: "article/initArticles",
      addArticle: "article/addArticle",
      updateArticle: "article/updateArticle",
      deleteArticle: "article/deleteArticle",
    }),

    async Insert(e) {
      await this.addArticle(e.data)
        .then((response) => {
          console.log(response);
          notify("L'Article a bien été ajouté!", "success", 2000);
        })
        .catch((error) => {
          console.log(error);
          notify("Echec d'ajout!", "error", 2000);
        });
    },

    async Update(e) {
      await this.updateArticle(e.data)
        .then((response) => {
          console.log(response);
          notify("L'Article a bien été modifié!", "success", 2000);
        })
        .catch((error) => {
          console.log(error);
          notify("Echec de modification!", "error", 2000);
        });
    },

    async Delete(e) {
      await this.deleteArticle(e.data.idArticle)
        .then((response) => {
          console.log(response);
          notify("L'Article a bien été supprimé!", "success", 2000);
        })
        .catch((error) => {
          console.log(error);
          notify("Echec de suppression!", "error", 2000);
        });
    },
    exportGrid() {
      let articles = this.getArticles;
      if (!articles) {
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
            const header = "Liste des Articles";
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
          pdfDoc.save("Liste_des_Articles.pdf");
        });
    },
  },
};
</script>
