import axios from "axios"
import support from "./support";
const strApiUrl = import.meta.env.VITE_API_URL;

export default {
    //------------------------------------------------------------------------------------
    async objGetPaginatedTutors(
        objPages_I
    ){
        let strUrl = strApiUrl +"/Tutor/GetPaginatedTutors?" 
            +"intPageNumber=" + objPages_I.intPageNumber +
            "&intPageSize="+ objPages_I.intPageSize 
            +"&strSearch="+ (objPages_I.strSearch == null ? "" : objPages_I.strSearch);

        let objApiResponse = await axios.get(strUrl, support.strGetToken());

        let objWorkshops = null;
        if(
            objApiResponse.data.intStatus == 200
        ) {
            objWorkshops = objApiResponse.data.objResponse
        }

        return objWorkshops
    },

    //------------------------------------------------------------------------------------
    async subSetTutor(
        objTutor_I
    ) {
        let strUrl = strApiUrl +"/Tutor/" +
            (objTutor_I.intPk == null ? "SetTutor" : "UpdateTutor");
            
        let objResponse = {
            intStatus : 400,
            strUserMessage : ""
        };

        try {
            let objApiResponse = await axios.post(
                strUrl, 
                objTutor_I, 
                support.strGetToken()
            );

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
            console.error("Error while setting tutor", Ex);
            objResponse.strUserMessage = "Invalid Data"
        }

        return objResponse;
        
    },

    //--------------------------------------------------------------------------------
    async subGetGetAllActiveTutors(
        //
    ) {
        let strUrl = strApiUrl +"/Tutor/GetAllActiveTutors";

        let objApiResponse = await axios.get(strUrl);

        let arrobjTutor = [];
        if(
            objApiResponse.data.intStatus == 200
        ) {
            arrobjTutor = objApiResponse.data.objResponse
        }

        return arrobjTutor
    },

    //--------------------------------------------------------------------------------
    async subGetGetAllTutors(
        //
    ) {
        let strUrl = strApiUrl +"/Tutor/GetAllTutors";

        let objApiResponse = await axios.get(strUrl);

        let arrobjTutor = [];
        if(
            objApiResponse.data.intStatus == 200
        ) {
            arrobjTutor = objApiResponse.data.objResponse
        }

        return arrobjTutor
    },

    //------------------------------------------------------------------------------------
}