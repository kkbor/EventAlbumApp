import { useParams } from 'react-router-dom';
import { useAlbumFromUrl } from '../../../features/auth/hooks/useAlbumFromUrl';

import '../home_page.css';


function AlbumFromUrl(){
    const { token } = useParams();
    const { album, loading, error } = useAlbumFromUrl(token);
    if (loading) return <div className='home-page-hp'>Ładowanie...</div>;
    if (error) return <div className='home-page-hp'>Błąd: {error}</div>;
    if (!album) return <div className='home-page-hp'>Nie znaleziono albumu.</div>;
    return (
    <div className='home-page-hp'>
      {loading && <p>Ładowanie...</p>}
      <div className='bar-home-hp'>
        Witaj na evencie {album.name}
      </div>
      
      <div className="block-hp">
        <h1>{album.name}</h1>
        <p>Start: {album.start}</p>
        <p>Koniec: {album.end}</p>
        <button className="btn-hp" >Przejrzyj aktualne zdjęcia</button>
        <button className="btn-hp" >Dodaj wspomienie</button>
      </div>
      {error && <p className="error">{error}</p>}
      
      
    </div>
  )
}
export default AlbumFromUrl

