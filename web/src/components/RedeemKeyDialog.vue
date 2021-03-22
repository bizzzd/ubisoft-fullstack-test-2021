<template>
  <v-row justify="center">
    <v-dialog
      v-model="dialog"
      persistent
      max-width="400px"
    >
      <template v-slot:activator="{ on, attrs }">        
        <v-btn
          color="primary"
          dark
          v-bind="attrs"
          v-on="on"
        >
          Redeem a key
        </v-btn>
      </template>
      <v-card v-if="dialog">
        <v-form v-model="isValid" @submit.prevent>
          <v-card-title>
            <span class="headline">Redeem a key</span>
          </v-card-title>
          <v-card-text>
            <v-text-field
              label="Key"
              v-model="key"
              required
              autofocus
              :rules="keyRules"
              :error-messages="keyErrors"
              @input="keyErrors = []"
            ></v-text-field>
          </v-card-text>
          <v-card-actions>
            <v-spacer></v-spacer>
            <v-btn
              text
              @click="dialog = false"
            >
              Close
            </v-btn>
            <v-btn
              color="primary"
              type="submit"
              text
              :disabled="!isValid"
              :loading="isSubmitting"
              @click="redeemKey"
            >
              Redeem
            </v-btn>
          </v-card-actions>
        </v-form>
      </v-card>
    </v-dialog>
  </v-row>
</template>

<script>
  import api from '@/services/api/game';

  export default {
    data: () => ({
      dialog: false,
      isValid: true,
      isSubmitting: false,
      key: '',
      keyRules: [
        v => !!v || 'Key is required'
      ],
      keyErrors: [],
    }),
    methods: {
      async redeemKey() {
        try {
          this.isSubmitting = true;
          const response = await api.redeemKey({ key: this.key });
          this.$emit('newGameAdded', response.data);
          this.key = '';
          this.dialog = false;
        } catch (e) {
          if (e && e.response && 
              e.response.status === 400 && 
              e.response.data && 
              e.response.data.errors && 
              e.response.data.errors.Key) {
            this.keyErrors = e.response.data.errors.Key;
          }
        } finally {
          this.isSubmitting = false;
        }
      }
    }
  }
</script>
