import Vue from 'vue';
import axios from 'axios';

import localStorage from '@/services/local-storage';
import store from '@/store';
import * as types from '@/store/mutation-types';

axios.defaults.baseURL = process.env.VUE_APP_API_URL || '';
axios.defaults.headers.post['Content-Type'] ='application/json;charset=utf-8';

axios.interceptors.request.use(
  (config) => {
    const urlsExcludedForBearerHeader = ['/account/authenticate']; 
    if (urlsExcludedForBearerHeader.indexOf(config.url) === -1) {
      config.headers.Authorization = `Bearer ${localStorage.getToken()}`;
    }
    return config;
  },
  (error) => Promise.reject(error)
);

// Add a response interceptor
axios.interceptors.response.use(
  (response) => {
    return response;
  },
  (error) => {
    if (error && error.response && error.response.status >= 500) {
      store.commit(types.SHOW_TOAST, { text: 'Internal server error', timeout: 5000, color: 'red' });
    } else if (error && error.response && error.response.status === 401) {
      store.commit(types.SHOW_TOAST, { text: 'Authentication failed', timeout: 5000, color: 'red' });
      store.dispatch('logout');
    }

    return Promise.reject(error)
  }
);

Plugin.install = (Vue) => {
  Vue.axios = axios;
  window.axios = axios;
  Object.defineProperties(Vue.prototype, {
    axios: {
      get() {
        return axios;
      }
    },
    $axios: {
      get() {
        return axios;
      }
    }
  });
};

Vue.use(Plugin);

export default Plugin;