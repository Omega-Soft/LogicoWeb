<template>
  <div class="content-block">
    <!-- <h2>Dashboard</h2> -->
    <h2>Bonjour {{ getSignedUser.prenom + " " + getSignedUser.nom }}</h2>
    <DxResponsiveBox :screen-by-width="screen" single-column-screen="sm">
      <DxRow :ratio="1" />
      <DxRow :ratio="1" />
      <DxRow :ratio="1" />
      <DxRow :ratio="3" screen="sm" />

      <DxCol :ratio="1" />
      <DxCol :ratio="1" />
      <DxCol :ratio="1" />

      <DxCol :ratio="3" screen="sm" />

      <DxItem>
        <DxLocation :row="0" :col="0" :colspan="1" screen="lg" />
        <DxLocation :row="0" :col="0" :colspan="1" screen="md" />
        <DxLocation :row="0" :col="0" :colspan="1" screen="sm" />
        <div
          class="dx-card responsive-paddings"
          @click="onDsClick(dsItems[0].path)"
        >
          <img :src="dsItems[0].img" />
          <h6>{{ dsItems[0].title }}</h6>
        </div>
      </DxItem>
      <DxItem>
        <DxLocation :row="0" :col="1" :colspan="1" screen="lg" />
        <DxLocation :row="0" :col="1" :colspan="1" screen="md" />
        <DxLocation :row="1" :col="0" :colspan="1" screen="sm" />
        <div
          class="dx-card responsive-paddings"
          @click="onDsClick(dsItems[1].path)"
        >
          <img :src="dsItems[1].img" />
          <h6>{{ dsItems[1].title }}</h6>
        </div>
      </DxItem>
      <DxItem>
        <DxLocation :row="0" :col="2" :colspan="1" screen="lg" />
        <DxLocation :row="0" :col="2" :colspan="1" screen="md" />
        <DxLocation :row="2" :col="0" :colspan="1" screen="sm" />
        <div
          class="dx-card responsive-paddings"
          @click="onDsClick(dsItems[2].path)"
        >
          <img :src="dsItems[2].img" />
          <h6>{{ dsItems[2].title }}</h6>
        </div>
      </DxItem>
    </DxResponsiveBox>
  </div>
</template>

<script>
import {
  DxResponsiveBox,
  DxItem,
  DxLocation,
  DxCol,
  DxRow,
} from "devextreme-vue/responsive-box";
import { mapGetters } from "vuex";
export default {
  components: {
    DxResponsiveBox,
    DxItem,
    DxLocation,
    DxCol,
    DxRow,
  },
  data() {
    return {
      screen(width) {
        if (width < 900 && width > 550) return "md";
        if (width <= 550) return "sm";
        else return "lg";
      },
      dsItems: [],
    };
  },
  computed: {
    ...mapGetters({
      getSignedUser: "login/getSignedUser",
    }),
  },
  methods: {
    onDsClick(page) {
      this.$router.push({ name: page });
    },
    getImgUrl(img) {
      var images = require.context("../assets/", false, /\.png$/);
      return images("./" + img);
    },
  },
  beforeMount() {
    let grp = this.getSignedUser.idGroupNavigation.designation.toLowerCase();
    switch (true) {
      case grp.includes("administration"):
        this.dsItems = [
          {
            title: "Utilisateurs",
            img: this.getImgUrl("users.png"),
            path: "users",
          },
          {
            title: "Groupes Utilisateurs",
            img: this.getImgUrl("group.png"),
            path: "groupes",
          },
          {
            title: "Droits d'Accèss",
            img: this.getImgUrl("roles.png"),
            path: "droits-access",
          },
        ];
        break;

      case grp.includes("stock"):
        this.dsItems = [
          {
            title: "Bon Réception",
            img: this.getImgUrl("br.png"),
            path: "list-bon-receptions",
          },
          {
            title: "Lots Receptions",
            img: this.getImgUrl("lot.png"),
            path: "lot-receptions",
          },
          {
            title: "Bon Transport",
            img: this.getImgUrl("bt.png"),
            path: "transport",
          },
        ];
        break;

      case grp.includes("compta"):
        this.dsItems = [
          {
            title: "Ajouter Facture",
            img: this.getImgUrl("addFacture.png"),
            path: "gestion-factures/ajouter/new",
          },
          {
            title: "Facturation",
            img: this.getImgUrl("facture.png"),
            path: "list-factures",
          },
          {
            title: "Bon Réception",
            img: this.getImgUrl("br.png"),
            path: "list-bon-receptions",
          },
        ];
        break;

      case grp.includes("contr"):
        this.dsItems = [
          {
            title: "Bon Réception",
            img: this.getImgUrl("br.png"),
            path: "list-bon-receptions",
          },
          {
            title: "Lots Receptions",
            img: this.getImgUrl("lot.png"),
            path: "lot-receptions",
          },
          {
            title: "Facturation",
            img: this.getImgUrl("facture.png"),
            path: "list-factures",
          },
        ];
        break;

      default:
        break;
    }
    console.log(this.dsItems);
  },
};
</script>
<style lang="scss" scoped>
.dx-card {
  border-radius: 7px;
  cursor: pointer;
  text-align: center;
  margin-top: 25px;
  padding-top: 20px !important;
  padding-bottom: 15px !important;
  margin-inline: 10px;
  // max-width: 260px !important;
  max-height: 250px;
  /* height: auto !important; */
  h6 {
    margin: 0;
    margin-top: 5px;
  }
  img {
    max-height: 100px;
    max-width: 100px;
    margin: 0px auto;
  }
}
.dx-card:hover {
  box-shadow: none;
}
</style>
