<template>
  <v-flex grow>
    <app-toolbar></app-toolbar>
    <v-main class="grey lighten-3 fill-height">
      <v-container>
          <v-sheet
            class="mt-4"
            fill-height
            min-height="40vh"
            rounded="lg"
          >
            <router-view></router-view>
          </v-sheet>
      </v-container>
    </v-main>
  </v-flex>
</template>

<script>
  import AppToolbar from '../components/AppToolbar.vue';

  export default {
    components: { AppToolbar },
    data: () => ({
      links: [
        { title: 'My Games', to: 'mygames' },
        { title: 'Profile', to: 'account' }, 
        { title: 'Users', to: 'users', adminOnly: true },
      ],      
    }),
    computed: {
      visibleLinks: function() {
        const isAdmin = this.$store.state.auth.user.isAdmin;
        return this.links.filter(l => !l.adminOnly || isAdmin);
      },
    }
}
</script>