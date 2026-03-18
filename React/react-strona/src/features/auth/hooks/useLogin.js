import { useNavigate } from "react-router-dom";
import { useApi } from '../apiConnection/Connection';
import { useState } from 'react';
import { useAuth } from '../context/AuthContext';
function useLogin(){
    const navigate = useNavigate();
    const [loading, setLoading] = useState("");
    const [error, setError] = useState("");
    const { saveToken } = useAuth();
    const api = useApi();
    async function login(email, password) {
    try {
      setLoading(true);
      setError("");
      const response = await api.post("/api/User/login", { email, password });

      if (response.data?.token) {
        saveToken(response.data?.token); // zapis do Context i localStorage
        navigate("/Home");
      } else {
        setError("Brak tokena w odpowiedzi");
      }
    } catch (err) {
      console.error(err);
      setError("Błąd logowania");
    } finally {
      setLoading(false);
    }
  }
    return{login,loading,error}
}
export default useLogin;