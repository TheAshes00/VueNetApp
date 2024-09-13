import axios from "axios";
const strApiUrl = import.meta.env.VITE_API_URL;

export default{
    //------------------------------------------------------------------------------------
    async testAxios(        
    ){
        let objApiResponse = await axios.get(strApiUrl+"/WeatherForecast");

        let objResponse = null;
        if (
            objApiResponse.status == 200
        ) {
            objResponse = objApiResponse.data
        }

        console.log("Axios test", objApiResponse)
        return objResponse;
    },

    //------------------------------------------------------------------------------------
    async getAllAcademicEntities(        
    ){
        let strUrl = strApiUrl+"/Student/GetAllAcademicEntities";
        let objApiResponse = await axios.get(strUrl);

        let objResponse = [];
        if (
            objApiResponse.data.intStatus == 200
        ) {
            objResponse = objApiResponse.data.objResponse
        }
        
        return objResponse;
    },

    //------------------------------------------------------------------------------------
    async setNewStudent(       
        objNewUser 
    ){
        let strUrl = strApiUrl+"/Student/SetStudent";
        let objApiResponse = await axios.post(strUrl,objNewUser);

        let objResponse = objApiResponse.data
                
        return objResponse;
    },

    //------------------------------------------------------------------------------------
    async getStudentLogin(       
        strNmCta_I 
    ){
        let strUrl = strApiUrl + "/Student/getStudent/" + strNmCta_I;
        let objApiResponse = await axios.get(strUrl);
        
        let objResponse = objApiResponse.data

        return objResponse;
    },

    //------------------------------------------------------------------------------------
    async setAdminLogin(
        strNmCta_I
    ) {
        let strUrl = strApiUrl + "/Student/GetStudent/" + strNmCta_I;
        let objApiResponse = await axios.get(strUrl);

        let objResponse = objApiResponse.data

        return objResponse;
    },

    //------------------------------------------------------------------------------------
    async setWorkshopAttendance(
        objWorkshopAttendance_I
    ) {
        let strUrl = strApiUrl + "/Workshop/SetWorkshopAttendance";
        
        let objApiResponse = await axios.post(strUrl,objWorkshopAttendance_I);

        let boolCompleted = false;

        if(
            objApiResponse.data.intStatus == 200
        ) {
            boolCompleted = true;
        }

        return boolCompleted;
    },
    //------------------------------------------------------------------------------------
}