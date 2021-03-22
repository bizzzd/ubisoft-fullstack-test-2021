<template>
  <v-flex class="loading-overlay-container">
    <v-toolbar flat dense>
      <v-toolbar-title>Personal info</v-toolbar-title>
    </v-toolbar>

    <v-container fluid px-10 pb-10>
      <v-form v-model="isValid" @submit.prevent>
        <v-row>
          <v-col cols="3">
            <v-subheader class="required">First Name</v-subheader>
          </v-col>
          <v-col cols="6">
            <v-text-field
              v-model="firstName"
              :rules="firstNameRules"
              outlined
              dense
            ></v-text-field>
          </v-col>
        </v-row>

        <v-row>
          <v-col cols="3">
            <v-subheader class="required">Last Name</v-subheader>
          </v-col>
          <v-col cols="6">
            <v-text-field
              v-model="lastName"
              :rules="lastNameRules"
              outlined
              dense
            ></v-text-field>
          </v-col>
        </v-row>

        <v-row>
          <v-col cols="3">
            <v-subheader class="required">E-mail</v-subheader>
          </v-col>
          <v-col cols="6">
            <v-text-field
              v-model="email"
              :rules="emailRules"
              :error-messages="emailAsyncErrors"
              outlined
              dense
              @input="debounceEmailCheck"              
            ></v-text-field>
          </v-col>
        </v-row>

        <v-row>
          <v-spacer></v-spacer>
          <v-btn 
            :disabled="!isValid || emailPendingAsyncCheck"
            :loading="isSubmitting"
            color="primary" 
            class="mr-4"
            type="submit"
            @click="save"
          >
            Save
          </v-btn>
        </v-row>
      </v-form>

      <div class="loading-overlay" v-if="isLoading">
        <v-progress-circular
          :size="50"
          color="primary"
          class="loading-spinner"
          indeterminate
        ></v-progress-circular>
      </div>
    </v-container>
  </v-flex>
</template>

<script>
  import axios from 'axios';
  import debounce from 'lodash.debounce';
  import api from '@/services/api/account';

  export default {
    data: () => ({
      isValid: true,
      isLoading: false,      
      isSubmitting: false,
      firstName: '',
      firstNameRules: [
        v => !!v || 'First Name is required',
        v => (v && v.length <= 100) || 'First Name must be less than 100 characters',
      ],
      lastName: '',
      lastNameRules: [
        v => !!v || 'Last Name is required',
        v => (v && v.length <= 100) || 'Last Name must be less than 100 characters',
      ],
      email: '',
      emailRules: [
        v => !!v || 'E-mail is required',
        v => /.+@.+\..+/.test(v) || 'E-mail must be valid',
        v => (v && v.length <= 255) || 'E-mail must be less than 255 characters',
      ],
      emailPendingAsyncCheck: false,
      emailAsyncErrors: [], 
      emailCheckCancelToken: null,
    }),

    async created() {
      this.debounceEmailCheck = debounce(this.validateEmailUniqueness, 100);

      try {
        this.isLoading = true;
        const response = await api.getPersonalInfo();
        
        this.email = response.data.email;
        this.firstName = response.data.firstName;
        this.lastName = response.data.lastName;
      } finally {
        this.isLoading = false;
      }
    },

    methods: {
      async validateEmailUniqueness() {
        if (this.emailCheckCancelToken) {
          this.emailCheckCancelToken.cancel();
        }

        this.emailCheckCancelToken = axios.CancelToken.source();
        this.emailAsyncErrors = [];
        this.emailPendingAsyncCheck = true;

        try {
          const response = await api.validateEmailUnique(this.email, this.emailCheckCancelToken.token);
          if (!response.data) {
            this.emailAsyncErrors = [`E-mail ${this.email} is already used by another user`];
          }
        } finally {
          this.emailPendingAsyncCheck = false;
        }
      },

      async save() {
        try {
          this.isSubmitting = true;
          const payload = { 
            email: this.email,
            firstName: this.firstName,
            lastName: this.lastName
          };
          await api.updatePersonalInfo(payload);
          this.$store.dispatch('updateUserPersonalInfo', payload);
        } finally {
          this.isSubmitting = false;
        }
      }
    }
  }
</script>
