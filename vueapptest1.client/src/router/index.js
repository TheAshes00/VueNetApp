import { createMemoryHistory, createRouter } from 'vue-router'

import HomeView from '@/views/HomeView.vue';
import RegisterView from '@/views/RegisterView.vue';
import UserActivitiesView from '@/views/UserActivitiesView.vue';

const routes = [
  { path: '/', component: HomeView },
  { path: '/register', component: RegisterView },
  { path: '/activities', component: UserActivitiesView },
]

const router = createRouter({
  history: createMemoryHistory(),
  routes,
});

export default router;