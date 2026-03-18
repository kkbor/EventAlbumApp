import "../pages/home_page.css"
import { formatDate } from "../utils/dateUtils"; 
export function AlbumTable({title, albums}) {
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
                  <button className="btn-hp">
                    Udostępnij
                  </button>
                )}</td>
                <td>{title === "Albumy" && (
                  <button className="btn-hp">
                    Zakończ
                  </button>
                )}</td>
                <td>{title === "Albumy" && (
                  <button className="btn-hp">
                    Kod QR
                  </button>
                )}</td>
              </tr>
            ))}
          </tbody>
        </table>
      )}
      {title === "Albumy" && (
        <button className="btncircle"><span>+</span></button>
      )}

    </div>
  );
}