import Vue from 'vue'
import Vuex from 'vuex'

Vue.use(Vuex)

// TYPES
//#region Targets
const ADD_TARGET = 'ADD_TARGET';
const EDIT_TARGET = 'EDIT_TARGET';
//#endregion

// Components
const targets = {
    isLoading: false,
    data: [
        {
            id: 'd8c433a6-8e02-4474-9ada-621a434deb8e',
            timestamp: "2018-05-14 18:00:00",
            hostname: "DC01",
            ip: "192.168.0.1",
            dumped: false
        },
        {
            id: '5d8ca0db-75ba-4f65-9e9f-4d8f8ce08d3a',
            timestamp: "2018-05-14 17:00:00",
            hostname: "MSSQL01",
            ip: "10.0.2.79",
            dumped: false
        },
        {
            id: 'bf3dbd6a-3f22-4bf7-ac12-45f3835dbf19',
            timestamp: "2018-05-14 16:00:00",
            hostname: "DC02",
            ip: "192.168.0.2",
            dumped: false
        },
        {
            id: '4a79543d-ebe9-4181-b805-e98b62bb35b6',
            timestamp: "2018-05-14 15:00:00",
            hostname: "SFR-TP",
            ip: "192.168.0.7",
            dumped: true
        }
    ]
}

// STATE
const state = {
    targets: targets
}

// ACTIONS
const actions = ({
    setCounter({ commit }, obj) {
        commit(MAIN_SET_COUNTER, obj)
    }
})

export default new Vuex.Store({
    state,
    mutations: {
        //#region targets
        [ADD_TARGET] (state, target) {
            state.targets.data.push(target);
        },
        [EDIT_TARGET] (state, target) {
            var element = state.targets.data.find(function(element) {
                return element.id == target.id
            });

            element.hostname = target.hostname;
            element.ip = target.ip;
        }
        //#endregion
    },
    actions
});
