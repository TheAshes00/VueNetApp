import { defineStore } from "pinia";
import students from "@/api/students";
export const useStudentStore = defineStore ('student', 
{
    state: () => ({
        objStudent: null,
        test: "test",
        objWeather: null,
        arrAcademicEntities: [],
        boolShowErrorUserAdded: false,
        boolErrorUserAdded: false,
        strUserMessage: ""
    }),
    getters: {
        getTest: (state) => state.test,
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
            this.boolShowErrorUserAdded = boolShowErrorUserAdded_I
        },

        //------------------------------------------------------------------------------------
        async getWeather(
        ) {
            let objResponse = await students.testAxios();
            this.objWeather = objResponse;
        },

        //------------------------------------------------------------------------------------
    },
} )