<template>
    <section class="login-content">
        <b-row class="m-0 align-items-center bg-white h-100">
            <b-col md="6">
                <b-row class="justify-content-center">
                    <b-col md="10">
                        <b-card class="card-transparent shadow-none d-flex justify-content-center mb-0 auth-card iq-auth-form">
                            <div class="navbar-brand d-flex align-items-center mb-3 text-primary">
                                <brand-logo></brand-logo>
                                <h4 class="logo-title ms-3 mb-0" data-setting="app_name"><brand-name></brand-name></h4>
                            </div>
                            <h2 class="mb-2 text-center">Sign In</h2>
                            <p class="text-center">Login to stay connected.</p>
                            <form novalidate v-on:submit.prevent="loginOnClicked">
                                <div class="row">
                                    <div class="col-lg-12">
                                        <div class="form-group">
                                            <label for="account" class="form-label">Account</label>
                                            <input type="text" class="form-control" id="account" v-model="account" aria-describedby="account" placeholder=" " />
                                        </div>
                                    </div>
                                    <div class="col-lg-12">
                                        <div class="form-group">
                                            <label for="password" class="form-label">Password</label>
                                            <input type="password" class="form-control" id="password" v-model="password" aria-describedby="password" placeholder=" " />
                                        </div>
                                    </div>
                                </div>
                                <div class="d-flex justify-content-center">
                                    <button type="submit" class="btn btn-primary">Sign In</button>
                                </div>
                            </form>
                        </b-card>
                    </b-col>
                </b-row>
                <div class="sign-bg">
                    <svg width="280" height="230" viewBox="0 0 431 398" fill="none" xmlns="http://www.w3.org/2000/svg">
                        <g opacity="0.05">
                            <rect x="-157.085" y="193.773" width="543" height="77.5714" rx="38.7857" transform="rotate(-45 -157.085 193.773)" fill="#3B8AFF" />
                            <rect x="7.46875" y="358.327" width="543" height="77.5714" rx="38.7857" transform="rotate(-45 7.46875 358.327)" fill="#3B8AFF" />
                            <rect x="61.9355" y="138.545" width="310.286" height="77.5714" rx="38.7857" transform="rotate(45 61.9355 138.545)" fill="#3B8AFF" />
                            <rect x="62.3154" y="-190.173" width="543" height="77.5714" rx="38.7857" transform="rotate(45 62.3154 -190.173)" fill="#3B8AFF" />
                        </g>
                    </svg>
                </div>
            </b-col>
            <div class="col-md-6 d-md-block d-none bg-primary p-0 vh-100 overflow-hidden">
                <img src="@/assets/images/auth/01.png" class="img-fluid gradient-main animated-scaleX" alt="images" loading="lazy" />
            </div>
        </b-row>
    </section>
</template>

<script setup lang="ts">
import {ref, onMounted} from 'vue'
import BrandLogo from "@/components/custom/logo/BrandLogo.vue"
import BrandName from "@/components/custom/logo/BrandName.vue"
import {makeToast, ToastColor} from "@/modules/Toast"
import router from "@/routes"
import {setToken} from "@/modules/Token";

const account = ref('')
const password = ref('')

const loginOnClicked = async () => {
    if (account.value === '' && password.value === '') {
        makeToast(`Failure`, `Please fill all the blank!`, ToastColor.Warning)
        return
    }
    
    fetch('api/Login', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({username: account.value, password: password.value})
    })
        .then(response => response.json().then(data => ({ok: response.ok, data})))
        .then(response => {
            if (response.ok) {
                makeToast(`Success`, `Login Success!`, ToastColor.Success)
                setToken(response.data.token)
                router.push({name: 'default.dashboard'})
            } else {
                makeToast(`Failure`, `${response.data.message}`, ToastColor.Error)
            }
        })
        .catch(error => {
            console.log(error)
            makeToast(`Error`, `${error.message}`, ToastColor.Error)
        })
}

onMounted(() => {
    
})
</script>

<style lang="scss" scoped></style>
