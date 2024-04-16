<template>
    <div :class="`${isLoading ? '' : 'hidden'} flex flex-col items-center mt-16 justify-top min-h-screen bg-gray-900`">
        <div class="animate-spin rounded-full h-32 w-32 border-b-4 border-gray-600"></div>
        <h2 class="text-lg font-semibold text-gray-300 mt-8">Loading...</h2>
        <p class="text-gray-500">Hang tight, we're getting things ready for you.</p>
    </div>

</template>

<script lang="ts">
import {defineComponent} from 'vue'

export default defineComponent({
    props: {
        isLoading: {
            type: Boolean,
            default: false
        }
    },
    methods: {
    }
})
</script>

<style scoped>
@import "@/assets/main.css";
@keyframes spin {
    to {
        transform: rotate(360deg);
    }
}

.animate-spin {
    animation: spin 1s linear infinite;
}
</style>