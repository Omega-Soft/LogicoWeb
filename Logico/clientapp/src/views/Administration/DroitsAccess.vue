<template>
  <div>
    <h2 class="content-block">Droits d'Accèss</h2>
    <DxDataGrid
      :show-borders="true"
      :ref="gridRef"
      :data-source="getRoles"
      :column-auto-width="true"
      key-expr="idRole"
      @row-updated="(e) => Update(e)"
      :focused-row-enabled="false"
      class="content-block"
      :repaint-changes-only="true"
      @selection-changed="selectedChanged"
    >
      <DxLoadPanel :enabled="true" />
      <DxPaging :page-size="10" />
      <DxPager :show-page-size-selector="true" :show-info="true" />
      <DxFilterRow :visible="true" />
      <DxEditing
        :allow-updating="true"
        :allow-deleting="false"
        :allow-adding="false"
        refresh-mode="reshape"
        mode="cell"
      >
      </DxEditing>

      <DxSpeedDialAction
        :index="1"
        :visible="false"
        :on-click="exportGrid"
        icon="exportpdf"
        label=""
      />

      <DxColumn caption="Groupe" data-field="idGroup">
        <DxLookup
          :data-source="getGroups"
          display-expr="designation"
          value-expr="idGroup"
        />
      </DxColumn>
      <DxColumn caption="Page" data-field="idPage">
        <DxLookup
          :data-source="getPages"
          display-expr="designation"
          value-expr="idPage"
        />
      </DxColumn>
      <DxColumn caption="Consultation" data-field="read" />
      <DxColumn caption="Ajout" data-field="add" />
      <DxColumn caption="Modification" data-field="update" />
      <DxColumn caption="Suppression" data-field="delete" />

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
import { DxItem } from "devextreme-vue/form";
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
    await this.initRoles();
    await this.initGroups();
    await this.initPages();
  },
  computed: {
    ...mapGetters({
      getRoles: "role/getRoles",
      getGroups: "group/getGroups",
      getPages: "page/getPages",
    }),
    grid() {
      return this.$refs[gridRef].instance;
    },
  },

  methods: {
    ...mapActions({
      initRoles: "role/initRoles",
      addRole: "role/addRole",
      updateRole: "role/updateRole",
      deleteRole: "role/deleteRole",
      initGroups: "group/initGroups",
      initPages: "page/initPages",
    }),
    saveGridInstance: function (e) {
      this.dataGridInstance = e.component;
    },
    refresh: function () {
      this.dataGridInstance.refresh();
    },
    async Insert(e) {
      await this.addRole(e.data)
        .then((response) => {
          console.log(response);
          notify("Le Droit a été ajouté!", "success", 2000);
        })
        .catch((error) => {
          console.log(error);
          notify("Echec d'ajout!", "error", 2000);
        });
    },

    async Update(e) {
      await this.updateRole(e.data)
        .then((response) => {
          console.log(response);
          notify("Le Droit a bien été modifié!", "success", 2000);
        })
        .catch((error) => {
          console.log(error);
          notify("Echec de modification!", "error", 2000);
        });
    },

    async Delete(e) {
      await this.deleteRole(e.data.idRole)
        .then((response) => {
          console.log(response);
          notify("Le Droit a bien été supprimé!", "success", 2000);
        })
        .catch((error) => {
          console.log(error);
          notify("Echec de suppression!", "error", 2000);
        });
    },
    exportGrid() {
      let role = this.getRoles;
      if (!role) {
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
            const header = "Liste des Utilisateurs";
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
          pdfDoc.save("Liste_des_Utilisateurs.pdf");
        });
    },
  },
};
</script>
