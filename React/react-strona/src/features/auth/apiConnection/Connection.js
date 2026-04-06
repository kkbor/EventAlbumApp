import { useAuth } from "../context/AuthContext";
const BASE_URL = "https://localhost:7263";

export function useApi() {
  const { token } = useAuth();

  async function request(endpoint, method = "GET", data, responseType = "json") {
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
    let result;

    if (responseType === "blob") {
      result = await res.blob(); 
    } else {
      const text = await res.text();
      console.log("RAW RESPONSE", text, res.status);
      result = text ? JSON.parse(text) : null;
    }

    if (!res.ok) {
      throw new Error(result?.message || "API ERROR");
    }

    return result;
  }

  return {
    get: (endpoint) => request(endpoint, "GET"),
    getBlob: (endpoint) => request(endpoint, "GET", null, "blob"),
    post: (endpoint, data) => request(endpoint, "POST", data),
    patch: (endpoint) => request(endpoint, "PATCH"),
    put: (endpoint, data) => request(endpoint, "PUT", data),
    delete: (endpoint) => request(endpoint, "DELETE"),
  };
}