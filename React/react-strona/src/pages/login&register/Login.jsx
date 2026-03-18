import google from '../../assets/google.png'
import { AuthForm } from '../../components/AuthForm';
import useLogin from '../../features/auth/hooks/useLogin';
import './login.css'
import { useState } from 'react';

function Login() {
    const [formData, setformData] = useState({
        email: "",
        password: ""
    });
    const {login, loading, error}= useLogin();
    const handleChange = (e) =>{
        setformData({
            ...formData,
            [e.target.name]: e.target.value
        })
    }
    
    const handleSubmit = (e) => {
        e.preventDefault();
        login(formData.email, formData.password);
    };
    return(
        <div className='login-page'>
            <div className="block-login">
                <AuthForm
                    mode = "login"
                    formData={formData}
                    onChange={handleChange}
                    onSubmit={handleSubmit}
                    loading={loading}
                    error={error}
                />
                <button className="btn-google">
                <img src={google} alt="Google" className="google-icon" />
                    Kontynuuj z Google
                </button>
            </div>
        </div>
    )

}

export default Login