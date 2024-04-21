<template>
    <div class="row">
        <div class="col-md-12 col-lg-8">
            <div class="row">
                <div class="col-md-12">
                    <div class="card" v-if="result.type === ResultType.SUCCESS">
                        <div class="flex-wrap card-header d-flex justify-content-between align-items-center">
                            <div class="header-title">
                                <h4 class="card-title">Gender Chart</h4>
                                <p class="mb-0">Total Male: {{countPlayersGender(0)}} Total Female: {{countPlayersGender(1)}}</p>
                            </div>
                            <div class="d-flex align-items-center align-self-center">
                                <div class="d-flex align-items-center text-primary">
                                    <svg xmlns="http://www.w3.org/2000/svg" width="12" viewBox="0 0 24 24"
                                         fill="currentColor">
                                        <g>
                                            <circle cx="12" cy="12" r="8" fill="currentColor"></circle>
                                        </g>
                                    </svg>
                                    <div class="ms-2">
                                        <span class="text-secondary">Male</span>
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
                                        <span class="text-secondary">Female</span>
                                    </div>
                                </div>
                            </div>
                            <div class="dropdown">
                                <a href="#" class="text-secondary dropdown-toggle" id="dropdownMenuButton22"
                                   data-bs-toggle="dropdown" aria-expanded="false"> This {{ genderPeriod }} </a>
                                <ul class="dropdown-menu dropdown-menu-end" aria-labelledby="dropdownMenuButton22">
                                    <li><a class="dropdown-item" href="#" @click="genderPeriod = 'Week'">This Week</a>
                                    </li>
                                    <li><a class="dropdown-item" href="#" @click="genderPeriod = 'Month'">This Month</a>
                                    </li>
                                    <li><a class="dropdown-item" href="#" @click="genderPeriod = 'Year'">This Year</a>
                                    </li>
                                </ul>
                            </div>
                        </div>
                        <div class="card-body">
                            <apexchart :height="256" type="area" id="d-main" :options="genderChart.options"
                                       :series="genderChart.series"/>
                        </div>
                    </div>
                    <div class="card" v-else>
                        <div class="flex-wrap card-header d-flex justify-content-between align-items-center">
                            <div class="header-title">
                                <h4 class="card-title">Gender Chart</h4>
                                <p class="mb-0"></p>
                            </div>
                        </div>
                        <div class="card-body">
                            <error-display @refresh-parent="fetchData"/>
                        </div>
                    </div>
                </div>
                <div class="col-md-12">
                    <div class="card" v-if="result.type === ResultType.SUCCESS">
                        <div class="flex-wrap card-header d-flex justify-content-between align-items-center">
                            <div class="header-title">
                                <h4 class="card-title">Gender Time Spent</h4>
                                <p class="mb-0"></p>
                            </div>
                            <div class="d-flex align-items-center align-self-center">
                                <div class="d-flex align-items-center text-primary">
                                    <svg xmlns="http://www.w3.org/2000/svg" width="12" viewBox="0 0 24 24"
                                         fill="currentColor">
                                        <g>
                                            <circle cx="12" cy="12" r="8" fill="currentColor"></circle>
                                        </g>
                                    </svg>
                                    <div class="ms-2">
                                        <span class="text-secondary">Male</span>
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
                                        <span class="text-secondary">Female</span>
                                    </div>
                                </div>
                            </div>
                            <div class="d-flex align-items-end">
                                <div class="dropdown me-3">
                                    <a href="#" class="text-secondary dropdown-toggle" id="dropdownMenuButton21"
                                       data-bs-toggle="dropdown" aria-expanded="false"> Level {{ genderLevelPeriod === 0 ? 'All' : genderLevelPeriod }} </a>
                                    <ul class="dropdown-menu dropdown-menu-end" aria-labelledby="dropdownMenuButton21">
                                        <li><a class="dropdown-item" href="#" @click="genderLevelPeriod = 0">Level All </a></li>
                                        <li><a class="dropdown-item" href="#" @click="genderLevelPeriod = 1">Level 1</a></li>
                                        <li><a class="dropdown-item" href="#" @click="genderLevelPeriod = 2">Level 2</a></li>
                                        <li><a class="dropdown-item" href="#" @click="genderLevelPeriod = 3">Level 3</a></li>
                                    </ul>
                                </div>
                                <div class="dropdown">
                                    <a href="#" class="text-secondary dropdown-toggle" id="dropdownMenuButton23"
                                       data-bs-toggle="dropdown" aria-expanded="false"> This {{ genderLevelDatePeriod }} </a>
                                    <ul class="dropdown-menu dropdown-menu-end" aria-labelledby="dropdownMenuButton23">
                                        <li><a class="dropdown-item" href="#" @click="genderLevelDatePeriod = 'Week'">This Week</a></li>
                                        <li><a class="dropdown-item" href="#" @click="genderLevelDatePeriod = 'Month'">This Month</a></li>
                                        <li><a class="dropdown-item" href="#" @click="genderLevelDatePeriod = 'Year'">This Year</a></li>
                                    </ul>
                                </div>
                            </div>
                        </div>
                        <div class="card-body">
                            <apexchart :height="256" type="area" id="d-main" :options="genderLevelChart.options"
                                       :series="genderLevelChart.series"/>
                        </div>
                    </div>
                    <div class="card" v-else>
                        <div class="flex-wrap card-header d-flex justify-content-between align-items-center">
                            <div class="header-title">
                                <h4 class="card-title">Gender Level Chart</h4>
                                <p class="mb-0"></p>
                            </div>
                        </div>
                        <div class="card-body">
                            <error-display @refresh-parent="fetchData"/>
                        </div>
                    </div>
                </div>
                <div class="col-md-12">
                    <div class="card" v-if="result.type === ResultType.SUCCESS">
                        <div class="card-header d-flex justify-content-between flex-wrap">
                            <div class="header-title">
                                <h4 class="card-title">Levels Chart</h4>
                            </div>
                            <div class="d-flex align-items-center align-self-center">
                                <div class="d-flex align-items-center text-primary">
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
                            <div class="dropdown">
                                <a href="#" class="text-secondary dropdown-toggle" id="dropdownMenuButton3"
                                   data-bs-toggle="dropdown" aria-expanded="false"> This {{levelPeriod}} </a>
                                <ul class="dropdown-menu dropdown-menu-end" aria-labelledby="dropdownMenuButton3">
                                    <li><a class="dropdown-item" href="#" @click="levelPeriod = 'Week'">This Week</a></li>
                                    <li><a class="dropdown-item" href="#" @click="levelPeriod = 'Month'">This Month</a></li>
                                    <li><a class="dropdown-item" href="#" @click="levelPeriod = 'Year'">This Year</a></li>
                                </ul>
                            </div>
                        </div>
                        <div class="card-body">
                            <apexchart :height="256" type="bar" class="dactivity1" id="dactivity1"
                                       :options="levelChart.options" :series="levelChart.series"/>
                        </div>
                    </div>
                    <div class="card" v-else>
                        <div class="card-header d-flex justify-content-between flex-wrap">
                            <div class="header-title">
                                <h4 class="card-title">Levels Chart</h4>
                            </div>
                        </div>
                        <div class="card-body">
                            <error-display @refresh-parent="fetchData"/>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="col-md-12 col-lg-4">
            <div class="row">
                <div class="col-md-12">
                    <div class="swiper-slide card card-slide">
                        <b-card-body v-if="result.type === ResultType.SUCCESS">
                            <div class="progress-widget">
                                <analytics-widget :size="(todayPlayers().length / result.data.length) * 100"
                                                  color="primary">
                                    <svg class="card-slie-arrow" width="24" height="24px" viewBox="0 0 24 24"
                                         v-if="todayPlayers().length === 0">
                                        <path fill="currentColor"
                                              d="M19,6.41L17.59,5L7,15.59V9H5V19H15V17H8.41L19,6.41Z"></path>
                                    </svg>
                                    <svg class="card-slie-arrow" width="24" height="24" viewBox="0 0 24 24" v-else>
                                        <path fill="currentColor"
                                              d="M5,17.59L15.59,7H9V5H19V15H17V8.41L6.41,19L5,17.59Z"></path>
                                    </svg>
                                </analytics-widget>
                                <div class="progress-detail">
                                    <p class="mb-2">Today Players: {{ todayPlayers().length }}</p>
                                    <h4 class="counter">Total Players: {{ result.data.length }}</h4>
                                </div>
                            </div>
                        </b-card-body>
                        <b-card-body v-else>
                            <error-display @refresh-parent="fetchData"/>
                        </b-card-body>
                    </div>
                </div>
                <div class="col-md-12">
                    <div v-if="result.type === ResultType.SUCCESS" class="card">
                        <div class="flex-wrap card-header d-flex justify-content-between">
                            <div class="header-title">
                                <h4 class="mb-2 card-title">Activity Overview</h4>
                                <p class="mb-0">
                                    <svg class="me-2" width="24" height="24" viewBox="0 0 24 24">
                                        <path fill="#17904b"
                                              d="M13,20H11V8L5.5,13.5L4.08,12.08L12,4.16L19.92,12.08L18.5,13.5L13,8V20Z"></path>
                                    </svg>
                                    last {{ activityPlayers().length }} activities
                                </p>
                            </div>
                        </div>
                        <div class="card-body">
                            <div class="mb-2 d-flex profile-media align-items-top" v-for="player in activityPlayers()"
                                 :key="player.playerId">
                                <div class="mt-1 profile-dots-pills border-primary"></div>
                                <div class="ms-4">
                                    <h6 class="mb-1"><strong>{{ player.name }}</strong> played
                                        <strong>{{ player.levelRecords.length }}</strong> levels.</h6>
                                    <span class="mb-0">{{ formatDate(player.createdAt) }}</span>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div v-else class="card">
                        <div class="flex-wrap card-header d-flex justify-content-between">
                            <div class="header-title">
                                <h4 class="mb-2 card-title">Activity Overview</h4>
                                <p class="mb-0">
                                    <svg class="me-2" width="24" height="24" viewBox="0 0 24 24">
                                        <path fill="#17904b"
                                              d="M13,20H11V8L5.5,13.5L4.08,12.08L12,4.16L19.92,12.08L18.5,13.5L13,8V20Z"></path>
                                    </svg>
                                    No activities...
                                </p>
                            </div>
                        </div>
                        <div class="card-body">
                            <error-display @refresh-parent="fetchData"/>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</template>

