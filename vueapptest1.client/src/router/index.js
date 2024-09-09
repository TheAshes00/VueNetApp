import { createMemoryHistory, createRouter } from 'vue-router'

import HomeView from '@/views/HomeView.vue';
import RegisterView from '@/views/RegisterView.vue';
import UserActivitiesView from '@/views/UserActivitiesView.vue';
import AdminHomeView from '@/views/AdminHomeView.vue';
import WorkshopView from '@/views/Admin/WorkshopView.vue';
import TutorView from '@/views/Admin/TutorView.vue';
import WorkshopTutorView from '@/views/Admin/WorkshopTutorView.vue';
import StudentReportsView from '@/views/Admin/StudentReportsView.vue';
import TutorReportView from '@/views/Admin/TutorReportView.vue';
import InventoryView from '@/views/Admin/InventoryView.vue';

const routes = [
  { path: '/', component: HomeView },
  { path: '/register', component: RegisterView },
  { path: '/activities', component: UserActivitiesView },
  { path: '/admin', component: AdminHomeView,
    children:[
      //                                                    // Workshops
      { path: 'workshop/Workshop', component: WorkshopView },
      { path: 'workshop/Tutor', component: TutorView },
      { path: 'workshop/WorkshopTutor', component: WorkshopTutorView },
      //                                                    // Reports
      { path: 'report/Student', component: StudentReportsView },
      { path: 'report/Tutor', component: TutorReportView },
      //                                                    // Inventory
      { path: 'inventory/Material', component: InventoryView },
    ]
  },
]

const router = createRouter({
  history: createMemoryHistory(),
  routes,
});

export default router;