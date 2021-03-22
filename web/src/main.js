import Vue from 'vue';

import './plugins/axios'
import './plugins/filters';
import AppLayout from './App.vue';
import vuetify from './plugins/vuetify';
import store from './store'
import router from './router'

import '@/styles/main.scss';

Vue.config.productionTip = false;

new Vue({
  vuetify,
  store,
  router,
  render: h => h(AppLayout)
}).$mount('#app')
