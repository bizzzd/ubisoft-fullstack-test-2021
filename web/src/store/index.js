import Vue from 'vue';
import Vuex from 'vuex';

import auth from './modules/auth';
import toast from './modules/toast';

Vue.use(Vuex);

export default new Vuex.Store({
  modules: {
    auth,
    toast,
  }
});
