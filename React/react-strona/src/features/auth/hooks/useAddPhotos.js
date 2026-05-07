import { useNavigate } from "react-router-dom";
import { useState } from "react";
import { useApi } from '../apiConnection/Connection';
export function useAddPhotos(){
    const navigate = useNavigate();
    const [loading, setLoading] = useState(false);
    const [error, setError] = useState("");
    const api = useApi();
    async function uploadPhotos(albumId, files) {
        try{
            setLoading(true);
            setError("");
            const formData = new FormData();

            formData.append("AlbumId", albumId);

            for (let i = 0; i < files.length; i++) {
                formData.append("Files", files[i]);
            }

            const response = await api.post(
                "/api/photo/addPhoto",
                formData,
                {
                    headers: {
                        "Content-Type": "multipart/form-data"
                    }
                }
            );

            return response.data;
        } catch (err) {
            console.error(err);
            setError(err.message || "Błąd uploadu zdjęć");
        } finally {
            setLoading(false);
        }
    }

    return { uploadPhotos, loading, error };
}