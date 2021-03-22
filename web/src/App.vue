<template>
 <v-app id="app">
    <transition name="fade">
      <router-view></router-view>
    </transition>

    <v-snackbar
      :color="toastColor"
      :value="toastVisible"
    >
      {{ toastText }}
      <template v-slot:action="{ attrs }">
        <v-btn
          color="white"
          text
          v-bind="attrs"
          @click="hideToast"
        >
          Close
        </v-btn>
      </template>
    </v-snackbar>
  </v-app>
</template>

<script>
  import { mapState, mapMutations } from 'vuex';
  
  import * as types from '@/store/mutation-types';

  export default {
    name: 'AppLayout',
    computed:  mapState({
      toastVisible: state => state.toast.visible, 
      toastText: state => state.toast.text, 
      toastColor: state => state.toast.color
    }),
    methods: {
      ...mapMutations({
        hideToast: types.HIDE_TOAST
      })
    }
  };
</script>
