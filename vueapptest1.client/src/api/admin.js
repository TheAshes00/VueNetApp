import axios from "axios"
const strApiUrl = import.meta.env.VITE_API_URL;

export default {
    //------------------------------------------------------------------------------------
    async objGetManagers(){
        let arrobjManagers = [];
        try{
            let strUrl = strApiUrl+"/Admin/GetAdmins";
            const objApiResponse = await axios.get(strUrl);
            if(
                objApiResponse.data.intStatus == 200
            )
            {
                arrobjManagers = objApiResponse.data.objResponse;
            }

            return arrobjManagers;
        } catch(Ex){
            console.error("Error while fetching info: ", Ex)
            return arrobjManagers;
        }
        
    },

    //------------------------------------------------------------------------------------
    async subLoginAdmin(
        objAdmin_I
    ){
        let objResponse = {
            intStatus: 400,
            strUserMessage: "Invalid username or password"
        }
        try{
            let strUrl = strApiUrl + "/Admin/LoginAdmin";

            let objApiResponse = await axios.post(strUrl,objAdmin_I);

            if(
                objApiResponse.data.intStatus == 200
            ) {
                objResponse = objApiResponse.data;
            }
        }catch(Ex){
            console.error('Error while loggin in: ', Ex)
        }
        

        return objResponse;
    },

    //--------------------------------------------------------------------------------
    async subGetPaginatedWorshops(
        objPages_I
    ) {
        let strUrl = strApiUrl +"/Workshop/GetPaginatedWorkshops?" 
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
    async subSetWorkshop(
        objWorkshop_I
    ) {
        let strUrl = strApiUrl +"/Workshop/" +
            (objWorkshop_I.intPk == null ? "SetWorkshop" : "UpdateWorkshop");
            
        let objResponse = {
            intStatus : 400,
            strUserMessage : ""
        };
        try{
            let objApiResponse = await axios.post(strUrl,objWorkshop_I);

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
            console.error("Error while setting workshop", Ex)
        }

        return objResponse;
        
    },

    //--------------------------------------------------------------------------------
    async subGetGetAllActiveWorkshops(
        //
    ) {
        let strUrl = strApiUrl +"/Workshop/GetAllActiveWorkshops";

        let objApiResponse = await axios.get(strUrl);

        let arrobjWorkshops = [];
        if(
            objApiResponse.data.intStatus == 200
        ) {
            arrobjWorkshops = objApiResponse.data.objResponse
        }

        return arrobjWorkshops
    },

    //--------------------------------------------------------------------------------
    async subGetStudentReport(
        objReportInfo_I
    ) {
        let strUrl = strApiUrl + "/Admin/GetStudentReport?strNmCta="+
            objReportInfo_I.strNmCta +"&dateStart="+objReportInfo_I.dateStart +
            "&dateEnd="+objReportInfo_I.dateEnd;

        let objApiResponse = await axios.get(strUrl);

        let objResponse = null;

        if (
            objApiResponse.data.intStatus == 200
        ) {
            objResponse = objApiResponse.data;
        }

        return objResponse;
    },

    //--------------------------------------------------------------------------------
    async subGetTutorReport(
        objReportInfo_I
    ) {
        let strUrl = strApiUrl + "/Admin/GetTutorReport?intPkTutor="+
            objReportInfo_I.intPkTutor +"&dateStart="+objReportInfo_I.dateStart +
            "&dateEnd="+objReportInfo_I.dateEnd;

        let objApiResponse = await axios.get(strUrl);

        let objResponse = null;

        if (
            objApiResponse.data.intStatus == 200
        ) {
            objResponse = objApiResponse.data;
        }

        return objResponse;
    }

    //--------------------------------------------------------------------------------


}
