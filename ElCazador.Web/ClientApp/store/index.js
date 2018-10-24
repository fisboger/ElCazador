import Vue from 'vue'
import Vuex from 'vuex'

Vue.use(Vuex)

// TYPES
//#region Targets
const ADD_TARGET = 'ADD_TARGET';
const EDIT_TARGET = 'EDIT_TARGET';
//#endregion

//#region User
const ADD_USER = 'ADD_USER';
const EDIT_USER = 'EDIT_USER';
//#endregion

//#region Log
const ADD_LOG_ENTRY = 'ADD_LOG_ENTRY';
//#endregion


// Components
const targets = {
    isLoading: false,
    data: []
};

const users = {
    isLoading: false,
    data: []
};

const log = {
    isLoading: false,
    data: []
}

// STATE
const state = {
    targets: targets,
    users: users,
    log: log
};


// ACTIONS
const actions = ({
    setCounter({ commit }, obj) {
        commit(MAIN_SET_COUNTER, obj)
    }
});

export default new Vuex.Store({
    state,
    mutations: {
        //#region targets
        [ADD_TARGET](state, target) {
            state.targets.data.push(target);
        },
        [EDIT_TARGET](state, target) {
            var element = state.targets.data.find(function (element) {
                return element.id == target.id;
            });

            element.hostname = target.hostname;
            element.ip = target.ip;
        },
        //#endregion
        //#region Users
        [ADD_USER](state, user) {
            state.users.data.push(user);
        },
        [EDIT_USER](state,user) {
            var element = state.users.data.find(function (element) {
                return element.id = user.id;
            });

            element.username = user.username;
            element.targetID = user.targetID;
            element.hash = user.hash;
            element.clearTextPw = user.clearTextPw;
            element.type = user.type;
            element.collectionMethod = user.collectionMethod;
        },
        //#endregion
        //#region Log
        [ADD_LOG_ENTRY](state, entry) {
            state.log.data.push(entry);
        }
        //#endregion
    },
    actions
});
