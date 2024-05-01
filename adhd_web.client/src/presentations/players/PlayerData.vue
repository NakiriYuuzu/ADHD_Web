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
    <modal-component v-model="modal" 
                     :title="`Player ${playerChart.series[0].name} Status`" 
                     :button-label="checkLevel() ? 'Submit' : ''" 
                     :label-on-clicked="submitOnClicked">
        <template #body>
            <div class="row">
                <div class="col-md-12 col-lg-12">
                    <div class="row">
                        <div class="col-md-12">
                            <div class="card" v-if="result.type === ResultType.SUCCESS">
                                <div class="flex-wrap card-header d-flex justify-content-between align-items-center">
                                    <div class="header-title">
                                        <h4 class="card-title">Player {{playerChart.series[0].name}}</h4>
                                    </div>
                                    <div class="invisible d-flex align-items-center align-self-center">
                                        <div class="d-flex align-items-center ms-3 text-primary">
                                            <svg xmlns="http://www.w3.org/2000/svg" width="12" viewBox="0 0 24 24"
                                                 fill="currentColor">
                                                <g>
                                                    <circle cx="12" cy="12" r="8" fill="currentColor"></circle>
                                                </g>
                                            </svg>
                                            <div class="ms-2">
                                                <span class="text-secondary">Level 01</span>
                                            </div>
                                        </div>
                                        <div class="d-flex align-items-center ms-3 text-info">
                                            <svg xmlns="http://www.w3.org/2000/svg" width="12" viewBox="0 0 24 24"
                                                 fill="currentColor">
                                                <g>
                                                    <circle cx="12" cy="12" r="8" fill="currentColor"></circle>
                                                </g>
                                            </svg>
                                            <div class="ms-2">
                                                <span class="text-secondary">Level 02</span>
                                            </div>
                                        </div>
                                        <div class="d-flex align-items-center ms-3 text-warning">
                                            <svg xmlns="http://www.w3.org/2000/svg" width="12" viewBox="0 0 24 24"
                                                 fill="currentColor">
                                                <g>
                                                    <circle cx="12" cy="12" r="8" fill="currentColor"></circle>
                                                </g>
                                            </svg>
                                            <div class="ms-2">
                                                <span class="text-secondary">Level 03</span>
                                            </div>
                                        </div>
                                        <div class="d-flex align-items-center ms-3 text-success">
                                            <svg xmlns="http://www.w3.org/2000/svg" width="12" viewBox="0 0 24 24"
                                                 fill="currentColor">
                                                <g>
                                                    <circle cx="12" cy="12" r="8" fill="currentColor"></circle>
                                                </g>
                                            </svg>
                                            <div class="ms-2">
                                                <span class="text-secondary">Level 04</span>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="card-body">
                                    <apexchart :height="256" type="area" id="d-main" :options="playerChart.options"
                                               :series="playerChart.series"/>
                                </div>
                            </div>
                            <div class="card" v-else>
                                <div class="flex-wrap card-header d-flex justify-content-between align-items-center">
                                    <div class="header-title">
                                        <h4 class="card-title">No Data.</h4>
                                        <p class="mb-0"></p>
                                    </div>
                                </div>
                                <div class="card-body">
                                    <error-display @refresh-parent="fetchData"/>
                                </div>
                            </div>
                            <div class="form-group" v-if="checkLevel()">
                                <label class="form-label" for="lv04">Level 04 Record:</label>
                                <input type="number" class="form-control" id="lv04" placeholder="ex: 202" v-model="playedTime" />
                            </div>
                            <div v-else></div>
                        </div>
                    </div>
                </div>
            </div>
        </template>
    </modal-component>
</template>

<script setup lang="ts">
import {onMounted, ref, watchEffect} from 'vue'
import {makeToast, ToastColor} from "@/modules/Toast"
import {setLoading} from "@/modules/Loading"
import {type LevelRecord, type Player} from '@/models/Player'
import {type Result, ResultType} from "@/domains/handlers/Result"

