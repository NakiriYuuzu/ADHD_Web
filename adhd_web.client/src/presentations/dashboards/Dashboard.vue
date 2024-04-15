<template>
    <div class="container mx-auto px-4">
        <h1 class="text-2xl font-bold text-center my-5">Player Information</h1>
        <div v-if="result.type == ResultType.LOADING" class="text-center">Loading...</div>
        <ul v-else-if="result.type == ResultType.SUCCESS" class="space-y-3">
            <li v-for="player in result.data" :key="player.playerId" class="p-4 shadow-lg rounded-lg">
                <p><strong>ID:</strong> {{ player.playerId }}</p>
                <p><strong>Name:</strong> {{ player.name }}</p>
                <p><strong>Age:</strong> {{ player.age }}</p>
                <p><strong>Gender:</strong> {{ player.gender }}</p>
            </li>
        </ul>
        <div v-else-if="result.type == ResultType.FAILURE" class="text-center text-red-500">Error: {{ result.error }}</div>
    </div>
</template>


<script lang="ts">
import {defineComponent} from 'vue'
import {type Player} from '@/models/Player'
import {type Result, ResultType} from "@/domains/handlers/Result"

export default defineComponent({
    name: 'Dashboard',
    computed: {
        ResultType() {
            return ResultType
        }
    },
    data(): { result: Result<Player, string> } {
        return {
            result: {type: ResultType.IDLE},
        };
    },
    methods: {
        async fetchData() {
            this.result = {type: ResultType.LOADING}
            fetch('api/Players?pageNumber=1&pageSize=10')
                .then(async r => {
                    const data = await r.json()
                    if (r.ok) return data
                    throw new Error(`${r.status}: ${data.message}` || 'An error occurred')
                })
                .then(json => {
                    this.result = {type: ResultType.SUCCESS, data: json}
                })
                .catch(e => {
                    this.result = {type: ResultType.FAILURE, error: e.message || 'An error occurred'}
                    console.log(this.result)
                    console.log('error')
                    console.log(e)
                })
        },
    },
    created() {
        this.fetchData()
    },
})
</script>