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
        let strUrl = strApiUrl+"/Student/GetStudent/"+strNmCta_I;
        let objApiResponse = await axios.get(strUrl);
        
        let objResponse = objApiResponse.data
                
        return objResponse;
    },

    //------------------------------------------------------------------------------------
    async getMaterialConfirmation(       
        arrstrMaterial_I
    ){
        let strArray = "/"
        if(
            arrstrMaterial_I.length == 1
        ) {
            strArray = strArray + "arrstrNumCtrlInt="+arrstrMaterial_I[0]
        } 
        else
        {
            for (let intIndex = 0; intIndex < arrstrMaterial_I.length; intIndex++) {
                if(
                    intIndex==0
                ){
                    strArray = strArray + "arrstrNumCtrlInt="+arrstrMaterial_I[intIndex];
                }
                else
                {
                    strArray = strArray + "&arrstrNumCtrlInt="+arrstrMaterial_I[intIndex];
                }
            }
        }
        let strUrl = strApiUrl+"/Material/GetAllMaterial"+strArray;
        let objApiResponse = await axios.get(strUrl);
        let objResponse = objApiResponse.data
                
        return objResponse;
    },
    //------------------------------------------------------------------------------------
    
    

}