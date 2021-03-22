<template>
  <v-card class="game-card">
    <div class="d-flex flex-no-wrap justify-space-between">
      <div>
        <v-card-title
          class="headline"
          v-text="game.name"
        ></v-card-title>

        <v-card-subtitle>
          <div>Date registered: {{ game.registeredDate | formatDate }}</div>
          <div>Age restriction: {{ game.ageRestriction || 'None' }}</div>
        </v-card-subtitle>

      </div>

      <v-avatar
        class="ma-3"
        size="85"
        tile
      >
        <v-img :src="game.thumbnail"></v-img>
      </v-avatar>
    </div>
  </v-card>
</template>

<script>
  export default {
    props: {
      game: Object,
    }
  }
</script>

<style lang="scss" scoped>
  .game-card {
    background: linear-gradient(0deg, rgba(247,247,247,1) 0%, rgba(220,220,220,1) 100%);
  }
</style>