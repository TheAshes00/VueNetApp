<script setup> 
import LoaderComponent from '@/components/LoaderComponent.vue';
import HeaderComponent from '@/components/HeaderComponent.vue';
</script>

<template>
  <HeaderComponent/>
  <div>
    <h2>Registro de usuario</h2>
    <div class="btn-group">
      <h3>Selecciona uno:</h3>
      <label for="alu">Alumno</label>
      <input type="radio" name="alu" id="alu" value="student" v-model="strUserSelected">

      <label for="doc">Docente</label>
      <input type="radio" name="doc" id="doc" value="teacher" v-model="strUserSelected">
    </div>

    <div v-if="strUserSelected">
      <form>
        <div class="name-form">
          <label for="name-new">Nombre:</label>
          <input type="text" name="name-new" id="name-new" placeholder="Ex: Molly">

          <label for="surename-new">Apellido:</label>
          <input type="text" name="surename-new" id="surename-new" placeholder="Ex: Evans">
        </div>
        
        <div>
          <label for="nmcta-new"> 
            {{  strStudentTextInput }}
          </label>
          <input type="text" name="nmcta-new" id="nmcta-new" placeholder="Ex: 1110038">

          <label for="orgacd-new">Organismo académico:</label>
          <select name="orgacd" id="orgacd">
            
          </select>
        </div>
        
      </form>
    </div>
    <LoaderComponent/>
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
    return{
      strUserSelected: "",
      strTextInput : (this.strUserSelected === "student" ?"Número de cuenta:" : "Número de empleado"),
      strteacherTextInput : "",
    }
  },
  methods: {
    //------------------------------------------------------------------------------------
    goToAbout() {
      this.$router.push('/register')
    },

    //------------------------------------------------------------------------------------
  },
  async created(){
    await this.studentStore.getAllAcademicEntities();
  },
}
</script>