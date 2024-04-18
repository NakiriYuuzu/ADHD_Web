<template>
    <div class="bg-red-900 border border-red-700 text-white px-4 py-3 rounded relative animate-pulse dark:bg-red-800 dark:border-red-600 flex flex-col items-center justify-center" role="alert">
        <!-- Icon with Animation -->
        <svg class="animate-bounce w-6 h-6 text-red-300 mb-2" fill="none" viewBox="0 0 24 24" stroke="currentColor">
            <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M13 16h-1v-4h1m0-4h-1v1h1V8m5 8.59L12 21l-6-4.41V5a2 2 0 012-2h8a2 2 0 012 2v11.59z"/>
        </svg>
        <!-- Text Content -->
        <div class="text-center">
            <strong class="font-bold">Oops! </strong>
            <span class="block sm:inline"> Something went wrong.</span>
        </div>
        <!-- Refresh Button -->
        <button @click="refreshParent" class="mt-2 bg-red-500 hover:bg-red-700 text-white font-bold py-2 px-4 rounded">
            Refresh
        </button>
    </div>
</template>

<script setup lang="ts">
import { defineEmits } from 'vue'

const emit = defineEmits(['refreshParent'])

const refreshParent = () => {
    emit('refreshParent')
}
</script>

<style scoped>
@import '@/assets/main.css';
@keyframes spin {
    0%, 100% { transform: rotate(0deg); }
    50% { transform: rotate(360deg); }
}
@keyframes appear {
    from { opacity: 0; transform: translateY(20px); }
    to { opacity: 1; transform: translateY(0); }
}
</style>