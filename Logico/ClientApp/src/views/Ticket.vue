<template>
  <div>
    <h2 class="content-block">Tickets</h2>
    <DxDataGrid
      :show-borders="true"
      :data-source="dataSource"
      :column-auto-width="true"
      key-expr="id"
      @row-removing="(e) => deleteTicket(e)"
      @row-inserting="(e) => insertTicket(e)"
      @row-updated="(e) => updateTicket(e)"
      :focused-row-enabled="true"
      :selection="{ mode: 'single' }"
      class="content-block"
      @selection-changed="selectedChanged"
    >
      <DxLoadPanel :enabled="true" />
      <DxPaging :page-size="5" />
      <DxPager :show-page-size-selector="true" :show-info="true" />
      <DxFilterRow :visible="true" />
      <DxEditing
        :allow-updating="true"
        :allow-deleting="true"
        :allow-adding="true"
        mode="popup"
      />
      <!-- <DxColumn
      data-field="id"
      :width="100"
      :visible="false"
      :allow-editing="false"
    /> -->
      <DxColumn data-field="titre" />
      <DxColumn data-field="priorite">
        <DxLookup
          :data-source="priorites"
          display-expr="designation"
          value-expr="id"
        />
      </DxColumn>
      <DxColumn data-field="etat">
        <DxLookup
          :data-source="statuts"
          display-expr="designation"
          value-expr="id"
        />
      </DxColumn>
      <DxColumn data-field="utilisateur">
        <DxLookup :data-source="users" display-expr="nom" value-expr="id" />
      </DxColumn>
    </DxDataGrid>
  </div>
</template>
<script>
import axios from "axios";
import {
  DxDataGrid,
  DxColumn,
  DxEditing,
  DxLookup,
  DxPager,
  DxPaging,
  DxFilterRow,
  DxLoadPanel,
} from "devextreme-vue/data-grid";

export default {
  components: {
    DxDataGrid,
    DxColumn,
    DxEditing,
    DxFilterRow,
    DxLookup,
    DxPager,
    DxPaging,
    DxLoadPanel,
  },
  data() {
    return {
      dataSource: [],
      priorites: [],
      statuts: [],
      users: [],
    };
  },
  mounted: async function () {
    await axios.get("https://localhost:44355/api/Tickets").then((response) => {
      console.log(response);
      this.dataSource = response.data;
    }),
      await axios
        .get("https://localhost:44355/api/Priorites")
        .then((response) => {
          console.log(response);
          this.priorites = response.data;
        });
    await axios.get("https://localhost:44355/api/statuts").then((response) => {
      console.log(response);
      this.statuts = response.data;
    });
    await axios.get("https://localhost:44355/api/users").then((response) => {
      console.log(response);
      this.users = response.data;
    });
  },
  methods: {
    deleteTicket(e) {
      var url = "https://localhost:44355/api/Tickets/" + e.data.id;
      axios
        .delete(url)
        .then((response) => {
          console.log(response);
        })
        .catch((error) => {
          console.log(error);
        });
    },

    insertTicket(e) {
      console.log(e);
      var tkt = {
        titre: e.data.titre,
        priorite: e.data.priorite,
        statut: e.data.etat,
        user: e.data.utilisateur,
      };
      axios
        .post("https://localhost:44355/api/Tickets", tkt)
        .then((response) => {
          console.log(response);
        })
        .catch((error) => {
          console.log(error);
        });
    },
    updateTicket(e) {
      console.log(e.data);
      var tkt = {
        titre: e.data.titre,
        priorite: e.data.priorite,
        statut: e.data.etat,
        user: e.data.utilisateur,
      };
      axios
        .put("https://localhost:44355/api/Tickets/" + e.data.id, tkt)
        .then((response) => {
          console.log(response);
        })
        .catch((error) => {
          console.log(error);
        });
    },
  },
};
</script>
