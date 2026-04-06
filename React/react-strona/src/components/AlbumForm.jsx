import "../pages/home_page/home_page.css"
export function AlbumForm({formData, onChange, onSubmit, loading, error}){
    return(
        <div className="block-crate">
            <form onSubmit={onSubmit}>
                <h2> Nowe wydarzenie </h2>
                <input className="data" name="name" placeholder="Tytuł wydarzenia" value={formData.name} onChange={onChange}/>
                <input className="data" name="startDate" type="datetime-local" placeholder="Data początkowa" value={formData.startDate} onChange={onChange}/>
                <input className="data" name="endDate" type="datetime-local" placeholder="Data zakończenia" value={formData.endDate} onChange={onChange}/>
                <button className="btn-login" disabled={loading} >{loading ? "Zapisywanie.." : "Stwórz"}</button>
            </form>
            {error && <p className="error">{error}</p>}
          
        </div>
    );
}