<template>
    <div>
        <button class="button" v-on:click="saveStudent">
            Guardar
        </button>
    </div>

    <div>
        <div v-if="studentStore.objStudent === null">
            Esperando nombre...
        </div>

        <div v-else>
            El nombre es {{ studentStore.objStudent.strName }}
        </div>
    </div>
</template>


<script>
import { useStudentStore } from '@/stores/student';
export default {
    setup(){
        const studentStore = useStudentStore()
        return {studentStore}
    },
    data(){
        return {

        }
    },
    methods : {
        //------------------------------------------------------------
        saveStudent(
        ){
            this.studentStore.getUser();
            console.log(this.studentStore.getTest)
        },

        //------------------------------------------------------------
    },
}
</script>