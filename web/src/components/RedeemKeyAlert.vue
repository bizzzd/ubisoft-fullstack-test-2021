<template>
  <v-alert
      prominent
      type="info"
    >
    <v-row align="center">
      <v-col class="grow">
        Got a key for a new game? Click the button to activate it!
      </v-col>
      <v-col class="shrink pr-6">
        <redeem-key-dialog @newGameAdded="onNewGameAdded"></redeem-key-dialog>
      </v-col>
    </v-row>
  </v-alert>
</template>

<script>
  import RedeemKeyDialog from './RedeemKeyDialog.vue';

  export default {
    components: {
      'redeem-key-dialog': RedeemKeyDialog,
    },
    methods: {
      onNewGameAdded(game) {
        this.$emit('newGameAdded', game);
      }
    }
  }
</script>
