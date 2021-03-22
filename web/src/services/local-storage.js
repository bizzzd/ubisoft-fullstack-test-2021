const USER_TOKEN_KEY = 'user';
const ACCESS_TOKEN_KEY = 'accessToken';

export default {
  getUser() {
    const userData = localStorage.getItem(USER_TOKEN_KEY);
    return userData ? JSON.parse(userData) : null;
  },

  storeUser(user) {
    localStorage.setItem(USER_TOKEN_KEY, JSON.stringify(user));
  },

  storeToken(token) {
    localStorage.setItem(ACCESS_TOKEN_KEY, token);
  },

  getToken() {
    return localStorage.getItem(ACCESS_TOKEN_KEY);
  },

  reset() {
    localStorage.removeItem(ACCESS_TOKEN_KEY);
    localStorage.removeItem(USER_TOKEN_KEY);
  }
};
