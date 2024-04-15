<template>
    <table :class="'table ' + className" ref="tableRef"></table>
</template>

<script setup>
import { onMounted, ref, defineProps, watch } from 'vue'
import 'datatables.net-bs5/css/dataTables.bootstrap5.min.css'
import 'datatables.net-bs5/js/dataTables.bootstrap5.min.js'
import 'datatables.net-bs5'
import $ from 'jquery'
import mitt from 'mitt'
const emitter = mitt()

const props = defineProps({
    data: Array,
    columns: Array,
    options: Object,
    className: String,
    isFooter: Boolean,
    isColumnFilter: Boolean,
    isToggleFilter: Boolean,
    isLanguageFilter: Boolean
})

const tableRef = ref(null)

// 初始化 DataTable
const initDataTable = () => {
    const table = $(tableRef.value).DataTable({
        autoWidth: false,
        data: props.data,
        columns: props.columns,
        dom: '<"row align-items-center"<"col-md-6" l><"col-md-6" f>><"table-responsive my-3" rt><"row align-items-center"<"col-md-6" i><"col-md-6" p>>',
        destroy: true,
        initComplete: function () {
            if (props.isFooter) {
                const footerRow = document.createElement('tr')
                props.columns.forEach((column) => {
                    const footerCell = document.createElement('th')
                    footerCell.textContent = column.title  // 使用 textContent 而非直接 append 字符串
                    footerRow.appendChild(footerCell)
                })
                $(tableRef.value).append($('<tfoot>').append(footerRow))
            }
        }
    })
    emitter.on('hook:beforeDestroy', () => {
        table.destroy()
    })
    return table
}

let dataTableInstance = null

onMounted(() => {
    dataTableInstance = initDataTable()
})

// 監聽 data 的變化並重新渲染 DataTable
watch(() => props.data, (newData) => {
    if (dataTableInstance) {
        dataTableInstance.clear()
        dataTableInstance.rows.add(newData)
        dataTableInstance.draw(false)
    }
}, {deep: true})

</script>

<style scoped>
@import 'datatables.net-bs5/css/dataTables.bootstrap5.min.css';
</style>
