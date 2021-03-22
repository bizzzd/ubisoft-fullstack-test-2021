import * as types from '@/store/mutation-types';
  
const mutations = {
  [types.SHOW_TOAST](state, {text, timeout, color}) {
    state.text = text;
    state.color = color;
    state.visible = true;
    if (timeout) {
      state.timeout = setTimeout(() => state.visible = false, timeout);
    }
  },
  [types.HIDE_TOAST](state) {
    state.visible = false;
    state.text = '';
    state.color = '';
    state.timeout = -1;
  }
};

const state = {
  visible: false,
  text: '',
  color: '',
  timeout: -1,
};

export default {
  state,
  mutations
};