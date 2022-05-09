<template>
  <div>
    <h2 class="content-block">Qualités</h2>
    <DxDataGrid
      :show-borders="true"
      :data-source="getQualites"
      :column-auto-width="true"
      key-expr="idQualite"
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
     
      <DxColumn caption="Code Qualite" data-field="codeQualite">
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
  DxEmailRule,
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
    DxEmailRule,
    DxPager,
    DxPaging,
    DxLoadPanel,
  },

  data() {
    
  },

  mounted: async function () {
    await this.initQualites();
  },

  computed: {
    ...mapGetters({
      getQualites: "qualite/getQualites",
    }),
  },

  methods: {
    ...mapActions({
      initQualites: "qualite/initQualites",
      addQualite: "qualite/addQualite",
      updateQualite: "qualite/updateQualite",
      deleteQualite: "qualite/deleteQualite",
    }),
     saveGridInstance: function(e) {
            this.dataGridInstance = e.component;
        },
        refresh: function() {
            this.dataGridInstance.refresh();
        },

    async Insert(e) {
      await this.addQualite(e.data)
        .then((response) => {
          console.log(response);
          notify("La Qualité a été ajouté!", "success", 2000);
        })
        .catch((error) => {
          console.log(error);
          notify("Echec d'ajout!", "error", 2000);
        });
    },

    async Update(e) {
      await this.updateQualite(e.data)
        .then((response) => {
          console.log(response);
          notify("La Qualite a bien été modifié!", "success", 2000);
        })
        .catch((error) => {
            console.log(error);
          notify("Echec de modification!", "error", 2000);
         
        });
       
    },

    async Delete(e) {
      await this.deleteQualite(e.data.idQualite)
        .then((response) => {
          console.log(response);
          notify("La Qualite a bien été supprimé!", "success", 2000);
        })
        .catch((error) => {
          console.log(error);
          notify("Echec de suppression!", "error", 2000);
        });
    },
  },
};
</script>
