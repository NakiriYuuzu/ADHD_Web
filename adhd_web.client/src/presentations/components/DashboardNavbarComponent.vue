<template>
    <nav :class="`nav navbar navbar-expand-xl navbar-light iq-navbar default`">
        <!-- <nav :class="`nav navbar navbar-expand-xl navbar-light iq-navbar ${headerNavbar} ${navbarHide.join('')}`"> -->
        <div class="container-fluid navbar-inner">
            <slot></slot>
            <div class="input-group search-input" v-if="isSearch">
                <span class="input-group-text" id="search-input">
                    <icon-component type="outlined" :size="18" icon-name="search"></icon-component>
                </span>
                <input type="search" class="form-control" placeholder="Search..."/>
            </div>
            <button class="navbar-toggler" type="button" data-bs-toggle="collapse"
                    data-bs-target="#navbarSupportedContent" aria-controls="navbarSupportedContent"
                    aria-expanded="false" aria-label="Toggle navigation">
                <span class="navbar-toggler-icon">
                    <span class="mt-2 navbar-toggler-bar bar1"></span>
                    <span class="navbar-toggler-bar bar2"></span>
                    <span class="navbar-toggler-bar bar3"></span>
                </span>
            </button>
            <div class="collapse navbar-collapse" id="navbarSupportedContent">
                <ul class="mb-2 navbar-nav ms-auto align-items-center navbar-list mb-lg-0">
                    <li class="nav-item dropdown">
                        <a class="nav-link py-0 d-flex align-items-center" href="#" id="navbarDropdown" role="button"
                           data-bs-toggle="/*dropdown*/" aria-expanded="false" @click.prevent="$event.preventDefault()">
                            <img :src="profileImage" alt="User-Profile"
                                 class="theme-color-default-img img-fluid avatar avatar-50 avatar-rounded"/>
                            <div class="caption ms-3 d-none d-md-block">
                                <h6 class="mb-0 caption-title">{{username}}</h6>
                                <p class="mb-0 caption-sub-title">{{role}}</p>
                            </div>
                        </a>
                    </li>
                </ul>
            </div>
        </div>
    </nav>
</template>
<script>
import {ref, onMounted, onUnmounted} from 'vue'
import IconComponent from "@/components/icons/IconComponent.vue"
import profileImage from '@/assets/images/avatars/Yuzu.png'

export default {
    components: {IconComponent},
    props: {
        isSearch: {
            type: Boolean,
            default: false
        },
        profileImage: {
            type: String,
            default: profileImage
        },
        username: {
            type: String,
            default: 'Yuuzu'
        },
        role: {
            type: String,
            default: 'Administrator'
        }
    },
    setup(props, {emit}) {
        const isHidden = ref(false)

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

        onMounted(() => {
            window.addEventListener('scroll', onscroll())
        })

        onUnmounted(() => {
            window.removeEventListener('scroll', onscroll())
        })
        return {
            isHidden,
            emit
        }
    }
}
</script>
