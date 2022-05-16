<template>
  <DxTabPanel
    :data-source="tabs"
    :loop="true"
    :animation-enabled="true"
    :swipe-enabled="true"
  >
    <!-- <template #title="{ data: tab }">
      <span> {{tab.title}} </span>
    </template> -->
    <template #item="{ data: tab }">
        <DxDataGrid
          v-if="tab.title === 'Articles'"
          :data-source="tab.data"
          :show-borders="true"
          :column-auto-width="true"
          :column-hiding-enabled="true"
        > 
        </DxDataGrid>
      <!-- <div class="tabpanel-item"> -->
        <!-- <Article v-if="tab.title === 'Articles'" :articles="tab.data"></Article> -->
        <!-- <Client v-else-if="tab.title === 'Client'" :client="tab.data.data.client"></Client> -->
      <!-- </div> -->
    </template>
  </DxTabPanel>
</template>

<script>
import DxTabPanel from 'devextreme-vue/tab-panel';
import { DxDataGrid } from 'devextreme-vue/data-grid';

// import Article from "../views/Parametrage/Article.vue"
// import Client from './client.vue';

export default {
  props: {
    BrData: {
      type: Object,
      default: ()=>{}
    }
  },
  components: {
    DxTabPanel,
    DxDataGrid,
    // Client
  },
  data() {
    return {
      tabs: [
        {
          title: "Articles", 
          data: this.BrData.data.articles
        },
        // {
        //   title: "Client",
        //   data: this.BrData
        // },
      ],
    };
  },
  beforeMount: function () {
    console.log("master details ....");
    console.log(this.tabs[0].data[0]);
  }
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

.tabpanel-item  > div {
    float: left;
    padding: 0
}

.tabpanel-item  p {
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
