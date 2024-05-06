import store from '@/store'

export const checkToken = () : boolean => {
    return store.state.token !== ''
}

export const setToken = (token: string) => {
    store.dispatch('setTokenAction', token)
}