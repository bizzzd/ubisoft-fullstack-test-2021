import axios from 'axios';

export default {
  getMyGames() {
    return axios.get('/games');
  },

  redeemKey(data) {
    return axios.post('/games/redeemkey', data);
  }
};