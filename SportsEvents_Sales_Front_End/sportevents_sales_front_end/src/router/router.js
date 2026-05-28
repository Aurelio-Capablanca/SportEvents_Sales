import { createRouter, createWebHistory } from "vue-router";
import Login from '../components/Login.vue'
import PublicDashboard from "@/views/Public/PublicDashboard.vue";
import PrivateDashboard from "@/views/Private/PrivateDashboard.vue";
import EventDetails from "@/views/Public/EventDetail.vue"
import CartView from "@/views/Public/CartView.vue";
import GameMaintenance from "@/views/Private/GameMaintenance.vue";
import StadiumMaintenance from "@/views/Private/StadiumMaintenance.vue";
import PriceZonesMaintenance from "@/views/Private/PriceZonesMaintenance.vue";
import UserManagement from "@/views/Private/UserManagement.vue";

const routes = [
    { path: '/', redirect: '/public-dashboard' }, // Redirect root to dashboard
    { path: '/public-dashboard', component: PublicDashboard, meta: { requiresAuth: true } },
    { path: '/login-client', component: Login },
    { path: '/admin', component: PrivateDashboard, meta: { requiresAuth: true } },
    { path: '/game-admin', component: GameMaintenance, meta: { requiresAuth: true } },
    { path: '/stadium-admin', component: StadiumMaintenance, meta: { requiresAuth: true } },
    { path: '/zone-price-admin', component: PriceZonesMaintenance, meta: { requiresAuth: true } },
    { path: '/user-admin', component: UserManagement, meta: { requiresAuth: true } },
    { path: '/game/:id', component: EventDetails},
    { path: '/cart/:id', component: CartView, meta: { requiresAuth: true } },
]


const router = createRouter({
    history: createWebHistory(),
    routes
});


router.beforeEach((to) => {
    const isAuthenticated = !!localStorage.getItem('token')
    console.log("is authenticated ? ", isAuthenticated);
    console.log("meta ? ", to.meta);
    if (to.meta.requiresAuth && !isAuthenticated) {
        return '/login-client'
    }
    return true
})

export default router