<template>
    <div class="popup">
        <div class="popup-inner">
            <slot></slot>
            <div class="bottom-warning">
                <button class="warning-close" @click="closePopUp">
                    Close
                </button>
            </div>
        </div>
    </div>
</template>

<script>
import { useAdminStore } from '@/stores/admin';
import { useStudentStore } from '@/stores/student';

export default {
    props: ['strParent'],
    setup(){
        const studentStore = useStudentStore();
        const adminStore = useAdminStore();
        return { studentStore, adminStore };
    },
    data(){
        return{
            strParentComponet: this.strParent,
        }
    },
    methods:{
        //--------------------------------------------------------------------------------
        closePopUp(){
            if (
                this.strParentComponet === "Login"
            ) {
                this.studentStore.setBoolShowStudentErrorMessage(false);
            }
            else if (
                this.strParent === "LoginAdmin"
            ) {
                this.adminStore.subSetBoolShowAdminLoginError(false);
            }
            else if (
                this.strParent === "Activities"
            )
            {
                this.studentStore.setBoolShowMaterialErrorMessage(false);
            }
            else if(
                this.strParent === 'Activities-Register'
            ) {
                this.studentStore.setBoolShowRegisterLoanErrorMessage(false);
            }
            else if(
                this.strParent === 'Activities-Return'
            ) {
                this.studentStore.setBoolShowReturnErrorMessage(false);
            }
            else if (
                this.strParent === 'Workshop'
            ) {
                this.adminStore.subCloseWorkshopWarning(false)
            }
            else if (
                this.strParent === 'Tutor'
            ) {
                this.adminStore.subCloseTutorWarning(false)
            }
            else if (
                this.strParent === 'Material'
            ) {
                this.adminStore.subCloseMaterialWarning(false)
            }
            else if (
                this.strParent === 'TutorWorkshop'
            ) {
                this.adminStore.subCloseTutorWorkshopWarning(false)
            }
            else if (
                this.strParent === 'Report-Student'
            ){
                this.adminStore.subCloseStudentReportError(false)
            }
            else
            {
                this.studentStore.setBoolShowErrorMessage(false);
            }
        },

        //--------------------------------------------------------------------------------
    }
}
</script>


<style scoped>
.popup{
    position: fixed;
    top: 0;
    left: 0;
    right: 0;
    bottom: 0;
    z-index: 99;
    background-color: rgba(0, 0, 0, 0.2);

    display: flex;
    align-items: center;
    justify-content: space-evenly;

}

.popup-inner{
    background: #c97981f5;
    padding: 32px;
    height: 270px;
    width: 270px;
    border-top: 24px solid #be1e2dc4;
    border-radius: 8px;
    text-align: justify;
    color: lightgrey;
}

.bottom-warning{
    display: flex;
    justify-content: center;
    padding: 32px;
}

.warning-contact{
    height: 40px;
    width: 91px;
    left: 107px;
    top: 0px;
    border-radius: 8px;
    padding: 8px 16px 8px 16px;
    background-color: #59A2C8;
    border-color: #59A2C8;
    color: #F5F5F5;
    display: flex;
    align-items: center;
    justify-content: center;
}

.warning-close{
    height: 40px;
    width: 91px;
    border-radius: 8px;
    padding: 8px 16px 8px 16px;
    background-color: #c45861;
    border-color: #c45861;
    color: #F5F5F5;
    display: flex;
    align-items: center;
    justify-content: center;
}
</style>
