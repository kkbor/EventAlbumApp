import { useState, useEffect } from "react";
import { useApi } from '../apiConnection/Connection';

export function useQR(){
    const [qr, setQr] = useState(null);
    const [loading, setLoading] = useState(false);
    const [error, setError] = useState("");
    const api = useApi();
    async function qrCode(qrToken){
        try{
            setLoading(true);
            setError("");
            const response = await api.getBlob(`/api/album/qr-image/${qrToken}`); 
            const imageUrl = URL.createObjectURL(response);
            setQr(imageUrl);
        }catch(err){
            console.error(err);
            setError(err.message || "Błąd pobierania zdjecia");
        }finally{
             setLoading(false);
        }
        
    }
    return { qr, qrCode, loading, error };

}