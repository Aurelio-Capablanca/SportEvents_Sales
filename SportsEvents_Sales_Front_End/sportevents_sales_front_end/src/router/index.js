import { createRouter, createWebHistory } from "vue-router";
import Login from '../components/Login.vue'
import PublicDashboard from "@/views/PublicDashboard.vue";
import PrivateDashboard from "@/views/PrivateDashboard.vue";


const routes = [
    { path: '/', redirect: '/public-dashboard' }, // Redirect root to dashboard
    { path: '/public-dashboard', component: PublicDashboard },
    { path: '/login-client', component: Login },
    { path: '/admin', component: PrivateDashboard, meta: { requiresAuth: true } }
]


const router = createRouter({
    history: createWebHistory(),
    routes
});


router.beforeEach((to) => {
    const isAuthenticated = !!localStorage.getItem('token')    
    if (to.meta.requiresAuth && !isAuthenticated) {
        // Just return the path you want to redirect to
        return '/login-client'
    }    
    return true
})

export default router