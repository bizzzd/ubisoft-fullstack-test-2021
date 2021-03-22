<template>
  <v-flex>
    <v-toolbar flat color="transparent">
      <v-toolbar-title>
          Users
      </v-toolbar-title>
    </v-toolbar>
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

      <edit-user-dialog
        :open="!!userToEdit" 
        :user="userToEdit" 
        @update:open="userToEdit=null"
        @updated="onUserUpdated"
      ></edit-user-dialog>

      <v-data-table
        :headers="headers"
        :items="users"
        :search="searchQuery"
        :loading="isLoading"
      >
        <template v-slot:item.dateOfBirth="{ item }">
            {{ item.dateOfBirth | formatDate }}
        </template>

        <template v-slot:item.isAdmin="{ item }">
            {{ item.isAdmin ? 'Yes' : 'No' }}
        </template>

        <template v-slot:item.actions="{ item }">
          <v-icon
            small
            class="mr-2"
            @click="openEditDialog(item)"
          >
            mdi-pencil
          </v-icon>
        </template>
      </v-data-table>
    </v-container>
  </v-flex>
</template>

<script>  
  import * as types from '@/store/mutation-types';
  import api from '@/services/api/user';
  import EditUserDialog from '../components/EditUserDialog.vue';

  export default {
    components: {
        EditUserDialog
    },
    data: () => ({
      isLoading: false,
      users: [],
      searchQuery: '',
      userToEdit: null,
      headers: [
          { text: 'User Account Id', value: 'userAccountId', sortable: false },
          { text: 'Name', value: 'fullName',  },
          { text: 'Date of Birth', value: 'dateOfBirth' },
          { text: 'E-mail', value: 'emailAddress' },
          { text: 'Admin', value: 'isAdmin' },
          { text: 'Actions', value: 'actions', sortable: false }
      ]
    }),

    created() {
      this.loadUsers();
    },

    methods: {
      async loadUsers() {
        try {
          this.isLoading = true;
          const response = await api.getUsers();
          this.users = response.data;
        } finally {
          this.isLoading = false;
        }      
      },

      openEditDialog(user) {
        this.userToEdit = user;
      },

      onUserUpdated(user) {
        const idx = this.users.findIndex(u => u.userAccountId === user.userAccountId);
        if (idx !== -1) {
          this.$set(this.users, idx, user);
          this.$store.commit(types.SHOW_TOAST, { text: `User account updated`, timeout: 2000, color: 'success' });
        }
      }
    }
  }
</script>
