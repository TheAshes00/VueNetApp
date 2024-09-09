<script>
export default{
    props:["SelectedObj"],
    setup(){

    },
    data(){
        return {
            objElement :{
                strName: this.SelectedObj.strName,
                boolActive: this.SelectedObj.boolActive
            }
        }
    },
    methods:{
        hideModal(){
            this.$emit('displayModal',false)
        }
    }
}

</script>
<template>
    <div class="modal" id="exampleModal" >
        <div class="modal-dialog">
            <div class="modal-content">
            <div class="modal-header">
                <h5 class="modal-title" id="exampleModalLabel">Add New Workshop</h5>
                <button type="button" class="btn-close" data-bs-dismiss="modal" 
                    aria-label="Close" v-on:click="hideModal"></button>
            </div>
            <div class="modal-body">
                <form>
                <div class="mb-3">
                    <label for="workshop-name" class="col-form-label">Name:</label>
                    <input type="text" class="form-control" id="recipient-name" 
                        v-model="objElement.strName">
                </div>
                <div class="mb-3">
                    <label for="workshop-active" class="col-form-label">Active:</label>
                    <input type="text" class="form-control" id="recipient-name" 
                        v-model="objElement.strName">
                </div>
                </form>
            </div>
            <div class="modal-footer">
                <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Close</button>
                <button type="button" class="btn btn-primary">Send message</button>
            </div>
            </div>
        </div>
    </div>
</template>