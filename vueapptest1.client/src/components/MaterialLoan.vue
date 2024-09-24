<script>
import { useStudentStore } from '@/stores/student';
import LoaderComponent from './LoaderComponent.vue';
import ShiftManager from './ShiftManager.vue';

export default{
    setup(){
        const studentStore = useStudentStore();
        return { studentStore }
    },
    data(){
        return {
            test:1,
            arrobjMaterials: [
                {
                    intId: 0,
                    strNumContInt: "",
                    boolShowIcon : true,
                    strName: ""
                },
            ],
            boolThShowIcon: true,
            intCountRows: 4,
            strText: "",
            objErrorBorder:{
                border: "2px solid #9b1717",
                color: "#9b1717"
            },
            boolNoMaterialInList : false,
            boolIsLoading: false,
            boolMaterialConfirmed: false
        }
    },
    components:{
        LoaderComponent,
        ShiftManager,
    },
    methods:{
        //--------------------------------------------------------------------------------
        addNewRow(){
            if(
                this.intCountRows > 0
            ) {
                let intLength = this.arrobjMaterials.length;
            
                let objNewMaterial = {
                    intId: intLength,
                    strNumContInt: "",
                    boolShowIcon : true,
                    strName: ""   
                }

                this.arrobjMaterials.push(objNewMaterial);

                this.intCountRows--;
            }
            
        },

        //--------------------------------------------------------------------------------
        subRemoveElement(
            intId_I
        ) {
            let arrobjFilterResult = this.arrobjMaterials.filter( 
                objMaterial => objMaterial.intId != intId_I
            );

            this.arrobjMaterials = this.subSetNewIds(arrobjFilterResult);
            
            this.intCountRows++;
        },

        //--------------------------------------------------------------------------------
        subSetNewIds(
            arrobjFilterResult_I
        ){
            for (let intIndex = 0; intIndex < arrobjFilterResult_I.length; intIndex++) {
                arrobjFilterResult_I[intIndex].intId = intIndex; 
            }
            return arrobjFilterResult_I
        },

        //--------------------------------------------------------------------------------
        async arrGetConfirmation(
            //
        ) {
            this.boolNoMaterialInList = false;
            this.boolIsLoading = true;

            let arrConfirm = this.arrstrMapMaterial();

            let arrstrFiltered = arrConfirm.filter(
                str => str != ""
            )

            let arrstrSet = new Set(arrstrFiltered);

            if(
                arrstrSet.size > 0
            ) {
                await this.studentStore.getMaterialConfirmation([...arrstrSet]);
                this.subSetMaterialError();
            }
            else
            {
                this.boolNoMaterialInList = true;
            }

            this.boolIsLoading = false;
        },

        //--------------------------------------------------------------------------------
        subSetMaterialError(
            //
        ) {
            if(
                !this.studentStore.boolMaterialError
            ) {
                this.arrobjMaterials = this.studentStore.arrobjMaterialConfirmed
                this.boolThShowIcon = false
            }
        },

        //--------------------------------------------------------------------------------
        arrstrMapMaterial(
        ){
            return this.arrobjMaterials.map(
                function(objMaterial){
                    let strNumContInt = "";
                    if(
                        objMaterial.strNumContInt
                    )
                    {
                        strNumContInt = objMaterial.strNumContInt
                    }

                    return strNumContInt;
                }
            );
        },


        //--------------------------------------------------------------------------------
        boolGetMaterialConfirmation(){
            return this.studentStore.arrobjMaterialConfirmed.length > 0
        },

        //--------------------------------------------------------------------------------
        subEditTable(){
            this.arrobjMaterials.forEach(objMaterial => {
                objMaterial.boolShowIcon=true;
            });
            this.boolThShowIcon = true; 
        },

        //--------------------------------------------------------------------------------
        subConfirmMaterialLoan(){
            this.boolMaterialConfirmed = true;
        }

        //--------------------------------------------------------------------------------
    },
}
</script>

