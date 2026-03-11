import "./Home.css"
import { Link } from "react-router-dom"

function Home() {
  return (
    <div className="home-page">

      <div className="top-buttons">
        <Link to="/login">
          <button className="btn1">log in</button>
        </Link>

        <Link to="/register">
          <button className="btn">sign up</button>
        </Link>
      </div>

      <div className="bar-home">
        Fotoksięga
      </div>

      <div className="block">
        Nadchodzące
      </div>

    </div>
  )
}

export default Home