<template>
    <default-navbar :is-search="false">
        <a href="#" class="navbar-brand">
            <brand-logo :color="true"/>
            <h4 class="logo-title d-block d-xl-none" data-setting="app_name">
                <brand-name></brand-name>
            </h4>
        </a>
        <div class="sidebar-toggle" data-toggle="sidebar" data-active="true" @click="toggleSidebar">
            <i class="icon d-flex">
                <svg width="20px" viewBox="0 0 24 24">
                    <path fill="currentColor"
                          d="M4,11V13H16L10.5,18.5L11.92,19.92L19.84,12L11.92,4.08L10.5,5.5L16,11H4Z"/>
                </svg>
            </i>
        </div>
    </default-navbar>
</template>
<script>
import {computed, ref, onMounted, onUnmounted} from 'vue'
import {useStore} from 'vuex'
import DefaultNavbar from '@/presentations/components/DashboardNavbarComponent.vue'
import BrandLogo from "@/components/custom/logo/BrandLogo.vue"
import BrandName from "@/components/custom/logo/BrandName.vue"

export default {
    components: {
        BrandName,
        BrandLogo,
        DefaultNavbar
    },
    setup() {
        const store = useStore()
        const themeSchemeDirection = computed(() => store.getters['setting/theme_scheme_direction'])
        const fullScreen = ref(false)
        const isHidden = ref(false)

        const openFullScreen = () => {
            if (fullScreen.value) {
                fullScreen.value = false
                document.exitFullscreen()
            } else {
                fullScreen.value = true
                document.documentElement.requestFullscreen()
            }
        }

        const onscroll = () => {
            const yOffset = document.documentElement.scrollTop
            const navbar = document.querySelector('.navs-sticky')
            if (navbar !== null) {
                if (yOffset >= 100) {
                    navbar.classList.add('menu-sticky')
                } else {
                    navbar.classList.remove('menu-sticky')
                }
            }
        }

        const toggleSidebar = () => {
            // Code Here
            if (sidebarType.value.includes('sidebar-mini')) {
                store.dispatch(
                    'setting/sidebar_type',
                    sidebarType.value.filter((item) => item !== 'sidebar-mini')
                )
            } else {
                store.dispatch('setting/sidebar_type', [...sidebarType.value, 'sidebar-mini'])
            }
        }

        onMounted(() => {
            window.addEventListener('scroll', onscroll())
        })

        onUnmounted(() => {
            window.removeEventListener('scroll', onscroll())
        })

        const updateRadio = (size) => {
            store.dispatch('setting/theme_font_size', size)
        }
        return {
            openFullScreen,
            updateRadio,
            toggleSidebar,
            fullScreen,
            isHidden,
            themeSchemeDirection
        }
    },
    props: {
        fullsidebar: {type: Boolean, default: false}
    }
}
</script>
<style>
.iq-product-menu-responsive .offcanvas-header {
    display: none;
}
</style>
