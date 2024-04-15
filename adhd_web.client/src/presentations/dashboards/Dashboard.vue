<template>
    <div class="row">
        <div class="col-sm-12">
            <div class="card">
                <div class="card-header d-flex justify-content-between">
                    <div class="header-title">
                        <h4 class="card-title">Bootstrap Datatables</h4>
                    </div>
                </div>
                <div class="card-body">
                    <p>Images in Bootstrap are made responsive with <code>.img-fluid</code>. <code>max-width:
                        100%;</code> and <code>height: auto;</code> are applied to the image so that it scales with the
                        parent element.</p>
                    <div class="table-responsive border-bottom my-3" v-if="result.type === ResultType.SUCCESS">
                        <data-table id="playersTable" :data="rowsData" :columns="columnsData"/>
                    </div>
                    <div v-else-if="result.type === ResultType.LOADING">
                        Loading...
                    </div>
                    <div v-else>
                        Error: {{ result.error }}
                    </div>
                </div>
            </div>
        </div>
    </div>
</template>

<script lang="ts">
import {defineComponent} from 'vue'
import {type Player} from '@/models/Player'
import {type Result, ResultType} from "@/domains/handlers/Result"
import $ from 'jquery'
import 'datatables.net-bs5'
import DataTable from "@/components/DataTable.vue"

export default defineComponent({
    computed: {
        ResultType() {
            return ResultType
        }
    },
    components: {DataTable},
    data() {
        return {
            intervalId: 0,
            result: {type: ResultType.IDLE, data: [], error: ''} as Result<Player[], string>,
            columnsData: [] as Array<{ title: string, data: string }>,
            rowsData: [] as Array<Player>
        }
    },
    methods: {
        async fetchData() {
            fetch('api/Players')
                .then(response => response.json().then(data => ({ok: response.ok, data})))
                .then(response => {
                    if (response.ok) {
                        const newData = JSON.stringify(response.data)
                        if (JSON.stringify(this.rowsData) !== newData) {
                            this.result = {type: ResultType.SUCCESS, data: response.data}
                            this.columnsData = this.columnsData.length === 0 ? Object.keys(response.data[0]).map(key => ({
                                title: key,
                                data: key
                            })) : this.columnsData
                            this.columnsData = this.columnsData.slice(0, -2)
                            this.rowsData = response.data
                        }
                    } else {
                        throw new Error(`${response.data.message}` || 'An error occurred')
                    }
                })
                .catch(error => {
                    this.result = {type: ResultType.FAILURE, error: error.message}
                })
        }
    },
    created() {
        this.fetchData()
    }
})
</script>

<style scoped>
@import 'datatables.net-bs5/css/dataTables.bootstrap5.min.css';
</style>