<script setup lang="ts">
// functional imports
import {setLoading} from "@/modules/Loading"
import {makeToast, ToastColor} from "@/modules/Toast"
import {formatDate, getStartOfPeriod} from "@/domains/utils/DateUtil"
import {type LevelRecord, type Player} from "@/models/Player"
import {type Result, ResultType} from "@/domains/handlers/Result"
import {onMounted, watchEffect, ref} from 'vue'

// components imports
import AnalyticsWidget from '@/components/widgets/AnalyticsWidget.vue'
import ErrorDisplay from "@/presentations/components/errors/ErrorDisplay.vue"

// functional
const fetchData = async () => {
    result.value = {type: ResultType.LOADING}
    fetch('api/Players')
        .then(response => response.json().then(data => ({ok: response.ok, data})))
        .then(response => {
            if (response.ok) {
                result.value = {type: ResultType.SUCCESS, data: response.data}
            } else {
                result.value = {type: ResultType.FAILURE, error: response.data.message}
            }
        })
        .catch(error => {
            result.value = {type: ResultType.FAILURE, error: error.message}
        })
}

const todayPlayers = () => {
    return playersData.value.filter(player => {
        return new Date(player.createdAt).toDateString() === new Date().toDateString()
    })
}

const activityPlayers = () => {
    return playersData.value.slice(0, 12).reverse()
}

