<template>
  <DxForm
    :form-data="client"
    :items="items"
    :col-count="3"
    :customize-item="customizeItem"
    class="address-form form-container"
  >
    <template #form-item="{ data: item }">
      <span>
        {{ item.editorOptions.value }}
      </span>
    </template>
  </DxForm>
</template>

<script>
import { DxForm } from 'devextreme-vue/form';

export default {
  components: { DxForm },
  props: {
    client: {
      type: Object,
      default: ()=>{}
    }
  },
  data() {
    return {
      items: [
        'codeClient',
        'raisonSociale',
        'adresse',
        'email',
        'tel',
        'site',
      ]
    };
  },
  methods: {
    customizeItem(item) {
      item.template = 'form-item';
    }
  },
};
</script>
<style>
.form-container {
  width: 100%;
  padding: 20px;
}
.address-form label {
  width: 30%;
  display: inline-block;
  font-weight: bold;
}
.address-form .dx-field-item-content {
  width: 70%;
  display: inline-block;
}
</style>