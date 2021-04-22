import axios from 'axios';

class userService {
    login = async userCred => {
        try {
            return await axios.post("/api/user", userCred)
        } catch (err) {
            alert(err)
        }

    };
    register = async userCred => {
        return await axios.post("/api/user/register", userCred);
    };
    get = async () => {
        return await axios.get("/api/user")
    }
}

export default new userService();