const countPeriodLength = (startOfPeriod: Date, period: string) => {
    switch(period) {
        case 'week':
            categories.value = ['Mon', 'Tue', 'Wed', 'Thu', 'Fri', 'Sat', 'Sun']
            return 7
        case 'month':
            const periodLength = new Date(startOfPeriod.getFullYear(), startOfPeriod.getMonth() + 1, 0).getDate()
            categories.value = Array.from({length: periodLength}, (_, i) => i + 1)
            return periodLength
        case 'year':
            categories.value = ['Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun', 'Jul', 'Aug', 'Sep', 'Oct', 'Nov', 'Dec']
            return 12
        default:
            throw new Error(`Invalid period: ${period}`)
    }
}

const countPlayersGender = (gender: number) => {
    // 0 = male | 1 = female
    const male = genderChart.value.series[0].data.reduce((a: number, b: number) => a + b, 0)
    const female = genderChart.value.series[1].data.reduce((a: number, b: number) => a + b, 0)
    return (gender === 0) ? male : female
}

const countGenderPeriod = (players: Player[], startOfPeriod: Date, period: string) => {
    const periodLength = countPeriodLength(startOfPeriod, period)
    const maleCounts = Array(periodLength).fill(0)
    const femaleCounts = Array(periodLength).fill(0)
    players.forEach(player => {
        const createdAt = new Date(player.createdAt)
        if (createdAt >= startOfPeriod) {
            const index = period === 'week' ? createdAt.getDay() : period === 'month' ? createdAt.getDate() - 1 : createdAt.getMonth()
            if (index >= 0 && index < periodLength) {
                switch (player.gender.toLowerCase()) {
                    case 'male':
                        maleCounts[index]++
                        break
                    case 'female':
                        femaleCounts[index]++
                        break
                }
            }
        }
    })
    return {
        male: maleCounts,
        female: femaleCounts
    }
}

