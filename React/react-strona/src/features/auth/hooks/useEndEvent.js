import { useNavigate } from "react-router-dom";
import { useApi } from "../apiConnection/Connection";
import { useState } from "react";
export function useEndEvent(){
    const navigate = useNavigate();
    const api = useApi();
    const [loading, setLoading] = useState(false);
    const [error, setError] = useState("");
     async function endEvent(id) {
        try{
            setLoading(true);
            setError("");
            const response = api.patch(`/api/Album/endEvent/${id}`)
            navigate("/Home");
        }catch (err){
            console.error("Błąd ładowania:", err);
            setError(err.message || "Błąd pobierania albumu");
        }finally{
            setLoading(false);
        }
    }
    return { endEvent, loading, error };
}