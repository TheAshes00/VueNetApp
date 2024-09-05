import admin from "@/api/admin";
import { defineStore } from "pinia";


export const  useAdminStore = defineStore('admin',{
    state: () => ({
        arrobjManagers: [],
    }),
    getters: {

    },
    actions: {
        //--------------------------------------------------------------------------------
        async subGetManagers(
            //
        ) {
            let objResponse = await admin.objGetManagers();
            this.arrobjManagers = objResponse;
        },
        
    }

})