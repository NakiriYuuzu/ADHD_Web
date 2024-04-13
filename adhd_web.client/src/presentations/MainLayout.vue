<template>
    <loader-component :show="isLoader"></loader-component>
    <sidebar-component></sidebar-component>
    <main class="main-content">
        <div :class="`position-relative  ${isBanner ? 'iq-banner ' + bannerStyle : ''}`">
            <header-component></header-component>
            <template v-if="isBanner">
                <sub-header></sub-header>
            </template>
        </div>
        <main-content-component>
            <router-view></router-view>
        </main-content-component>
        <footer-component></footer-component>
    </main>
</template>

<script setup lang="ts">
import LoaderComponent from "@/components/custom/loader/LoaderComponent.vue"
import SidebarComponent from "@/presentations/components/SidebarComponent.vue"
import MainContentComponent from "@/components/partials/MainContentComponent.vue"
import HeaderComponent from "@/presentations/components/headers/HeaderComponent.vue"
import SubHeader from "@/presentations/components/headers/Subheader.vue"
import FooterComponent from "@/presentations/components/FooterComponent.vue"

import {ref, computed, onMounted} from 'vue'
import {useRoute} from 'vue-router'
import {useStore} from 'vuex'

const route = useRoute()
const store = useStore()
const isBanner = computed(() => route.meta.isBanner)
const bannerStyle = computed(() => store.getters['setting/header_banner'])
const isLoader = ref(true)

onMounted(() => {
    setTimeout(() => {
        isLoader.value = false
    }, 300)
})
</script>