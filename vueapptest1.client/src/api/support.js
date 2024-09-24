export default {
    //------------------------------------------------------------------------------------
    strGetToken(){
        let strToken = sessionStorage.getItem("authToken");
        let config = {
            headers: {
                Authorization: `Bearer ${strToken}`,
                "Content-Type": "application/json",
            },
        };

        return config;
    },

    //------------------------------------------------------------------------------------
    subResetToken(){
        sessionStorage.clear();
    },

    //------------------------------------------------------------------------------------
}