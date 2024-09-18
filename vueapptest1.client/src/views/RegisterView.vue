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
    <h2>Sign Up</h2>
    <div class="">
      <h3 class="p-ub">Choose one:</h3>
      <div class="register-options">
        <div class="form-check">
          <input class="form-check-input" type="radio" name="flexRadioDefault" id="flexRadioDefault1" 
            value="student" v-model="strUserSelected">
          <label class="form-check-label" for="flexRadioDefault1">
            Student
          </label>
        </div>
        <div class="form-check">
          <input class="form-check-input" type="radio" name="flexRadioDefault" id="flexRadioDefault2" 
            value="teacher" v-model="strUserSelected" v-on:change="subResetIdnumber">
          <label class="form-check-label" for="flexRadioDefault2">
            Teacher
          </label>
        </div>
      </div>
    </div>

    <div v-if="strUserSelected">
      <form v-on:submit.prevent="subSetNewUser">
        <div class="form-half">
          <div class="top">
            <label class="label-text" for="name-new">Name:</label>
            <input type="text" name="name-new" id="name-new" placeholder="Ex: Molly" 
              v-model="objNewUser.strName" 
              required>

            <label for="surename-new">Surename:</label>
            <input class="label-text" type="text" name="surename-new" id="surename-new" 
              placeholder="Ex: Evans" v-model="objNewUser.strSurename" 
              required>

          </div>

          <div class="middle">
            <label class="label-text" for="nmcta-new"> 
              {{  (this.strUserSelected === "student" ? "Account Number:" : "Employee ID") }}
            </label>
            <input type="text" name="nmcta-new" id="nmcta-new" placeholder="Ex: 1110038" 
              maxlength="7" v-model="objNewUser.strNmCta" v-if="this.strUserSelected === 'student'" 
              required>

            <input class="label-text" type="text" name="nmcta-new" id="nmcta-new" 
              placeholder="Ex: 11138" maxlength="5" v-model="objNewUser.strNmCta" required v-else>

            <label class="label-text" for="orgacd-new">Academic Entity:</label>
            <select name="orgacd-new" id="orgacd-new" v-if="studentStore.arrAcademicEntities != []" 
              v-model="objNewUser.intPkAcademy" v-on:change="subReset"
              required
            >
              <option value="" disabled>Choose one...</option>
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

          <div class="bottom" v-if="objNewUser.intPkAcademy" v-show="!boolCheckUni()">  
            <div class="register-options">
              <div class="form-check" v-if="boolCheckUni()">
                <input class="form-check-input" type="radio" id="prepa" name="prepa" value="Preparatoria" 
                v-model="strBachelorDegree" v-bind:checked="boolCheckUni()">
                <label class="form-check-label" for="prepa">High School</label><br>
              </div>
              <div class="register-options" v-else>
                <div class="form-check">
                  <input class="form-check-input" type="radio" id="licenciatura" name="licenciatura" value="lic" 
                  v-model="strBachelorDegree">
                  <label class="form-check-label" for="licenciatura">Bachelors</label><br>
                </div>
                <div class="form-check">
                  <input class="form-check-input" type="radio" id="maestria" name="licenciatura" value="ma" 
                  v-model="strBachelorDegree">
                  <label class="form-check-label" for="maestria">Masters</label><br>
                </div>
                <div class="form-check">
                  <input class="form-check-input" type="radio" id="doctorado" name="licenciatura" value="doc" 
                  v-model="strBachelorDegree">
                  <label class="form-check-label" for="doctorado">PhD</label>
                </div>
              </div>
              
            </div>
          </div>
        </div>
        <div class="form-half" v-if="strBachelorDegree">
          <div class="form-half-select" v-if="strBachelorDegree==='lic' && objNewUser.intPkAcademy == 1">
            <label>Which one:   </label>
            <select id="carrera" v-model="objNewUser.strBachelors" required>
              <option value="" class="form-control" disabled>Choose one...</option>
              <option value="ICO" class="form-control">ICO</option>
              <option value="IME" class="form-control">IME</option>
              <option value="ICI" class="form-control">ICI</option>
              <option value="ISES" class="form-control">ISES</option>
              <option value="IEL" class="form-control">IEL</option>
            </select>
          </div>

          <div class="form-half-input" 
            v-else>
            <label for="mas-nombre">{{ !boolCheckUni() ? 'Which one:' : '' }}</label>
            <input type="text" id="mas-nombre" v-model.lazy="objNewUser.strBachelors" 
              required :disabled="boolCheckUni()"
            >
          </div>
        </div>
        <div class="button-container button-align">
          <button type="submit" class="color-button shadow-box button-align" v-if="!boolLoading">
            Register
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
    boolCheckUni(
    ) {
      let boolTest = false;
      if(
        this.objNewUser.intPkAcademy
      ){
        let objByGroup = this.studentStore.arrAcademicEntities.find(
          x => x.strGroupName === "Preparatoria");


        let arrobjAcademy = objByGroup.darrAcademies

        let obj = arrobjAcademy.find(x => x.intPk == this.objNewUser.intPkAcademy)

        if(
          obj && 
          this.strUserSelected === 'student'
        ){
          this.objNewUser.strBachelors = "Preparatoria"
          boolTest = true;
        }
      }

      return boolTest
        
    },

    //------------------------------------------------------------------------------------
    subReset(
      //
    ){
      if(
        this.boolCheckUni()
      ){
        // Do nothing
      }
      else
      {
        this.objNewUser.strBachelors = ""
      }
    },

    //------------------------------------------------------------------------------------
    subResetIdnumber(
      //
    ){
      if(
        this.strUserSelected === "teacher"
      ){
        if(
          this.objNewUser.strNmCta &&
          this.objNewUser.strNmCta.length > 5
        ){
          // let test = this.objNewUser.strNmCta.slice(0,5);
          this.objNewUser.strNmCta = ""
        }
      }
    },

    //------------------------------------------------------------------------------------


  },
  //------------------------------------------------------------------------------------
  async created(){
    this.getAcademicEntities();
  }
  //------------------------------------------------------------------------------------
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