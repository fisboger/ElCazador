import Vue from 'vue'
import axios from 'axios'
import router from './router'
import store from './store'
import { sync } from 'vuex-router-sync'
import App from 'components/app-root'

import ElementUI from 'element-ui'

Vue.use(ElementUI);

var signalR = require('@aspnet/signalr/dist/browser/signalr.min.js');

Vue.prototype.$http = axios;
Vue.prototype.$signalR = signalR;

sync(store, router)


const app = new Vue({
    store,
    router,
    ...App
})

export {
    app,
    router,
    store
}