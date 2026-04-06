import { useState, useEffect } from "react";
import { useApi } from '../apiConnection/Connection';
export function useAlbums() {
    const api = useApi();
    const [activeAlbums, setActiveAlbums] = useState([]);
    const [endedAlbums, setEndedAlbums] = useState([]);
    const [loading, setLoading] = useState(false);
    const [error, setError] = useState("");
    
    useEffect(() => {
        async function fetchAlbums() {
            
            setLoading(true);
            setError("");
            try {

                const activeResponse = await api.get("/api/album/active");
                const endedResponse = await api.get("/api/album/ended");

                setActiveAlbums(activeResponse.data || []); 
                setEndedAlbums(endedResponse.data || []);
            } catch (err) {
                console.error(err);
                setError(err.message || "Błąd pobierania albumów");
            } finally {
                setLoading(false);
            }
        }

    fetchAlbums();
  }, []);

  return { activeAlbums, endedAlbums, loading, error };
}//14:13