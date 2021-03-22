<template>
  <v-row justify="center">
    <v-dialog
      v-model="open"
      persistent
      max-width="600px"
    >
      <v-card>
        <v-form v-model="isValid" @submit.prevent>
          <v-card-title>
            <span class="headline">Edit user account</span>
          </v-card-title>
          <v-card-text>
            <v-container>
              <v-row>
                <v-col col="6">
                  <v-text-field
                    v-model="firstName"
                    :rules="firstNameRules"
                    label="First Name"
                  ></v-text-field>
                </v-col>
                <v-col col="6">
                  <v-text-field
                    v-model="lastName"
                    :rules="lastNameRules"
                    label="Last Name"
                  ></v-text-field>
                </v-col>
              </v-row>
              <v-row>
                <v-col col="6">
                  <v-text-field
                    v-model="password"
                    :rules="passwordRules"
                    label="Password"
                  ></v-text-field>
                </v-col>
                <v-col col="6">
                  <v-text-field
                    v-model="email"
                    :rules="emailRules"
                    :error-messages="emailErrors"
                    @input="emailErrors=[]"
                    label="E-mail"
                  ></v-text-field>
                </v-col>                
              </v-row>
              <v-row>
                <v-col cols="6">
                  <v-menu
                    v-model="datePickerMenu"
                    :close-on-content-click="false"
                    transition="scale-transition"
                    offset-y
                    min-width="auto"
                  >
                    <template v-slot:activator="{ on, attrs }">
                      <v-text-field
                        v-model="dateOfBirth"
                        label="Birthday date"
                        prepend-icon="mdi-calendar"
                        readonly
                        v-bind="attrs"
                        v-on="on"
                      ></v-text-field>
                    </template>
                    <v-date-picker
                      ref="picker"
                      v-model="dateOfBirth"
                      :max="new Date().toISOString().substr(0, 10)"
                      min="1950-01-01"
                      @change="datePickerMenu = false"
                    ></v-date-picker>
                  </v-menu>
                </v-col>

                <v-col cols="6">
                   <v-switch
                      v-model="isAdmin"
                      label="Admin"
                    ></v-switch>
                </v-col>
              </v-row>
            </v-container>
          </v-card-text>
          <v-card-actions>
            <v-spacer></v-spacer>
            <v-btn
              text
              @click="toggleOpen(false)"
            >
              Close
            </v-btn>
            <v-btn
              color="primary"
              type="submit"
              text
              :disabled="!isValid"
              :loading="isSubmitting"
              @click="save"
            >
              Save
            </v-btn>
          </v-card-actions>
        </v-form>
      </v-card>
    </v-dialog>
  </v-row>
</template>

<script>
  import api from '@/services/api/user';

  export default {
    props: {
      user: Object,
      open: Boolean,
    },

    data: () => ({
      isValid: true,
      isSubmitting: false,
      firstName: '',
      firstNameRules: [
        v => !!v || 'First Name is required'
      ],
      lastName: '',
      lastNameRules: [
        v => !!v || 'Last Name is required'
      ],
      password: '',
      passwordRules: [
        v => !!v || 'Password is required'
      ],
      email: '',
      emailRules: [
        v => !!v || 'Email is required'
      ],
      emailErrors: [],
      dateOfBirth: '',
      isAdmin: false,
      datePickerMenu: false,
    }),

    methods: {
      async save() {
        try {
          this.isSubmitting = true;
          const payload = {
            ...this.user,
            firstName: this.firstName,
            lastName: this.lastName,
            emailAddress: this.email,
            dateOfBirth: this.dateOfBirth,
            password: this.password,
            isAdmin: this.isAdmin
          };
          await api.updateUser(payload);

          this.$emit('updated', payload);
          this.toggleOpen(false);
        } catch (e) {
          if (e && e.response && 
              e.response.status === 400 && 
              e.response.data && 
              e.response.data.errors && 
              e.response.data.errors.Key) {
            this.emailErrors = e.response.data.errors.EmailAddress;
          }
        } finally {
          this.isSubmitting = false;
        }
      },

      toggleOpen(value) {
        this.$emit('update:open', value);
      }
    },
    watch: {
      user: function (u) {
        this.firstName = u ? u.firstName : '';
        this.lastName = u ? u.lastName : '';
        this.email = u ? u.emailAddress : '';
        this.dateOfBirth = u ? u.dateOfBirth.substr(0, 10) : '';
        this.password = u ? u.password : '';
        this.isAdmin = u ? u.isAdmin : false;
      }
    }
  }
</script>
