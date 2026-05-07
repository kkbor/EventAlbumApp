import { useState } from "react";
import { useCreateAlbum } from "../../features/auth/hooks/useCreateAlbum";
import './home_page.css'
import { PhotoForm } from "../../../components/PhotoForm";

function AddPhotos(){
    const [formData,setformData] = useState({
        name: "",
        startDate: "",
        endDate: "",
    })
    const {createAlbum, loading, error} = useCreateAlbum();
    const handleChange = (e) =>{
        setformData({
            ...formData,
            [e.target.name]: e.target.value
        })
    }
    const handleSubmit = (e) => {
        e.preventDefault();  
        createAlbum(formData.name,formData.startDate,formData.endDate);
    }
    return(
        <div className='home-page-hp'>
            <div className='bar-home-hp'>
                Witaj użytkowniku
                <button className="btncircleUser" onClick={() => navigate("/User")}>
                👤
                </button>
            </div>
                <PhotoForm
                    formData={formData}
                    onChange={handleChange}
                    onSubmit={handleSubmit}
                    loading={loading}
                    error ={error}
                    />
            </div>
        
    )
    
}
export default AddPhotos;