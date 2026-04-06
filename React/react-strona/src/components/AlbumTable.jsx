import { useNavigate } from "react-router-dom";
import "../pages/home_page/home_page.css"
import { formatDate } from "../utils/dateUtils"; 
export function AlbumTable({title, albums, onShowQr, onEndEvent}) {
  const navigate = useNavigate();
  return (
    <div className="block-hp">
      
    
    <h2 className="table-title">{title}</h2>
      {albums.length === 0 ? (
        <p>Brak albumów</p>
      ) : (
        <table>
       
          <tbody>
            {albums.map((album) => (
              
              <tr key={album.id}>
                <td>{album.name}</td>
                <td>{formatDate(album.start)}</td>
                <td>{formatDate(album.end)}</td>
                <td>{title === "Archiwum" && (
                  <button className="btn-hp">
                    Podgląd
                  </button>
                )}</td>
                <td>{title === "Archiwum" && (
                  <button className="btn-hp" onClick={() => onShowQr(album.qrToken)}>
                    Udostępnij
                  </button>
                )}</td>
                <td>{title === "Albumy" && (
                  <button className="btn-hp" onClick={() => onEndEvent(album.id)}>
                    Zakończ
                  </button>
                )}</td>
                <td>{title === "Albumy" && (
                  <button className="btn-hp" onClick={() => onShowQr(album.qrToken)}>
                      Kod QR
                  </button>
                )}</td>
              </tr>
            ))}
          </tbody>
        </table>
      )}
      {title === "Albumy" && (
        <button className="btncircle" onClick={() => navigate("/create")}><span>+</span></button>
      )}

    </div>
  );
}