import { useEffect, useState, useCallback } from "react";
import { useApi } from "../apiConnection/Connection";

export function useAlbumFromUrl(token){
    const api = useApi();
    const [album, setAlbum]=useState(null);
    const [loading, setLoading] = useState(false);
    const [error, setError] = useState("");
    const fetchAlbums = useCallback(async () => {
        if (!token) return;

        try {
            setLoading(true);
            setError("");
            console.log("Wysyłam zapytanie dla tokenu:", token);
            
            // Sprawdź czy ścieżka w api.get pasuje do Route w Controllerze
            const response = await api.get(`/api/Album/by-qr/${token}`); 
            
            // WAŻNE: .NET zwraca obiekt ApiResponse, więc dane są w response.data
            setAlbum(response.data); 
        } catch (err) {
            console.error("Błąd ładowania:", err);
            setError(err.message || "Błąd pobierania albumu");
        } finally {
            setLoading(false);
        }
    }, []);

    // TO URUCHAMIA ZAPYTANIE
    useEffect(() => {
        fetchAlbums();
    }, [fetchAlbums]);

    return { album, loading, error };
}