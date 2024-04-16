<template>
    <table :class="'table-hover table ' + className" ref="tableRef"></table>
</template>

<script setup>
import { onMounted, onBeforeUnmount, ref, defineProps, getCurrentInstance } from 'vue'
import 'datatables.net-bs5/css/dataTables.bootstrap5.min.css'
import 'datatables.net-bs5/js/dataTables.bootstrap5.min.js'
import 'datatables.net-bs5'
import $ from 'jquery'

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

const { emit } = getCurrentInstance()

const tableRef = ref(null)

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
                props.columns.forEach(column => {
                    const footerCell = document.createElement('th')
                    footerCell.textContent = column.title
                    footerRow.appendChild(footerCell)
                })
                $(tableRef.value).append($('<tfoot>').append(footerRow))
            }
        }
    })
    table.on('click', 'tr', function () {
        const data = table.row(this).data();
        emit('row-clicked', data)
    })
    onBeforeUnmount(() => {
        table.destroy()
    })
}

onMounted(() => {
    initDataTable()
})
</script>


<style scoped>
@import 'datatables.net-bs5/css/dataTables.bootstrap5.min.css';
</style>

