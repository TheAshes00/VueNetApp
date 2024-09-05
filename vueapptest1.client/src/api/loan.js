import axios from "axios";
const strApiUrl = import.meta.env.VITE_API_URL;

export default{
    //------------------------------------------------------------------------------------
    async setMaterialLoan(
        arrstrNumCtrlInt_I
    ){  
        try {
            let strUrl = strApiUrl + "/Loan/RegisterLoan";
            const objApiResponse = await axios.post(strUrl,arrstrNumCtrlInt_I);

            if (
                objApiResponse.data.intStatus == 200
            ) {
                console.log("Api Response: ", objApiResponse.data)
            }
            else
            {
                console.log("Not completed: ", objApiResponse.data)
            }
            return objApiResponse.data;
        }catch(
            Ex
        ) {
            console.error("Error: ",Ex);
            return {
                intStatus: 400,
                strUserMessage: "Something wrong"
            }
        }
    },

    //------------------------------------------------------------------------------------
    async arrGetMaterialToReturn(
        strNmCta_I
    ){
        let strUrl = strApiUrl + "/Loan/MaterialToReturn/"+strNmCta_I;

        let objApiResponse = await axios.get(strUrl);

        let objResponse = [];

        if(
            objApiResponse.data.intStatus == 200
        ){
            objResponse = objApiResponse.data.objResponse
        }

        return objResponse;
    },

    //------------------------------------------------------------------------------------
    async subReturnMaterial(
        objMaterialToReturn_I
    ){
        let strUrl = strApiUrl +"/Loan/ReturnMaterial";
        let boolCompleted = false;

        try{
            let objApiResponse = await axios.post(strUrl,objMaterialToReturn_I);
        
            if(
                objApiResponse.data.intStatus == 200
            ) {
                boolCompleted = true;
            }
        }catch(Ex){
            console.error("Error while updating material loan", Ex)
        }
        

        return boolCompleted;
    }
    //------------------------------------------------------------------------------------
}