import * as types from '@/store/mutation-types';
import router from '@/router';
//import { handleError } from '@/utils/utils';
import api from '@/services/api/account';
import localStorage from '@/services/local-storage';

const getters = {
  isAuthenticated: (state) => !!state.token
};

const actions = {
  async login({ commit }, payload) {
    try {
      const response = await api.authenticate(payload);
      const user = {
        email: response.data.email,
        name: response.data.name,
        isAdmin: response.data.isAdmin
      };

      localStorage.storeUser(user);
      localStorage.storeToken(response.data.accessToken);
  
      commit(types.SAVE_USER, user);
      commit(types.SAVE_TOKEN, response.data.accessToken);
  
      router.push({ name: 'home' });
    } catch(e) {
      if (e && e.response && e.response.status === 401) {
        throw 'Email or password is invalid';
      } else if (e.response && e.response.statusText) {
        throw e.response.statusText;
      } else {
        throw 'Unexpected error';
      }
    }
  },

  updateUserPersonalInfo({ commit, state }, user) {
    const userData = { ...state.user };
    userData.email = user.email;
    userData.name = `${user.firstName} ${user.lastName}`;
    commit(types.SAVE_USER, userData);
    localStorage.storeUser(userData);
  },

  logout({ commit }) {
    localStorage.reset();

    commit(types.LOGOUT);
    router.push({ name: 'login' });
  }
}

const mutations = {
  [types.LOGOUT](state) {
    state.user = null;
    state.token = null;
  },
  [types.SAVE_TOKEN](state, token) {
    state.token = token;
  },
  [types.SAVE_USER](state, user) {
    state.user = user;
  }
};

const state = {
  user: localStorage.getUser(),
  token: localStorage.getToken() || null
};

export default {
  state,
  getters,
  actions,
  mutations
};