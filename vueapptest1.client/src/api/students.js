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
        let objApiResponse = await axios.get(strApiUrl+"/GetAllAcademicEntities");

        let objResponse = [];
        if (
            objApiResponse.status == 200
        ) {
            objResponse = objApiResponse.data
        }

        console.log("Axios test", objApiResponse)
        return objResponse;
    },


    //------------------------------------------------------------------------------------

}