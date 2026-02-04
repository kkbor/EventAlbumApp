import '../style/home_page.css'
import { Link, Routes, Route } from 'react-router-dom';
function Home_page(){
    return (
    <div className='home-page-hp'>
      <div className='bar-home-hp'>
        Witaj użytkowniku
      </div>
      <div className='block-hp'>
        Wydarzenia
      </div>
      <div className='block-hp'>
        Albumy
      </div>
    </div>
  )
}
export default Home_page