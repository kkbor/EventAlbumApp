import google from '../../assets/google.png'
import { AuthForm } from '../../components/AuthForm';
import useRegister from '../../features/auth/hooks/useRegister';
import './login.css'
import { useState } from 'react';
function Register() {
    const [formData, setformData] = useState({
        name: "",
        surname: "",
        email: "",
        password: ""
    })
    const {register, loading, error} = useRegister();
    const handleChange = (e) =>{
        setformData({
            ...formData,
            [e.target.name]: e.target.value
        })
    }
    const handleSubmit = (e) => {
        e.preventDefault();  
        register(formData.name,formData.surname,formData.email,formData.password);
    }
    return(
        <div className='login-page'>
            <div className="block-login">

                <AuthForm
                    mode = "register"
                    formData={formData}
                    onChange={handleChange}
                    onSubmit={handleSubmit}
                    loading={loading}
                    error ={error}
                />
                <button className="btn-google">
                <img src={google} alt="Google" className="google-icon" />
                    Kontynuuj z Google
                </button>
            </div>
        </div>
    )

}

export default Register