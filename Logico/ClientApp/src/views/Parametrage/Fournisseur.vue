<template>
  <div>
    <h2 class="content-block">Fournisseurs</h2>
    <DxDataGrid
      :show-borders="true"
      :ref="gridRef"
      :data-source="getFournisseurs"
      :column-auto-width="true"
      key-expr="idFournisseur"
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
       >
        <!-- <DxPopup
          :show-title="true"
          title="Fournisseur Info"
        />
          <DxForm>
          <DxItem
            :col-count="3"
            :col-span="2"
            item-type="group"
          > -->
           <DxColumn caption="Code Fournisseur" data-field="codeFournisseur">
        <DxRequiredRule />
      </DxColumn>

      <DxColumn caption="Raison Sociale" data-field="raisonSociale">
        <DxRequiredRule />
      </DxColumn>

      <DxColumn caption="Email" data-field="email">
        <DxRequiredRule />
        <DxEmailRule />
      </DxColumn>

            <!-- <DxItem
              :col-span="2"
              :editor-options="{ height: 100 }"
              data-field="Notes"
              editor-type="dxTextArea"
            />
          </DxItem> -->
<!-- 
          <DxItem
            :col-count="2"
            :col-span="2"
            item-type="group"
            caption="Home Address"
          > -->
             <DxColumn caption="Adresse" data-field="adresse"> </DxColumn>

            <DxColumn caption="Téléphone" data-field="tel"> </DxColumn>
          <!-- </DxItem>
        </DxForm>-->
      </DxEditing> 
      <DxSpeedDialAction
        :index="1"
        :on-click="exportGrid"
        icon="exportpdf"
        label=""
      />

      <DxColumn caption="Code Fournisseur" data-field="codeFournisseur">
        <DxRequiredRule />
      </DxColumn>

      <DxColumn caption="Raison Sociale" data-field="raisonSociale">
        <DxRequiredRule />
      </DxColumn>

      <DxColumn caption="Email" data-field="email">
        <DxRequiredRule />
        <DxEmailRule />
      </DxColumn>

      <DxColumn caption="Adresse" data-field="adresse"> </DxColumn>

      <DxColumn caption="Téléphone" data-field="tel"> </DxColumn>

      <!-- <DxColumn
        caption="Date A nouveau"
        data-field="dateAnouveau"
        data-type="date"
      >
      </DxColumn>

      <DxColumn caption="Fax" data-field="fax"> </DxColumn>

      <DxColumn caption="Site" data-field="site"> </DxColumn>

      <DxColumn caption="Code Postal" data-field="codePostal"> </DxColumn>

      <DxColumn caption="Ville" data-field="ville">
      </DxColumn>

      <DxColumn caption="IsFrsMP" data-field="isFrsMp"> </DxColumn>

      <DxColumn caption="IsFrsPF" data-field="isFrsPf"> </DxColumn>

      <DxColumn caption="IsFrsCharges" data-field="isFrsCharges"> </DxColumn>-->
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
  DxForm,
  DxPopup,
  DxEmailRule,
  DxPaging,
  DxFilterRow,
  DxLoadPanel,
} from "devextreme-vue/data-grid";
import { DxItem } from 'devextreme-vue/form';
import DxSpeedDialAction from "devextreme-vue/speed-dial-action";
import { jsPDF } from "jspdf";
import "jspdf-autotable";
import { exportDataGrid as exportDataGridToPdf } from "devextreme/pdf_exporter";
import notify from "devextreme/ui/notify";
import { mapGetters, mapActions } from "vuex";
const gridRef = "grid";
export default {
  components: {
    DxDataGrid,
    DxColumn,
    DxEditing,
    DxItem,
    DxForm,
    DxPopup,
    DxRequiredRule,
    DxSpeedDialAction,
    DxFilterRow,
    DxLookup,
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
    await this.initFournisseurs();
  },

  computed: {
    ...mapGetters({
      getFournisseurs: "fournisseur/getFournisseurs",
    }),
    grid() {
      return this.$refs[gridRef].instance;
    },
  },

  methods: {
    ...mapActions({
      initFournisseurs: "fournisseur/initFournisseurs",
      addFournisseur: "fournisseur/addFournisseur",
      updateFournisseur: "fournisseur/updateFournisseur",
      deleteFournisseur: "fournisseur/deleteFournisseur",
    }),
    saveGridInstance: function (e) {
      this.dataGridInstance = e.component;
    },
    refresh: function () {
      this.dataGridInstance.refresh();
    },

    async Insert(e) {
      await this.addFournisseur(e.data)
        .then((response) => {
          console.log(response);
          notify("Le Fournissseur a été ajouté!", "success", 2000);
        })
        .catch((error) => {
          console.log(error);
          notify("Echec d'ajout!", "error", 2000);
        });
    },

    async Update(e) {
      await this.updateFournisseur(e.data)
        .then((response) => {
          console.log(response);
          notify("Le Fournisseur a bien été modifié!", "success", 2000);
        })
        .catch((error) => {
          console.log(error);
          notify("Echec de modification!", "error", 2000);
        });
    },

    async Delete(e) {
      await this.deleteFournisseur(e.data.idFournisseur)
        .then((response) => {
          console.log(response);
          notify("Le Fournisseur a bien été supprimé!", "success", 2000);
        })
        .catch((error) => {
          console.log(error);
          notify("Echec de suppression!", "error", 2000);
        });
    },
    exportGrid() {
      let fournisseur = this.getFournisseurs;
      if (!fournisseur) {
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
            const header = "Liste des Fournisseurs";
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
          pdfDoc.save("Liste_des_Fournisseurs.pdf");
        });
    },
  },
};
</script>
