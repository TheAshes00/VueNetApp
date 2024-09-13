import axios from "axios";
const strApiUrl = import.meta.env.VITE_API_URL;

export default {
    //------------------------------------------------------------------------------------
    async subGetPaginatedTutorWorkshop(
        objPages_I
    ) {
        let strUrl = strApiUrl +"/TutorWorkshop/GetPaginatedTutorWorkshops?" 
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
    async subSetTutorWorkshop(
        objTutorWorkshop_I
    ) {
        //                                                  // Set timespan format
        
        let strUrl = strApiUrl +"/TutorWorkshop/" +( 
            objTutorWorkshop_I.intPk == null ? "SetTutorWorkshop" : "UpdateTutorWorkshop")
            
        let objResponse = {
            intStatus : 400,
            strUserMessage : ""
        };
        try{
            let objApiResponse = await axios.post(strUrl,objTutorWorkshop_I);

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
            console.error("Error while setting tutor workshop", Ex)
        }

        return objResponse;
        
    },

    //------------------------------------------------------------------------------------
    async subGetFilteredTutors(
        intPkWorkshop_I
    ){
        let strUrl = strApiUrl +"/TutorWorkshop/GetWorkshopTutor/"+intPkWorkshop_I;

        let objApiResponse = await axios.get(strUrl);

        return objApiResponse.data
    },

    //------------------------------------------------------------------------------------
}