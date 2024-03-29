import 'devextreme/dist/css/dx.common.css';
import './themes/generated/theme.base.css';
import './themes/generated/theme.additional.css';
import { createApp }  from "vue";
import router from "./router";
import {store} from "./store"

import App from "./App";
import appInfo from "./app-info";

const app = createApp(App);
app.use(router);
app.use(store);
app.config.globalProperties.$appInfo = appInfo;
app.mount('#app');
