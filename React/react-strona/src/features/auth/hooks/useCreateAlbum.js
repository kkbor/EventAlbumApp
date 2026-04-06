import { useNavigate } from "react-router-dom";
import { useState } from "react";
import { useApi } from '../apiConnection/Connection';
export function useCreateAlbum(){
    const navigate = useNavigate();
    const [loading, setLoading] = useState(false);
    const [error, setError] = useState("");
    const api = useApi();
    async function createAlbum(name, startDate, endDate){
        try{
            setLoading(true);
            setError("");
            const start = new Date(startDate).toISOString();
            const end = new Date(endDate).toISOString();
            const result = await api.post('/api/Album/create',{name,start,end});
            navigate("/Home");
        }catch (err){
            console.error(err);
        }finally{
            setLoading(false);
        }

    }
    return {createAlbum,loading,error}
}