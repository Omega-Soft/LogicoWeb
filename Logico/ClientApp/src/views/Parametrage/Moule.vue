<template>
  <div>
    <h2 class="content-block">Moules</h2>
    <DxDataGrid
      :show-borders="true"
      :data-source="getMoules"
      :column-auto-width="true"
      key-expr="idMoule"
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
      <DxPaging :page-size="10"  />
      <DxPager :show-page-size-selector="true" :show-info="true" />
      <DxFilterRow :visible="true" />
      <DxEditing
        :allow-updating="true"
        :allow-deleting="true"
        :allow-adding="true"
        refresh-mode="reshape"
        mode="popup"
      />
     
      <DxColumn caption="Code Moule" data-field="codeMoule">
        <DxRequiredRule/>
      </DxColumn>

      <DxColumn caption="Designation" data-field="designation">
        <DxRequiredRule/>
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
import { mapGetters, mapActions } from "vuex";

export default {
  components: {
    DxDataGrid,
    DxColumn,
    DxEditing,
    DxRequiredRule,
    DxFilterRow,
    DxLookup,
    DxPager,
    DxPaging,
    DxLoadPanel,
  },

  data() {
    
  },

  mounted: async function () {
    await this.initMoules();
  },

  computed: {
    ...mapGetters({
      getMoules: "moule/getMoules",
    }),
  },

  methods: {
    ...mapActions({
      initMoules: "moule/initMoules",
      addMoule: "moule/addMoule",
      updateMoule: "moule/updateMoule",
      deleteMoule: "moule/deleteMoule",
    }),
     saveGridInstance: function(e) {
            this.dataGridInstance = e.component;
        },
        refresh: function() {
            this.dataGridInstance.refresh();
        },

    async Insert(e) {
      await this.addMoule(e.data)
        .then((response) => {
          console.log(response);
          notify("Le Moule a été ajouté!", "success", 2000);
        })
        .catch((error) => {
          console.log(error);
          notify("Echec d'ajout!", "error", 2000);
        });
    },

    async Update(e) {
      await this.updateMoule(e.data)
        .then((response) => {
          console.log(response);
          notify("Le Moule a bien été modifié!", "success", 2000);
        })
        .catch((error) => {
            console.log(error);
          notify("Echec de modification!", "error", 2000);
         
        });
       
    },

    async Delete(e) {
      await this.deleteMoule(e.data.idMoule)
        .then((response) => {
          console.log(response);
          notify("Le Moule a bien été supprimé!", "success", 2000);
        })
        .catch((error) => {
          console.log(error);
          notify("Echec de suppression!", "error", 2000);
        });
    },
  },
};
</script>
