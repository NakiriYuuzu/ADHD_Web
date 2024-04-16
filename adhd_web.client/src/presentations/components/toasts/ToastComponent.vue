<template>
    <div class="position-relative">
        <div class="toast-container position-absolute top-0 end-0 px-3">
            <div class="toast fade show" role="alert" aria-live="assertive" aria-atomic="true"
                 v-for="toast in toasts" :key="toast.id">
                <div class="toast-header">
                    <svg class="bd-placeholder-img rounded me-2" width="20" height="20"
                         xmlns="http://www.w3.org/2000/svg" aria-hidden="true" preserveAspectRatio="xMidYMid slice"
                         focusable="false">
                        <rect width="100%" height="100%" :fill="toast.color"></rect>
                    </svg>
                    <strong class="me-auto">{{ toast.title }}</strong>
                    <small class="text-muted">{{ convertDate(toast.date) }}</small>
                    <button type="button" class="btn-close" aria-label="Close" @click="closeToast(toast.id)"></button>
                </div>
                <div class="toast-body">
                    {{ toast.message }}
                </div>
            </div>
        </div>
    </div>
</template>

<script setup lang="ts">
import { ref, watchEffect } from 'vue'
import { useStore } from 'vuex'
import { type Toast } from '@/models/Toast'

const store = useStore()
const toasts = ref<Array<Toast>>([])

watchEffect(() => {
    toasts.value = store.getters.toasts
})

const convertDate = (date: number) => {
    return new Date(date).toLocaleString()
}

const closeToast = (id: number) => {
    store.dispatch('removeToastAction', id)
}
</script>
