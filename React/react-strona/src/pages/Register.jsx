import '../index.css'
import google from '../assets/google.png'
import '../style/Login.css'
import { useNavigate } from "react-router-dom";
import { api } from '../apiConnection/Connection';
import { useState } from 'react';
function Register() {
     const navigate = useNavigate();
        const [name, setName] = useState("");
        const [surname, setSurname] = useState("");
        const [email, setEmail] = useState("");
        const [password, setPassword] = useState("");
        const [error, setError] = useState("");
        const handleRegister = async() => {
            try{
                const response = await api.post("/api/User/register",{name, surname,email,password});
                console.log("Register succesful");
                navigate("/login");
            }catch(err){
                console.error(err);
                setError(err.message);
            }
        }
    return(
        <div className='login-page'>
            <div className='block-login'>
                Rejestracja
                <input className="data" type="name" placeholder="Name" value={name} onChange={(e) => setName(e.target.value)}/>
                <input className='data'type="surname" placeholder="Surname"  value={surname} onChange={(e) => setSurname(e.target.value)}/>
                <input className="data" type="email" placeholder="Email" value={email} onChange={(e) => setEmail(e.target.value)}/>
                <input className='data'type="password" placeholder="Password"  value={password} onChange={(e) => setPassword(e.target.value)}/>
                
                    <button className="btn-login" onClick={handleRegister}>zaloguj się</button>
                
                
                 
                <button className="btn-google">
                <img src={google} alt="Google" className="google-icon" />
                Kontynuuj z Google
                </button>
            </div>
        </div>
    )

}

export default Register