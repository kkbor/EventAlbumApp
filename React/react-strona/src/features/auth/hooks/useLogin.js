import { useNavigate } from "react-router-dom";
import { useApi } from '../apiConnection/Connection';
import { useState } from 'react';
import { useAuth } from "../context/AuthContext";
function useLogin(){
    const navigate = useNavigate();
    const [loading, setLoading] = useState(false);
    const [error, setError] = useState("");
    const api = useApi();
    const { login: loginUser } = useAuth();
    async function login(email, password) {
    try {
      setLoading(true);
      setError("");
      const response = await api.post("/api/User/login", { email, password });

      if (response?.data?.token) {
        const user = response.data; 
        loginUser(user);
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