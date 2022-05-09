<template>
  <div>
    <h2 class="content-block">Camions</h2>
    <DxDataGrid
      :show-borders="true"
      :data-source="getCamions"
      :column-auto-width="true"
      key-expr="idCamion"
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
     
    
      <DxColumn caption="Matricule" data-field="matricule">
        <DxRequiredRule/>
      </DxColumn>

        <DxColumn caption="Transporteur" data-field="idTransporteur">
        <DxLookup
          :data-source="getTransporteurs"
          display-expr="raisonSociale"
          value-expr="idTransporteur"
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
    await this.initCamions();
    await this.initTransporteurs();
  },

  computed: {
    ...mapGetters({
      getCamions: "camion/getCamions",
      getTransporteurs:"transporteur/getTransporteurs"
    }),
  },

  methods: {
    ...mapActions({
      initCamions: "camion/initCamions",
      initTransporteurs: "transporteur/initTransporteurs",
      addCamion: "camion/addCamion",
      updateCamion: "camion/updateCamion",
      deleteCamion: "camion/deleteCamion",
    }),
     saveGridInstance: function(e) {
            this.dataGridInstance = e.component;
        },
        refresh: function() {
            this.dataGridInstance.refresh();
        },

    async Insert(e) {
      await this.addCamion(e.data)
        .then((response) => {
          console.log(response);
          console.log(e.data);
          notify("Le Camion a été ajouté!", "success", 2000);
        })
        .catch((error) => {
          console.log(error);
          notify("Echec d'ajout!", "error", 2000);
        });
    },

    async Update(e) {
      await this.updateCamion(e.data)
        .then((response) => {
          console.log(response);
          notify("Le Camion a bien été modifié!", "success", 2000);
        })
        .catch((error) => {
            console.log(error);
          notify("Echec de modification!", "error", 2000);
         
        });
       
    },

    async Delete(e) {
      await this.deleteCamion(e.data.idCamion)
        .then((response) => {
          console.log(response);
          notify("Le Camion a bien été supprimé!", "success", 2000);
        })
        .catch((error) => {
          console.log(error);
          notify("Echec de suppression!", "error", 2000);
        });
    },
  },
};
</script>
