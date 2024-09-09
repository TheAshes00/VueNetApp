<script>
import Material from '@/components/MaterialLoan.vue'
import HeaderComponent from '@/components/HeaderComponent.vue';
import PopupWarning from "@/components/PopUpWarning.vue"
import { useStudentStore } from '@/stores/student';
import PopupSuccess from '@/components/PopupSuccess.vue';
import ReturnMaterial from '@/components/ReturnMaterial.vue';

export default{
  setup(){
    const userStore = useStudentStore();
    return { userStore }
  },
  components:{
    Material,
    HeaderComponent,
    PopupWarning,
    PopupSuccess,
    ReturnMaterial
  },
  data(){
      return {
          strActionSelected: "",
          strUser : "Fernando",
      }
  },
  methods:{
    goHome(){
      this.$router.push("/");
    }
  },
};
</script>

<template>
  <Transition>
    <PopupWarning v-if="userStore.boolShowMaterialError" :strParent="'Activities'">
      {{userStore.strUserMessage}}
    </PopupWarning>
  </Transition>

  <Transition>
    <PopupWarning v-if="userStore.boolLoanError && userStore.boolShowLoanError" 
      :strParent="'Activities-Register'"
    >
      {{ studentStore.strUserMessage }}
    </PopupWarning>
  </Transition>

  <Transition>
    <PopupWarning v-if="userStore.boolLoanError && userStore.boolShowLoanError" 
      :strParent="'Activities-Return'"
    >
      {{ studentStore.strUserMessage }}
    </PopupWarning>
  </Transition>

  <Transition>
    <!-- Popup success component, using slot to display corresponing completed message -->
    <PopupSuccess v-if="(userStore.boolLoanCompleted || userStore.boolReturnCompleted)" 
      :strComponent="'Activities'"
    >
      {{ userStore.boolLoanCompleted 
        ? 'User loan registered succesfully' 
        : 'Material returned succesfully' 
      }}
    </PopupSuccess>
  </Transition>


    <HeaderComponent/>
  
  <h2>
      Welcome
  </h2>
  
  <div class="pad-8">
      <div class="row">
        <div class="col-sm-4 col-md-4" >
          <label for="user-name">
            Name: 
          </label>
          <input type="text" name="user-name" id="user-name" class="shadow-box" 
            :value="userStore.objStudent.strName +' '+ userStore.objStudent.strSurename" disabled>
        </div>
        <div class="col-sm-4 col-md-4" >
          <label for="user-id">
            ID:
          </label>
          <input type="text" name="user-id" id="user-id" class="shadow-box" 
            :value="userStore.objStudent.strNmCta" disabled>
        </div>
        <div class="col-sm-4 col-md-4" >
          <label for="user-course">
            Course:
          </label>
          <input type="text" name="user-course" id="user-course" class="shadow-box" 
          :value="userStore.objStudent.strBachelors" disabled>
        </div>
      </div>
  </div>
  <h3>
      Choose one:
  </h3>
  <div class="register-options">
      <div class="form-check">
        <input class="form-check-input" type="radio" name="flexRadioDefault" id="flexRadioDefault1"
          value="material" v-model="strActionSelected">
        <label class="form-check-label" for="flexRadioDefault1">
          Material Loan
        </label>
      </div>
      <div class="form-check">
        <input class="form-check-input" type="radio" name="flexRadioDefault" id="flexRadioDefault2"
          value="workshop" v-model="strActionSelected">
        <label class="form-check-label" for="flexRadioDefault2">
          Workshop
        </label>
      </div>
      <div class="form-check">
        <input class="form-check-input" type="radio" name="flexRadioDefault" id="flexRadioDefault3"
          value="return" v-model="strActionSelected">
        <label class="form-check-label" for="flexRadioDefault3">
          Return material
        </label>
      </div>
  </div>

      <div v-if="strActionSelected === 'material'">
        <Material></Material>
      </div>
      <div v-else-if="strActionSelected === 'return'">
        <ReturnMaterial/>
      </div>
</template>

<style scoped>
header {
  line-height: 1.5;
  width: 100%;
    /* color: #f2f2f2; */
    text-align: center;
    padding: 14px 16px;
    text-decoration: none;
    font-size: 17px;
    display: flex;
    justify-content: flex-end;
}


</style>