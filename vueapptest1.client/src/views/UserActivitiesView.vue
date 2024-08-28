<script>
export default{
    data(){
        return {
            strActionSelected: "",
            strUser : "Fernando",
        }
    },
};
</script>

<template>
    <header>
        <nav>
            <button type="button">Sign out</button>
        </nav>
    </header>
    <h2>
        Welcome
    </h2>
    <div>
        <input type="text" name="user" id="user" :value="strUser" disabled>
    </div>
    <h3>
        Choose one:
    </h3>
    <div class="register-options">
        <div class="form-check">
          <input class="form-check-input" type="radio" name="flexRadioDefault" id="flexRadioDefault1" 
            value="student" v-model="strUserSelected">
          <label class="form-check-label" for="flexRadioDefault1">
            Material Loan
          </label>
        </div>
        <div class="form-check">
          <input class="form-check-input" type="radio" name="flexRadioDefault" id="flexRadioDefault2" 
            value="teacher" v-model="strUserSelected">
          <label class="form-check-label" for="flexRadioDefault2">
            Workshop
          </label>
        </div>
        <div class="form-check">
          <input class="form-check-input" type="radio" name="flexRadioDefault" id="flexRadioDefault2" 
            value="teacher" v-model="strUserSelected">
          <label class="form-check-label" for="flexRadioDefault2">
            Return material
          </label>
        </div>
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