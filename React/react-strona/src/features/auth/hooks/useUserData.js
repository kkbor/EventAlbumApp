import { useState, useEffect } from "react";
import { useApi } from '../apiConnection/Connection';

export function useUserData() {
    const api = useApi();
    const [userData, setUserData] = useState(null);
    const [loading, setLoading] = useState(false);
    const [error, setError] = useState("");
    
    useEffect(() => {
        async function fetchUserData() {
            
            setLoading(true);
            setError("");
            try {

                const response = await api.get("/api/User/user");

                setUserData(response.data);
            } catch (err) {
                console.error(err);
                setError(err.message || "Błąd pobierania albumów");
            } finally {
                setLoading(false);
            }
        }

    fetchUserData();
  }, []);

  return { userData, loading, error };
}//14:13