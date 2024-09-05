<script>
import { useAdminStore } from '@/stores/admin';
import { useStudentStore } from '@/stores/student';
import LoaderComponent from './LoaderComponent.vue';

export default{
    props:["strParent"],
    setup(){
        const adminStore = useAdminStore();
        const studentStore = useStudentStore();
        return { adminStore, studentStore }
    },
    async created(){
        await this.adminStore.subGetManagers();
    },
    data(){
        return{
            //
            intPk: "",
            boolIsLoading: false
        }
    },
    components:{
        LoaderComponent,
    },
    methods: {
        //--------------------------------------------------------------------------------
        async subConfirmLoan(){
            this.boolIsLoading = true;

            if(
                this.intPk
            ) {
                this.subSetAction()
            }

            this.boolIsLoading = false;
        },

        //--------------------------------------------------------------------------------
        async subSetAction(){
            if(
                this.strParent === "Return"
            ) {
                let objReturnConfirmation = {
                    arrstrNumContInt : this.studentStore.getMaterialConfirmationIds,
                    strNmCta: this.studentStore.getStudentNumCta,
                    intAdminPk: this.intPk,
                    arrintPKLoan: this.studentStore.getIntPkLoan
                }

                await this.studentStore.subReturnMaterial(objReturnConfirmation)
            }
            else
            {

                let objLoanConfirmation = {
                    arrstrNumContInt : this.studentStore.getMaterialConfirmationIds,
                    strNmCta: this.studentStore.getStudentNumCta,
                    intAdminPk: this.intPk
                }

                await this.studentStore.subRegisterMaterialLoan(objLoanConfirmation);
            }
        }

        //--------------------------------------------------------------------------------
    },
}
</script>

<template>
    <div class="select-container">
        <label for="manager">Shift Manager: </label>
        <select name="manager" id="manager" v-model="intPk">
            <option value="" disabled>Choose one...</option>
            <option v-for="(admin) in adminStore.arrobjManagers" :key="admin.intPk" 
            :value="admin.intPk">
            {{ admin.strName }}
            </option>
        </select>
    </div>

    <div>
        <div v-if="boolIsLoading">
            <LoaderComponent  />
        </div>
        <button type="button" class="border-button button-align" v-on:click="subConfirmLoan" v-else>
            {{ strParent==="Return" ? 'Confirm Return' : 'Confirm Loan' }}
            <span>
                <svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" class="bi bi-check-circle" viewBox="0 0 16 16">
                    <path d="M8 15A7 7 0 1 1 8 1a7 7 0 0 1 0 14m0 1A8 8 0 1 0 8 0a8 8 0 0 0 0 16"/>
                    <path d="m10.97 4.97-.02.022-3.473 4.425-2.093-2.094a.75.75 0 0 0-1.06 1.06L6.97 11.03a.75.75 0 0 0 1.079-.02l3.992-4.99a.75.75 0 0 0-1.071-1.05"/>
                </svg>
            </span>
        </button>
    </div>
</template>


<style scoped>
.select-container{
    width: 50%;
    padding: 16px;
}

select option, optgroup{
  background: whitesmoke;
  color: #597e56;
}
</style>