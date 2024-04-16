export enum ToastColor {
    Success = '#1aa053',
    Error = '#c03221',
    Info = '#007aff',
    Warning = '#f16a1b'
}

export type Toast = {
    id: number
    title: string
    message: string
    color: ToastColor
    date: number
}