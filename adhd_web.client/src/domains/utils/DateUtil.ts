export const formatDate = (date: string): string => {
    const dateObj = new Date(date)
    return dateObj.toLocaleString('en-US')
}

export const getStartOfPeriod = (date: Date, period: string) => {
    const result = new Date(date.getFullYear(), period === 'month' ? date.getMonth() : 0, period === 'day' ? date.getDate() : 1)
    result.setHours(0, 0, 0, 0)
    return result
}
