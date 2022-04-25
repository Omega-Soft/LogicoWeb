<template>
  <div>
    <h2 class="content-block">Articles</h2>
    <DxDataGrid
      :show-borders="true"
      :data-source="getArticles"
      :column-auto-width="true"
      key-expr="idArticle"
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
      <DxColumn caption="Code Article" data-field="codeArticle">
        <DxRequiredRule/>
      </DxColumn>
      <DxColumn caption="Designation" data-field="designation">
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
      updateArticle: "article/updateArticle",
      deleteArticle: "article/deleteArticle",
    }),

    async Insert(e) {
      await this.addArticle(e.data)
        .then((response) => {
          console.log(response);
          notify("L'Article a bien été ajouté!", "success", 2000);
        })
        .catch((error) => {
          console.log(error);
          notify("Echec d'ajout!", "error", 2000);
        });
    },

    async Update(e) {
      await this.updateArticle(e.data)
        .then((response) => {
          console.log(response);
          notify("L'Article a bien été modifié!", "success", 2000);
        })
        .catch((error) => {
          console.log(error);
          notify("Echec de modification!", "error", 2000);
        });
    },

    async Delete(e) {
      await this.deleteArticle(e.data.idArticle)
        .then((response) => {
          console.log(response);
          notify("L'Article a bien été supprimé!", "success", 2000);
        })
        .catch((error) => {
          console.log(error);
          notify("Echec de suppression!", "error", 2000);
        });
    },
  },
};
</script>
