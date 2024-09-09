import axios from "axios";
const strApiUrl = import.meta.env.VITE_API_URL;

export default {
    //------------------------------------------------------------------------------------
    async getMaterialConfirmation(
        arrstrMaterial_I
    ) {
        let strArray = "?"
        if (
            arrstrMaterial_I.length == 1
        ) {
            strArray = strArray + "arrstrNumCtrlInt=" + arrstrMaterial_I[0]
        }
        else {
            for (let intIndex = 0; intIndex < arrstrMaterial_I.length; intIndex++) {
                if (
                    intIndex == 0
                ) {
                    strArray = strArray + "arrstrNumCtrlInt=" + arrstrMaterial_I[intIndex];
                }
                else {
                    strArray = strArray + "&arrstrNumCtrlInt=" + arrstrMaterial_I[intIndex];
                }
            }
        }
        let strUrl = strApiUrl + "/Material/GetAllMaterial" + strArray;
        let objApiResponse = await axios.get(strUrl);
        let objResponse = objApiResponse.data

        return objResponse;
    },

    //------------------------------------------------------------------------------------
    async subGetPaginatedMaterial(
        objPages_I
    ) {
        let strUrl = strApiUrl +"/Material/GetPaginatedMaterial?" 
            +"intPageNumber=" + objPages_I.intPageNumber +
            "&intPageSize="+ objPages_I.intPageSize 
            +"&strSearch="+ (objPages_I.strSearch == null ? "" : objPages_I.strSearch);

        let objApiResponse = await axios.get(strUrl);

        let objWorkshops = null;
        if(
            objApiResponse.data.intStatus == 200
        ) {
            objWorkshops = objApiResponse.data.objResponse
        }

        return objWorkshops
    },

    //------------------------------------------------------------------------------------
    async subSetMaterial(
        objMaterial_I
    ) {
        let strUrl = strApiUrl +"/Material/" +
            (
                (
                    objMaterial_I.strNumCtrlInt == null || 
                    objMaterial_I.strNumCtrlInt == ""
                ) ? "SetMaterial" : "UpdateMaterial");
            
        let objResponse = {
            intStatus : 400,
            strUserMessage : ""
        };
        try{
            let objApiResponse = await axios.post(strUrl,objMaterial_I);

            if(
                objApiResponse.data.intStatus == 200
            ) {
                objResponse = objApiResponse.data;
            }
            else
            {
                objResponse.strUserMessage = objApiResponse.data.strUserMessage
            }

        }
        catch(Ex){
            objResponse.strUserMessage = "Something wrong";
            console.error("Error while setting material", Ex)
        }

        return objResponse;
        
    },

    //------------------------------------------------------------------------------------
}