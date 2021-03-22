import HomeComponent from '@/pages/Home.vue';
import LoginComponent from '@/pages/Login.vue';
import ProfileComponent from '@/pages/Profile.vue';
import MyGamesComponent from '@/pages/MyGames.vue';
import UsersComponent from '@/pages/Users.vue';
import PersonalInfoComponent from '@/components/PersonalInfo.vue';
import PasswordComponent from '@/components/Password.vue';
import store from '@/store';

export const routes = [
  {
    path: '/',
    name: 'home',
    meta: {
      requiresAuth: true
    },
    redirect: () => {
      return store.state.auth.user.isAdmin 
        ? '/users'
        : '/my-games'; 
    },
    component: HomeComponent,
    children: [
      {
        path: 'profile',
        name: 'home.profile',
        redirect: '/profile/personal-info',
        component: ProfileComponent,
        children: [
          {
            path: 'personal-info',
            name: 'home.profile.personal',
            component: PersonalInfoComponent
          },
          {
            path: 'password',
            name: 'home.profile.password',
            component: PasswordComponent
          }
        ]
      },
      {
        path: 'my-games',
        name: 'home.mygames',
        component: MyGamesComponent,    
        meta: {
          userOnly: true
        },    
      },
      {
        path: 'users',
        name: 'home.users',
        component: UsersComponent,    
        meta: {
          adminOnly: true
        },    
      }      
    ]
  },
  {
    path: '/login',
    name: 'login',
    component: LoginComponent
  }
];