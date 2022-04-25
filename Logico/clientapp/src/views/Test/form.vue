<template>
  <h2 class="content-block">{{action}} Tickets</h2>
  <div class="content-block">
    <div class="field content-block">
      <div class="label">Title</div>

      <DxTextBox
       v-model:value="getTicket.titre"
        :max-length="40"
        value-change-event="keyup"
      />

      <div class="label espace">Message</div>
      <DxTextArea
        :height="90"
        :max-length="maxLength"
        v-model:value="getTicket.message"
      />

      <div class="prt">
        <div class="espace">Priorité</div>
        <DxSelectBox
          :data-source="getPriorites"
          display-expr="designation"
          v-model:value="getTicket.priorite"
          value-expr="id"
        />
      </div>

      <div class="sts">
        <div class="espace">Statut</div>
        <DxSelectBox
          :data-source="getStatuts"
          display-expr="designation"
          v-model:value="getTicket.statut"
          value-expr="id"
        />
        <DxButton text="cancel" type="danger" styling-mode="text" @click="() =>  this.$router.go(-1)" class="btn" />
        <DxButton
          text="save"
          type="success"
          styling-mode="text"
          @click="addTicket()"
          class="btn"
        />
      </div>
    </div>
  </div>
</template>

<script>
import DxTextBox from "devextreme-vue/text-box";
import DxTextArea from "devextreme-vue/text-area";
import DxSelectBox from "devextreme-vue/select-box";
import DxButton from "devextreme-vue/button";
import notify from "devextreme/ui/notify";
import DxLookup from "devextreme-vue/data-grid";
import { mapGetters, mapActions } from "vuex";

export default {
  components: {
    DxTextBox,
    DxTextArea,
    DxSelectBox,
    DxButton,
    DxLookup,
    notify
  },
  data() {
    return {
      
      action: this.$route.params.action,
      id: parseInt(this.$route.params.id),
      ticket: {
        titre: "",
        priorite: 1,
        statut: 1,
        user: 1,
        message: "",
      },
    };
  },
  mounted: async function () {
    await this.initPriorites();
    await this.initStatuts();
    console.log("ghgfdfg");
    console.log(this.getTicket);
  },
  computed: {
    ...mapGetters({
      getTicket:"ticket/getCurrentTicket",
      getPriorites: "priority/getPriorites",
      getStatuts: "status/getStatuts",
    }),
  },
  methods: {
    ...mapActions({
      addTicketApi: "ticket/addTicket",
      initPriorites: "priority/initPriorites",
      initStatuts: "status/initStatuts",
       initDataForUpdate: "ticket/initDataForUpdate",
    }), 
    addTicket: function () {
       if (this.action == "Ajouter") {
     this.addTicketApi().then(() => {
              notify(
                "Le Ticket est enegistrer",
                "success",
                2000
              );
              this.$router.go(-1);    //this.$router.push({name: "ticketStore"}); 
            })
            .catch(() => {
              notify("Echec..!!", "error", 1500);
            });;
       } else if (this.action == "Modifier" && this.id > 0) {
         this.ticket["id"]=this.id;
          this.$store.dispatch("ticket/updateTicket", this.id );
          notify(
            "Le ticket a etes modifié avec success...!!",
            "success",
            2000
          );
          this.$router.go(-1);
        }
     
    }
    
  },
  beforeMount() {
    if (this.action == "Modifier" && this.id > 0) {
      this.initDataForUpdate(this.id)
        .catch(() => {
          this.$router.back();
        });
    }
  },
  beforeUnmount() {
    var currentTicket={
        titre: "",
        priorite: 1,
        statut: 1,
        user: 1,
        message: "",
      }
    this.$store.commit("ticket/setCurrentTicket",currentTicket);
  },
};
</script>

<style scoped>
.espace {
  margin-top: 25px;
}
.prt {
  width: 45%;
  float: left;
}
.sts {
  width: 45%;
  float: right;
}
.btn {
  margin-top: 20px;
  float: right;
}
</style>
