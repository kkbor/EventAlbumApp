import "../pages/login&register/login.css"
export function AuthForm({mode, formData, onChange, onSubmit, loading, error}) {
  return (
    <div >
    <h2>{mode === "login" ? "Logowanie" : "Rejestracja"}</h2>

    <form onSubmit={onSubmit}>
        {mode === "register" && (
            <>
                <input className="data" name="name" placeholder="Imię" value={formData.name} onChange={onChange}/>

                <input className="data" name="surname" placeholder="Nazwisko" value={formData.surname} onChange={onChange}/>
            </>
        )}

        <input className="data"  name="email" type="email" placeholder="Email" value={formData.email} onChange={onChange}/>

        <input className="data" name="password" type="password" placeholder="Hasło" value={formData.password} onChange={onChange}/>

        <button disabled={loading} className="btn-login">{loading ? "Ładowanie..." : mode === "login" 
            ? "Zaloguj się" : "Zarejestruj się"}
        </button>
      </form>

      {error && <p className="error">{error}</p>}
    </div>
  );
}