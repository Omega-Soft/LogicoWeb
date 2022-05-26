<template>
  <DxTabPanel
    :data-source="tabs"
    :loop="true"
    :animation-enabled="true"
    :swipe-enabled="true"
  >
    <template #title="{ data: tab }">
      <span> {{ tab.title }} </span>
    </template>
    <template #item="{ data: tab }">
      <div class="tabpanel-item">
        <DxDataGrid
          v-if="tab.title === 'Users'"
          :data-source="tab.data"
          :show-borders="true"
          :column-auto-width="true"
          :column-hiding-enabled="true"
        >
          <DxColumn caption="Username" data-field="username"/>
          <DxColumn caption="Nom" data-field="nom"/>
          <DxColumn caption="Prénom" data-field="prenom"/>
        </DxDataGrid>
        <DxDataGrid
          v-if="tab.title === 'Pages'"
          :data-source="tab.data"
          :show-borders="true"
          :column-auto-width="true"
          :column-hiding-enabled="true"
        >
          <DxColumn caption="Code" data-field="codePage"/>
          <DxColumn caption="Interface" data-field="designation"/>
          <DxColumn caption="Ajout" data-field="add"/>
          <DxColumn caption="Modification" data-field="update"/>
          <DxColumn caption="Suppression" data-field="delete"/>
        </DxDataGrid>
      </div>
    </template>
  </DxTabPanel>
</template>

<script>
import DxTabPanel from "devextreme-vue/tab-panel";
import { DxDataGrid,DxColumn } from "devextreme-vue/data-grid";

// import Article from "../views/Parametrage/Article.vue"
// import Client from './client.vue';

export default {
  props: {
    users: {
      type: Object,
      default: () => {},
    },
  },
  components: {
    DxTabPanel,
    DxDataGrid,
    DxColumn
    // Client
  },
  data() {
    return {
      tabs: [
        {
          title: "Users",
          data: this.users.data.users,
        },
        {
          title: "Pages",
          data: this.users.data.pages,
        },
      ],
    };
  },
};
</script>

<style>
.tabpanel-item {
  /* height: 200px; */
  user-select: none;
  margin: 0px;
  padding-left: 0px;
  padding-top: 20px;
}

.mobile .tabpanel-item {
  padding-top: 10px;
}

.tabpanel-item > div {
  float: left;
  padding: 0;
}

.tabpanel-item p {
  font-size: 16px;
  margin: 0;
}

.item-box {
  font-size: 16px;
  margin: 15px 0 45px 10px;
}

.caption {
  font-size: 18px;
  font-weight: 500;
}

.option {
  margin-top: 10px;
}
</style>
