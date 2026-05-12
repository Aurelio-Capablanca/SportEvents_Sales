import { createRouter, createWebHistory } from "vue-router";
import Login from '../components/Login.vue'
import PublicDashboard from "@/views/Public/PublicDashboard.vue";
import PrivateDashboard from "@/views/Private/PrivateDashboard.vue";


const routes = [
    { path: '/', redirect: '/public-dashboard' }, // Redirect root to dashboard
    { path: '/public-dashboard', component: PublicDashboard , meta: { requiresAuth: true } },
    { path: '/login-client', component: Login },
    { path: '/admin', component: PrivateDashboard, meta: { requiresAuth: true } }
]


const router = createRouter({
    history: createWebHistory(),
    routes
});


router.beforeEach((to) => {
    const isAuthenticated = !!localStorage.getItem('token')
    console.log("is authenticated ? ",isAuthenticated);
    console.log("meta ? ",to.meta);
    if (to.meta.requiresAuth &&  !isAuthenticated) {        
        return '/login-client'
    }
    return true
})

export default router