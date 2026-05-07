import "../pages/home_page/Photos/addPhotos.css"
export function PhotoForm({formData, onChange, onSubmit, loading, error}){
    return(
        <div className="block-crate">
            <form onSubmit={onSubmit}>
                <h2> Nowe zdjęcia</h2>
               <button className="btn-login" disabled={loading} >{loading ? "Zapisywanie.." : "Stwórz"}</button>
            </form>
            {error && <p className="error">{error}</p>}
          
        </div>
    );
}