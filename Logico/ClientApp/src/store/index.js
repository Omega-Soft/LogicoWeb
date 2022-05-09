import { createStore } from 'vuex'
import article from "./modules/Parametrage/article"
import fournisseur from "./modules/Parametrage/fournisseur"
import moule from "./modules/Parametrage/moule"
import qualite from "./modules/Parametrage/qualite"
import bateau from "./modules/Parametrage/bateau"
import camion from "./modules/Parametrage/camion"
import transporteur from "./modules/Parametrage/transporteur"
import origine from "./modules/Parametrage/origine"
import provenance from "./modules/Parametrage/provenance"
import ticket from "./modules/Test/ticket"
import priority from "./modules/Test/priority"
import status from "./modules/Test/status"
import user from "./modules/Test/user"
import login from "./modules/Auth/login"

export const store = createStore({
  modules: {
    article,
    fournisseur,
    moule,
    bateau,
    qualite,
    camion,
    transporteur,
    origine,
    provenance,
    ticket,
    priority,
    status,
    user,
    login
  }
})