const countGenderLevelPeriod = (players: Player[], startOfPeriod: Date, period: string) => {
    const periodLength = countPeriodLength(startOfPeriod, period)
    const maleCounts = Array(periodLength).fill(0)
    const femaleCounts = Array(periodLength).fill(0)
    players.forEach(player => {
        const createdAt = new Date(player.createdAt)
        if (createdAt >= startOfPeriod) {
            const index = period === 'week' ? createdAt.getDay() : period === 'month' ? createdAt.getDate() - 1 : createdAt.getMonth()
            if (index >= 0 && index < periodLength) {
                switch (player.gender.toLowerCase()) {
                    case 'male':
                        if (genderLevelPeriod.value === 0) {
                            maleCounts[index] += player.levelRecords.filter(level => level.time > 0).reduce((a, b) => a + b.time, 0)
                        } else {
                            maleCounts[index] += player.levelRecords.filter(level => level.time > 0 && level.levelNumber === genderLevelPeriod.value - 1).reduce((a, b) => a + b.time, 0)
                        }
                        break
                    case 'female':
                        if (genderLevelPeriod.value === 0) {
                            femaleCounts[index] += player.levelRecords.filter(level => level.time > 0).reduce((a, b) => a + b.time, 0)
                        } else {
                            femaleCounts[index] += player.levelRecords.filter(level => level.time > 0 && level.levelNumber === genderLevelPeriod.value - 1).reduce((a, b) => a + b.time, 0)
                        }
                        break
                }
            }
        }
    })
    return {
        male: maleCounts,
        female: femaleCounts
    }
}

const countLevelPeriod = (levels: LevelRecord[], startOfPeriod: Date, period: string) => {
    const periodLength = countPeriodLength(startOfPeriod, period)
    const levelCounts = Array(4).fill(0).map(() => Array(periodLength).fill(0))

    levels.forEach(level => {
        const createdAt = new Date(level.createdAt)
        if (createdAt >= startOfPeriod) {
            const index = period === 'week' ? createdAt.getDay() : period === 'month' ? createdAt.getDate() - 1 : createdAt.getMonth()
            if (index >= 0 && index < periodLength && level.levelNumber >= 0 && level.levelNumber < 4) {
                levelCounts[level.levelNumber][index]++
            }
        }
    })

    return {
        levelOne: levelCounts[0],
        levelTwo: levelCounts[1],
        levelThree: levelCounts[2],
        levelFour: levelCounts[3]
    }
}

