import {createRouter, createWebHistory} from 'vue-router'

const authChildRoutes = (prefix: string) => [
    {
        path: 'login',
        name: prefix + '.login',
        meta: { auth: false, name: 'Login' },
        component: () => import('@/views/auth/default/SignIn.vue')
    },
    {
        path: 'register',
        name: prefix + '.register',
        meta: { auth: false, name: 'Register' },
        component: () => import('@/views/auth/default/SignUp.vue')
    },
    {
        path: 'reset-password',
        name: prefix + '.reset-password',
        meta: { auth: false, name: 'Reset Password' },
        component: () => import('@/views/auth/default/ResetPassword.vue')
    },
    {
        path: 'varify-email',
        name: prefix + '.varify-email',
        meta: { auth: false, name: 'Varify Email' },
        component: () => import('@/views/auth/default/VarifyEmail.vue')
    },
    {
        path: 'lock-screen',
        name: prefix + '.lock-screen',
        meta: { auth: false, name: 'Lock Screen' },
        component: () => import('@/views/auth/default/LockScreen.vue')
    }
]

const defaultChildRoutes = (prefix: string) => [
    {
        path: '',
        name: prefix + '.dashboard',
        meta: { auth: true, name: 'Home', isBanner: true },
        component: () => import('@/views/dashboards/IndexPage.vue')
        // component: () => import('@/presentations/dashboards/Dashboard.vue')
    },
    // icons
    {
        path: '/icons',
        name: prefix + '.icons',
        meta: { auth: true, name: 'Icons', isBanner: true },
        component: () => import('@/views/icons/SolidIcon.vue')
    },
    // Users Pages
    {
        path: '/user-list',
        name: prefix + '.user-list',
        meta: { auth: true, name: 'User List', isBanner: true },
        component: () => import('@/views/user/ListPage.vue')
    },
    {
        path: '/user-add',
        name: prefix + '.user-add',
        meta: { auth: true, name: 'User Add', isBanner: true },
        component: () => import('@/views/user/AddPage.vue')
    },
    {
        path: '/user-profile',
        name: prefix + '.user-profile',
        meta: { auth: true, name: 'User Add', isBanner: true },
        component: () => import('@/views/user/ProfilePage.vue')
    },
    {
        path: '/privacy-setting',
        name: prefix + '.user-privacy-setting',
        meta: { auth: true, name: 'Privacy Setting', isBanner: true },
        component: () => import('@/views/user/PrivacySetting.vue')
    },
    // Extra Pages
    {
        path: '/privacy-policy',
        name: prefix + '.privacy-policy',
        meta: { auth: true, name: 'Privacy Policy', isBanner: true },
        component: () => import('@/views/extra/PrivacyPolicy.vue')
    },
    {
        path: '/terms-and-conditions',
        name: prefix + '.terms-and-conditions',
        meta: { auth: true, name: 'Terms and Conditions', isBanner: true },
        component: () => import('@/views/extra/TermsAndConditions.vue')
    },
    {
        path: '/admin-permissions',
        name: prefix + '.admin-permissions',
        meta: { auth: true, name: 'Admin Permissions', isBanner: true },
        component: () => import('@/views/admin/AdminPage.vue')
    }
]

const errorRoutes = (prefix: string) => [
    // Error Pages
    {
        path: '404',
        name: prefix + '.404',
        meta: { auth: true, name: 'Error 404', isBanner: true },
        component: () => import('@/views/errors/Error404Page.vue')
    },
    {
        path: '500',
        name: prefix + '.500',
        meta: { auth: true, name: 'Error 500', isBanner: true },
        component: () => import('@/views/errors/Error500Page.vue')
    },
    {
        path: 'maintenance',
        name: prefix + '.maintenance',
        meta: { auth: true, name: 'Maintenance', isBanner: true },
        component: () => import('@/views/errors/MaintenancePage.vue')
    }
]

const routes = [
    {
        path: '/',
        name: 'home',
        component: () => import('../presentations/MainLayout.vue'),
        children: defaultChildRoutes('default')
    },
    // Auth Skins
    {
        path: '/auth',
        name: 'auth',
        component: () => import('../layouts/guest/BlankLayout.vue'),
        children: authChildRoutes('auth')
    },
    // Errors Pages
    {
        path: '/errors',
        name: 'errors',
        component: () => import('../layouts/guest/BlankLayout.vue'),
        children: errorRoutes('errors')
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
