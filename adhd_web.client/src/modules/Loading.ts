import store from '@/store'

export const setLoading = (loading: boolean) => {
    store.dispatch('updateLoadingAction', loading)
}

export const getLoading = () => {
    return store.state.isLoading
}