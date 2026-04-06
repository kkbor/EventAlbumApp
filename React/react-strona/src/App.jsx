import { Routes, Route, Navigate } from 'react-router-dom'
import Login from './pages/login&register/Login.jsx'
import Register from './pages/login&register/Register.jsx'
import Home_page from './pages/home_page/Home_page.jsx'
import Home from './pages/home/Home.jsx'
import { useAuth } from './features/auth/context/AuthContext';
import CreateAlbum from './pages/home_page/CreateAlbum.jsx'
import AlbumFromUrl from './pages/home_page/Albums/AlbumFromUrl.jsx'

function App() {
  const { isLoggedIn, loading } = useAuth();
  if (loading) return <div>Loading...</div>; 
  return (
    <Routes>
      <Route path="/" element={<Home />} />
      <Route path="/Login" element={<Login />} />
      <Route path="/Register" element={<Register />} />
      <Route
        path="/Home"
        element={isLoggedIn ? <Home_page /> : <Navigate to="/Login" />}
      />
      <Route path="/Create" element={<CreateAlbum/>}/>
      <Route path="/album/:token" element={<AlbumFromUrl />} />
    </Routes>
  );
}
export default App
