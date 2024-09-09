<script>
import ErrorMessage from '@/components/ErrorMessage.vue';
import LoaderComponent from '@/components/LoaderComponent.vue';
import PopupSuccess from '@/components/PopupSuccess.vue';
import PopUpWarning from '@/components/PopUpWarning.vue';
// import ModalComponent from '@/components/ModalComponent.vue';
import { useAdminStore } from '@/stores/admin';

export default {
    setup(){
        const adminStore = useAdminStore();
        return { adminStore }
    },
    async created(){
        let objPages = {
            intPageNumber: 1,
            intPageSize: 5,
            strSearch: null
        }
        await this.adminStore.subGetAllTutors(objPages);
        this.subPaginate()
    },
    components: {
        // ModalComponent,
        LoaderComponent,
        PopupSuccess,
        PopUpWarning,
        ErrorMessage,
    },
    data(){
        return {
            boolShowModal: false,
            objSelectedElement:{
                intPk: null,
                strIdentification: null,
                strName: "",
                strSurename: "",
                boolActive: true,
                byteGender: "0",
                strBachelors: ""
            },
            arrTutors: [],       // Lista de Tutors
            intTotalCount: 0,       // Total de registros
            intPageNumber: 1,       // Página actual
            intPageSize: 5,        // Tamaño de página
            intTotalPages: 0,       // Número total de páginas,
            strSearch: "",
            boolIsLoading: false,
            boolSearchDone: false,
            strModalTitle: "",
            boolEditInModal: false,
            boolMissingInfo: false,
        }
    },
    methods:{
        //--------------------------------------------------------------------------------
        async subRealoadTutors(
            //            
        ){
            this.boolIsLoading = true;
            this.boolSearchDone = false;
            this.strSearch = "";

            let objPages = {
                intPageNumber: 1,
                intPageSize: 5,
                strSearch: null
            }
            await this.adminStore.subGetAllTutors(objPages);
            this.subPaginate()

            this.boolIsLoading = false;
        },
        //--------------------------------------------------------------------------------
        async subSearchTutor(){
            if(
                this.strSearch
            ) {
                this.boolSearchDone = true

                this.boolIsLoading = true;

                let objPages = {
                    intPageNumber: this.intPageNumber,
                    intPageSize: 5,
                    strSearch: this.strSearch
                }
                await this.adminStore.subGetAllTutors(objPages);
                this.subPaginate();

                this.boolIsLoading = false;
            }
        },

        //--------------------------------------------------------------------------------
        subPaginate(){
            if(
                this.adminStore.objPaginateTutors != null
            ){
                //                                          //Get info from getter
                let objPagTut = this.adminStore.objGetObjPaginateTutors;

                this.intPageNumber =  objPagTut.intPageNumber;
                this.intPageSize = objPagTut.intPageSize;
                this.arrTutors = objPagTut.objPaginatedObject; 
                this.intTotalCount = objPagTut.intTotalCount;

                
                this.intTotalPages = Math.ceil(this.intTotalCount / this.intPageSize)
            }
            
        },

        //--------------------------------------------------------------------------------
        async changePage(
            intNewPage_I
        ) {
            this.intPageNumber = intNewPage_I;
            let objPages = {
                intPageNumber: this.intPageNumber,
                intPageSize: 5,
                strSearch: null
            }

            if(
                this.strSearch
            ) {
                objPages.strSearch = this.strSearch;
            }

            await this.adminStore.subGetAllTutors(objPages);
            this.subPaginate();
        },

        //--------------------------------------------------------------------------------
        editTutor(
            intPk_I
        ){
            this.boolEditInModal = true;
            this.strModalTitle = "Edit Tutor";
            let objTutor = this.arrTutors.find(
                obj => obj.intPk === intPk_I);
            this.objSelectedElement = objTutor
            
            this.boolShowModal = true;
        },

        //--------------------------------------------------------------------------------
        hideModal(
            boolShowModal_I
        ) {
            this.boolEditInModal = false;
            this.boolShowModal = boolShowModal_I;
            this.objSelectedElement ={
                strIdentification: null,
                strName: "",
                strSurename: "",
                boolActive: true,
                byteGender: "",
                strBachelors: ""
            };
        },

        //--------------------------------------------------------------------------------
        async subSaveEdit(
            boolEdit_I
        ) {
            if(
                boolEdit_I
            ){
                console.log("Edit", this.objSelectedElement)
                this.objSelectedElement.intnPk = this.objSelectedElement.intPk;
            }
            else
            {
                console.log("Add", this.objSelectedElement)
                
            }

            if(
                this.boolCheckModal()
            ) {
                await this.adminStore.subSetAddTutor(this.objSelectedElement)

                this.subRealoadTutors()

                this.hideModal(false)
            }
            else {
                this.showHideErrorMessage(true);
            }
        },

        //--------------------------------------------------------------------------------
        subAddNew(){
            this.boolEditInModal = false;
            this.strModalTitle = "New Tutor";
            this.boolShowModal = true;
        },

        //--------------------------------------------------------------------------------
        boolCheckModal(){
            let boolIsValid = false

            if(
                this.objSelectedElement.strName &&
                this.objSelectedElement.strSurename &&
                this.objSelectedElement.byteGender &&
                this.objSelectedElement.strBachelors
            )
            {
                boolIsValid = true
            }

            if(
                !this.boolEditInModal &&
                (
                    this.objSelectedElement.strIdentification == null  ||
                    (
                        this.objSelectedElement.strIdentification != null &&
                        (
                            this.objSelectedElement.strIdentification.length != 7 ||
                            this.objSelectedElement.strIdentification.length != 5
                        )
                    )
                )
            ) {
                boolIsValid = false
            }

            return boolIsValid;
        },

        //--------------------------------------------------------------------------------
        showHideErrorMessage(
            boolShow_I
        ){
            this.boolMissingInfo = boolShow_I
        },
    }
}
</script>
<template>
    <h3>Add / Edit Tutor</h3>
    <PopupSuccess :strComponent="'Tutor'" v-if="adminStore.boolTutorCompleted">
        Action completed succesfully 
    </PopupSuccess>
    <PopUpWarning :strParent="'Tutor'" v-if="adminStore.boolTutorError && adminStore.boolShowTutorError">
        {{ adminStore.strUserName }}
    </PopUpWarning>
    <!-- Modal -->
    <div class="modal modal-display" v-show="boolShowModal">
      <div class="modal-dialog">
        <div class="modal-content">
          <div class="modal-header">
            <h5 class="modal-title">{{ strModalTitle }}</h5>
            <button type="button" class="btn-close" aria-label="Close" @click="hideModal(false)"></button>
          </div>
          <div class="modal-body">
            <!-- Error message -->
             
            <ErrorMessage @hide-error = "showHideErrorMessage"  v-if="boolMissingInfo" />

            <!-- END Error message -->
            <form class="modal-form">
                <div class="form-row">
                    <div class="mb-3">
                        <label for="tutor-name" class="col-form-label">*Name:</label>
                        <input type="text" class="form-control" id="recipient-name" 
                            v-model="objSelectedElement.strName" required>
                    </div>
                    <div class="mb-3">
                        <label for="tutor-name" class="col-form-label">*Surename:</label>
                        <input type="text" class="form-control" id="recipient-name" 
                            v-model="objSelectedElement.strSurename" required>
                    </div>
                </div>
              
              <div class="form-row">
                <div class="mb-3">
                    <label for="tutor-name" class="col-form-label">*Bachelors:</label>
                    <input type="text" class="form-control" id="recipient-name" 
                        v-model="objSelectedElement.strBachelors" required>
                </div>
                <div class="mb-3">
                    <label for="tutor-name" class="col-form-label">*#Identification:</label>
                    <input type="text" maxlength="7" class="form-control" id="recipient-name" v-model="objSelectedElement.strIdentification" 
                        :disabled="boolEditInModal" required>
                </div>
              </div>
              
              <div class="form-row">
                    <div class="mb-3">
                        <label for="register-options">Gender</label>
                        <div class="register-options" id="register-options">
                            <div class="form-check">
                                <input class="form-check-input" type="radio" name="flexRadioDefault" id="flexRadioDefault1"
                                value="0" v-model="objSelectedElement.byteGender">
                                <label class="form-check-label" for="flexRadioDefault1">
                                F
                                </label>
                            </div>
                            <div class="form-check">
                                <input class="form-check-input" type="radio" name="flexRadioDefault" id="flexRadioDefault2"
                                value="1" v-model="objSelectedElement.byteGender">
                                <label class="form-check-label" for="flexRadioDefault2">
                                M
                                </label>
                            </div>
                        </div>
                    </div>

                    <div class="mb-3 form-switch">
                        <label for="switch-check">Status</label>
                        <div id="switch-check">
                            <label class="switch">
                                <input type="checkbox" name="switch-check-in" id="switch-check-in" 
                                    v-model="objSelectedElement.boolActive" :disabled="!boolEditInModal"
                                    required>
                                <span class="slider round"></span>
                            </label>
                        </div>
                    </div>
              </div>

              
            </form>
          </div>
          <div class="modal-footer">
            <button type="button" class="cancel button-align" v-on:click="hideModal(false)"> 
                Close
                <span>
                    <svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" class="bi bi-x-octagon" viewBox="0 0 16 16">
                        <path d="M4.54.146A.5.5 0 0 1 4.893 0h6.214a.5.5 0 0 1 .353.146l4.394 4.394a.5.5 0 0 1 .146.353v6.214a.5.5 0 0 1-.146.353l-4.394 4.394a.5.5 0 0 1-.353.146H4.893a.5.5 0 0 1-.353-.146L.146 11.46A.5.5 0 0 1 0 11.107V4.893a.5.5 0 0 1 .146-.353zM5.1 1 1 5.1v5.8L5.1 15h5.8l4.1-4.1V5.1L10.9 1z"/>
                        <path d="M4.646 4.646a.5.5 0 0 1 .708 0L8 7.293l2.646-2.647a.5.5 0 0 1 .708.708L8.707 8l2.647 2.646a.5.5 0 0 1-.708.708L8 8.707l-2.646 2.647a.5.5 0 0 1-.708-.708L7.293 8 4.646 5.354a.5.5 0 0 1 0-.708"/>
                    </svg>
                </span>
            </button>
            <button type="submit" id="bt_add" class="border-button button-align" v-on:click="subSaveEdit(boolEditInModal)"> 
                Save
                <span class="">
                    <svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" class="bi bi-plus-circle" viewBox="0 0 16 16">
                        <path d="M8 15A7 7 0 1 1 8 1a7 7 0 0 1 0 14m0 1A8 8 0 1 0 8 0a8 8 0 0 0 0 16"/>
                        <path d="M8 4a.5.5 0 0 1 .5.5v3h3a.5.5 0 0 1 0 1h-3v3a.5.5 0 0 1-1 0v-3h-3a.5.5 0 0 1 0-1h3v-3A.5.5 0 0 1 8 4"/>
                    </svg>
                </span>
            </button>
          </div>
        </div>
      </div>
    </div>
    <!-- END MODAL -->

    <div class="center-box">
        <button type="button" id="bt_add" class="border-button button-align" v-on:click="subAddNew"> 
            Add New Tutor
            <span class="">
                <svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" class="bi bi-plus-circle" viewBox="0 0 16 16">
                    <path d="M8 15A7 7 0 1 1 8 1a7 7 0 0 1 0 14m0 1A8 8 0 1 0 8 0a8 8 0 0 0 0 16"/>
                    <path d="M8 4a.5.5 0 0 1 .5.5v3h3a.5.5 0 0 1 0 1h-3v3a.5.5 0 0 1-1 0v-3h-3a.5.5 0 0 1 0-1h3v-3A.5.5 0 0 1 8 4"/>
                </svg>
            </span>
        </button>
    </div>
    <div class="table-options">
        <div>
            <label for="select-page-size">Elements per page</label>
            <select name="select-page-size" id="select-page-size">
                <option value="5" selected disabled>5</option>
            </select>
        </div>
        <div>
            <form v-on:submit.prevent="subSearchTutor">
                <div v-if="boolSearchDone">
                    <button type="button" class="cancel button-align" v-on:click="subRealoadTutors"> 
                        Remove
                        <span>
                            <svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" class="bi bi-x-octagon" viewBox="0 0 16 16">
                                <path d="M4.54.146A.5.5 0 0 1 4.893 0h6.214a.5.5 0 0 1 .353.146l4.394 4.394a.5.5 0 0 1 .146.353v6.214a.5.5 0 0 1-.146.353l-4.394 4.394a.5.5 0 0 1-.353.146H4.893a.5.5 0 0 1-.353-.146L.146 11.46A.5.5 0 0 1 0 11.107V4.893a.5.5 0 0 1 .146-.353zM5.1 1 1 5.1v5.8L5.1 15h5.8l4.1-4.1V5.1L10.9 1z"/>
                                <path d="M4.646 4.646a.5.5 0 0 1 .708 0L8 7.293l2.646-2.647a.5.5 0 0 1 .708.708L8.707 8l2.647 2.646a.5.5 0 0 1-.708.708L8 8.707l-2.646 2.647a.5.5 0 0 1-.708-.708L7.293 8 4.646 5.354a.5.5 0 0 1 0-.708"/>
                            </svg>
                        </span>
                    </button>
                </div>
                <div v-else>
                    <label for="search">Search</label>
                    <div class="center-box">
                        <input name="search" id="search" type="text" class="search-input"  v-model="strSearch" required>
                        
                        <button type="submit" class="border-button button-align search-button">
                            <svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" class="bi bi-search" viewBox="0 0 16 16">
                                <path d="M11.742 10.344a6.5 6.5 0 1 0-1.397 1.398h-.001q.044.06.098.115l3.85 3.85a1 1 0 0 0 1.415-1.414l-3.85-3.85a1 1 0 0 0-.115-.1zM12 6.5a5.5 5.5 0 1 1-11 0 5.5 5.5 0 0 1 11 0"/>
                            </svg>
                        </button>
                    </div>
                </div>
            </form>
            
        </div>
    </div>
    <div class="">
        <div class="table-responsive-sm">
            <LoaderComponent v-if="boolIsLoading" />
            <table class="table align-middle table-color" v-else>
                <thead>
                    <tr>
                        <th>
                            Identification
                        </th>
                        <th>
                            Name
                        </th>
                        <th>
                            Surename
                        </th>
                        <th>
                            Gender
                        </th>
                        <th>
                            Bachelors
                        </th>
                        <th>
                            Status
                        </th>
                        <th class="d-flex justify-content-center">
                            Actions
                        </th>
                    </tr>
                </thead>
                <tbody class="tbody-color">
                    <tr v-for="tutor in arrTutors" :key="tutor.intPk">
                        <td>{{ tutor.strIdentification }}</td>
                        <td>{{ tutor.strName }}</td>
                        <td>{{ tutor.strSurename }}</td>
                        <td>{{ tutor.byteGender == 1 ? 'M' : 'F' }}</td>
                        <td>{{ tutor.strBachelors }}</td>
                        <td>{{ tutor.boolActive ? 'Active' : 'Inactive' }}</td>
                        <td>
                            <div class="actions button-align gap-actions-buttons">
                                <button type="button" class="border-button button-align pad-8" v-on:click="editTutor(tutor.intPk)">
                                    Edit
                                    <svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" class="bi bi-pencil-square" viewBox="0 0 16 16">
                                        <path d="M15.502 1.94a.5.5 0 0 1 0 .706L14.459 3.69l-2-2L13.502.646a.5.5 0 0 1 .707 0l1.293 1.293zm-1.75 2.456-2-2L4.939 9.21a.5.5 0 0 0-.121.196l-.805 2.414a.25.25 0 0 0 .316.316l2.414-.805a.5.5 0 0 0 .196-.12l6.813-6.814z"/>
                                        <path fill-rule="evenodd" d="M1 13.5A1.5 1.5 0 0 0 2.5 15h11a1.5 1.5 0 0 0 1.5-1.5v-6a.5.5 0 0 0-1 0v6a.5.5 0 0 1-.5.5h-11a.5.5 0 0 1-.5-.5v-11a.5.5 0 0 1 .5-.5H9a.5.5 0 0 0 0-1H2.5A1.5 1.5 0 0 0 1 2.5z"/>
                                    </svg>
                                </button>
                                <!-- <button type="button" class="cancel button-align pad-8" v-on:click="disableWorkshop(workshop.intPk)">
                                    Disable
                                    <svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" class="bi bi-ban" viewBox="0 0 16 16">
                                        <path d="M15 8a6.97 6.97 0 0 0-1.71-4.584l-9.874 9.875A7 7 0 0 0 15 8M2.71 12.584l9.874-9.875a7 7 0 0 0-9.874 9.874ZM16 8A8 8 0 1 1 0 8a8 8 0 0 1 16 0"/>
                                    </svg>
                                </button> -->
                            </div>
                        </td>
                    </tr>
                </tbody>
            </table>
            <div class="pagination">
                <button @click="changePage(intPageNumber - 1)" :disabled="intPageNumber === 1">
                    Prev
                </button>
                <span>{{ intPageNumber }} / {{ intTotalPages }}</span>
                <button @click="changePage(intPageNumber + 1)" :disabled="intPageNumber === intTotalPages">
                    Next
                </button>
            </div>
        </div>
    </div>


    <!-- <ModalComponent :SelectedObj="objSelectedElement" 
        @display-modal="hideModal" v-if="boolShowModal" /> -->
