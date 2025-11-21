// Variável para armazenar a função de logout fornecida pelo componente principal (App.js)
let onLogoutCallback = () => {
    console.warn("Logout function not registered. Cannot force logout from interceptor.");
};

/**
 * Registra a função de logout do componente principal.
 * Esta função será chamada pelo interceptor do Axios em caso de token expirado (401/403).
 * @param {Function} callback A função handleLogout de App.js, que limpa o estado e o token.
 */
export const registerLogoutCallback = (callback) => {
    onLogoutCallback = callback;
};

/**
 * Função exportada para ser chamada pelo interceptor do Axios.
 */
export const forceLogout = () => {
    onLogoutCallback();
};