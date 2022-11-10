import { createApp } from 'vue'
import {store} from "./store/store";
import App from './App.vue'

import toast from "vue-toastification";
import "vue-toastification/dist/index.css";

const app = createApp(App)
app.use(store)
app.use(toast)
app.mount('#app')
