import { defineStore } from "pinia";
import students from "@/api/students";
export const useStudentStore = defineStore ('student', 
{
    state: () => ({
        objStudent: null,
        test: "test",
        objWeather: null,
        arrAcademicEntities: [],
    }),
    getters: {
        getTest: (state) => state.test,
    },
    actions: {
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
        async getWeather(
        ) {
            let objResponse = await students.testAxios();
            this.objWeather = objResponse;
        },

        //------------------------------------------------------------------------------------
        async getAllAcademicEntities(
        ) {
            let objResponse = await students.getAllAcademicEntities();

            if(
                objResponse != []
            ) {
                this.arrAcademicEntities = objResponse
            }
        }

        //------------------------------------------------------------------------------------
    },
} )