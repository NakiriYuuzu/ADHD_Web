export type LevelRecord = {
    id: string,
    playerId: string,
    levelNumber: number,
    difficulty: string,
    score: number,
    time: number,
    createdAt: string
}

export type Player = {
    playerId: string,
    name: string,
    age: number,
    gender: string,
    createdAt: string,
    levelRecords: LevelRecord[]
}
