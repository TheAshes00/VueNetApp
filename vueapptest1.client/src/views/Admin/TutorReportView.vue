<script>
import PopUpWarning from '@/components/PopUpWarning.vue';
import { useAdminStore } from '@/stores/admin';

export default {
    setup(){
        const adminStore = useAdminStore();
        return { adminStore }
    },
    async created(){
        await this.adminStore.subGetAllTutor();
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
                this.objReportInfo.intPkTutor &&
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

                await this.adminStore.subGetTutorReport(objReportInfoCopy)

                this.boolSearch = true
            }
            
        },

        //--------------------------------------------------------------------------------
    }
}
</script>

<template>
    <PopUpWarning :strParent="'Report-Student'" 
        v-if="adminStore.boolTutorReportError && adminStore.boolShowTutorReportError"
    >
        {{ adminStore.strMessage }}
    </PopUpWarning>
    <h3 class="pad-8">
        Tutor Report
    </h3>
    <div class="">
        <form class="table-options" v-on:submit.prevent="subGetStudentReport">
            <div>
                <label for="select-tutor">Tutor:</label>
                <select name="select-tutor" id="select-tutor" v-model="objReportInfo.intPkTutor">
                    <option value="" disabled>Choose one...</option>
                    <option v-for="objTutor in adminStore.arrobjAllTutors" :key="objTutor.intPk" 
                        :value="objTutor.intPk"
                    >
                        {{ objTutor.strName + ' ' + objTutor.strSurename }}
                    </option>
                </select>
            </div>

            <div>
                <label for="date-start">From:</label>
                <input type="date"  id="date-start" name="date-start" v-model="objReportInfo.dateStart">
            </div>
            
            <div>
                <label for="date-finish">To:</label>
                <input type="date" id="date-finish" name="date-finish" v-model="objReportInfo.dateEnd">
            </div>

            <div>
                <button type="submit" id="bt_add" class="border-button button-align"> 
                    Get tutor report
                    <span class="">
                        <svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" class="bi bi-search" viewBox="0 0 16 16">
                            <path d="M11.742 10.344a6.5 6.5 0 1 0-1.397 1.398h-.001q.044.06.098.115l3.85 3.85a1 1 0 0 0 1.415-1.414l-3.85-3.85a1 1 0 0 0-.115-.1zM12 6.5a5.5 5.5 0 1 1-11 0 5.5 5.5 0 0 1 11 0"/>
                        </svg>
                    </span>
                </button>
            </div>
        </form>
    </div>
    <h6 class="center-box pad-8 mt-3" v-if="boolSearch">
        Workshop Report
    </h6>
    <!-- <div class="" v-if="boolSearch && adminStore.objTutorReport != null && adminStore.objTutorReport.length > 0">
        <div class="container-fluid grid-material" id="material-list">
            <div class="row row-color">
                <div class="col-sm-3 col-md-3">
                    Workshop
                </div>
                <div class="col-sm-3 col-md-3">
                    Total Sessions
                </div>
                <div class="col-sm-3 col-md-3">
                    Total Attendance
                </div>
            </div>
            <div class="row pad-8 align-items-center" 
                v-for="(objTutorReport,index) in adminStore.objTutorReport"
                :key="objTutorReport.strWorkshop+index" 
            >
                <div class="col-sm-3 col-md-3">
                    {{objTutorReport.strWorkshop}}
                </div>
                <div class="col-sm-3 col-md-3">
                    {{objTutorReport.intTotalSessions}}
                </div>
                <div class="col-sm-3 col-md-3">
                    {{ objTutorReport.intTotalAttendance }}
                </div>
            </div>
        </div>
    </div> -->
    <div class="" id="report-list" v-if="boolSearch && adminStore.objTutorReport != null && adminStore.objTutorReport.length > 0">
        <div class="table-responsive-sm">
            <table class="table align-middle table-color">
                <thead>
                    <tr>
                        <th>
                            Workshop
                        </th>
                        <th>
                            Total Sessions
                        </th>
                        <th>
                            Total Attendance
                        </th>
                    </tr>
                </thead>
                <tbody class="tbody-color">
                    <tr v-for="(objTutorReport,index) in adminStore.objTutorReport"
                        :key="objTutorReport.strWorkshop+index" 
                    >
                        <td>
                            {{objTutorReport.strWorkshop}}
                        </td>
                        <td>
                            {{objTutorReport.intTotalSessions}}
                        </td>
                        <td>
                            {{ objTutorReport.intTotalAttendance }}
                        </td>
                    
                    </tr>
                </tbody>
            </table>
        </div>
    </div>
    <div v-else-if="boolSearch && adminStore.objTutorReport != null && adminStore.objTutorReport.length == 0">
        No workshop attendance found in the specified period of time
    </div>
</template>

<style scoped>
.container-fluid{
    border: 1px white solid;
    border-radius: 20px;
    color: whitesmoke;
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

.table-color{
    color: #c7ffc2;
}
.thead-color {
    color: #c7ffc2;
}

.tbody-color{
    color: whitesmoke;
}

#report-list {
    overflow-y: auto;
    max-height: 150px;
    overflow-x: hidden;
    padding-bottom: 8px;
}
</style>