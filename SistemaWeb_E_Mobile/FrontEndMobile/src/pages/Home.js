import React, { useEffect, useState, useMemo } from 'react'
import { Dimensions, StyleSheet, Text, TouchableOpacity, View, ActivityIndicator, ScrollView } from 'react-native'
import { PieChart } from 'react-native-chart-kit'
import api from '../services/api.js';

const screenWidth = Dimensions.get('window').width

const SummaryCard = ({ title, value, color }) => (
    <View style={[styles.card, { borderLeftColor: color, borderLeftWidth: 5 }]}>
        <Text style={styles.cardValue}>{value}</Text>
        <Text style={styles.cardTitle}>{title}</Text>
    </View>
)

export default function HomeScreen({ setActiveTab, user }) {
    const [error, setError] = useState(null);
    const [tickets, setTickets] = useState([]);


    useEffect(() => {
        const fetchTickets = async () => {
            try {
                setError(null);

                const response = await api.get(`/tickets/${user.IdUsuario}`);
                const data = response.data;

                if (data.success) {
                    setTickets(data.Tickets || []);
                } else {
                    if (data.message === "Nenhum chamado encontrado") {
                        setTickets([]);
                    } else {
                        throw new Error(data.message || "Erro ao buscar dados");
                    }
                }
            } catch (err) {
                console.error("Erro no fetch:", err);
            }
        };

        fetchTickets();
    }, [user]);

    const summaryCards = useMemo(() => {
        const total = tickets.length;
        const pendentes = tickets.filter(t => t.StatusChamado === 'Pendente').length;
        const emAndamento = tickets.filter(t => t.StatusChamado === 'Em Andamento').length;
        const resolvidos = tickets.filter(t => t.StatusChamado === 'Resolvido').length;

        return [
            { title: "Meus Chamados", value: total, color: "#02356c" },
            { title: "Chamados Pendentes", value: pendentes, color: "#ffc107" },
            { title: "Em Andamento", value: emAndamento, color: "#c97c00ff" },
            { title: "Chamados Resolvidos", value: resolvidos, color: "#28a745" },
        ];
    }, [tickets]);

    const chartData = useMemo(() => {
        if (!tickets.length) {
            return [{ name: "Nenhum chamado", population: 1, color: "#ccc", legendFontColor: "#7F7F7F", legendFontSize: 14 }];
        }

        const baixa = tickets.filter(t => t.PrioridadeChamado === 'Baixa').length;
        const media = tickets.filter(t => t.PrioridadeChamado === 'Média').length;
        const alta = tickets.filter(t => t.PrioridadeChamado === 'Alta').length;

        const data = [
            { name: "Baixa", population: baixa, color: "#28a745", legendFontColor: "#7F7F7F", legendFontSize: 14 },
            { name: "Média", population: media, color: "#007bff", legendFontColor: "#7F7F7F", legendFontSize: 14 },
            { name: "Alta", population: alta, color: "#dc3545", legendFontColor: "#7F7F7F", legendFontSize: 14 }
        ];

        if (baixa === 0 && media === 0 && alta === 0) {
            return [{ name: "Nenhum classificado", population: 1, color: "#ccc", legendFontColor: "#7F7F7F", legendFontSize: 14 }];
        }

        return data.filter(item => item.population > 0);
    }, [tickets]);


    const handleNewTicket = () => {
        setActiveTab('CreateTicket');
    };

    return (
        <ScrollView style={styles.container}>
            <View style={styles.screen}>
                <Text style={styles.title}>Olá! {user ? user.Nome : 'Usuário'}</Text>
            </View>

            <TouchableOpacity style={styles.newTicketButton} onPress={handleNewTicket}>
                <Text style={styles.newTicketButtonText}>+ NOVO CHAMADO</Text>
            </TouchableOpacity>

            {error && (
                <View style={styles.errorContainer}>
                    <Text style={styles.errorText}>{error}</Text>
                </View>
            )}

            <View style={styles.summaryContainer}>
                {summaryCards.map((card, index) => (
                    <SummaryCard key={index} {...card} />
                ))}
            </View>

            <View style={styles.chartContainer}>
                <Text style={styles.chartHeader}>Prioridade dos Chamados</Text>
                <PieChart
                    data={chartData}
                    width={screenWidth - 60}
                    height={220}
                    chartConfig={{
                        backgroundColor: '#02356c',
                        backgroundGradientFrom: '#02356c',
                        backgroundGradientTo: '#02356c',
                        color: (opacity = 1) => `rgba(255, 255, 255, ${opacity})`,
                    }}
                    accessor={"population"}
                    backgroundColor={"transparent"}
                    paddingLeft={"15"}
                    center={[10, 0]}
                    hasLegend={true}
                />
            </View>
        </ScrollView>
    );
}

const styles = StyleSheet.create({
    screen: {
        padding: 20,
        alignItems: 'center',
        justifyContent: 'center',
    },
    title: {
        fontSize: 24,
        fontWeight: 'bold',
        marginBottom: 10,
        color: '#333',
    },
    newTicketButton: {
        backgroundColor: '#02356c',
        paddingVertical: 12,
        paddingHorizontal: 30,
        borderRadius: 8,
        marginBottom: 30,
        alignSelf: 'center',
    },
    newTicketButtonText: {
        color: '#fff',
        fontSize: 16,
        fontWeight: '900',
        textAlign: 'center',
    },
    summaryContainer: {
        flexDirection: 'row',
        flexWrap: 'wrap',
        justifyContent: 'space-between',
        marginHorizontal: 15,
        marginBottom: 30,
    },
    card: {
        backgroundColor: '#fff',
        padding: 10,
        borderRadius: 8,
        width: screenWidth / 2 - 40,
        marginBottom: 10,
        alignItems: 'center',
        elevation: 3,
        shadowColor: '#000',
        shadowOffset: { width: 0, height: 1 },
        shadowOpacity: 0.2,
        shadowRadius: 1.41,
    },
    cardValue: {
        fontSize: 22,
        fontWeight: 'bold',
        color: '#333',
    },
    cardTitle: {
        fontSize: 12,
        textAlign: 'center',
        color: '#777',
        marginTop: 2,
    },
    chartContainer: {
        backgroundColor: '#02356c',
        marginHorizontal: 10,
        borderRadius: 10,
        paddingBottom: 10,
        marginBottom: 30,
    },
    chartHeader: {
        fontSize: 18,
        fontWeight: 'bold',
        marginBottom: 10,
        marginTop: 15,
        color: '#fff',
        textAlign: 'center',
    },
})