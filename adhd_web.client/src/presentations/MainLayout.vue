<template>
    <sidebar-component></sidebar-component>
    <main class="main-content">
        <div :class="`position-relative  ${isBanner ? 'iq-banner ' + bannerStyle : ''}`">
            <header-component></header-component>
            <toast-component></toast-component>
            <template v-if="isBanner">
                    <sub-header></sub-header>
                </template>
            <loading-component :is-loading="isLoading"></loading-component>
        </div>
        <main-content-component :hidden="isLoading">
            <router-view></router-view>
        </main-content-component>
        <footer-component></footer-component>
    </main>
</template>

<script setup lang="ts">
import LoadingComponent from "@/components/custom/loader/LoadingComponent.vue";
import SidebarComponent from "@/presentations/components/SidebarComponent.vue"
import MainContentComponent from "@/components/partials/MainContentComponent.vue"
import HeaderComponent from "@/presentations/components/headers/HeaderComponent.vue"
import SubHeader from "@/presentations/components/headers/Subheader.vue"
import FooterComponent from "@/presentations/components/FooterComponent.vue"

import {computed} from 'vue'
import {useRoute} from 'vue-router'
import {useStore} from 'vuex'
import ToastComponent from "@/presentations/components/toasts/ToastComponent.vue";

const route = useRoute()
const store = useStore()
const isBanner = computed(() => route.meta.isBanner)
const bannerStyle = computed(() => store.getters['setting/header_banner'])
const isLoading = computed(() => store.getters.isLoading)
</script>