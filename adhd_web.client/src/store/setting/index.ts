import { initialState as state } from './state'
import actions from './actions.js'
import mutations from './mutations'
import getters from './getters.js'

export default {
  namespaced: true,
  state,
  getters,
  actions,
  mutations
}
