<template>
  <DxTabPanel
    :data-source="tabs"
    :loop="true"
    :animation-enabled="true"
    :swipe-enabled="true"
  >

    <template #item="{ data: tab }">
      <div class="tabpanel-item">
        <Livraisons v-if="tab.title === 'Livraisons'" :livraisons="tab.data.data.bls"></Livraisons>
        <Client v-else-if="tab.title === 'Client'" :client="tab.data.data.client"></Client>
      </div>
    </template>
  </DxTabPanel>
</template>

<script>
import DxTabPanel from 'devextreme-vue/tab-panel';

import Livraisons from "./livraisons.vue"
import Client from './client.vue';

export default {
  props: {
    FactureData: {
      type: Object,
      default: ()=>{}
    }
  },
  components: {
    DxTabPanel,
    Livraisons,
    Client
  },
  data() {
    return {
      tabs: [
        {
          "title": "Livraisons", 
          "data": this.FactureData
        },
        {
          "title": "Client",
          "data": this.FactureData
        },
      ],
    };
  },
  beforeMount() {
    console.log(this.FactureData.data);
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