// DataTable & Components
import 'datatables.net-bs5'
import DataTable from "@/components/DataTable.vue"
import ModalComponent from "@/presentations/components/modals/ModalComponent.vue"
import ErrorDisplay from "@/presentations/components/errors/ErrorDisplay.vue"
import {formatDate} from "@/domains/utils/DateUtil";

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
                        data: key,
                        render: function (data: any) {
                            switch (key) {
                                case 'createdAt':
                                    return formatDate(data)
                                case 'levelRecords':
                                    const level = calculateProgress(data)
                                    return `
                                    <div class="mb-2 d-flex align-items-center">
                                        <h6>${level}%</h6>
                                    </div>
                                    <div class="shadow-none progress bg-soft-primary w-100" style="height: 4px">
                                        <div class="progress-bar ${level === 100 ? 'bg-success' : level > 50 ? 'bg-primary' : 'bg-warning'}" data-toggle="progress-bar" role="progressbar" aria-valuenow="100" aria-valuemin="0" aria-valuemax="100" style="width: ${level}%; transition: width 2s ease 0s"></div>
                                    </div>`
                                default:
                                    return data
                            }
                        }
                    })) : columnsData.value
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

const checkLevel = () : boolean => {
    return playerChart.value.series[0].data.length === 3
}

const rowOnClicked = (rowData: any) => {
    if (rowData === undefined) return
    selectedPlayer.value = rowData
    playerChart.value.series[0].name = rowData.name
    playerChart.value.series[0].data = rowData.levelRecords.map((record: LevelRecord) => record.time)
    modal.value = true
}

const submitOnClicked = () => {
    fetch('api/Levels', {
        method: 'POST',
        headers: {
            'Accept': '*/*',
            'Content-Type': 'application/json'
        },
        body: JSON.stringify({
            "playerId": selectedPlayer.value.id,
            "levelNumber": 3,
            "difficulty": "normal",
            "score": 0,
            "time": playedTime.value
        })
    })
        .then(response => response.json().then(data => ({ok: response.ok, data})))
        .then(response => {
            if (response.ok) {
                makeToast(`Success`, `Player ${playerChart.value.series[0].name} record has been saved.`, ToastColor.Success)
                fetchData()
            } else {
                makeToast(`Error`, `${response.data.message}`, ToastColor.Error)
            }
        })
        .catch(error => {
            makeToast(`Error`, `${error.message}`, ToastColor.Error)
        })
    playedTime.value = ''
}

const calculateProgress = (data: LevelRecord[]): number => {
    if (data.length === 0) return 0
    const levels = data.map(record => record.levelNumber).length
    return (levels / 4) * 100
}

const result = ref({type: ResultType.IDLE, error: ''} as Result<Player[], string>)
let columnsData = ref<{ title: string; data: string; render: Function }[]>([])
let rowsData = ref([])
const modal = ref(false)
const playedTime = ref('')
const selectedPlayer = ref({} as any)
const playerChart = ref({
    series: [
        {
            name: 'None',
            data: []
        } as any
    ],
    options: {
        chart: {
            fontFamily: '"Inter", sans-serif, "Apple Color Emoji", "Segoe UI Emoji", "Segoe UI Symbol", "Noto Color Emoji"',
            height: 256,
            type: 'area',
            toolbar: {
                show: true
            },
            sparkline: {
                enabled: false
            }
        },
        colors: ['#3a57e8', '#079aa2', '#f16a1b', '#1aa053'],
        dataLabels: {
            enabled: false
        },
        stroke: {
            curve: 'smooth',
            width: 3
        },
        yaxis: {
            show: true,
            labels: {
                show: true,
                minWidth: 19,
                maxWidth: 19,
                style: {
                    colors: '#8A92A6'
                },
                offsetX: -5
            }
        },
        legend: {
            show: false
        },
        xaxis: {
            labels: {
                minHeight: 22,
                maxHeight: 22,
                show: true,
                style: {
                    colors: '#8A92A6'
                }
            },
            lines: {
                show: true // or just here to disable only x-axis grids
            },
            categories: ['Lv01', 'Lv02', 'Lv03', 'Lv04'] as any
        },
        grid: {
            show: false
        },
        fill: {
            type: 'gradient',
            gradient: {
                shade: 'dark',
                type: 'vertical',
                shadeIntensity: 0,
                gradientToColors: undefined, // optional, if not defined - uses the shades of same color in series
                inverseColors: true,
                opacityFrom: 0.4,
                opacityTo: 0.1,
                stops: [0, 50, 80],
                colors: ['#3a57e8', '#4bc7d2']
            }
        },
        tooltip: {
            enabled: true,
            y: {
                formatter: function (val: any) {
                    return val + ' seconds'
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
.invisible {
    opacity: 0;
}
</style>