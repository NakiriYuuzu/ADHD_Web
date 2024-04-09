import {createRouter, createWebHistory} from 'vue-router'

const routes = [
    {
        path: '/',
        name: 'Dashboard',
        component: () => import('../components/Dashboard/Dashboard.vue')
    },
    // {
    //     path: '/about',
    //     name: 'About',
    //     component: () => import('../views/About.vue')
    // }
]

const baseURL = import.meta.env.BASE_URL

const router = createRouter({
    linkActiveClass: 'active',
    linkExactActiveClass: 'exact-active',
    history: createWebHistory(baseURL),
    base: baseURL,
    routes
})

export default router