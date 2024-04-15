<template>
    <default-sidebar>
        <ul class="navbar-nav iq-main-menu" id="sidebar-menu">
            <side-menu title="Home" :static-item="true"></side-menu>
            <side-menu isTag="router-link" title="Dashboard" icon="view-grid" :icon-size="10" icon-type="solid"
                       :route="{ to: 'default.dashboard' }"></side-menu>
        </ul>
    </default-sidebar>
</template>

<script setup lang="ts">
import DefaultSidebar from "@/components/custom/sidebar/DefaultSidebar.vue"
import SideMenu from "@/components/custom/nav/SideMenu.vue"

import {ref} from 'vue'
import {useRoute} from 'vue-router'

const currentRoute = ref('')
const route = useRoute()

const toggle = (route) => {
    if (route === currentRoute.value && route.includes('.')) {
        const menu = currentRoute.value.split('.')
        return (currentRoute.value = menu[menu.length - 2])
    }
    if (route !== currentRoute.value && currentRoute.value.includes(route)) {
        return (currentRoute.value = '')
    }
    if (route !== currentRoute.value) {
        return (currentRoute.value = route)
    }
    if (route === currentRoute.value) {
        return (currentRoute.value = '')
    }
    return (currentRoute.value = '')
}
toggle(route.name)
</script>