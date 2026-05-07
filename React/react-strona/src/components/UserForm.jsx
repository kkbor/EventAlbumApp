export function UserForm({mode,formData,onChange,onSubmit}){
    return(
        <form onSubmit={onSubmit}>
            <h2>dane użytkownika</h2>
            <input
                className= "data"
                name = "name"
                placeholder="imię"
                value={formData.name}
                onChange={onChange}
                disabled={mode === "view"}
            />
            <input
                className= "data"
                name = "surname"
                placeholder="nazwisko"
                value={formData.surname}
                onChange={onChange}
                disabled={mode === "view"}
            />
            <input
                className= "data"
                name = "email"
                placeholder="email"
                value={formData.email}
                onChange={onChange}
                disabled={mode === "view"}
            />
            {mode === "edit" &&(
                <button className="btn-hp" type="submit">
                    zapisz
                </button>
            )}
        </form>
    );

}