<template>
  <div>
    <h2 class="content-block">Tickets</h2>
    <DxDataGrid
      :show-borders="true"
      :data-source="getTickets"
      :focused-row-enabled="true"
      :column-auto-width="true"
      :column-hiding-enabled="true"
      :selection="{ mode: 'single' }"
      key-expr="id"
      class="content-block"
      @selection-changed="selectedChanged"
      @row-removing="(e) => deleteTicket(e)"
    >
      <DxLoadPanel :enabled="true" />
     
      <DxPaging :page-size="1" />
      <DxPager :show-page-size-selector="true" :show-info="true" />
      <DxFilterRow :visible="true" />
      <DxScrolling mode="virtual" row-rendering-mode="virtual" />
      <DxSpeedDialAction
        :on-click="addTicket"
        :index="1"
        icon="add"
        :label="selectedRowIndex !== -1 ? 'Ajouter' : ''"
      />
      <DxSpeedDialAction
        :visible="selectedRowIndex !== -1"
        :on-click="updateTicket"
        :index="2"
        icon="edit"
        label="Modifier"
      />

      <DxEditing
        :allow-updating="false"
        :allow-deleting="true"
        :allow-adding="false"
        mode="form"
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
          :data-source="getPriorites"
          display-expr="designation"
          value-expr="id"
        />
      </DxColumn>
      <DxColumn data-field="etat">
        <DxLookup
          :data-source="getStatuts"
          display-expr="designation"
          value-expr="id"
        />
      </DxColumn>
      <DxColumn data-field="utilisateur">
        <DxLookup :data-source="getUsers" display-expr="nom" value-expr="id" />
      </DxColumn>
    </DxDataGrid>
  </div>
</template>
<script>
import {
  DxDataGrid,
  DxColumn,
  DxEditing,
  DxLookup,
  DxPager,
  DxPaging,
  DxFilterRow,
  DxScrolling,
  DxLoadPanel,
} from "devextreme-vue/data-grid";
import { mapGetters, mapActions } from "vuex";
import DxSpeedDialAction from "devextreme-vue/speed-dial-action";

export default {
  components: {
    DxDataGrid,
    DxColumn,
    DxEditing,
    DxFilterRow,
    DxScrolling,
    DxLookup,
    DxPager,
    DxPaging,
    DxLoadPanel,
    DxSpeedDialAction,
  },
  data() {
    return {
      selectedId: null,
      selectedRowIndex: -1,
    };
  },
  mounted: async function () {
    await this.initTickets();
    await this.initPriorites();
    await this.initStatuts();
    await this.initUsers();
  },
  computed: {
    ...mapGetters({
      getTickets: "ticket/getTickets",
      getPriorites: "priority/getPriorites",
      getStatuts: "status/getStatuts",
      getUsers: "user/getUsers",
    }),
  },
  methods: {
    ...mapActions({
      initTickets: "ticket/initTickets",
      // addTicket: "ticket/addTicket",
      //updateTicket: "ticket/updateTicket",
      deleteTicket: "ticket/deleteTicket",
      initPriorites: "priority/initPriorites",
      initStatuts: "status/initStatuts",
      initUsers: "user/initUsers",
    }),
    selectedChanged(e) {
      this.selectedId = e.selectedRowKeys[0];
      this.selectedRowIndex = e.component.getRowIndexByKey(
        e.selectedRowKeys[0]
      );
    },
    addTicket: function () {
      this.$router.push({
        name: "form",
        params: { action: "Ajouter", id: "nouveau" },
      });
    },
    updateTicket: function () {
      this.$router.push({
        name: "form",
        params: { action: "Modifier", id: this.selectedId },
      });
    },
  },
};
</script>
