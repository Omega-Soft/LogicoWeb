<template>
  <div>
    <h2 class="content-block">Interfaces d'Application</h2>
    <DxDataGrid
      :show-borders="true"
      :ref="gridRef"
      :data-source="getPages"
      :column-auto-width="true"
      key-expr="idPage"
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
      >
      </DxEditing>
      <DxColumn caption="Code" data-field="codePage">
        <DxRequiredRule />
      </DxColumn>
      <DxColumn caption="Page" data-field="designation">
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
  DxForm,
  DxPopup,
  DxEmailRule,
  DxPaging,
  DxFilterRow,
  DxLoadPanel,
} from "devextreme-vue/data-grid";
import { DxItem } from "devextreme-vue/form";
import DxSpeedDialAction from "devextreme-vue/speed-dial-action";
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
    await this.initPages();
  },
  computed: {
    ...mapGetters({
      getPages: "page/getPages",
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
      initPages: "page/initPages",
      addPage: "page/addPage",
      updatePage: "page/updatePage",
      deletePage: "page/deletePage",
    }),
    saveGridInstance: function (e) {
      this.dataGridInstance = e.component;
    },
    refresh: function () {
      this.dataGridInstance.refresh();
    },
    async Insert(e) {
      await this.addPage(e.data)
        .then((response) => {
          console.log(response);
          notify("La page a été ajouté!", "success", 2000);
        })
        .catch((error) => {
          console.log(error);
          notify("Echec d'ajout!", "error", 2000);
        });
    },

    async Update(e) {
      await this.updatePage(e.data)
        .then((response) => {
          console.log(response);
          notify("La page a bien été modifiée!", "success", 2000);
        })
        .catch((error) => {
          console.log(error);
          notify("Echec de modification!", "error", 2000);
        });
    },

    async Delete(e) {
      await this.deletePage(e.data.idPage)
        .then((response) => {
          console.log(response);
          notify("La page a bien été supprimée!", "success", 2000);
        })
        .catch((error) => {
          console.log(error);
          notify("Echec de suppression!", "error", 2000);
        });
    },
  },
};
</script>
