<template>
    <div class="popup">
        <div class="popup-inner">
            <slot></slot>
            <div class="bottom-succes">
                <button class="success-close" @click="closePopUp">
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
    props:['strComponent'],
    setup(){
        const studentStore = useStudentStore();
        const adminStore = useAdminStore();
        return { studentStore, adminStore };
    },
    data(){
        return {
            strUsedByComponent: this.strComponent,
        }
    },
    methods:{
        //--------------------------------------------------------------------------------
        closePopUp(){
            if(
                this.strUsedByComponent === 'Register'
            ) {
                this.$router.push('/')
            }
            else if (
                this.strUsedByComponent === 'Activities'
            ) {
                this.studentStore.$reset(); 
                this.adminStore.$reset();
                this.$router.push('/')
            }
            else if(
                this.strUsedByComponent === 'Workshop'
            ){
                this.adminStore.subCloseSucces(false);
            }
            else if(
                this.strUsedByComponent === 'Tutor'
            ){
                this.adminStore.subCloseTutorSuccess(false);
            }
            else if(
                this.strUsedByComponent === 'Material'
            ){
                this.adminStore.subCloseMaterialSuccess(false);
            }
            else if (
                this.strUsedByComponent === 'TutorWorkshop'
            ){
                this.adminStore.subCloseTutorWorkshopSuccess(false)
            }
            else if (
                this.strUsedByComponent == 'Activities-Workshop'
            ){
                this.studentStore.$reset(); 
                this.adminStore.$reset();
                this.$router.push('/')
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
    background: #f2fff2;
    padding: 32px;
    height: 210px;
    width: 270px;
    border-top: 27px solid #d6f5d6;
    border-radius: 8px;
    text-align: justify;
    color: #a2a6a2;
}

.bottom-succes{
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

.success-close{
    height: 40px;
    width: 91px;
    border-radius: 8px;
    padding: 8px 16px 8px 16px;
    background-color: #e4fae4;
    border: 1px solid #6cc0c0;
    display: flex;
    align-items: center;
    justify-content: center;
    color: #a2a6a2;
    font-weight: 600;
}
</style>
