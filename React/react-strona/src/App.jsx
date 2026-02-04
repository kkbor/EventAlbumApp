import './App.css'
import './index.css'
import { Link, Routes, Route } from 'react-router-dom';
import Login from './pages/Login.jsx';
import Register from './pages/Register.jsx';
import Home_page from './pages/Home_page.jsx';
function Home() {
  return (
    <div className='home-page'>
      <div className='top-buttons'>
        <Link to = "/Login">
        <button className="btn1">log in</button>
        </Link>
        <Link to = "/Register">
        <button className="btn">sign up</button>
        </Link>
      </div>
      <div className='bar-home'>
        Fotoksięga
      </div>
      <div className='block'>
        Nadchodzące
      </div>
    </div>
  )
}

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
