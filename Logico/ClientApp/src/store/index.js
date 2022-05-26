import { createStore } from 'vuex'
import login from "./modules/Auth/login"
import user from "./modules/Administration/user"
import group from "./modules/Administration/group"
import role from "./modules/Administration/role"
import page from "./modules/Administration/page"
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
import bonReception from "./modules/BonReception/bonReception"

export const store = createStore({
  modules: {
    login,
    user,
    group,
    role,
    page,
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
    bonReception
  }
})
