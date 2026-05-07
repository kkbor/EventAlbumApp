
import '../home_page.css';
import '../UserPage/userPage.css'
import { useUserData } from '../../../features/auth/hooks/useUserData';
import { useNavigate } from 'react-router-dom';
import { useEffect, useState } from 'react';
import { UserForm } from '../../../components/UserForm';


function UserPage(){
    const navigate = useNavigate();
    const {userData, loading, error} = useUserData();
    const [mode, setMode] = useState("view");
    const [formData, setFormData] = useState(null);
    useEffect(() => {
      if (userData) setFormData(userData);
    }, [userData]);
    if (loading) return <div className='home-page-hp'>Ładowanie...</div>;
    if (error) return <div className='home-page-hp'>Błąd: {error}</div>;
    if (!userData) return <div className='home-page-hp'>Nie znaleziono użytkownika.</div>;
    const handleChange = (e) => {
      const { name, value } = e.target;
      setFormData((prev) => ({ ...prev, [name]: value }));
    };
    const handleSubmit = (e) => {
      e.preventDefault();

      if (mode === "edit") {
        console.log("Zapis do API:", formData);
        setMode("view");
      }
    };
    console.log(userData);
    return (
    <div className='home-page-hp'>
      <div className='bar-home-hp'>
        Witaj użytkowniku
        <button className="btncircleUser">
          👤
        </button>
      </div>
      
     
      <div className="block-hp">
         <div className="user-content">
          <div className="user-data">
            <UserForm
              mode={mode}
              formData={userData}
              onChange={handleChange}
              onSubmit={handleSubmit}
            />
            <button
              className="btn-us"
              onClick={() => setMode(mode === "view" ? "edit" : "view")}
            >
              {mode === "view" ? "Edytuj" : "Anuluj"}
            </button>
            <button className="btn-us" >Zmień hasło</button>
            <button className="btn1-us"  onClick={() => navigate("/")}>Wyloguj się</button>
          </div>
          <div className="user-photos">
            <h2>Zdjęcia</h2>

            {userData.photos?.length > 0 && (
              <div className="photo-grid">
                {userData.photos.map((photo) => {
                  console.log("PHOTO:", photo);
                  console.log("PATH:", photo.path);

                  return (
                    <img
                      key={photo.id}
                      src={photo.path}
                      alt="user"
                      className="photo-item"
                    />
                  );
                })}
              </div>
            )}

          </div>
        </div>
        
      </div>
      {error && <p className="error">{error}</p>}
      
      
    </div>
  )
}
export default UserPage

