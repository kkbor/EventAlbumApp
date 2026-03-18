import { useAuth } from "../context/AuthContext";
const BASE_URL = "https://localhost:7263";

export function useApi() {
  const { token } = useAuth();

  async function request(endpoint, method = "GET", data) {
    console.log("Token wysyłany do backendu:", token);

    const options = {
      method,
      headers: {
        "Content-Type": "application/json",
        ...(token && { Authorization: `Bearer ${token}` }),
      },
    };

    if (data && method !== "GET") {
      options.body = JSON.stringify(data);
    }

    const res = await fetch(`${BASE_URL}${endpoint}`, options);
    const text = await res.text(); // ✅ tylko raz
  console.log("RAW RESPONSE", text, res.status);

    const parsed = text ? JSON.parse(text) : null; // ✅ ręczne parsowanie

    if (!res.ok) {
      throw new Error(parsed?.message || "API ERROR");
    }

    return parsed;
  }

  return {
    get: (endpoint) => request(endpoint, "GET"),
    post: (endpoint, data) => request(endpoint, "POST", data),
    put: (endpoint, data) => request(endpoint, "PUT", data),
    delete: (endpoint) => request(endpoint, "DELETE"),
  };
}