import { Routes, Route } from 'react-router-dom'
import Login from './pages/login&register/Login.jsx'
import Register from './pages/login&register/Register.jsx'
import Home_page from './pages/Home_page.jsx'
import Home from './pages/home/Home.jsx'

function App() {
  return (
    <Routes>
      <Route path="/" element={<Home />} />
      <Route path="/Login" element={<Login />} />
      <Route path="/Register" element={<Register />} />
      <Route path="/Home" element={<Home_page />} />
    </Routes>
  );
}
export default App
