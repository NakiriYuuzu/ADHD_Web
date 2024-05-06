import {createStore, createLogger} from 'vuex'
import setting from './setting'
import {type Toast} from "@/modules/Toast"

const debug = process.env.NODE_ENV !== 'production'
export default createStore({
    state: {
        shareOffcanvas: false,
        isLoading: false,
        toasts: [] as Toast[],
        token: ''
        
    },
    getters: {
        shareOffcanvas: (state) => state.shareOffcanvas,
        isLoading: (state) => state.isLoading,
        isLogin: (state) => state.token,
        toasts: (state) => state.toasts
    },
    mutations: {
        openBottomCanvasCommit(state, payload) {
            state[payload.name] = payload.value
        },
        setLoadingCommit(state, status) {
            state.isLoading = status
        },
        setToastCommit(state, toast: Toast) {
            state.toasts.push(toast)
        },
        removeToastCommit(state, toastId: number) {
            state.toasts = state.toasts.filter(toast => toast.id !== toastId)
        },
        setTokenCommit(state, token: string) {
            state.token = token
        }
    },
    actions: {
        openBottomCanvasAction({commit}, payload) {
            commit('openBottomCanvasCommit', payload)
        },
        updateLoadingAction({commit}, status) {
            commit('setLoadingCommit', status)
        },
        addToastAction({commit, state}, toast: Toast) {
            let newToast = {
                id: state.toasts.length + 1,
                title: toast.title,
                message: toast.message,
                color: toast.color,
                date: Date.now(),
                duration: toast.duration
            }
            commit('setToastCommit', newToast)
            
            if (toast.duration === 0) return
            
            setTimeout(() => {
                commit('removeToastCommit', newToast.id)
            }, newToast.duration)
        },
        removeToastAction({commit}, toastId: number) {
            commit('removeToastCommit', toastId)
        },
        setTokenAction({commit}, token: string) {
            commit('setTokenCommit', token)
        }
    },
    modules: {
        setting: setting
    },
    strict: debug,
    plugins: debug ? [createLogger()] : []
})
