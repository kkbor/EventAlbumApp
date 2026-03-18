import { Routes, Route, Navigate } from 'react-router-dom'
import Login from './pages/login&register/Login.jsx'
import Register from './pages/login&register/Register.jsx'
import Home_page from './pages/Home_page.jsx'
import Home from './pages/home/Home.jsx'
import { useAuth } from './features/auth/context/AuthContext';

function App() {
  const { isLoggedIn } = useAuth();
  return (
    <Routes>
      <Route path="/" element={<Home />} />
      <Route path="/Login" element={<Login />} />
      <Route path="/Register" element={<Register />} />
      <Route
        path="/Home"
        element={isLoggedIn ? <Home_page /> : <Navigate to="/Login" />}
      />
    </Routes>
  );
}
export default App
