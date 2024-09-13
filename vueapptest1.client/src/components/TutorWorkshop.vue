<script>
import { useAdminStore } from '@/stores/admin';
import LoaderComponent from './LoaderComponent.vue';
import { useStudentStore } from '@/stores/student';

            
export default {
    setup(){
        const adminStore = useAdminStore();
        const studentStore = useStudentStore();
        return { adminStore, studentStore }
    },
    data(){
        return {
            boolIsLoading: false,
            objSelectedInfo : {
                intPkTutorWorkshop: "",
                intPkTutor : "",
                intPkWorkshop : ""
            },
             arrobjFilteredWorkshopTutor:[],
             boolReady: false,
             strMessage: "",
             boolWorkshopAttendance : false
        }
    },
    created(){
        this.adminStore.subGetAllActiveWorkshops();
    },
    components: {
        LoaderComponent
    },
    methods:{
        //--------------------------------------------------------------------------------
        async subGetFilteredTutors(){
            this.boolIsLoading = true;
            
            await this.adminStore.subGetFilteredTutor(this.objSelectedInfo.intPkWorkshop)
            this.arrobjFilteredWorkshopTutor = this.adminStore.arrobjTutor;

            this.boolIsLoading = false;
        },

        //--------------------------------------------------------------------------------
        async subSetWorkshopAttendance(
            //            
        ) {
            this.boolWorkshopAttendance = true;

            let objWorkshopAttendance = {
                intPkTutorWorkshop : this.objSelectedInfo.intPkTutorWorkshop,
                strNmCta: this.studentStore.getStudentNumCta,
            }

            console.log(objWorkshopAttendance)
            await this.studentStore.subSetWorkshopAttendance(objWorkshopAttendance)

            this.boolWorkshopAttendance = false;
        },

        //--------------------------------------------------------------------------------
    }
}
</script>
<template>
    <div>
        <div class="table-button-container">
            <div class="container-fluid grid-material" id="material-list">
                <div class="row">
                    <div class="col-sm-6 col-md-6">
                        Workshop
                    </div>
                    <div class="col-sm-6 col-md-6">
                        Tutor
                    </div>
                </div>
                <div class="row pad-8 align-items-center" >
                    <div class="col-sm-6 col-md-6">
                        <label for="select-workshop">
                            Workshops
                        </label>
                        <select name="select-workshop" id="select-workshop" v-model="objSelectedInfo.intPkWorkshop"
                        v-on:change="subGetFilteredTutors">
                            <option value="" disabled>Choose one...</option>
                            <option v-for="workshop in adminStore.arrobjWorkshops" :key="workshop.intPk" 
                                :value="workshop.intPk"
                            >
                                {{ workshop.strWorkshop }}
                            </option> 
                        </select>
                    </div>
                    <div class="col-sm-6 col-md-6">
                        <div v-if="boolIsLoading">
                            <LoaderComponent/>
                        </div>
                        <div v-else>
                            <div>
                                <label for="select-tutor">
                                    Available Tutor
                                </label>
                                <select name="select-tutor" id="select-tutor" 
                                    v-model="objSelectedInfo.intPkTutorWorkshop" 
                                    v-if="adminStore.strMessage == ''">
                                    <option value="" disabled >Choose one...</option>
                                        <option v-for="tutor in arrobjFilteredWorkshopTutor" :key="tutor.intPkTutorWorkshop" 
                                        :value="tutor.intPkTutorWorkshop "
                                    >
                                        {{ tutor.strFullName + ' - ' + tutor.strFormatHour }}
                                    </option> 
                                </select>
                                <div v-else>
                                    {{ adminStore.strMessage }}
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="button-bottom">
            <div v-if="boolWorkshopAttendance">
                <LoaderComponent/>
            </div>
            <button type="button" class="border-button button-align" v-on:click="subSetWorkshopAttendance" 
                :disabled="!(objSelectedInfo.intPkWorkshop && objSelectedInfo.intPkTutorWorkshop)" v-else>
                Register Attendance
            </button>
        </div>
    </div>
</template>

<style scoped>
.table-button-container{
    display: flex;
    flex-direction: column;
    align-items: center;
    justify-content: center;
    padding: 0px 12px;
}

.grid-material{
    width: 60%;
    padding: 8px;
}

.border-button{
    border: 1px solid #c7ffc2;
}

.button-bottom{
    display: flex;
    justify-content: center;
    gap: 14px;
}

</style>