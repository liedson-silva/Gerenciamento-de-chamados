import axios from "axios"

const api = axios.create({
    baseURL: "https://gerenciamento-de-chamados.vercel.app",
})

// Adiciona o token no header `authorization` em todas as requisições
api.interceptors.request.use(
    (config) => {
        const token = localStorage.getItem('token')
        if (token) {
            config.headers = config.headers || {}
            config.headers['authorization'] = token
        }
        return config
    },
    (error) => Promise.reject(error)
)

// Interceptor para lidar com token expirado/inválido
api.interceptors.response.use(
    (response) => response,
    (error) => {
        const status = error.response?.status
        const failedUrl = error.config?.url;

        if ((status === 401 || status === 403) && failedUrl !== '/login') {
            try {
                localStorage.removeItem('token')
            } catch (e) { }
            try {
                sessionStorage.setItem('authError', 'Token expirado, faça login novamente.')
            } catch (e) {}

            window.location.href = '/'
            return Promise.reject(error);
        }

        return Promise.reject(error)
    }
)

export default api