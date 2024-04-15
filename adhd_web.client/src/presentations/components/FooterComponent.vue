<template>
    <!-- Footer Section Start -->
    <footer :class="`footer ${footerStyle}`">
        <div class="footer-body">
            <ul class="left-panel list-inline mb-0 p-0"></ul>
            <div class="right-panel">©{{thisYear}} <span data-setting="app_name"><brand-name></brand-name></span>, Made with by <a href="https://nakiriyuuzu.github.io/NYuuzu/HomePage.html" target="_blank">Yuuzu</a>.</div>
        </div>
    </footer>
    <!-- Footer Section End -->
    <b-offcanvas v-model="shareOffcanvas" @hide="hideShareOffcanvas" placement="bottom" title="Share">
        <share-offcanvas></share-offcanvas>
    </b-offcanvas>
</template>

<script>
import {computed, watch, ref} from 'vue'
import {useStore} from 'vuex'
import ShareOffcanvas from '@/components/widgets/ShareOffcanvasNew.vue'
import BrandName from "@/components/custom/logo/BrandName.vue"

export default {
    components: {BrandName, ShareOffcanvas},
    setup() {
        const store = useStore()
        const thisYear = new Date().getFullYear()
        const footerStyle = computed(() => store.getters['setting/footer_style'])

        // Bottom Canvas
        const shareOffcanvas = ref(false)
        watch(
            () => store.getters.shareOffcanvas,
            (newValue) => {
                shareOffcanvas.value = newValue
            }
        )
        const hideShareOffcanvas = () => {
            store.dispatch('openBottomCanvasAction', {name: 'shareOffcanvas', value: false})
        }
        return {
            footerStyle,
            shareOffcanvas,
            hideShareOffcanvas,
            thisYear
        }
    }
}
</script>
