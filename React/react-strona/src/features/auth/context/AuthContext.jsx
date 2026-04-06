import { createContext, useContext, useState, useEffect } from "react";
import AuthUtils from "../../../utils/authUtils";

const AuthContext = createContext();

export function AuthProvider({ children }) {
  const [user, setUser] = useState(null);  
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    const storedUser = AuthUtils.getUser();
    if (storedUser) {
      setUser(storedUser);
    }
    setLoading(false);
  }, []);

  const login = (userData) => {
    AuthUtils.setUser(userData);
    setUser(userData);
  };

  const logout = () => {
    AuthUtils.removeUser();
    setUser(null);
  };

  const token = user?.token || null;
  const isLoggedIn = !!token;

  return (
    <AuthContext.Provider value={{ user, token, login, logout, isLoggedIn, loading }}>
      {children}
    </AuthContext.Provider>
  );
}

export function useAuth() {
  return useContext(AuthContext);
}