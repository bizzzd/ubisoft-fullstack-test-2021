import axios from 'axios';

export default {
  authenticate(payload) {
    return axios.post('/account/authenticate', payload);
  },

  getPersonalInfo() {
    return axios.get('/account/personalinfo');
  },

  validateEmailUnique(email, cancelToken) {
    return axios.get(`/account/validateemail?email=${email}`, { cancelToken });
  },

  updatePersonalInfo(data) {
    return axios.put('/account/personalinfo', data);
  },

  changePassword(data) {
    return axios.put('/account/password', data);
  }
};