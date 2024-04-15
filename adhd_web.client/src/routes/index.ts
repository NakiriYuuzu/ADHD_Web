import {createRouter, createWebHistory} from 'vue-router'

const defaultChildRoutes = (prefix: string) => [
    {
        path: '',
        name: prefix + '.dashboard',
        meta: { auth: true, name: 'Home', isBanner: true },
        // component: () => import('@/views/dashboards/IndexPage.vue')
        component: () => import('@/presentations/dashboards/Dashboard.vue')
    }
]

const routes = [
    {
        path: '/',
        name: 'home',
        component: () => import('../presentations/MainLayout.vue'),
        children: defaultChildRoutes('default')
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