<template>
    <div v-if="!boolMaterialConfirmed">
        <div class="table-button-container">
            <div class="pad-8" v-if="boolThShowIcon">
                <button type="button" id="bt_add" class="border-button button-align" v-on:click="addNewRow"> 
                    Add New Material {{ '('+(intCountRows)+')' }}
                    <span class="">
                        <svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" class="bi bi-plus-circle" viewBox="0 0 16 16">
                            <path d="M8 15A7 7 0 1 1 8 1a7 7 0 0 1 0 14m0 1A8 8 0 1 0 8 0a8 8 0 0 0 0 16"/>
                            <path d="M8 4a.5.5 0 0 1 .5.5v3h3a.5.5 0 0 1 0 1h-3v3a.5.5 0 0 1-1 0v-3h-3a.5.5 0 0 1 0-1h3v-3A.5.5 0 0 1 8 4"/>
                        </svg>
                    </span>
                </button>
            </div>
            <div class="container-fluid grid-material" id="material-list">
                <div class="row">
                    <div class="col-sm-6 col-md-6">
                        Material Code
                    </div>
                    <div class="col-sm-6 col-md-6" v-if="boolThShowIcon">
                        Action
                    </div>
                    <div class="col-sm-6 col-md-6" v-else>
                        Material Name
                    </div>
                </div>
                <div class="row pad-8 align-items-center" v-for="(objMaterial) in arrobjMaterials" :key="objMaterial.intId">
                    <div class="col-sm-6 col-md-6">
                        <div class="button-align">
                            <input type="text" :class="(!objMaterial.boolShowIcon ? 'shadow-box' : '')" name="material-id" id="material-id" v-model="objMaterial.strNumContInt" 
                                :disabled="!objMaterial.boolShowIcon" v-bind:style="boolNoMaterialInList ? objErrorBorder : {}"
                                >
                        </div>
                    </div>
                    <div class="col-sm-6 col-md-6" v-if="objMaterial.boolShowIcon">
                        <button type="button" class="cancel button-align" v-on:click="subRemoveElement(objMaterial.intId)"> 
                            Remove
                            <span>
                                <svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" class="bi bi-x-octagon" viewBox="0 0 16 16">
                                    <path d="M4.54.146A.5.5 0 0 1 4.893 0h6.214a.5.5 0 0 1 .353.146l4.394 4.394a.5.5 0 0 1 .146.353v6.214a.5.5 0 0 1-.146.353l-4.394 4.394a.5.5 0 0 1-.353.146H4.893a.5.5 0 0 1-.353-.146L.146 11.46A.5.5 0 0 1 0 11.107V4.893a.5.5 0 0 1 .146-.353zM5.1 1 1 5.1v5.8L5.1 15h5.8l4.1-4.1V5.1L10.9 1z"/>
                                    <path d="M4.646 4.646a.5.5 0 0 1 .708 0L8 7.293l2.646-2.647a.5.5 0 0 1 .708.708L8.707 8l2.647 2.646a.5.5 0 0 1-.708.708L8 8.707l-2.646 2.647a.5.5 0 0 1-.708-.708L7.293 8 4.646 5.354a.5.5 0 0 1 0-.708"/>
                                </svg>
                            </span>
                        </button>
                    </div>
                    <div class="col-sm-6 col-md-6" v-else>
                        <input type="text" class="shadow-box" name="material-name" id="material-name" 
                            v-bind:value="objMaterial.strName" disabled
                        >
                    </div>
                </div>
            </div>
        </div>
        <div class="button-bottom" v-if="boolThShowIcon">
            <div v-if="boolIsLoading">
                <LoaderComponent/>
            </div>
            <button type="button" class="border-button button-align" v-on:click="arrGetConfirmation" 
                :disabled="intCountRows == 5" v-else>
                Show Material
            </button>
        </div>

        <div class="button-bottom" v-else>
            <div>
                <button type="button" class="border-button button-align" v-on:click="subConfirmMaterialLoan">
                    Confirm Material Loan
                    <span>
                        <svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" class="bi bi-check-circle" viewBox="0 0 16 16">
                            <path d="M8 15A7 7 0 1 1 8 1a7 7 0 0 1 0 14m0 1A8 8 0 1 0 8 0a8 8 0 0 0 0 16"/>
                            <path d="m10.97 4.97-.02.022-3.473 4.425-2.093-2.094a.75.75 0 0 0-1.06 1.06L6.97 11.03a.75.75 0 0 0 1.079-.02l3.992-4.99a.75.75 0 0 0-1.071-1.05"/>
                        </svg>
                    </span>
                </button>
            </div>

            <div>
                <button type="button" class="cancel button-align" v-on:click="subEditTable"> 
                    Cancel
                    <span>
                        <svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" class="bi bi-x-octagon" viewBox="0 0 16 16">
                            <path d="M4.54.146A.5.5 0 0 1 4.893 0h6.214a.5.5 0 0 1 .353.146l4.394 4.394a.5.5 0 0 1 .146.353v6.214a.5.5 0 0 1-.146.353l-4.394 4.394a.5.5 0 0 1-.353.146H4.893a.5.5 0 0 1-.353-.146L.146 11.46A.5.5 0 0 1 0 11.107V4.893a.5.5 0 0 1 .146-.353zM5.1 1 1 5.1v5.8L5.1 15h5.8l4.1-4.1V5.1L10.9 1z"/>
                            <path d="M4.646 4.646a.5.5 0 0 1 .708 0L8 7.293l2.646-2.647a.5.5 0 0 1 .708.708L8.707 8l2.647 2.646a.5.5 0 0 1-.708.708L8 8.707l-2.646 2.647a.5.5 0 0 1-.708-.708L7.293 8 4.646 5.354a.5.5 0 0 1 0-.708"/>
                        </svg>
                    </span>
                </button>
            </div>
        </div>
    </div>
    <div class="shift-manager-container" v-else>
        <ShiftManager></ShiftManager>
    </div>

</template>


<style style>
.shift-manager-container{
    display: flex;
    flex-direction: column;
    align-items: center;
}

.table-button-container{
    display: flex;
    flex-direction: column;
    align-items: center;
    justify-content: center;
    padding: 0px 12px;
}

.grid-material{
    width: 60%;
    padding: 8px;
}

.border-button{
    border: 1px solid #c7ffc2;
}

.button-bottom{
    display: flex;
    justify-content: center;
    gap: 14px;
}

/* width */
::-webkit-scrollbar {
    width: 8px;
    border-radius: 3px;
}

/* Handle on hover */
::-webkit-scrollbar-thumb:hover {
    background: #cdcdcd;
}

#material-list {
    overflow-y: auto;
    max-height: 150px;
    overflow-x: hidden;
    padding-bottom: 8px;
}
</style>