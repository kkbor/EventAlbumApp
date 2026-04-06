const API_BASE = "https://localhost:7263";
const FRONT_BASE = "http://localhost:5173";
class UrlService{
    static getQrImageUrl(token) {
        return `${API_BASE}/api/album/qr-image/${token}`;
    }
    static getAlbumByQrApi(token) {
        return `${API_BASE}/api/album/by-qr/${token}`;
    }
    static getShareUrl(token) {
        return `${FRONT_BASE}/album/${token}`;
    }
    static getQrViewUrl(token) {
        return `${FRONT_BASE}/qr/${token}`;
    }
}
export default UrlService;