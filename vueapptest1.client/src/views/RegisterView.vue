<template>
  <!-- Header component -->
  <HeaderComponent/>

  <!-- Popup warning component, using slot to display corresponing error message -->
  <PopUpWarning v-if="studentStore.boolShowErrorUserAdded && studentStore.boolErrorUserAdded">
    {{ studentStore.strUserMessage }}
  </PopUpWarning>

  <!-- Popup success component, using slot to display corresponing completed message -->
  <PopupSuccess v-if="boolCompleted" :strComponent="'Register'">
    User registration completed succesfully
  </PopupSuccess>
  <div>
    <h2>Registro de usuario</h2>
    <div class="">
      <h3 class="p-ub">Selecciona uno:</h3>
      <div class="register-options">
        <div class="form-check">
          <input class="form-check-input" type="radio" name="flexRadioDefault" id="flexRadioDefault1" 
            value="student" v-model="strUserSelected">
          <label class="form-check-label" for="flexRadioDefault1">
            Alumno
          </label>
        </div>
        <div class="form-check">
          <input class="form-check-input" type="radio" name="flexRadioDefault" id="flexRadioDefault2" 
            value="teacher" v-model="strUserSelected">
          <label class="form-check-label" for="flexRadioDefault2">
            Docente
          </label>
        </div>
      </div>
    </div>

    <div v-if="strUserSelected">
      <form v-on:submit.prevent="subSetNewUser">
        <div class="form-half">
          <div class="top">
            <label class="label-text" for="name-new">Nombre:</label>
            <input type="text" name="name-new" id="name-new" placeholder="Ex: Molly" 
              v-model="objNewUser.strName" 
              required>

            <label for="surename-new">Apellido:</label>
            <input class="label-text" type="text" name="surename-new" id="surename-new" 
              placeholder="Ex: Evans" v-model="objNewUser.strSurename" 
              required>

          </div>

          <div class="middle">
            <label class="label-text" for="nmcta-new"> 
              {{  (this.strUserSelected === "student" ? "Número de cuenta:" : "Número de empleado") }}
            </label>
            <input type="text" name="nmcta-new" id="nmcta-new" placeholder="Ex: 1110038" 
              maxlength="7" v-model="objNewUser.strNmCta" v-if="this.strUserSelected === 'student'" 
              required>

            <input class="label-text" type="text" name="nmcta-new" id="nmcta-new" 
              placeholder="Ex: 11138" maxlength="5" v-model="objNewUser.strNmCta" required v-else>

            <label class="label-text" for="orgacd-new">Organismo académico:</label>
            <select name="orgacd-new" id="orgacd-new" v-if="studentStore.arrAcademicEntities != []" 
              v-model="objNewUser.intPkAcademy" required>
              <option value="" disabled>Seleccione una:</option>
              <optgroup v-for="objAcademyType in studentStore.arrAcademicEntities" 
                :key="objAcademyType.strGroupName" :label="objAcademyType.strGroupName"
              >
                <option v-for="objAcademy in objAcademyType.darrAcademies" :key="objAcademy.intPk" 
                  :value="objAcademy.intPk"
                >
                {{ objAcademy.strAcademyName }}
                </option>
              </optgroup>
            </select>
          </div>

          <div class="bottom" v-if="objNewUser.intPkAcademy">  
            <div class="register-options">
              <div class="form-check">
                <input class="form-check-input" type="radio" id="licenciatura" name="licenciatura" value="lic" 
                v-model="strBachelorDegree">
                <label class="form-check-label" for="licenciatura">Licenciatura</label><br>
              </div>
              <div class="form-check">
                <input class="form-check-input" type="radio" id="maestria" name="licenciatura" value="ma" 
                v-model="strBachelorDegree">
                <label class="form-check-label" for="maestria">Maestría</label><br>
              </div>
              <div class="form-check">
                <input class="form-check-input" type="radio" id="doctorado" name="licenciatura" value="doc" 
                v-model="strBachelorDegree">
                <label class="form-check-label" for="doctorado">Doctorado</label>
              </div>
            </div>
          </div>
        </div>
        <div class="form-half" v-if="strBachelorDegree">
          <div class="form-half-select" v-if="strBachelorDegree==='lic' && objNewUser.intPkAcademy == 1">
            <label>Especifique:   </label>
            <select id="carrera" v-model="objNewUser.strBachelors" required>
              <option value="" class="form-control" disabled>Selecciona uno...</option>
              <option value="ICO" class="form-control">ICO</option>
              <option value="IME" class="form-control">IME</option>
              <option value="ICI" class="form-control">ICI</option>
              <option value="ISES" class="form-control">ISES</option>
              <option value="IEL" class="form-control">IEL</option>
            </select>
          </div>

          <div class="form-half-input" 
            v-else>
            <label for="mas-nombre">Especifique:</label>
            <input type="text" id="mas-nombre" v-model.lazy="objNewUser.strBachelors" required>
          </div>
        </div>
        <div class="button-container ">
          <button type="submit" class="color-button shadow-box" v-if="!boolLoading">
            Registrar usuario
          </button>
          <LoaderComponent v-else/>
        </div>
      </form>
    </div>
  </div>
