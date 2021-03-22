<template>
   <v-container fluid fill-height>
      <v-layout align-center justify-center>
         <v-flex xs12 sm8 md4>
            <v-card class="elevation-12">
               <v-form v-model="isValid" @submit.prevent>
                  <v-toolbar dark>
                     <v-toolbar-title>Acme Games</v-toolbar-title>
                     <v-spacer></v-spacer>
                     <v-img :src="require('../assets/logo.svg')" max-width="100"></v-img>
                  </v-toolbar>

                  <v-card-text>
                     <v-text-field
                        prepend-icon="mdi-account"
                        v-model="username"
                        label="Email"
                        type="text"
                        :disabled="isLoading"
                        :rules="usernameRules"
                     ></v-text-field>

                     <v-text-field
                        prepend-icon="mdi-lock"
                        v-model="password"                        
                        label="Password"
                        type="password"
                        :rules="passwordRules"
                        :disabled="isLoading"
                     ></v-text-field>
                  </v-card-text>

                  <v-card-actions>
                     <v-slide-y-reverse-transition>
                        <v-flex v-if="error" class="red--text pl-2">
                           {{error}}
                        </v-flex>
                     </v-slide-y-reverse-transition>
                     <v-spacer></v-spacer>
                     <v-btn 
                        type="submit"
                        color="primary" 
                        large 
                        :loading="isLoading"
                        :disabled="!isValid"
                        @click="signIn"
                     >
                        Sign In
                     </v-btn>
                  </v-card-actions>
               </v-form>
            </v-card>
         </v-flex>
      </v-layout>
   </v-container>
</template>

<script>
import router from '@/router';

export default {
   name: 'Login',
   data: () => ({
      isLoading: false,
      isValid: true,
      username: '',
      usernameRules: [
         v => !!v || 'Email is required'
      ],
      password: '',
      passwordRules: [
         v => !!v || 'Password is required'
      ],
      error: ''
   }), 
   created() {
      if (this.$store.getters.isAuthenticated) {
         router.push({ name: 'home' })
      }
   },   
   methods: {
      async signIn() {
         try {
            this.error = '';
            this.isLoading = true;
            await this.$store.dispatch('login', {
               username: this.username,
               password: this.password 
            });
         } catch(e) {
            this.error = e;
            setTimeout(() => this.error = '', 3000);
         } finally {
            this.isLoading = false;
         }
      }
   }
};
</script>
