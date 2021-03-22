<template>
 <div class="text-xs-center">
    <v-menu
      v-model="menu"
      max-width="300"
      offset-y
      bottom
      left      
      nudge-bottom="15"
    >
      <template v-slot:activator="{ on }">
        <v-btn
          dark
          icon
          v-on="on"
        >
          <v-icon>mdi-account-circle</v-icon>
        </v-btn>
      </template>

      <v-list>
        <v-list-item>
          <v-list-item-icon>
            <v-avatar color="indigo">
              <v-icon dark>
                mdi-account-circle
              </v-icon>
            </v-avatar>
          </v-list-item-icon>

          <v-list-item-content>
            <v-list-item-title>{{name}}</v-list-item-title>
            <v-list-item-subtitle>{{email}}</v-list-item-subtitle>
          </v-list-item-content>
        </v-list-item>

        <v-divider></v-divider>
      </v-list>

      <v-list nav dense>
        <v-list-item :to="{ name: 'home.profile' }" active-class="no-highlight">
          <v-list-item-icon>
            <v-icon>mdi-account-edit</v-icon>
          </v-list-item-icon>

          <v-list-item-content>
              <v-list-item-title>Manage your account</v-list-item-title>
          </v-list-item-content>
        </v-list-item>

        <v-list-item @click="logout">
          <v-list-item-icon>
            <v-icon>mdi-export</v-icon>
          </v-list-item-icon>

          <v-list-item-content>
              <v-list-item-title>Sign out</v-list-item-title>
          </v-list-item-content>
        </v-list-item>        
      </v-list>
    </v-menu>
  </div>
</template>

<script>
  import { mapState } from 'vuex';

  export default {
    data: () => ({      
      menu: false
    }),
    computed: mapState({
      email: state => state.auth.user.email, 
      name: state => state.auth.user.name, 
    }),
    methods: {
      logout() {
        this.$store.dispatch('logout');
      }
    }
  }
</script>

<style scoped>
.no-highlight.v-list-item--active::before{
  opacity: 0;  
}
</style>