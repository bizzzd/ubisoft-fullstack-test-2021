<template>
  <v-flex>
    <v-toolbar flat color="transparent">
      <v-toolbar-title>Profile</v-toolbar-title>
    </v-toolbar>
    <v-divider></v-divider>

    <v-container fluid pa-0 class="tabs-wrapper">
      <v-tabs class="tabs" vertical background-color="transparent">
        <v-tab v-for="tab in tabs" :key="tab.to" :to="{ name: tab.to }">
          <v-icon left v-text="tab.icon"></v-icon> {{tab.title}}
        </v-tab>
      </v-tabs>

      <v-flex class="tab-content">
          <router-view></router-view>
      </v-flex>
    </v-container>
  </v-flex>
</template>

<script>  
  export default {
    data: () => ({
      tabs: [
        { title: 'Personal info', icon: 'mdi-badge-account-horizontal-outline', to: 'home.profile.personal' },
        { title: 'Change password', icon: 'mdi-lock', to: 'home.profile.password' }
      ]
    })
  }
</script>

<style lang="scss" scoped>
  .tabs-wrapper {
    display: flex;
  }
  .tabs {   
    width: auto;
    flex-shrink: 1;
  }
  .tab-content {
    width: 100%;
  }
</style>