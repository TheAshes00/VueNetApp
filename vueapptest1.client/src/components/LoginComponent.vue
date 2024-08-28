<script>
export default {
    data() {
        return {
            boolHelpActive: false,
            strIdentificaionNumber : "",
        }
    },
    methods: {
        subActivateHelpArea(){
            this.boolHelpActive = !this.boolHelpActive;
        },
        subLoginUser(){
            this.$router.push("/activities");
        }
    }
}
</script>
<template>
    <div class="container-fluid">
        <form class="form-attributes ">
            <div class="form-top">
                <label for="nmct" class="text-login-color text-login"> 
                    Número de indentificacíon 
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
                    <div class="help" v-show="boolHelpActive">
                        <p class="text-login-color ">
                            <span class="nmcta-color ">Número de cuenta</span> 
                            en caso de ser <span class="nmcta-color"> alumno.</span><br>
                            <span class="nmemp-color">Número de empleado</span>
                            en caso de ser <span class="nmemp-color"> docente/administrativo</span><br>
                        </p>
                    </div>
                </Transition>
                <input type="text" name="nmct" id="nmct">
            </div>
            <div class="form-bottom">
                <button class="button login-button shadow-box" type="button" v-on:click="subLoginUser">
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

</style>