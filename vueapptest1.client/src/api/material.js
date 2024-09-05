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
}