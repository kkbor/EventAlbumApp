import { useNavigate } from "react-router-dom";
import { useApi } from '../apiConnection/Connection';
import { useState } from 'react';
function useRegister(){
    const navigate = useNavigate();
    const [loading, setLoading] = useState("");
    const [error, setError] = useState("");
    const api = useApi();
    async function register(name,surname,email,password){
        try{
            setLoading(true);
            setError("");
            const response = await api.post("/api/User/register",{name, surname,email,password});
            navigate("/Login");
        }catch (err){
            console.error(err);
        }finally{
            setLoading(false);
        }
    }
    return{register,loading,error}
}
export default useRegister;