</template>


<style scoped>
.form-row{
  display: flex;
  gap: 8px;
  justify-content: center;
}
.form-switch{
    display: flex;
    margin: 0px;
    flex-direction: column;
    justify-content: center;
    align-items: center;
}
.switch {
    position: relative;
    display: inline-block;
    width: 40px !important;
    height: 24px;
}

    .switch input {
        opacity: 0;
        width: 0;
        height: 0;
    }

input:checked + .slider {
    background-color: #6cc091;
}

input:focus + .slider {
    box-shadow: 0 0 1px #6cc091;
}

input:checked + .slider:before {
    background-color: white;
    -webkit-transform: translateX(13px);
    -ms-transform: translateX(13px);
    transform: translateX(13px);
}

/* Rounded sliders */
.slider.round {
    position: absolute;
    cursor: pointer;
    top: 0;
    left: 0;
    right: 0;
    bottom: 0;
    background-color: #ccc;
    -webkit-transition: .4s;
    transition: .4s;
    border-radius: 34px;
}


    .slider.round:before {
        position: absolute;
        content: "";
        height: 18px;
        width: 18px;
        left: 4px;
        bottom: 3px;
        background-color: #38444d;
        -webkit-transition: .4s;
        transition: .4s;
        border-radius: 50%;
    }

.table-options{
    display: flex;
    justify-content: space-between;
    align-items: center;
}
.letter-space{
    letter-spacing:  2px;
}
.gap-actions-buttons{
    gap: 10px;
}
.center-box {
  display: flex;
  align-items: center;
  justify-content: center;
}
.pagination{
    justify-content: center;
    align-items: center;
}
.table-color{
    color: #c7ffc2;
}
.thead-color {
    color: #c7ffc2;
}

.tbody-color{
    color: whitesmoke;
}

.search-button{
    border-radius: 0px 10px 10px 0px;
    margin: 0px;
    height: 2.65em;
    padding: 5px;
}

.search-input{
    border-radius: 10px 0px 0px 10px !important;
}

.modal-display{
    display: block;
}

.modal-content{
    background-color: #100f14d4;
    background-clip: padding-box;
    border: 1px solid rgba(0, 0, 0, .2);
    width: 75%;
}

.modal-footer{
    justify-content: space-around;
}

.modal-form{
    display: flex;
    align-items: center;
    justify-content: space-evenly;
    flex-direction: column;
}

</style>