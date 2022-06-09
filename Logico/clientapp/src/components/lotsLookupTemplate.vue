<template>
  <DxDropDownBox
    :ref="dropDownBoxRefName"
    :drop-down-options="dropDownOptions"
    :data-source="dataSource"
    v-model:value.sync="currentValue"
    display-expr="codeLot"
    value-expr="idLot"
    content-template="contentTemplate"
  >
    <template #contentTemplate="{}">
      <DxDataGrid
        :data-source="dataSource"
        :remote-operations="true"
        :height="250"
        :selected-row-keys="[currentValue]"
        :hover-state-enabled="true"
        :on-selection-changed="onSelectionChanged"
        :focused-row-enabled="true"
        :focused-row-key="currentValue"
        key-expr="codeLot"
      >
        <DxColumn data-field="idLot" :width="100"/>
        <DxColumn data-field="codeLot" />
        <DxColumn data-field="journee" type="date"/>
        <DxPaging :enabled="true" :page-size="10" />
        <DxFilterRow :visible="true" />
        <DxScrolling mode="infinite" />
        <DxSelection mode="single" />
      </DxDataGrid>
    </template>
  </DxDropDownBox>
</template>
<script>
import {
  DxDataGrid,
  DxFilterRow,
  DxPaging,
  DxSelection,
  DxScrolling,
  DxColumn,
} from "devextreme-vue/data-grid";
import DxDropDownBox from "devextreme-vue/drop-down-box";
const dropDownBoxRefName = "dropDownBoxRef";
export default {
  components: {
    DxDataGrid,
    DxFilterRow,
    DxPaging,
    DxSelection,
    DxScrolling,
    DxColumn,
    DxDropDownBox,
  },
  props: {
    value: {
      type: Number,
      default: null,
    },
    onValueChanged: {
      type: Function,
      default: () => function () {},
    },
    dataSource: {
      type: Object,
      default: () => {},
    },
  },
  data() {
    return {
      currentValue: this.value,
      dropDownOptions: { width: 500 },
      dropDownBoxRefName,
    };
  },
  methods: {
    onSelectionChanged(selectionChangedArgs) {
      this.currentValue = selectionChangedArgs.selectedRowKeys[0];
      this.onValueChanged(this.currentValue);
      if (selectionChangedArgs.selectedRowKeys.length > 0) {
        this.$refs[dropDownBoxRefName].instance.close();
      }
    },
  },
};
</script>