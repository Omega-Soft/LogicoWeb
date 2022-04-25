import { createStore } from 'vuex'
import ticket from "./modules/Test/ticket"
import priority from "./modules/Test/priority"
import status from "./modules/Test/status"
import user from "./modules/Test/user"
import login from "./modules/Auth/login"

export const store = createStore({
  modules: {
    ticket,
    priority,
    status,
    user,
    login
  }
})
