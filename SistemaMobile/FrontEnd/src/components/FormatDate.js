export const formatDate = (dateString) => {
    if (!dateString) {
        return 'N/A'
    }
    const date = new Date(dateString)
    if (isNaN(date.getTime())) {
        return 'Data Inválida'
    }
    return date.toLocaleDateString('pt-BR', { timeZone: 'UTC' })
}