const updateGenderChart = () => {
    try {
        const now = new Date()
        const period = genderPeriod.value.toLowerCase()
        const startOfPeriod = getStartOfPeriod(now, period)
        const timePeriodPlayers = playersData.value.filter(player => new Date(player.createdAt) >= startOfPeriod)
        const counts = countGenderPeriod(timePeriodPlayers, startOfPeriod, period)
        genderChart.value.series[0].data = counts.male
        genderChart.value.series[1].data = counts.female
        genderChart.value.options.xaxis.categories.splice(0, genderChart.value.options.xaxis.categories.length, ...categories.value)
    } catch (error: any) {
        result.value = {type: ResultType.FAILURE, error: error.message}
    }
}

const updateGenderLevelChart = () => {
    try {
        const now = new Date()
        const period = genderLevelDatePeriod.value.toLowerCase()
        const startOfPeriod = getStartOfPeriod(now, period)
        const timePeriodPlayers = playersData.value.filter(player => new Date(player.createdAt) >= startOfPeriod)
        const counts = countGenderLevelPeriod(timePeriodPlayers, startOfPeriod, period)
        console.log(genderLevelChart.value)
        genderLevelChart.value.series[0].data = counts.male
        genderLevelChart.value.series[1].data = counts.female
        genderLevelChart.value.options.xaxis.categories.splice(0, genderChart.value.options.xaxis.categories.length, ...categories.value)
    } catch (error: any) {
        result.value = {type: ResultType.FAILURE, error: error.message}
    }
}

const updateLevelChart = () => {
    try {
        const now = new Date()
        const period = levelPeriod.value.toLowerCase()
        const startOfPeriod = getStartOfPeriod(now, period)
        levelsData.value = playersData.value.map(player => player.levelRecords).flat()
        const timePeriodPlayers = levelsData.value.filter(level => new Date(level.createdAt) >= startOfPeriod)
        const counts = countLevelPeriod(timePeriodPlayers, startOfPeriod, period)
        levelChart.value.series[0].data = counts.levelOne
        levelChart.value.series[1].data = counts.levelTwo
        levelChart.value.series[2].data = counts.levelThree
        levelChart.value.series[3].data = counts.levelFour
        levelChart.value.options.xaxis.categories.splice(0, levelChart.value.options.xaxis.categories.length, ...categories.value)
    } catch (error: any) {
        result.value = {type: ResultType.FAILURE, error: error.message}
    }
}

// variables
const result = ref({type: ResultType.LOADING, error: ''} as Result<Player[], string>)
const playersData = ref([] as Player[])
const levelsData = ref([] as LevelRecord[])
const categories = ref([] as any)
const genderPeriod = ref('Week')
const genderLevelDatePeriod = ref('Week')
const genderLevelPeriod = ref(0)
const levelPeriod = ref('Week')

// chart Data.
const genderChart = ref({
    series: [
        {
            name: 'male',
            data: []
        } as any,
        {
            name: 'female',
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
        colors: ['#3a57e8', '#079aa2'],
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
            categories: ['Mon', 'Tue', 'Wed', 'Thu', 'Fri', 'Sat', 'Sun'] as any
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
            enabled: true
        }
    }
})
const genderLevelChart = ref({
    series: [
        {
            name: 'male',
            data: []
        } as any,
        {
            name: 'female',
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
                show: false // or just here to disable only x-axis grids
            },
            categories: ['Mon', 'Tue', 'Wed', 'Thu', 'Fri', 'Sat', 'Sun'] as string[]
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
            enabled: true
        }
    }
})
const levelChart = ref({
    series: [
        {
            name: 'level 1',
            data: []
        } as any,
        {
            name: 'level 2',
            data: []
        } as any,
        {
            name: 'level 3',
            data: []
        } as any,
        {
            name: 'level 4',
            data: []
        } as any
    ],
    options: {
        chart: {
            type: 'bar',
            height: 256,
            stacked: true,
            toolbar: {
                show: true
            }
        },
        colors: ['#3a57e8', '#4bc7d2', '#f16a1b', '#1aa053'],
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
            categories: ['Mon', 'Tue', 'Wed', 'Thu', 'Fri', 'Sat', 'Sun'] as any,
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
                    return val.toString()
                }
            }
        }
    }
})

watchEffect(() => {
    switch (result.value.type) {
        case ResultType.SUCCESS:
            setLoading(false)
            playersData.value = result.value.data
            updateGenderChart()
            updateGenderLevelChart()
            updateLevelChart()
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