</template>

<script>
import { useStudentStore } from '@/stores/student';
import { defineAsyncComponent } from 'vue';
import LoaderComponent from '@/components/LoaderComponent.vue';
import PopUpWarning from '@/components/PopUpWarning.vue';
import PopupSuccess from '@/components/PopupSuccess.vue';
// import { mapState } from 'pinia';
export default {
  setup(){
    const studentStore = useStudentStore();
    return {studentStore}
  },
  data(){
    return{
      strUserSelected: "",
      strTextInput : "",
      objNewUser: {
        strNmCta : "",
        strName : "",
        strSurename : "",
        strBachelors : "",
        intPkAcademy : ""
      },
      strBachelorDegree : "",
      boolLoading: false,
      boolUserSent: false,
      boolCompleted: false,
    }
  },
  components:{
    LoaderComponent,
    PopUpWarning,
    PopupSuccess,
    HeaderComponent: defineAsyncComponent({
      loader: () => import("@/components/HeaderComponent.vue"),
      loadingComponent: LoaderComponent,
      delay: 200
    }),
    
  },
  computed:{
    // ...mapState(useStudentStore,["arrAcademicEntities"])
  },
  methods: {
    //------------------------------------------------------------------------------------
    goShowPopupSucces() {
      if (
        !this.studentStore.boolErrorUserAdded && this.boolUserSent
      )
      {
        this.boolCompleted = true;
      }
    },

    //------------------------------------------------------------------------------------
    async getAcademicEntities(){
      await this.studentStore.getAllAcademicEntities();
    },

    //------------------------------------------------------------------------------------
    boolValidateUser(){
      let boolValidate = false;
      if(
        this.objNewUser.intPkAcademy &&
        this.objNewUser.strBachelors &&
        this.objNewUser.strName &&
        this.objNewUser.strSurename &&
        (
          this.objNewUser.strNmCta && 
          !isNaN(Number(this.objNewUser.strNmCta)) &&
          (this.objNewUser.strNmCta.length == 7 ||
          this.objNewUser.strNmCta.length == 5
          )
        )
      ) {
        boolValidate = true;
      }

      return boolValidate;
    },

    //------------------------------------------------------------------------------------
    async subSetNewUser(){
      this.boolLoading = true;
      if(
        this.boolValidateUser()
      ) {
        await this.studentStore.setNewUser(this.objNewUser);
        this.boolUserSent = true;
      }
      this.boolLoading = false;
      this.goShowPopupSucces();
    },

    //------------------------------------------------------------------------------------


  },
  async mounted(){
    this.getAcademicEntities();
  }
}
</script>


<style scoped>
.p-ub{
  padding: 6px 18px;
}

.form-half .top, .middle{
  display: flex;
  gap: 16px;
  justify-content: center;
  margin: 16px 0px;
}

input[type="text"],select{
  width: 180px;
  margin: 0px 16px
}

select option, optgroup{
  background: whitesmoke;
  color: #597e56;
}
.label-text{
  width: 56px;
}

.form-half-input, .form-half-select{
  display: flex;
  justify-content: center;
  padding: 4px 8px;
  margin: 16px 0px;
}

.button-container{
  display: flex;
  justify-content: center;
}

.color-button{
  background: radial-gradient(black, rgb(9 9 9 / 47%));
}
</style>