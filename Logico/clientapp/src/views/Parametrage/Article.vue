<template>
  <div>
    <h2 class="content-block">Articles</h2>
    <DxDataGrid
      :show-borders="true"
      :data-source="getArticles"
      :column-auto-width="true"
      key-expr="id"
      @row-inserting="(e) => Insert(e)"
      @row-updated="(e) => Update(e)"
      @row-removing="(e) => Delete(e)"
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
      <DxColumn caption="Code Article" data-field="code" />
      <DxColumn caption="Designation" data-field="article" />
      
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
import { mapGetters, mapActions } from "vuex";

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
    };
  },

  mounted: async function () {
    await this.initArticles();
  },
  
  computed: {
    ...mapGetters({
      getArticles: "article/getArticles",
    }),
  },

  methods: {
    ...mapActions({
      initArticles: "article/initArticles",
      addArticle: "article/addArticle",
      // updateArticle: "article/updateArticle",
      // deleteArticle: "article/deleteArticle",
    }),
    
    Insert(e) {
      console.log(e);
      
      this.addArticle(e.data)
        .then((response) => {
          console.log(response);
        })
        .catch((error) => {
          console.log(error);
        });
    },
    update(e) {
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
    delete(e) {
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
  },

};
</script>
