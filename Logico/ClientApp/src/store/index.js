import { createStore } from 'vuex'
import ticket from "./modules/ticket"
import priority from "./modules/priority"
import status from "./modules/status"
import user from "./modules/user"
import login from "./modules/login"

export const store = createStore({
  modules: {
    ticket,
    priority,
    status,
    user,
    login
  }
})
