import { useState } from "react";
import { useCreateAlbum } from "../../features/auth/hooks/useCreateAlbum";
import './home_page.css'
import { AlbumForm } from "../../components/AlbumForm";

function CreateAlbum(){
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
                <button className="btncircleUser">
                👤
                </button>
            </div>
                <AlbumForm
                    formData={formData}
                    onChange={handleChange}
                    onSubmit={handleSubmit}
                    loading={loading}
                    error ={error}
                    />
            </div>
        
    )
    
}
export default CreateAlbum;