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


}
