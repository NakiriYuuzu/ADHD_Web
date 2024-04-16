<template>
    <button @click="addToast" class="btn btn-primary">Show Toast</button>
</template>

<script setup lang="ts">
import { useStore } from 'vuex'
import { type Toast, ToastColor } from "@/models/Toast"

const store = useStore()
let x = 0
const addToast = () => {
    store.dispatch('addToastAction', {
        title: 'Toast Title ' + x,
        message: 'Toast Message ' + x++,
        color: ToastColor.Success
    } as Toast)
}
</script>