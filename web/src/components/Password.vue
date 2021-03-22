<template>
  <v-flex>
    <v-toolbar flat dense>
      <v-toolbar-title>Change password</v-toolbar-title>
    </v-toolbar>

    <v-container fluid px-10 pb-10>
      <v-form v-model="isValid" @submit.prevent>
        <v-row>
          <v-col cols="3">
            <v-subheader :class="{ 'required': isDirty }">Current password</v-subheader>
          </v-col>
          <v-col cols="6">
            <v-text-field
              v-model="currentPassword"
              :rules="currentPasswordRules"
              :error-messages="currentPasswordErrors"
              type="password"
              required
              outlined
              dense
              @input="currentPasswordErrors=[]"
            ></v-text-field>
          </v-col>
        </v-row>

        <v-row>
          <v-col cols="3">
            <v-subheader :class="{ 'required': isDirty }">New password</v-subheader>
          </v-col>
          <v-col cols="6">
            <v-text-field
              v-model="newPassword"
              :rules="newPasswordRules"
              type="password"
              required
              outlined
              dense
            ></v-text-field>
          </v-col>
        </v-row>

        <v-row>
          <v-col cols="3">
            <v-subheader :class="{ 'required': isDirty }">Repeat new password</v-subheader>
          </v-col>
          <v-col cols="6">
            <v-text-field
              v-model="newPasswordRepeat"
              :rules="[...newPasswordRepeatRules, passwordConfirmationRule]"
              type="password"
              required
              outlined
              dense
            ></v-text-field>
          </v-col>
        </v-row>

        <v-row>
          <v-spacer></v-spacer>
          <v-btn 
            :disabled="!isValid" 
            :loading="isSubmitting" 
            color="primary" 
            class="mr-4"
            type="submit"
            @click="changePassword"
          >
            Change password
          </v-btn>
        </v-row>
      </v-form>
    </v-container>
  </v-flex>
</template>

<script>
  import api from '@/services/api/account';

  export default {
    data: () => ({
      isValid: true,
      isSubmitting: false,
      currentPassword: '',
      currentPasswordRules: [
        v => !!v || 'Password is required',
      ],
      newPassword: '',
      newPasswordRules: [
        v => !!v || 'New password is required'
      ],
      newPasswordRepeat: '',
      newPasswordRepeatRules: [
        v => !!v || 'Please repeat the new password'
      ],
      currentPasswordErrors: [],
    }),

    computed: {
      isDirty() {
        return this.newPassword || this.currentPassword || this.newPasswordRepeat;
      },

      passwordConfirmationRule() {
        return () => (this.newPassword === this.newPasswordRepeat) || 'Password must match'
      }
    },

    methods: {
      async changePassword() {
        try {
          this.isSubmitting = true;
          const payload = { 
            currentPassword: this.currentPassword,
            newPassword: this.newPassword,
            newPasswordRepeat: this.newPasswordRepeat
          };
          await api.changePassword(payload);
        } catch(e) {
          if (e && e.response && e.response.status === 400) {
            if (e.response.data.errors && e.response.data.errors.CurrentPassword) {
              this.currentPasswordErrors = e.response.data.errors.CurrentPassword;
            }
          }
        } finally {
          this.isSubmitting = false;
        }        
      }
    }
  }
</script>