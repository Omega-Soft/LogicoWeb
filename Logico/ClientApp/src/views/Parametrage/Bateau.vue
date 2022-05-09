<template>
  <div>
    <h2 class="content-block">Bateaux</h2>
    <DxDataGrid
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
     
      <DxColumn caption="Code Bateau" data-field="code">
        <DxRequiredRule/>
      </DxColumn>

      <DxColumn caption="Nom" data-field="nom">
        <DxRequiredRule/>
      </DxColumn>

      <DxColumn caption="Type Bateau" data-field="type">
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
    await this.initBateaux();
  },

  computed: {
    ...mapGetters({
      getBateaux: "bateau/getBateaux",
    }),
  },

  methods: {
    ...mapActions({
      initBateaux: "bateau/initBateaux",
      addBateau: "bateau/addBateau",
      updateBateau: "bateau/updateBateau",
      deleteBateau: "bateau/deleteBateau",
    }),
     saveGridInstance: function(e) {
            this.dataGridInstance = e.component;
        },
        refresh: function() {
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
  },
};
</script>
