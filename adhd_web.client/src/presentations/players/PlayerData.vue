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
                    <div class="table-responsive border-bottom my-3" v-else>
                        <error-display @refresh-parent="fetchData"/>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <button @click="modal = true" class="btn btn-primary">Launch demo modal</button>
    <modal-component v-model="modal" title="Modal title">
        <template #body>

        </template>
    </modal-component>
</template>

<script setup lang="ts">
import {ref, watchEffect, onMounted} from 'vue'
import {makeToast, ToastColor} from "@/modules/Toast"
import {setLoading} from "@/modules/Loading"
import {type Player} from '@/models/Player'
import {type Result, ResultType} from "@/domains/handlers/Result"

// DataTable & Components
import 'datatables.net-bs5'
import DataTable from "@/components/DataTable.vue"
import ModalComponent from "@/presentations/components/modals/ModalComponent.vue"
import ErrorDisplay from "@/presentations/components/errors/ErrorDisplay.vue"

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
    modal.value = true
}

const result = ref({type: ResultType.IDLE, error: ''} as Result<Player[], string>)
let columnsData = ref<{ title: string; data: string; }[]>([])
let rowsData = ref([])
const modal = ref(false)
const conversionChart = ref({
    series: [
        {
            name: 'Successful deals',
            data: [30, 50, 35, 60, 40, 60, 60, 30, 50, 35]
        },
        {
            name: 'Failed deals',
            data: [40, 50, 55, 50, 30, 80, 30, 40, 50, 55]
        }
    ],
    options: {
        chart: {
            type: 'bar',
            height: 256,
            stacked: true,
            toolbar: {
                show: false
            }
        },
        colors: ['#3a57e8', '#4bc7d2'],
        plotOptions: {
            bar: {
                horizontal: false,
                columnWidth: '28%',
                endingShape: 'rounded',
                borderRadius: 5
            }
        },
        legend: {
            show: false
        },
        dataLabels: {
            enabled: false
        },
        stroke: {
            show: true,
            width: 2,
            colors: ['transparent']
        },
        xaxis: {
            categories: ['S', 'M', 'T', 'W', 'T', 'F', 'S', 'M', 'T', 'W'],
            labels: {
                minHeight: 20,
                maxHeight: 20,
                style: {
                    colors: '#8A92A6'
                }
            }
        },
        yaxis: {
            title: {
                text: ''
            },
            labels: {
                minWidth: 19,
                maxWidth: 19,
                style: {
                    colors: '#8A92A6'
                }
            }
        },
        fill: {
            opacity: 1
        },
        tooltip: {
            y: {
                formatter: function (val: any) {
                    return '$ ' + val + ' thousands'
                }
            }
        }
    }
})

watchEffect(() => {
    switch (result.value.type) {
        case ResultType.SUCCESS:
            setLoading(false)
            break
        case ResultType.FAILURE:
            setLoading(false)
            makeToast(`Error`, `${result.value.error}`, ToastColor.Error)
            result.value.error = ''
            result.value = {type: ResultType.IDLE}
            break
        case ResultType.LOADING:
            setLoading(true)
            break
        case ResultType.IDLE:
            setLoading(false)
            break
    }
})

onMounted(() => {
    fetchData()
})
</script>

<style scoped>
@import 'datatables.net-bs5/css/dataTables.bootstrap5.min.css';
</style>