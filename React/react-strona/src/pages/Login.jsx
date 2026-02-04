import '../index.css'
import google from '../assets/google.png'
import '../style/Login.css'
import { useNavigate } from "react-router-dom";
import { api } from '../apiConnection/Connection';
import { useState } from 'react';
function Login() {
    const navigate = useNavigate();
    const [email, setEmail] = useState("");
    const [password, setPassword] = useState("");
    const [error, setError] = useState("");
    const handleLogin = async() => {
        try{
            const response = await api.post("/api/User/login",{email,password});
            console.log("Login succesful");
            navigate("/Home");
        }catch(err){
            console.error(err);
            setError(err.message);
        }
    }
    return(
        <div className='login-page'>
            <div className='block-login'>
                Logowanie
                <input className="data" type="email" placeholder="Email" value={email} onChange={(e) => setEmail(e.target.value)} />
                <input className='data'type="password" placeholder="Password" value={password} onChange={(e) => setPassword(e.target.value)}/>
                 <button className="btn-login" onClick={handleLogin}>zaloguj się</button>
                 
                <button className="btn-google">
                <img src={google} alt="Google" className="google-icon" />
                Kontynuuj z Google
                </button>
            </div>
        </div>
    )

}

export default Login