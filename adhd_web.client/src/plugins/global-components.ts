// Register global components
import TabButton from "@/components/bootstrap/tab/TabButton.vue"
import TabPane from "@/components/bootstrap/tab/TabPane.vue"
import BrandLogo from "@/components/custom/logo/BrandLogo.vue"
import BrandName from "@/components/custom/logo/BrandName.vue"
import IconComponent from "@/components/icons/IconComponent.vue"

export default {
  install: (app) => {
    // Tab Components
    app.component('tab-button', TabButton.default)
    app.component('tab-pane', TabPane.default)

    // Brand Components
    app.component('brand-logo', BrandLogo.default)
    app.component('brand-name', BrandName.default)

    // Icon Components
    app.component('icon-component', IconComponent.default)
  }
}