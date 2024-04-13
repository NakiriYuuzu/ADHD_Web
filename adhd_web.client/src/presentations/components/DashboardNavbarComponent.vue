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
<!--                    Language Package-->
<!--                    <li class="nav-item dropdown">-->
<!--                        <a href="#" class="search-toggle nav-link" id="dropdownMenuButton2" data-bs-toggle="dropdown"-->
<!--                           aria-haspopup="true" aria-expanded="false">-->
<!--                            <img src="@/assets/images/Flag/flag001.png" class="img-fluid rounded-circle" alt="user"-->
<!--                                 style="height: 30px; min-width: 30px; width: 30px"/>-->
<!--                            <span class="bg-primary"></span>-->
<!--                        </a>-->
<!--                        <div class="p-0 sub-drop dropdown-menu dropdown-menu-end" aria-labelledby="dropdownMenuButton2">-->
<!--                            <b-card no-body class="m-0 border-0 shadow-none">-->
<!--                                <div class="p-0">-->
<!--                                    <ul class="list-group list-group-flush p-0">-->
<!--                                        <li class="iq-sub-card list-group-item">-->
<!--                                            <a class="p-0" href="#"><img src="@/assets/images/Flag/flag-03.png"-->
<!--                                                                         alt="img-flaf" class="img-fluid me-2"-->
<!--                                                                         style="width: 15px; height: 15px; min-width: 15px"/>Spanish</a>-->
<!--                                        </li>-->
<!--                                        <li class="iq-sub-card list-group-item">-->
<!--                                            <a class="p-0" href="#"><img src="@/assets/images/Flag/flag-04.png"-->
<!--                                                                         alt="img-flaf" class="img-fluid me-2"-->
<!--                                                                         style="width: 15px; height: 15px; min-width: 15px"/>Italian</a>-->
<!--                                        </li>-->
<!--                                        <li class="iq-sub-card list-group-item">-->
<!--                                            <a class="p-0" href="#"><img src="@/assets/images/Flag/flag-02.png"-->
<!--                                                                         alt="img-flaf" class="img-fluid me-2"-->
<!--                                                                         style="width: 15px; height: 15px; min-width: 15px"/>French</a>-->
<!--                                        </li>-->
<!--                                        <li class="iq-sub-card list-group-item">-->
<!--                                            <a class="p-0" href="#"><img src="@/assets/images/Flag/flag-05.png"-->
<!--                                                                         alt="img-flaf" class="img-fluid me-2"-->
<!--                                                                         style="width: 15px; height: 15px; min-width: 15px"/>German</a>-->
<!--                                        </li>-->
<!--                                        <li class="iq-sub-card list-group-item">-->
<!--                                            <a class="p-0" href="#"><img src="@/assets/images/Flag/flag-06.png"-->
<!--                                                                         alt="img-flaf" class="img-fluid me-2"-->
<!--                                                                         style="width: 15px; height: 15px; min-width: 15px"/>Japanese</a>-->
<!--                                        </li>-->
<!--                                    </ul>-->
<!--                                </div>-->
<!--                            </b-card>-->
<!--                        </div>-->
<!--                    </li>-->
<!--                    Notification Package-->
<!--                    <li class="nav-item dropdown">-->
<!--                        <a href="#" class="nav-link" id="notification-drop" data-bs-toggle="dropdown">-->
<!--                            <icon-component type="dual-tone" icon-name="bell"></icon-component>-->
<!--                            <span class="bg-danger dots"></span>-->
<!--                        </a>-->
<!--                        <div class="p-0 sub-drop dropdown-menu dropdown-menu-end" aria-labelledby="notification-drop">-->
<!--                            <b-card no-body class="m-0 shadow-none">-->
<!--                                <div class="py-3 card-header d-flex justify-content-between bg-primary">-->
<!--                                    <b-card-title>-->
<!--                                        <h5 class="mb-0 text-white">All Notifications</h5>-->
<!--                                    </b-card-title>-->
<!--                                </div>-->
<!--                                <b-card-body class="p-0">-->
<!--                                    <a href="#" class="iq-sub-card">-->
<!--                                        <div class="d-flex align-items-center">-->
<!--                                            <img class="avatar-40 rounded-pill bg-soft-primary p-1"-->
<!--                                                 src="@/assets/images/shapes/01.png" alt=""/>-->
<!--                                            <div class="ms-3 w-100">-->
<!--                                                <h6 class="mb-0">Emma Watson Bni</h6>-->
<!--                                                <div class="d-flex justify-content-between align-items-center">-->
<!--                                                    <p class="mb-0">95 MB</p>-->
<!--                                                    <small class="float-end font-size-12">Just Now</small>-->
<!--                                                </div>-->
<!--                                            </div>-->
<!--                                        </div>-->
<!--                                    </a>-->
<!--                                    <a href="#" class="iq-sub-card">-->
<!--                                        <div class="d-flex align-items-center">-->
<!--                                            <div class="">-->
<!--                                                <img class="avatar-40 rounded-pill bg-soft-primary p-1"-->
<!--                                                     src="@/assets/images/shapes/02.png" alt=""/>-->
<!--                                            </div>-->
<!--                                            <div class="ms-3 w-100">-->
<!--                                                <h6 class="mb-0">New customer is join</h6>-->
<!--                                                <div class="d-flex justify-content-between align-items-center">-->
<!--                                                    <p class="mb-0">Cyst Bni</p>-->
<!--                                                    <small class="float-end font-size-12">5 days ago</small>-->
<!--                                                </div>-->
<!--                                            </div>-->
<!--                                        </div>-->
<!--                                    </a>-->
<!--                                    <a href="#" class="iq-sub-card">-->
<!--                                        <div class="d-flex align-items-center">-->
<!--                                            <img class="avatar-40 rounded-pill bg-soft-primary p-1"-->
<!--                                                 src="@/assets/images/shapes/03.png" alt=""/>-->
<!--                                            <div class="ms-3 w-100">-->
<!--                                                <h6 class="mb-0">Two customer is left</h6>-->
<!--                                                <div class="d-flex justify-content-between align-items-center">-->
<!--                                                    <p class="mb-0">Cyst Bni</p>-->
<!--                                                    <small class="float-end font-size-12">2 days ago</small>-->
<!--                                                </div>-->
<!--                                            </div>-->
<!--                                        </div>-->
<!--                                    </a>-->
<!--                                    <a href="#" class="iq-sub-card">-->
<!--                                        <div class="d-flex align-items-center">-->
<!--                                            <img class="avatar-40 rounded-pill bg-soft-primary p-1"-->
<!--                                                 src="@/assets/images/shapes/04.png" alt=""/>-->
<!--                                            <div class="w-100 ms-3">-->
<!--                                                <h6 class="mb-0">New Mail from Fenny</h6>-->
<!--                                                <div class="d-flex justify-content-between align-items-center">-->
<!--                                                    <p class="mb-0">Cyst Bni</p>-->
<!--                                                    <small class="float-end font-size-12">3 days ago</small>-->
<!--                                                </div>-->
<!--                                            </div>-->
<!--                                        </div>-->
<!--                                    </a>-->
<!--                                </b-card-body>-->
<!--                            </b-card>-->
<!--                        </div>-->
<!--                    </li>-->
<!--                    Mail Package-->
<!--                    <li class="nav-item dropdown">-->
<!--                        <a href="#" class="nav-link" id="mail-drop" data-bs-toggle="dropdown" aria-haspopup="true"-->
<!--                           aria-expanded="false">-->
<!--                            <icon-component type="dual-tone" icon-name="message"></icon-component>-->
<!--                            <span class="bg-primary count-mail"></span>-->
<!--                        </a>-->
<!--                        <div class="p-0 sub-drop dropdown-menu dropdown-menu-end" aria-labelledby="mail-drop">-->
<!--                            <b-card no-body class="m-0 shadow-none">-->
<!--                                <div class="py-3 card-header d-flex justify-content-between bg-primary">-->
<!--                                    <b-card-title>-->
<!--                                        <h5 class="mb-0 text-white">All Message</h5>-->
<!--                                    </b-card-title>-->
<!--                                </div>-->
<!--                                <b-card-body class="p-0">-->
<!--                                    <a href="#" class="iq-sub-card">-->
<!--                                        <div class="d-flex align-items-center">-->
<!--                                            <div class="">-->
<!--                                                <img class="avatar-40 rounded-pill bg-soft-primary p-1"-->
<!--                                                     src="@/assets/images/shapes/01.png" alt=""/>-->
<!--                                            </div>-->
<!--                                            <div class="ms-3">-->
<!--                                                <h6 class="mb-0">Bni Emma Watson</h6>-->
<!--                                                <small class="float-start font-size-12">13 Jun</small>-->
<!--                                            </div>-->
<!--                                        </div>-->
<!--                                    </a>-->
<!--                                    <a href="#" class="iq-sub-card">-->
<!--                                        <div class="d-flex align-items-center">-->
<!--                                            <div class="">-->
<!--                                                <img class="avatar-40 rounded-pill bg-soft-primary p-1"-->
<!--                                                     src="@/assets/images/shapes/02.png" alt=""/>-->
<!--                                            </div>-->
<!--                                            <div class="ms-3">-->
<!--                                                <h6 class="mb-0">Lorem Ipsum Watson</h6>-->
<!--                                                <small class="float-start font-size-12">20 Apr</small>-->
<!--                                            </div>-->
<!--                                        </div>-->
<!--                                    </a>-->
<!--                                    <a href="#" class="iq-sub-card">-->
<!--                                        <div class="d-flex align-items-center">-->
<!--                                            <div class="">-->
<!--                                                <img class="avatar-40 rounded-pill bg-soft-primary p-1"-->
<!--                                                     src="@/assets/images/shapes/03.png" alt=""/>-->
<!--                                            </div>-->
<!--                                            <div class="ms-3">-->
<!--                                                <h6 class="mb-0">Why do we use it?</h6>-->
<!--                                                <small class="float-start font-size-12">30 Jun</small>-->
<!--                                            </div>-->
<!--                                        </div>-->
<!--                                    </a>-->
<!--                                    <a href="#" class="iq-sub-card">-->
<!--                                        <div class="d-flex align-items-center">-->
<!--                                            <div class="">-->
<!--                                                <img class="avatar-40 rounded-pill bg-soft-primary p-1"-->
<!--                                                     src="@/assets/images/shapes/04.png" alt=""/>-->
<!--                                            </div>-->
<!--                                            <div class="ms-3">-->
<!--                                                <h6 class="mb-0">Variations Passages</h6>-->
<!--                                                <small class="float-start font-size-12">12 Sep</small>-->
<!--                                            </div>-->
<!--                                        </div>-->
<!--                                    </a>-->
<!--                                    <a href="#" class="iq-sub-card">-->
<!--                                        <div class="d-flex align-items-center">-->
<!--                                            <div class="">-->
<!--                                                <img class="avatar-40 rounded-pill bg-soft-primary p-1"-->
<!--                                                     src="@/assets/images/shapes/05.png" alt=""/>-->
<!--                                            </div>-->
<!--                                            <div class="ms-3">-->
<!--                                                <h6 class="mb-0">Lorem Ipsum generators</h6>-->
<!--                                                <small class="float-start font-size-12">5 Dec</small>-->
<!--                                            </div>-->
<!--                                        </div>-->
<!--                                    </a>-->
<!--                                </b-card-body>-->
<!--                            </b-card>-->
<!--                        </div>-->
<!--                    </li>-->
                    <li class="nav-item dropdown">
                        <a class="nav-link py-0 d-flex align-items-center" href="#" id="navbarDropdown" role="button"
                           data-bs-toggle="dropdown" aria-expanded="false">
                            <img :src="profileImage" alt="User-Profile"
                                 class="theme-color-default-img img-fluid avatar avatar-50 avatar-rounded"/>
                            <div class="caption ms-3 d-none d-md-block">
                                <h6 class="mb-0 caption-title">{{username}}</h6>
                                <p class="mb-0 caption-sub-title">{{role}}</p>
                            </div>
                        </a>
                        <ul class="dropdown-menu dropdown-menu-end" aria-labelledby="navbarDropdown">
                            <li>
                                <router-link class="dropdown-item" :to="{ name: 'default.user-profile' }">Profile
                                </router-link>
                            </li>
                            <li>
                                <router-link class="dropdown-item" :to="{ name: 'default.user-privacy-setting' }">
                                    Privacy Setting
                                </router-link>
                            </li>
                            <li>
                                <hr class="dropdown-divider"/>
                            </li>
                            <li>
                                <router-link class="dropdown-item" :to="{ name: 'auth.login' }">Logout</router-link>
                            </li>
                        </ul>
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
