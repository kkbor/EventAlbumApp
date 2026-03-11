const BASE_URL = "https://localhost:7263";
async function request(endpoint, method = "POST",data) {
    const options ={
        method,
        headers:{
            "Content-Type": "application/json",
        }
    };
    if(data){
        options.body=JSON.stringify(data);
    }
    const res = await fetch(`${BASE_URL}${endpoint}`, options);
    if(!res.ok){
        const error = await res.text();
        throw new Error(error || "API ERROR");
        
    }
    return res.json();
}
export const api = {
    post:(edpoint, data) => request(edpoint,"POST", data),
};