<script>
import PopUpWarning from '@/components/PopUpWarning.vue';
import { useAdminStore } from '@/stores/admin';

export default {
    setup(){
        const adminStore = useAdminStore();
        return { adminStore }
    },
    data(){
        return {
            objReportInfo: {
                intPkTutor: "",
                dateStart: "",
                dateEnd: ""
            },
            boolSearch: false
        }
    },
    components: {
        PopUpWarning,
    },
    methods: {
        //--------------------------------------------------------------------------------
        async subGetStudentReport(){
            if(
                this.objReportInfo.strNmCta &&
                this.objReportInfo.dateStart
            )
            {
                let objReportInfoCopy = this.objReportInfo;

                let inputDate = new Date(this.objReportInfo.dateStart);
                let formattedStartDate = inputDate.toISOString().split('T')[0];
                objReportInfoCopy.strDate = formattedStartDate;

                if(
                    objReportInfoCopy.dateEnd
                ){
                    let inputDate = new Date(objReportInfoCopy.dateEnd);
                    let formattedEndDate = inputDate.toISOString().split('T')[0];
                    objReportInfoCopy.dateEnd = formattedEndDate;
                }

                await this.adminStore.subGetStudentReport(objReportInfoCopy)

                this.boolSearch = true
            }
            
        },

        //--------------------------------------------------------------------------------
    }
}
</script>

<template>
    <PopUpWarning :strParent="'Report-Student'" 
        v-if="adminStore.boolStudentReportError && adminStore.boolShowStudentReportError"
    >
        {{ adminStore.strMessage }}
    </PopUpWarning>
    <h3 class="pad-8">
        Student Report
    </h3>
    <div class="">
        <form class="table-options" v-on:submit.prevent="subGetStudentReport">
            <div>
                <label for="student-id">Identification #:</label>
                <input type="text" id="student-id" name="student-id" v-model="objReportInfo.strNmCta" required>
            </div>

            <div>
                <label for="date-start">From:</label>
                <input type="date"  id="date-start" name="date-start" v-model="objReportInfo.dateStart" required>
            </div>
            
            <div>
                <label for="date-finish">To:</label>
                <input type="date" id="date-finish" name="date-finish" v-model="objReportInfo.dateEnd">
            </div>

            <div>
                <button type="submit" id="bt_add" class="border-button button-align"> 
                    Get student report
                    <span class="">
                        <svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" class="bi bi-search" viewBox="0 0 16 16">
                            <path d="M11.742 10.344a6.5 6.5 0 1 0-1.397 1.398h-.001q.044.06.098.115l3.85 3.85a1 1 0 0 0 1.415-1.414l-3.85-3.85a1 1 0 0 0-.115-.1zM12 6.5a5.5 5.5 0 1 1-11 0 5.5 5.5 0 0 1 11 0"/>
                        </svg>
                    </span>
                </button>
            </div>
        </form>
    </div>
    
    <div class="student-info" v-if="adminStore.objStudentReport != null">
        <div>
            <label for="student-name">Student</label>
            <input type="text" id="student-name" name="student-name" 
                :value="adminStore.objStudentReport.strStudentName" 
                disabled>
        </div>

        <div>
            <label for="student-course">Course</label>
            <input type="text" id="student-course" name="student-course" 
            :value="adminStore.objStudentReport.strCourse" 
            disabled>
        </div>
    </div>

    <div class="tables-container">
        <div>
            <h6 class="center-box" v-if="boolSearch">
                Material Loan Report
            </h6>

            <div>
                <div class=""  v-if="boolSearch && 
                    adminStore.objStudentReport != null && adminStore.objStudentReport.arrLoanReport != null"
                >
                    <div class="container-fluid grid-material" id="material-list">
                        <div class="row row-color">
                            <div class="col-sm-4 col-md-4">
                                Date
                            </div>
                            <div class="col-sm-2 col-md-2">
                                Hour
                            </div>
                            <div class="col-sm-6 col-md-6">
                                Material
                            </div>
                        </div>
                        <div class="row pad-8 align-items-center" 
                            v-for="(objLoanReport,index) in adminStore.objStudentReport.arrLoanReport" 
                            :key="objLoanReport.strDate+index+objLoanReport.strHour"
                        >
                            <div class="col-sm-4 col-md-4">
                                {{ objLoanReport.strDate }}
                            </div>
                            <div class="col-sm-2 col-md-2">
                                {{ objLoanReport.strHour }}
                            </div>
                            <div class="col-sm-6 col-md-6">
                                <div class="row" v-for="(strMaterial,index) in objLoanReport.arrstrMaterial" 
                                    :key="strMaterial+index">
                                    {{ strMaterial }}
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div v-else-if="boolSearch && adminStore.objStudentReport.arrLoanReport == null">
                    No material loans found in the specified period of time
                </div>
            </div>
        </div>

        <div>
            <h6 class="center-box" v-if="boolSearch">
                Workshop Attendance Report
            </h6>
            <div class="" v-if="boolSearch && adminStore.objStudentReport != null && adminStore.objStudentReport.arrWorkshopReport != null">
                <div class="container-fluid grid-material" id="material-list">
                    <div class="row row-color">
                        <div class="col-sm-4 col-md-4">
                            Date
                        </div>
                        <div class="col-sm-2 col-md-2">
                            Hour
                        </div>
                        <div class="col-sm-3 col-md-3">
                            Workshop Name
                        </div>
                        <div class="col-sm-3 col-md-3">
                            Tutor Name 
                        </div>
                    </div>
                    <div class="row pad-8 align-items-center" 
                        v-for="(objWorkshopReport,index) in adminStore.objStudentReport.arrWorkshopReport"
                        :key="objWorkshopReport.strDate+index+objWorkshopReport.strHour" 
                    >
                        <div class="col-sm-4 col-md-4">
                            {{objWorkshopReport.strDate}}
                        </div>
                        <div class="col-sm-2 col-md-2">
                            {{objWorkshopReport.strHour}}
                        </div>
                        <div class="col-sm-3 col-md-3">
                            {{ objWorkshopReport.strWorkshopName }}
                        </div>
                        <div class="col-sm-3 col-md-3">
                            {{ objWorkshopReport.strTutorName }}
                        </div>
                    </div>
                </div>
            </div>
            <div v-else-if="boolSearch && adminStore.objStudentReport?.arrWorkshopReport == null">
                No workshop attendance found in the specified period of time
            </div>
        </div>
    </div>

</template>

<style scoped>
.row{
    padding: 4px 12px;
}
.tables-container{
    display: flex;
    justify-content: space-around;
    padding-top: 12px;
    margin-top: 12px;
}
.container-fluid{
    border: 1px white solid;
    border-radius: 10px;
    color: whitesmoke;
    width: 100%;
}
.table-options{
    display: flex;
    justify-content: space-between;
    align-items: flex-end;
}
.letter-space{
    letter-spacing:  2px;
}
.gap-actions-buttons{
    gap: 10px;
}
.center-box {
  display: flex;
  align-items: center;
  justify-content: center;
}

.row-color{
    color: #c7ffc2;
}

.student-info{
    display: flex;
    gap: 14px;
}
</style>