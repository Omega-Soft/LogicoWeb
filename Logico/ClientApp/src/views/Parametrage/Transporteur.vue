<template>
  <div>
    <h2 class="content-block">Transporteurs</h2>
    <DxDataGrid
      :show-borders="true"
      :data-source="getTransporteurs"
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
     
      <DxColumn caption="Code Transporteurs" data-field="code">
        <DxRequiredRule/>
      </DxColumn>

      <DxColumn caption="Raison Sociale" data-field="raisonSociale">
        <DxRequiredRule/>
      </DxColumn> 

      <DxColumn caption="Adresse" data-field="adresse">
      
      </DxColumn>

       <DxColumn caption="Téléphone" data-field="tel">
        
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
    await this.initTransporteurs();
  },

  computed: {
    ...mapGetters({
      getTransporteurs: "transporteur/getTransporteurs",
    }),
  },

  methods: {
    ...mapActions({
      initTransporteurs: "transporteur/initTransporteurs",
      addTransporteur: "transporteur/addTransporteur",
      updateTransporteur: "transporteur/updateTransporteur",
      deleteTransporteur: "transporteur/deleteTransporteur",
    }),
     saveGridInstance: function(e) {
            this.dataGridInstance = e.component;
        },
        refresh: function() {
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
  },
};
</script>