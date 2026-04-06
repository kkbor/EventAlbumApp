const STORAGE_KEY = "auth_session";
class AuthUtils{
    static setUser(user){
        localStorage.setItem(STORAGE_KEY,JSON.stringify(user));
    }
    static getUser(){
        const data = localStorage.getItem(STORAGE_KEY);
        return data ? JSON.parse(data) : null; 
    }
    static getToken(){
        const user = this.getUser();
        return user?.token || null;
    }
    static removeUser(){
        localStorage.removeItem(STORAGE_KEY)
    }
     static isLoggedIn() {
        return !!this.getToken();
    }
}
export default AuthUtils;