<template>
  <div>
    <h2 class="content-block">Utilisateurs</h2>
    <DxDataGrid
      :show-borders="true"
      :ref="gridRef"
      :data-source="getUsers"
      :column-auto-width="true"
      key-expr="idUser"
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
      @editor-prepared="editorLauncher"
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
        mode="popup"
      >
      </DxEditing>

      <DxSpeedDialAction
        :index="1"
        :visible="Array.from(getUsers).length > 0"
        :on-click="exportGrid"
        icon="exportpdf"
        label=""
      />
      <DxColumn caption="Username" data-field="username">
        <DxRequiredRule />
      </DxColumn>
      <DxColumn
        caption="Password"
        data-field="password"
        :visible="false"
        name="password"
      >
        <DxRequiredRule />
      </DxColumn>
      <DxColumn caption="Nom" data-field="nom"></DxColumn>

      <DxColumn caption="Prénom" data-field="prenom"></DxColumn>
      <DxColumn caption="Groupe" data-field="idGroup">
        <DxLookup
          :data-source="getGroups"
          display-expr="designation"
          value-expr="idGroup"
        />
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
    await this.initUsers();
    await this.initGroups();
  },
  computed: {
    ...mapGetters({
      getUsers: "user/getUsers",
      getGroups: "group/getGroups",
      getUserPages: "login/getUserPages",
    }),
    userRoles() {
      return this.getUserPages.find((e) => e.page === "ADMIN");
    },
    grid() {
      return this.$refs[gridRef].instance;
    },
  },

  methods: {
    ...mapActions({
      initUsers: "user/initUsers",
      addUser: "user/addUser",
      updateUser: "user/updateUser",
      deleteUser: "user/deleteUser",
      initGroups: "group/initGroups",
    }),
    saveGridInstance: function (e) {
      this.dataGridInstance = e.component;
    },
    refresh: function () {
      this.dataGridInstance.refresh();
    },
    editorLauncher() {
      var pwd = Array.from(
        document.getElementsByClassName("dx-texteditor-input")
      ).find((x) => x.id.includes("password"));
      if (pwd != null) {
        pwd.type = "password";
      }
    },
    async Insert(e) {
      await this.addUser(e.data)
        .then((response) => {
          console.log(response);
          notify("L'Utilisateur a été ajouté!", "success", 2000);
        })
        .catch((error) => {
          console.log(error);
          notify("Echec d'ajout!", "error", 2000);
        });
    },

    async Update(e) {
      await this.updateUser(e.data)
        .then((response) => {
          console.log(response);
          notify("L'utilisateur a bien été modifié!", "success", 2000);
        })
        .catch((error) => {
          console.log(error);
          notify("Echec de modification!", "error", 2000);
        });
    },

    async Delete(e) {
      await this.deleteUser(e.data.idUser)
        .then((response) => {
          console.log(response);
          notify("L'Utilisateur a bien été supprimé!", "success", 2000);
        })
        .catch((error) => {
          console.log(error);
          notify("Echec de suppression!", "error", 2000);
        });
    },
    exportGrid() {
      let user = this.getUsers;
      if (!user) {
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
