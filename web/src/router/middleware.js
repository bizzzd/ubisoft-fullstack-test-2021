import store from '@/store';

export function checkAccessMiddleware(to, from, next) {
  const requiresAuth = to.matched.some((record) => record.meta.requiresAuth);

  if (requiresAuth && !store.getters.isAuthenticated) {
    return next('/login');
  }

  if (store.getters.isAuthenticated) {
    const isAdmin = store.state.auth.user.isAdmin;
    if (to.matched.some((r) => (r.meta.adminOnly && !isAdmin) || (r.meta.userOnly && isAdmin))) {
      return next('/home');
    }  
  }

  return next();
}