<template>
  <v-flex>
    <v-toolbar flat color="transparent">
      <v-toolbar-title>
          My Games
      </v-toolbar-title>
    </v-toolbar>
    <v-divider></v-divider>

    <v-container fluid>
      <v-row>
        <v-col cols="12">
          <redeem-key-alert @newGameAdded="onNewGameAdded"></redeem-key-alert>
        </v-col>
      </v-row>
    </v-container>

      <v-divider></v-divider>

    <v-container fluid>
      <v-row >
        <v-col cols="12">
          <v-text-field
            v-model="searchQuery"
            append-icon="mdi-magnify"
            label="Search"
            filled
            rounded
          ></v-text-field>
        </v-col>
      </v-row>

      <div v-if="isLoading" class="loading-overlay-container pa-10">
        <v-progress-circular
          :size="50"
          color="primary"
          class="loading-spinner"
          indeterminate
        ></v-progress-circular>
      </div>

      <v-row dense v-else>
        <v-col cols="12">
          <my-game 
            v-for="game in filteredGames" 
            :key="game.ownershipId" 
            :game="game"
            class="mb-3"
            :class="{ 'just-added': game.isNew }"
          ></my-game>
        </v-col>
      </v-row>
    </v-container>
  </v-flex>
</template>

<script>  
  import * as types from '@/store/mutation-types';
  import MyGameComponent from '@/components/MyGame.vue';
  import RedeemKeyAlert from '../components/RedeemKeyAlert.vue';
  import api from '@/services/api/game';

  export default {
    components: {
      'my-game': MyGameComponent,
      'redeem-key-alert': RedeemKeyAlert,
    },
    data: () => ({
      isLoading: false,
      games: [],
      searchQuery: '',
    }),

    created() {
      this.loadGames();
    },

    methods: {
      async loadGames() {
        try {
          this.isLoading = true;
          const response = await api.getMyGames();
          this.games = response.data;
        } finally {
          this.isLoading = false;
        }      
      },

      onNewGameAdded(game) {
        this.games.splice(0, 0, { ...game, isNew: true });
        this.$store.commit(types.SHOW_TOAST, { text: `New game added!`, timeout: 3000, color: 'success' });
      }
    },

    computed: {
      filteredGames() {
        return this.games.filter(g => g.name.toLowerCase().includes(this.searchQuery.toLowerCase()));
      }
    }
  }
</script>

<style lang="scss" scoped>
  @keyframes fadein {
    from { opacity: 0; }
    to   { opacity: 1; }
  }  

  .just-added {
   animation: fadein 2s;  
  }
</style>