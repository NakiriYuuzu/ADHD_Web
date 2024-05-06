import {createRouter, createWebHistory} from 'vue-router'

const defaultChildRoutes = (prefix: string) => [
    {
        path: '',
        name: prefix + '.dashboard',
        meta: {auth: true, name: 'Home', isBanner: true},
        component: () => import('@/presentations/dashboards/Dashboard.vue')
    },
    {
        path: 'players',
        name: prefix + '.players',
        meta: {auth: true, name: 'Players', isBanner: true},
        component: () => import('@/presentations/players/PlayerData.vue')
    }
]

const authChildRoutes = (prefix: string) => [
    {
        path: '',
        name: prefix + '.login',
        meta: {auth: false, name: 'Login'},
        component: () => import('@/presentations/verify/Login.vue')
    },
    // {
    //     path: 'register',
    //     name: prefix + '.register',
    //     meta: {auth: false, name: 'Register'},
    //     component: () => import('@/views/auth/RegisterPage.vue')
    // }
]

const routes = [
    {
        path: '/',
        name: 'default',
        component: () => import('../presentations/MainLayout.vue'),
        children: defaultChildRoutes('default')
    },
    {
        path: '/auth',
        name: 'auth',
        component: () => import('../layouts/guest/BlankLayout.vue'),
        children: authChildRoutes('auth')
    },
    {
        path: '/:pathMatch(.*)*',
        name: 'not-found',
        component: () => import('@/views/errors/Error404Page.vue')
    }
]

const baseURL = import.meta.env.BASE_URL

const router = createRouter({
    linkActiveClass: 'active',
    linkExactActiveClass: 'exact-active',
    history: createWebHistory(baseURL),
    routes: routes
})

export default router