<template>
  <DxTabPanel
    :data-source="tabs"
    :loop="true"
    :animation-enabled="true"
    :swipe-enabled="true"
  >
    <template #item="{ data: tab }">
      <div class="tabpanel-item">
        <Article v-if="tab.title === 'Articles'" :articles="tab.data.data.lignesBl"></Article>
      </div>
    </template>
  </DxTabPanel>
</template>

<script>
import DxTabPanel from 'devextreme-vue/tab-panel';

import Article from "../bon_livraison/articles.vue"

export default {
  props: {
    BlData: {
      type: Object,
      default: ()=>{}
    }
  },
  components: {
    DxTabPanel,
    Article,
  },
  data() {
    return {
      tabs: [
        {
          "title": "Articles", 
          "data": this.BlData
        }
      ],
    };
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
