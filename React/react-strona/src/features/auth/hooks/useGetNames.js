import { useState, useEffect } from "react";
import { useApi } from '../apiConnection/Connection';
export function useGetNames() {
    const api = useApi();
    const [namesAlbums, setNamesAlbums] = useState(null);
    const [loading, setLoading] = useState(false);
    const [error, setError] = useState("");
    
    useEffect(() => {
        async function fetchAlbums() {
            setLoading(true);
            setError("");
            try {
                const response = await api.get("/api/album/names");
                setNamesAlbums(response.data); 
            } catch (err) {
                console.error(err);
                setError(err.message || "Błąd pobierania albumów");
            } finally {
                setLoading(false);
            }
        }

    fetchAlbums();
  }, []);

  return { namesAlbums , loading, error };
}