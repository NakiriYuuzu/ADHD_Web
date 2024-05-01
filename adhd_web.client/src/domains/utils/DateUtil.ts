export const formatDate = (date: string): string => {
    const dateObj = new Date(date)
    return dateObj.toLocaleString('en-US')
}

export const getStartOfPeriod = (date: Date, period: string) => {
    const result = new Date(date)
    const lowPeriod = period.toLowerCase()
    if (lowPeriod === 'week') {
        const day = result.getDay()
        result.setDate(result.getDate() - day)
    } else if (lowPeriod === 'month') {
        result.setDate(1)
    }
    else if (lowPeriod === 'year') {
        result.setMonth(0, 1)
    }
    result.setHours(0, 0, 0, 0)
    return result
}
