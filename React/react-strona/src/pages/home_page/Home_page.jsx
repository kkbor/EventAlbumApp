import './home_page.css'
import { AlbumTable } from '../../components/AlbumTable.jsx';
import { useAlbums } from '../../features/auth/hooks/useActiveAlbums.js';
import { useQR } from '../../features/auth/hooks/useQR.js';
import { useState } from 'react';
import UrlService from '../../features/auth/services/urlService.js';
import { useEndEvent } from '../../features/auth/hooks/useEndEvent.js';


function Home_page(){
  const { activeAlbums, endedAlbums, loading, error } = useAlbums();
  const {qr,qrCode, loadingqr, errorqr} = useQR();
  const [qrVisible, setQrVisible] = useState(false);
  const [qrUrl, setQrUrl] = useState("");
  const { endEvent, loading: ending, error: endError } = useEndEvent();
  
    function showQr(token){
      qrCode(token);
      setQrUrl(UrlService.getShareUrl(token));
      setQrVisible(true);
    }
    function showQrUrl(token){
      setQrUrl(UrlService.getShareUrl(token)); 
      setQrVisible(true);
    }
    return (
    <div className='home-page-hp'>
      <div className='bar-home-hp'>
        Witaj użytkowniku
        <button className="btncircleUser">
          👤
        </button>
      </div>
      
      {loading && <p>Ładowanie...</p>}
      {ending && <p>Zakończanie albumu...</p>}
      {error && <p className="error">{error}</p>}
      {endError && <p className="error">{endError}</p>}
      <AlbumTable title="Albumy" albums={activeAlbums} onShowQr={showQr} onEndEvent={endEvent} />
      <AlbumTable title="Archiwum" albums={endedAlbums} onShowQr={showQrUrl}/>
      {loadingqr && <p>Ładowanie QR...</p>}
      {errorqr && <p className="error">{errorqr}</p>}
      
      {qrVisible && (
        <div className="qr-overlay" onClick={() => setQrVisible(false)}>
          <div className="qr-modal" onClick={(e) => e.stopPropagation()}>
            <button className="close-btn" onClick={() => setQrVisible(false)}>×</button>

            {qr && (
              <img src={qr} alt="QR Code" className='qr-image'/>
            )}

            <input type="text" value={qrUrl} readOnly className="qr-url"/>

            <button
              className='btn1-hp'
              onClick={() => navigator.clipboard.writeText(qrUrl)}
            >
              Kopiuj link
            </button>
          </div>
        </div>
      )}
    </div>
  )
}
export default Home_page

