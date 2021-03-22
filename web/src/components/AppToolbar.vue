<template>
  <v-app-bar app dark flat>
    <v-container class="py-0 fill-height">
      <span class="mr-10" >
        <img src="@/assets/logo.svg" width="50">
        <div>Acme Games</div>
      </span>

      <v-btn
        v-for="link in visibleLinks"
        :key="link.title"
        :to="{ name: link.to }"
        text
      >
        {{ link.title }}
      </v-btn>

      <v-spacer></v-spacer>

      <logged-in-user-menu></logged-in-user-menu>
    </v-container>
  </v-app-bar>
</template>

<script>
  import LoggedInUserMenu from './LoggedInUserMenu.vue';
  export default {
    components: { LoggedInUserMenu },
    data: () => ({
      links: [
        { title: 'My Games', to: 'home.mygames', userOnly: true },
        { title: 'Users', to: 'home.users', adminOnly: true },
        { title: 'Profile', to: 'home.profile' }, 
      ],      
    }),
    computed: {
      visibleLinks: function() {
        const isAdmin = this.$store.state.auth.user.isAdmin;
        return this.links.filter(l => (l.userOnly && !isAdmin) || (l.adminOnly && isAdmin) || (!l.userOnly && !l.adminOnly));
      }
    }
  }
</script>