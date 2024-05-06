<template>
    <toast-component></toast-component>
    <router-view></router-view>
</template>
<script>
import ToastComponent from "@/presentations/components/toasts/ToastComponent.vue";

export default {
    name: 'blank-layout',
    components: {ToastComponent}
}
</script>
