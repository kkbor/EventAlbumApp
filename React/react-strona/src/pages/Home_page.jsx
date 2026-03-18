import './home_page.css'
import { AlbumTable } from '../components/AlbumTable.jsx';
import { useAlbums } from '../features/auth/hooks/useActiveAlbums.js';


function Home_page(){
  const { activeAlbums, endedAlbums, loading, error } = useAlbums();

  
    return (
    <div className='home-page-hp'>
      <div className='bar-home-hp'>
        Witaj użytkowniku
        <button className="btncircleUser">
          👤
        </button>
      </div>
      
      {loading && <p>Ładowanie...</p>}
      {error && <p className="error">{error}</p>}
      <AlbumTable title="Albumy" albums={activeAlbums} />
      <AlbumTable title="Archiwum" albums={endedAlbums} />
    </div>
  )
}
export default Home_page

