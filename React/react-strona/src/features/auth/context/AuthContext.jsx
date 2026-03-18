import React, { createContext, useState, useContext, useEffect } from "react";

const AuthContext = createContext();

export function AuthProvider({ children }) {
  // Inicjalizacja stanu z localStorage, jeśli istnieje token
  const [token, setToken] = useState(() => localStorage.getItem("TOKEN_KEY"));

  // Zapis tokena do stanu i localStorage
  const saveToken = (newToken) => {
    localStorage.setItem("TOKEN_KEY", newToken);
    setToken(newToken);
  };

  // Usunięcie tokena
  const removeToken = () => {
    localStorage.removeItem("TOKEN_KEY");
    setToken(null);
  };

  const isLoggedIn = !!token;

  return (
    <AuthContext.Provider value={{ token, saveToken, removeToken, isLoggedIn }}>
      {children}
    </AuthContext.Provider>
  );
}

export function useAuth() {
  const context = useContext(AuthContext);
  if (!context) {
    throw new Error("useAuth musi być użyty wewnątrz AuthProvider");
  }
  return context;
}