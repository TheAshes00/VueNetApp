<script>
import { useStudentStore } from '@/stores/student';
import PopUpWarning from './PopUpWarning.vue';
import LoaderComponent from './LoaderComponent.vue';
import { useAdminStore } from '@/stores/admin';

export default {
    setup(){
        const studentStore = useStudentStore();
        const adminStore = useAdminStore();
        return { studentStore, adminStore }
    },
    data() {
        return {
            boolHelpActive: false,
            strIdentificaionNumber : "",
            boolIsLoading: false,
            boolAdminCommand: false,
            objAdmin:{
                strUsername: "",
                strPassword: ""
            }
        }
    },
    components: {
        PopUpWarning,
        LoaderComponent
    },
    methods: {
        //--------------------------------------------------------------------------------
        subActivateHelpArea(){
            this.boolHelpActive = !this.boolHelpActive;
        },

        //--------------------------------------------------------------------------------
        async subLoginUser(){
            this.boolIsLoading = true;

            if (
                this.boolIndetificationNumberIsSet()
            )
            {
                if(
                    this.boolAdminCommand 
                ) {
                    if(
                        this.objAdmin.strPassword &&
                        this.objAdmin.strUsername
                    ){
                        await this.adminStore.subLoginAdmin(this.objAdmin)

                        let boolLoginError = this.adminStore.boolGetBoolAdminLoginError;
                        console.log(boolLoginError)
                        if(
                            !boolLoginError
                        ){
                            this.$router.push('/admin');
                        }
                    }
                    
                } 
                else
                {
                    this.subActivateAdminLogin()
                }
                
            }
                
            this.boolIsLoading = false;
        },

        //--------------------------------------------------------------------------------
        async subActivateAdminLogin(){
            if(
                this.strIdentificaionNumber === '0000000'
            ){
                this.boolAdminCommand = true;
            }
            else
            {
                await this.studentStore.getLoginConfirm(this.strIdentificaionNumber)

                if(
                    !this.studentStore.boolUserNotFound
                ) {
                    this.$router.push("/activities");
                }
            }
        },


        //--------------------------------------------------------------------------------
        async subShowErrorMessage(){
            this.studentStore.setBoolShowStudentErrorMessage(false)
        },

        //--------------------------------------------------------------------------------
        boolIndetificationNumberIsSet(){
            let boolIsReady = false;
            if (
                this.strIdentificaionNumber &&
                !isNaN(Number(this.strIdentificaionNumber))
            ) {
                boolIsReady = true;
            }
            return boolIsReady;
        },

        //--------------------------------------------------------------------------------

    }
}
</script>
<template>
    <PopUpWarning v-if="(studentStore.boolUserNotFound && studentStore.boolShowErrorNotFound) ||
        (adminStore.boolAdminLoginError && adminStore.boolShowAdminLoginError)" 
        :strParent="studentStore.boolUserNotFound ? 'Login' : 'LoginAdmin'">
        {{ studentStore.boolUserNotFound ? studentStore.strUserMessage : adminStore.strUserName}}
    </PopUpWarning>
    <div class="container-fluid">
        <form class="form-attributes" v-on:submit.prevent="subLoginUser">
            <div class="form-top" v-if="!boolAdminCommand">
                <label for="nmct" class="text-login-color text-login"> 
                    Identification number:
                    <span>
                        <button class="help-button" type="button" v-on:click="subActivateHelpArea">
                            <svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" class="bi bi-question-circle" viewBox="0 0 16 16">
                                <path d="M8 15A7 7 0 1 1 8 1a7 7 0 0 1 0 14m0 1A8 8 0 1 0 8 0a8 8 0 0 0 0 16"/>
                                <path d="M5.255 5.786a.237.237 0 0 0 .241.247h.825c.138 0 .248-.113.266-.25.09-.656.54-1.134 1.342-1.134.686 0 1.314.343 1.314 1.168 0 .635-.374.927-.965 1.371-.673.489-1.206 1.06-1.168 1.987l.003.217a.25.25 0 0 0 .25.246h.811a.25.25 0 0 0 .25-.25v-.105c0-.718.273-.927 1.01-1.486.609-.463 1.244-.977 1.244-2.056 0-1.511-1.276-2.241-2.673-2.241-1.267 0-2.655.59-2.75 2.286m1.557 5.763c0 .533.425.927 1.01.927.609 0 1.028-.394 1.028-.927 0-.552-.42-.94-1.029-.94-.584 0-1.009.388-1.009.94"/>
                            </svg>    
                        </button>
                    </span>
                </label> 
                
                <Transition>
                    <div class="help pad-8" v-show="boolHelpActive">
                        <p class="text-login-color ">
                            <span class="nmcta-color ">Account number</span> 
                            in case of being <span class="nmcta-color"> student.</span><br>
                            <span class="nmemp-color">Employee ID</span>
                            in case of beaing <span class="nmemp-color"> teacher/ UNI staff </span><br>
                        </p>
                    </div>
                </Transition>
                <input type="text" maxlength="7" name="nmct" id="nmct" v-model="strIdentificaionNumber">
            </div>

            <div class="form-top" v-else>
                <label for="admin-username" class="text-login-color text-login">Username</label>
                <input type="text" name="admin-username" id="admin-username" v-model="objAdmin.strUsername">

                <label for="admin-password" class="text-login-color text-login">Password</label>
                <input type="password" name="admin-password" id="admin-password" v-model="objAdmin.strPassword">
            </div>
            <div class="form-bottom">
                <LoaderComponent v-if="boolIsLoading">
                </LoaderComponent>
                <button class="button login-button button-align shadow-box" type="button" 
                    v-on:click="subLoginUser" v-else>
                    Login
                </button>
            </div>
        </form>
    </div>
</template>

<style scoped>

.v-enter-active,
.v-leave-active {
  transition: opacity 0.5s ease;
}

.v-enter-from,
.v-leave-to {
  opacity: 0;
}
.text-login{
    background: rgb(0 0 0 / 12%);
    border-radius: 10px;
}
.text-login-color {
    color: whitesmoke;
}
.container-fluid {
    display: flex;
    place-content: center;
    padding-top: 36px;
    margin-top: 36px;
}
.form-attributes {
    border: 1px whitesmoke solid;
    border-radius: 5px;
    padding: 16px;
    width: 30%;
    background: rgb(0, 0, 0, 0.3);
}
.help {
    border: 1px whitesmoke solid;
    background: rgb(0, 0, 0, 0.5);
    margin-bottom: 16px;
    border-radius: 10px;
}
.help-button {
    background: transparent;
    color: #c7ffc2;
    border: none;
    padding: 0px 10px;
}

.nmcta-color {
    color: #6CC0C0;
}

.nmemp-color {
    color: #F9D082;
}

.login-button{
    margin-top: 1em;
    background: radial-gradient(black, rgb(9 9 9 / 47%));
    width: 100%;
}

.form-bottom {
    display: flex;
    justify-content: center;
}

.popup-width{
    width: 240px;
}
</style>