import { Navigate } from "react-router-dom";

function Protected({children}){

    var token = localStorage.getItem("token");
    if(!token){
        return <Navigate to="/UserLogin"/>
    }
    return children;
}

export default Protected;
                   