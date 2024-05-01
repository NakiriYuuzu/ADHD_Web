<template>
    <div class="section-block">
    <pre><code class="language-markup">
      <b-button v-b-tooltip.hover title="Tooltip!">Tooltip on top</b-button>
      <b-button v-b-tooltip.hover.right title="Tooltip on end">Tooltip on end</b-button>
      <b-button v-b-tooltip.hover.bottom title="Tooltip on bottom">Tooltip on bottom</b-button>
      <b-button v-b-tooltip.hover.left title="Tooltip on start">Tooltip on start</b-button>
      <b-button v-b-tooltip.hover.true title="'<em>Tooltip</em> <u>with</u> <b>HTML</b>'">Tooltip with HTML</b-button>
    </code></pre>
    </div>
</template>