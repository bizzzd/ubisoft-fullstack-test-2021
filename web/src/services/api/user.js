import axios from 'axios';

export default {
  getUsers() {
    return axios.get('/users');
  },

  updateUser(data) {
    return axios.put(`/users/${data.userAccountId}`, data);
  }
};