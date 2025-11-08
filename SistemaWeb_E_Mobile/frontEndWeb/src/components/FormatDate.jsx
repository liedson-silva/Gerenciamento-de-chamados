export const formatDate = (dateString) => {
    if (!dateString) return 'N/A';
    
    const date = new Date(dateString);
    
    // Formata DD/MM/AAAA e usa 'UTC' para garantir que o dia salvo seja exibido
    return date.toLocaleDateString('pt-BR', { timeZone: 'UTC' }); 
};