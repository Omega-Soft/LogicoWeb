<template>
  <DxDataGrid
    :data-source="livraisons"
    :show-borders="true"
    :column-hiding-enabled="true"
  >
    <DxColumn
      data-field="numBl"
      caption="Numero"
    />
    <DxColumn
      data-field="destination"
      caption="Destination"
    />
    <DxColumn
      data-field="dateBl"
      data-type="date"
      caption="Date"
    />
    <DxColumn
      data-field="typeVente"
      caption="Type de vente"
    />
    <DxColumn
      data-field="montantDh"
      caption="Montant (Dhs)"
    />
    <DxColumn
      :width="500"
      data-field="observation"
      caption="Observation"
    />
  </DxDataGrid>
</template>
<script>
import { DxDataGrid, DxColumn } from 'devextreme-vue/data-grid';

export default {
  components: { DxDataGrid, DxColumn },
  props: {
    livraisons: {
      type: Object,
      default: ()=>{}
    }
  },
  beforeMount(){
    console.log(this.livraisons);
  }
};
</script>
<style>
.master-detail-caption {
    padding: 0;
    font-size: 14px;
    font-weight: bold;
}
</style>
