import axios from "axios"
import AsyncStorage from '@react-native-async-storage/async-storage';
import { forceLogout } from '../utils/authUtils';

const api = axios.create({
    baseURL: "http://192.168.1.13:3000"
})

// Interceptor para adicionar o token no header `authorization`
api.interceptors.request.use(
    async (config) => {
        try {
            const token = await AsyncStorage.getItem('token');
            if (token) {
                config.headers = config.headers || {};
                config.headers['authorization'] = token;
            }
        } catch (e) {
            console.error("Erro ao obter token do AsyncStorage:", e);
        }
        return config;
    },
    (error) => Promise.reject(error)
);

// Interceptor para lidar com token expirado/inválido
api.interceptors.response.use(
    (response) => response,
    async (error) => {
        const status = error.response?.status;

        if (status === 401 || status === 403) {
            console.warn("Sessão expirada ou não autorizada. Forçando logout.");

            try {
                await AsyncStorage.removeItem('token');
            } catch (e) {
                console.error("Erro ao remover token do AsyncStorage:", e);
            }

            forceLogout();
        }
        return Promise.reject(error);
    }
);

export default api;