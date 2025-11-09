// Configuração necessária para Jest rodar com ESModules (import/export)
export default {
  testEnvironment: 'node',
  transform: {}, // Desliga Babel e permite suporte nativo
  testMatch: [
    '**/tests/**/*.test.js',
    '**/?(*.)+(spec|test).js',
  ],
};
