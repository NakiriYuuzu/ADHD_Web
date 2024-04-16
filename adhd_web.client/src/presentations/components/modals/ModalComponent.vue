<template>
    <transition
        enter-active-class="duration-300 ease-out"
        enter-from-class="opacity-0 scale-95"
        enter-to-class="opacity-100 scale-100"
        leave-active-class="duration-200 ease-in"
        leave-from-class="opacity-100 scale-100"
        leave-to-class="opacity-0 scale-95">
        <div v-if="isVisible" class="fixed inset-0 flex items-center justify-center bg-black bg-opacity-50 transition-opacity">
            <!-- Modal content -->
            <div class="bg-gray-800 p-3 rounded-lg shadow-lg z-10 min-w-[300px] min-h-[200px] flex flex-col justify-center items-center" style="color: #ecf0f1;">
                <!-- Header -->
                <div class="flex justify-between items-center mb-3 w-full">
                    <h3 class="text-lg font-bold">{{ title }}</h3>
                    <!-- Close button -->
                    <button @click="closeModal" class="text-xl leading-none scale-150">&times;</button>
                </div>
                <!-- Body -->
                <div class="mb-3 w-full">
                    <slot name="body">This is a beautifully styled modal.</slot>
                </div>
                <!-- Horizontal line -->
                <hr class="border-gray-600 mb-3 w-full">
                <!-- Footer -->
                <div class="flex justify-end w-full">
                    <slot name="footer">
                        <button
                            @click="closeModal"
                            class="bg-blue-500 text-white active:bg-blue-600 font-bold uppercase text-sm px-4 py-2.5 rounded shadow hover:shadow-lg outline-none focus:outline-none mr-1"
                            type="button"
                            style="transition: all 0.15s ease 0s;">
                            Close
                        </button>
                    </slot>
                </div>
            </div>
        </div>
    </transition>
</template>



<script lang="ts">
import {defineComponent, toRefs} from 'vue'

export default defineComponent({
    props: {
        modelValue: {
            type: Boolean,
            default: false
        },
        title: {
            type: String,
            default: 'Modal Title'
        }
    },
    setup(props, {emit}) {
        // To allow for reactivity and toRefs destructuring
        const {modelValue} = toRefs(props)

        // Emit update to v-model binding
        const closeModal = () => {
            emit('update:modelValue', false)
        }

        return {
            isVisible: modelValue,
            closeModal
        }
    }
})
</script>

<style scoped>
@import "@/assets/main.css";
</style>
