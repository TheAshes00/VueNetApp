<script>
import { useStudentStore } from '@/stores/student';
import ShiftManager from './ShiftManager.vue';

export default {
    setup(){
        const studentStore = useStudentStore();
        return { studentStore }
    },
    data(){
        return {
            boolIsLoading:false,
            boolConfirmReturn: false,
            arrObjMaterialToReturn: []
        }
    },
    async created(){
        await this.studentStore.subGetMaterialToReturn(this.studentStore.getStudentNumCta);

    },
    components:{
        ShiftManager,
    },
    methods:{
        //--------------------------------------------------------------------------------
        async subReturnMaterial(
            //
        ){
            this.boolConfirmReturn = true;
        },

        //--------------------------------------------------------------------------------
        subMapArrObjMaterialToReturn(){
            this.arrObjMaterialToReturn = this.studentStore.arrObjMaterialToReturn.map(
                ojbLoan => ojbLoan.arrobjMaterial
            );
        }

        //--------------------------------------------------------------------------------
    }
}
</script>

<template>
    <div v-if="!boolConfirmReturn">
        <div v-if="studentStore.arrObjMaterialToReturn.length == 0">
            <div class="table-button-container">
                No material to return
            </div>
        </div>
        
        <div v-else>
            <div>
                <h4>
                    Loan information:
                </h4>
            </div>
            <div class="table-button-container">
                <div class="container-fluid grid-material" id="material-list">
                    <div class="row">
                        <div class="col-sm-4 col-md-4">
                            Loan Date
                        </div>
                        <div class="col-sm-4 col-md-4">
                            Material Code
                        </div>
                        <div class="col-sm-4 col-md-4">
                            Material Name
                        </div>
                    </div>
                    <div class="row pad-8 align-items-center" 
                        v-for="objMaterial in studentStore.arrObjMaterialToReturn" :key="objMaterial.intPkLoan" 
                    >
                        <div class="col-sm-4 col-md-4">
                            <input type="text" name="loan-date" id="loan-date" 
                                :value="objMaterial.strLoanDate" disabled>
                        </div>
                        <div class="col-sm-8 col-md-8">
                            <div class="row" v-for="material in objMaterial.arrobjMaterial" 
                                :key="material.strPkMaterial">
                                <div class="col-sm-6 col-md-6">
                                    <input type="text" class="shadow-box" name="material-id" id="material-id" 
                                        :value="material.strPkMaterial" 
                                        disabled
                                        >
                                </div>

                                <div class="col-sm-6 col-md-6">
                                    <input type="text" class="shadow-box" name="material-name" id="material-name" 
                                        v-bind:value="material.strMaterialName" disabled
                                    >
                                </div>
                            </div>
                        </div>
                        
                    </div>
                </div>
            </div>
            <div class="button-bottom">
                <button type="button" class="border-button button-align" 
                    v-on:click="subReturnMaterial">
                    Return material
                </button>
            </div>
        </div>
    </div>

    <div class="shift-manager-container" v-else>
        <ShiftManager :strParent="'Return'" />
    </div>
</template>


<style style>
.shift-manager-container{
    display: flex;
    flex-direction: column;
    align-items: center;
}

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

/* width */
::-webkit-scrollbar {
    width: 8px;
    border-radius: 3px;
}

/* Handle on hover */
::-webkit-scrollbar-thumb:hover {
    background: #cdcdcd;
}

#material-list {
    overflow-y: auto;
    max-height: 150px;
    overflow-x: hidden;
    padding-bottom: 8px;
}
</style>