<template>
    <div class="row">
        <div class="col-lg-12">
            <div class="card">
                <div class="card-header d-flex justify-content-between">
                    <div class="header-title">
                        <h4 class="card-title">ADHD DataTable</h4>
                    </div>
                </div>
                <div class="card-body">
                    <div class="table-responsive border-bottom my-3" v-if="result.type === ResultType.SUCCESS">
                        <data-table id="playersTable" :data="rowsData" :columns="columnsData"
                                    @rowClicked="rowOnClicked"/>
                    </div>
                    <div v-else-if="result.type === ResultType.FAILURE">
                        Error: {{ result.error }}
                    </div>
                    <div v-else></div>
                </div>
            </div>
        </div>
    </div>
    <button @click="modal = true" class="btn btn-primary">Launch demo modal</button>
    <modal-component v-model="modal" title="Modal title">
        <template #body>
            <p>NAN</p>
        </template>
    </modal-component>
</template>

<script setup lang="ts">
import {ref, watchEffect, onMounted} from 'vue'
import {useStore} from 'vuex'
import {type Player} from '@/models/Player'
import {type Result, ResultType} from "@/domains/handlers/Result"

// DataTable & Components
import 'datatables.net-bs5'
import DataTable from "@/components/DataTable.vue"
import ModalComponent from "@/presentations/components/modals/ModalComponent.vue"

const store = useStore()
const result = ref({type: ResultType.IDLE, error: ''} as Result<Player[], string>)
let columnsData = ref<{ title: string; data: string; }[]>([])
let rowsData = ref([])

const modal = ref(false)

watchEffect(() => {
    if (result.value.type === ResultType.LOADING) store.dispatch('updateLoadingAction', true)
    else store.dispatch('updateLoadingAction', false)
})

onMounted(() => {
    fetchData()
})

const fetchData = async () => {
    result.value = {type: ResultType.LOADING}
    fetch('api/Players')
        .then(response => response.json().then(data => ({ok: response.ok, data})))
        .then(response => {
            if (response.ok) {
                const newData = JSON.stringify(response.data)
                if (JSON.stringify(rowsData) !== newData) {
                    result.value = {type: ResultType.SUCCESS, data: response.data}
                    columnsData.value = columnsData.value.length === 0 ? Object.keys(response.data[0]).map(key => ({
                        title: key,
                        data: key
                    })) : columnsData.value
                    // Rework data.
                    columnsData.value = columnsData.value.slice(0, -2)
                    rowsData = response.data
                }
            } else {
                result.value = {type: ResultType.FAILURE, error: response.data.message}
            }
        })
        .catch(error => {
            result.value = {type: ResultType.FAILURE, error: error.message}
        })
}

const rowOnClicked = (rowData: Player) => {
    console.log(rowData)
}

</script>

<style scoped>
@import 'datatables.net-bs5/css/dataTables.bootstrap5.min.css';
</style>