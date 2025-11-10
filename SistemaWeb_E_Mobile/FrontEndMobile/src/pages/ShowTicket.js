import { View, Text, StyleSheet, TouchableOpacity, ScrollView } from 'react-native';
import { formatDate } from '../components/FormatDate';
import api from '../services/api';
import { useEffect, useState } from 'react';

const DetailRow = ({ label, value }) => {
    return (
        <View style={styles.detailRow}>
            <Text style={styles.detailLabel}>{label}:</Text>
            <Text style={styles.detailValue}>
                {value}
            </Text>
        </View>
    );
};

const ShowTicket = ({ setActiveTab, state }) => {

    const { user, ticket } = state;
    const [replyText, setReplyText] = useState(null);

    async function fetchSuggestedSolution(idTicket) {
        try {
            const response = await api.get(`/get-reply-ticket/${idTicket}`)
            if (response.data.success) {
                const solutionRecord = response.data.Tickets.find(
                    (record) => record.Acao === "Solução Aplicada"
                )
                if (solutionRecord) {
                    const solutionText = solutionRecord.Solucao
                    setReplyText(solutionText)
                } else {
                    setReplyText("O ticket ainda não foi resolvido.");
                }
            } else {
                setReplyText("Falha ao buscar o histórico do ticket.")
            }
        } catch (error) {
            setReplyText("Erro de conexão. Tente novamente mais tarde.")
        }
    }
    useEffect(() => {
        if (ticket && ticket.IdChamado) {
            fetchSuggestedSolution(ticket.IdChamado)
        }
    }, [ticket])
 
    const handleGoHome = () => {
        setActiveTab('Home');
    };

    return (
        <ScrollView style={styles.container}>
            <View style={styles.contentWrapper}>
                <View style={styles.greenBlock}>
                    <Text style={styles.sectionHeader}>Dados do formulário</Text>

                    <DetailRow label="Id do chamado" value={ticket?.IdChamado} />
                    <DetailRow label="Id do usuário" value={ticket?.FK_IdUsuario} />
                    <DetailRow label="Título" value={ticket?.Titulo} />
                    <DetailRow label="Data" value={formatDate(ticket?.DataChamado)} />
                    <DetailRow label="Status" value={ticket?.StatusChamado} />
                    <DetailRow label="Prioridade" value={ticket?.PrioridadeChamado} />
                    <DetailRow label="Descrição do problema" value={ticket?.Descricao} />
                    <DetailRow label="Solução Sugerida/Aplicada" value={replyText}/>

                </View>

                <TouchableOpacity style={styles.homeButton} onPress={handleGoHome}>
                    <Text style={styles.homeButtonText}>Página inicial</Text>
                </TouchableOpacity>
            </View>
        </ScrollView>
    );
};

const styles = StyleSheet.create({
    container: {
        marginTop: 20,
        flex: 1,
        padding: 15,
    },
    contentWrapper: {
        alignItems: 'center',
    },
    greenBlock: {
        backgroundColor: '#caf7bd',
        borderColor: '#03672D',
        borderWidth: 1,
        borderRadius: 10,
        padding: 20,
        width: '100%',
        marginBottom: 30,
        shadowColor: '#000',
        shadowOffset: { width: 0, height: 2 },
        shadowOpacity: 0.2,
        shadowRadius: 5,
        elevation: 3,
    },
    sectionHeader: {
        fontSize: 22,
        fontWeight: 'bold',
        color: '#333',
        marginBottom: 20,
        paddingBottom: 5,
    },
    detailRow: {
        flexDirection: 'row',
        marginBottom: 15,
        alignItems: 'flex-start',
    },
    detailLabel: {
        fontSize: 16,
        fontWeight: '600',
        color: '#03672D',
        marginRight: 5,
        width: '45%',
    },
    detailValue: {
        fontSize: 16,
        color: '#43AE70',
        flex: 1,
        flexWrap: 'wrap',
    },
    homeButton: {
        backgroundColor: '#333',
        opacity: 0.9,
        borderRadius: 8,
        paddingVertical: 12,
        paddingHorizontal: 30,
        width: '50%',
        alignItems: 'center',
        shadowColor: '#000',
        shadowOffset: { width: 0, height: 2 },
        shadowOpacity: 0.15,
        shadowRadius: 3,
        elevation: 2,
    },
    homeButtonText: {
        color: '#fff',
        fontSize: 16,
        fontWeight: 'bold',
    },
});

export default ShowTicket;