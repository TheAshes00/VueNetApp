<script>
import { useStudentStore } from '@/stores/student';

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
        }
    },
    methods:{
        //--------------------------------------------------------------------------------
        addNewRow(){
            let intLength = this.arrobjMaterials.lengt;

            let objNewMaterial = {
                intId: intLength,
                strNumContInt: "",
                boolShowIcon : true,
                strName: ""   
            }

            this.arrobjMaterials.push(objNewMaterial);
        },

        //--------------------------------------------------------------------------------
        subRemoveElement(
            intId_I
        ) {
            let arrobjFilterResult = this.arrobjMaterials.filter( 
                objMaterial => objMaterial.intId == intId_I
            );

            this.arrobjMaterials = arrobjFilterResult;
        },

        //--------------------------------------------------------------------------------
        async arrGetConfirmation(
            //
        ) {
            let arrConfirm = this.arrobjMaterials.map(
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

            let arrstrFiltered = arrConfirm.filter(
                str => str != ""
            )

            if(
                arrstrFiltered.length > 0
            ) {
                
                await this.studentStore.getMaterialConfirmation(arrstrFiltered)
            }

        }

        //--------------------------------------------------------------------------------
        //--------------------------------------------------------------------------------
    },
}
</script>

<template>
    <div class="table-button-container">
        <div class="pad-8">
            <button type="button" id="bt_add" class="border-button" v-on:click="addNewRow"> 
                Add New Material
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
            <div class="row pad-8 align-items-center" v-for="objMaterial in arrobjMaterials" :key="objMaterial.intId">
                <div class="col-sm-6 col-md-6">
                    <input type="text" name="material-id" id="material-id" v-model="objMaterial.strNumContInt" 
                        :disabled="!objMaterial.boolShowIcon"
                        >
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
                    <input type="text" name="material-name" id="material-name" 
                        v-bind:value="objMaterial.strName" disabled
                    >
                </div>
            </div>
        </div>
    </div>
    <div>
        <button type="button" v-on:click="arrGetConfirmation">
            Show Material
        </button>
    </div>
</template>


<style style>
.table-button-container{
    display: flex;
    flex-direction: column;
    align-items: center;
    justify-content: center;
}

.grid-material{
    width: 60%;
    padding: 8px;
}

.border-button{
    border: 1px solid #c7ffc2;
}

.pad-8{
    padding: 8px;
}

.button-align{
    display: flex;
    align-items: center;
    gap: 4px;

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
    max-height: 200px;
    overflow-x: hidden;
}
</style>