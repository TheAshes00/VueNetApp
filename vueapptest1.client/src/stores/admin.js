import admin from "@/api/admin";
import material from "@/api/material";
import tutor from "@/api/tutor";
import { defineStore } from "pinia";


export const  useAdminStore = defineStore('admin',{
    state: () => ({
        arrobjManagers: [],
        strAdminUsername: "",
        strUserName: "",
        boolAdminLoginError: false,
        boolShowAdminLoginError: false,
        objPaginateWorkshops: null,
        boolWorkshopError: false,
        boolShowWorkshopError: false,
        boolWorkshopCompleted: false,
        objPaginateTutors: null,
        boolTutorError: false,
        boolShowTutorError: false,
        boolTutorCompleted: false,
        objPaginateMaterial: null,
        boolMaterialError: false,
        boolShowMaterialError: false,
        boolMaterialCompleted: false,

    }),
    getters: {
        //--------------------------------------------------------------------------------
        objGetObjPaginateWorkshops: (state) => state.objPaginateWorkshops,

        //--------------------------------------------------------------------------------
        objGetObjPaginateTutors: (state) => state.objPaginateTutors,

        //--------------------------------------------------------------------------------
        objGetObjPaginateMaterial: (state) => state.objPaginateMaterial 

        //--------------------------------------------------------------------------------

    },
    actions: {
        //--------------------------------------------------------------------------------
        async subGetManagers(
            //
        ) {
            let objResponse = await admin.objGetManagers();
            this.arrobjManagers = objResponse;
        },

        //--------------------------------------------------------------------------------
        async subLoginAdmin(
            objAdmin_I
        ) {
            let objResponse = await admin.subLoginAdmin(objAdmin_I);

            if(
                objResponse.intStatus == 200
            ) {
                this.strAdminUsername = objAdmin_I.strUsername
                this.boolAdminLoginError = false;
                this.boolShowAdminLoginError = false;
                this.strUserName = "";
                sessionStorage.setItem('strToken', objResponse.token)
            }
            else
            {
                this.boolAdminLoginError = true;
                this.boolShowAdminLoginError = true;
                this.strUserName = objResponse.strUserMessage;
                sessionStorage.clear();
            }
        },

        //--------------------------------------------------------------------------------
        subSetBoolShowAdminLoginError(
            boolShowAdminLoginError_I
        ) {
            this.boolShowAdminLoginError = boolShowAdminLoginError_I;
            this.strAdminUsername = "";
        },

        //--------------------------------------------------------------------------------
        async subGetAllWorkshops(
            objPages_I
        ) {
            let objResponse = await admin.subGetPaginatedWorshops(objPages_I);
            this.objPaginateWorkshops = objResponse;

        },

        //--------------------------------------------------------------------------------
        async subSetAddWorkshop(
            objPages_I
        ) {
            this.boolWorkshopCompleted = false
            let objResponse = await admin.subSetWorkshop(objPages_I);
            if(
                objResponse.intStatus == 200
            ) {
                this.boolWorkshopError = false
                this.boolShowWorkshopError = false
                this.strUserName = "";
                this.boolWorkshopCompleted = true;
            }
            else
            {
                this.boolWorkshopError = true
                this.boolShowWorkshopError = true
                this.strUserName = objResponse.strUserMessage;
            }
            
            

        },

        //--------------------------------------------------------------------------------
        async subCloseWorkshopWarning(
            boolShowWorkshopError_I
        ) {
            this.boolShowWorkshopError = boolShowWorkshopError_I;
        },

        //--------------------------------------------------------------------------------
        async subCloseSucces(
            boolWorkshopCompleted_I
        ) {
            this.boolWorkshopCompleted = boolWorkshopCompleted_I;
        },

        //--------------------------------------------------------------------------------
        async subGetAllTutors(
            objPages_I
        ) {
            let objResponse = await tutor.objGetPaginatedTutors(objPages_I);
            this.objPaginateTutors = objResponse;

        },

        //--------------------------------------------------------------------------------
        async subSetAddTutor(
            objPages_I
        ) {
            this.boolTutorCompleted = false

            let objResponse = await tutor.subSetTutor(objPages_I);
            if(
                objResponse.intStatus == 200
            ) {
                this.boolTutorError = false
                this.boolShowTutorError = false
                this.strUserName = "";
                this.boolTutorCompleted = true;
            }
            else
            {
                this.boolTutorError = true
                this.boolShowTutorError = true
                this.strUserName = objResponse.strUserMessage;
            }

        },

        //--------------------------------------------------------------------------------
        async subCloseTutorWarning(
            boolShowTutorError_I
        ) {
            this.boolShowTutorError = boolShowTutorError_I;
        },

        //--------------------------------------------------------------------------------
        async subCloseTutorSuccess(
            boolTutorCompleted_I
        ) {
            this.boolTutorCompleted = boolTutorCompleted_I;
        },

        //--------------------------------------------------------------------------------
        async subGetAllPaginatedMaterial(
            objPages_I
        ) {
            let objResponse = await material.subGetPaginatedMaterial(objPages_I);
            this.objPaginateMaterial = objResponse;

        },

        //--------------------------------------------------------------------------------
        async subSetAddMaterial(
            objPages_I
        ) {
            this.boolMaterialCompleted = false

            let objResponse = await material.subSetMaterial(objPages_I);
            if(
                objResponse.intStatus == 200
            ) {
                this.boolMaterialError = false
                this.boolShowMaterialError = false
                this.strUserName = "";
                this.boolMaterialCompleted = true;
            }
            else
            {
                this.boolMaterialError = true
                this.boolShowMaterialError = true
                this.strUserName = objResponse.strUserMessage;
            }

        },

        //--------------------------------------------------------------------------------
        async subCloseMaterialWarning(
            boolShowMaterialError_I
        ) {
            this.boolShowMaterialError = boolShowMaterialError_I;
        },

        //--------------------------------------------------------------------------------
        async subCloseMaterialSuccess(
            boolMaterialCompleted_I
        ) {
            this.boolMaterialCompleted = boolMaterialCompleted_I;
        },

        //--------------------------------------------------------------------------------
    }

})