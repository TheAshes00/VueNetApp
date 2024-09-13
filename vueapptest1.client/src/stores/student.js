import { defineStore } from "pinia";
import students from "@/api/students";
import material from '@/api/material'
import loan from "@/api/loan";
export const useStudentStore = defineStore ('student', 
{
    state: () => ({
        objStudent: null,
        test: "test",
        objWeather: null,
        arrAcademicEntities: [],
        boolShowErrorUserAdded: false,
        boolErrorUserAdded: false,
        strUserMessage: "",
        boolUserNotFound: false,
        boolShowErrorNotFound: false,
        arrobjMaterialConfirmed: [],
        boolShowMaterialError: false,
        boolMaterialError: false,
        boolLoanError: false,
        boolShowLoanError: false,
        boolLoanCompleted: false,
        arrObjMaterialToReturn: [],
        boolReturnCompleted: false,
        boolReturnError: false,
        boolShowReturnError: false,
        boolWorkshopAttendanceCompleted: false,

    }),
    getters: {
        //------------------------------------------------------------------------------------
        getTest: (state) => state.test,

        //------------------------------------------------------------------------------------
        getMaterialConfirmationIds: (state) => 
            state.arrobjMaterialConfirmed.map(objMaterial => objMaterial.strNumContInt),

        //------------------------------------------------------------------------------------
        getStudentNumCta: (state) => state.objStudent.strNmCta,

        //------------------------------------------------------------------------------------
        getIntPkLoan: (state) => state.arrObjMaterialToReturn.map(obj => obj.intPkLoan),

        //------------------------------------------------------------------------------------

    },
    actions: {
        //------------------------------------------------------------------------------------
        async getAllAcademicEntities(
        ) {
            let objResponse = await students.getAllAcademicEntities();
            if(
                objResponse != []
            ) {
                this.arrAcademicEntities = objResponse
            }
        },
        //------------------------------------------------------------------------------------
        getUser(        
        ) {
            let objTest = {
                numCuenta: 1110038,
                strName: "Fernando Kin Agraz Carbajal",
                strUniversity: "Ingeniería",
                strCareer : "ICO"
            }

            this.objStudent = objTest;
        },

        //------------------------------------------------------------------------------------
        async setNewUser(
            objNewUser_I
        ) {
            let objResponse = await students.setNewStudent(objNewUser_I);
            
            if (
                objResponse.intStatus == 200
            )
            {
                this.boolErrorUserAdded = false;
                this.boolShowErrorUserAdded = false;
                this.strUserMessage = "";
            }
            else{
                this.boolErrorUserAdded = true;
                this.boolShowErrorUserAdded = true;
                this.strUserMessage = objResponse.strUserMessage;
            }
        },

        //------------------------------------------------------------------------------------
        setBoolShowErrorMessage(
            boolShowErrorUserAdded_I
        ) {
            this.strUserMessage = "";
            this.boolShowErrorUserAdded = boolShowErrorUserAdded_I
        },

        //------------------------------------------------------------------------------------
        async getWeather(
        ) {
            let objResponse = await students.testAxios();
            this.objWeather = objResponse;
        },

        //------------------------------------------------------------------------------------
        async getLoginConfirm(
            strNmCta_I
        ) {
            let objApiResponse = await students.getStudentLogin(strNmCta_I);
            if(
                objApiResponse.intStatus == 200
            ){
                this.boolUserNotFound = false;
                this.boolShowErrorNotFound = false;
                this.strUserMessage = ""
                this.objStudent = objApiResponse.objResponse
            }
            else
            {
                this.boolUserNotFound = true;
                this.boolShowErrorNotFound = true;
                this.objStudent = null;
                this.strUserMessage = objApiResponse.strUserMessage;
            }
        },

        //------------------------------------------------------------------------------------
        setBoolShowStudentErrorMessage(
            boolShowErrorUserNotFound_I
        ) {
            this.strUserMessage = "";
            this.boolShowErrorNotFound = boolShowErrorUserNotFound_I
        },

        //------------------------------------------------------------------------------------
        async getMaterialConfirmation(
            arrstrMaterial_I
        ) {
            let objApiResponse = await material.getMaterialConfirmation(arrstrMaterial_I);

            if (
                objApiResponse.intStatus == 200
            ) {
                this.strUserMessage = "";
                this.boolShowMaterialError = false;
                this.boolMaterialError = false
                this.arrobjMaterialConfirmed = objApiResponse.objResponse.arrObjMaterial;
            }
            else {
                this.strUserMessage = objApiResponse.strUserMessage;
                this.boolShowMaterialError = true;
                this.boolMaterialError = true;
            }
        },

        //------------------------------------------------------------------------------------
        setBoolShowMaterialErrorMessage(
            boolShowMaterialErrorMessage_I
        ) {
            this.strUserMessage = "";
            this.boolShowMaterialError = boolShowMaterialErrorMessage_I;
        },

        //------------------------------------------------------------------------------------
        async subRegisterMaterialLoan(
            objLoanConfirmation_I
        ){
            const objResponse = await loan.setMaterialLoan(objLoanConfirmation_I);    

            if(
                objResponse.intStatus == 200
            )
            {
                this.boolLoanCompleted = true;
                this.boolLoanError = false;
                this.boolShowLoanError = false;
                this.strUserMessage = "";
            }
            else
            {
                this.boolLoanCompleted = false;
                this.boolLoanError = true;
                this.boolShowLoanError = true;
                this.strUserMessage = objResponse.strUserMessage;
            }
        },

        //------------------------------------------------------------------------------------
        setBoolShowRegisterLoanErrorMessage(
            boolShowLoanError_I
        ) {
            this.boolShowLoanError = boolShowLoanError_I;
            this.strUserMessage = "";
        },

        //------------------------------------------------------------------------------------
        async subGetMaterialToReturn(
            strNmCta_I
        ) {
            let objResponse = await loan.arrGetMaterialToReturn(strNmCta_I);

            this.arrObjMaterialToReturn = objResponse;
        },

        //------------------------------------------------------------------------------------
        async subReturnMaterial(
            objMaterial_I
        ) {
            let objResponse = await loan.subReturnMaterial(objMaterial_I);

            if(
                objResponse
            ) {
                this.boolReturnCompleted = true
                this.boolReturnError = false
                this.boolShowReturnError = false
            }
            else
            {
                this.boolReturnCompleted = false
                this.boolReturnError = true
                this.boolShowReturnError = true
                this.strUserMessage = "An error ocurred while returning material"
            }
        },

        //------------------------------------------------------------------------------------
        setBoolShowReturnErrorMessage(
            boolShowReturnError_I
        ) {
            this.boolShowReturnError = boolShowReturnError_I
            this.strUserMessage = "";
        },

        //------------------------------------------------------------------------------------
        async subSetWorkshopAttendance(
            objWorkshopAttendance_I
        ) {
            let boolCompleted = await students.setWorkshopAttendance(objWorkshopAttendance_I);

            if(
                boolCompleted
            ) {
                this.boolWorkshopAttendanceCompleted = true;
            }
        },

        //------------------------------------------------------------------------------------
        
    },
} )