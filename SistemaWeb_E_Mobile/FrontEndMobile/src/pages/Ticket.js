import { useState, useEffect } from 'react';
import { View, Text, StyleSheet, FlatList, TouchableOpacity } from 'react-native';
import api from '../services/api';

const statusColors = {
    'Resolvido': '#50eb89ff',
    'Pendente': 'yellow',
    'Em andamento': 'orange',
    'Análise': '#6c757d',
};

const TicketItem = ({ ticket, onPress }) => {
    const statusColor = statusColors[ticket.StatusChamado] || '#6c757d';

    return (
        <TouchableOpacity style={styles.itemContainer} onPress={() => onPress(ticket)}>
            <View style={styles.titleColumn}>
                <Text style={styles.itemTitle} numberOfLines={1}>{ticket.Titulo}</Text>
            </View>

            <View style={styles.statusColumn}>
                <View style={[styles.statusCircle, { backgroundColor: statusColor }]} />
                <Text style={styles.itemStatus}>{ticket.StatusChamado}</Text>
            </View>
        </TouchableOpacity>
    );
};

const Ticket = ({ setActiveTicket, user }) => {

    const [tickets, setTickets] = useState([]);
    const [error, setError] = useState(null);

    useEffect(() => {
        const fetchTickets = async () => {
            setError(null);
            try {
                const response = await api.get(`/tickets/${user.IdUsuario}`);
                if (response.data.success) {
                    setTickets(response.data.Tickets || []);
                } else {
                    if (response.data.message === "Nenhum chamado encontrado") {
                        setTickets([]);
                    } else {
                        throw new Error(response.data.message || "Erro ao buscar chamados");
                    }
                }
            } catch (err) {
                console.error("Erro no fetch:", err);
                setError("Falha ao carregar chamados.");
            }
        };
        fetchTickets();
    }, [user]);

    const handleTicketPress = (ticket) => {
        setActiveTicket('ShowTicket', { user: user, ticket: ticket });
    };

    return (
        <View style={styles.container}>
            <View style={styles.header}>
                <Text style={styles.headerTitle}>TÍTULO</Text>
                <Text style={styles.headerStatus}>STATUS</Text>
            </View>

            <View style={styles.whiteBlock}>
                {error ? (
                    <View style={styles.centeredContainer}>
                        <Text style={styles.errorText}>{error}</Text>
                    </View>
                ) : tickets.length === 0 ? (
                    <View style={styles.centeredContainer}>
                        <Text>Você ainda não abriu nenhum chamado.</Text>
                    </View>
                ) : (
                    <FlatList
                        data={tickets}
                        keyExtractor={(item) => item.IdChamado.toString()}
                        renderItem={({ item }) => (
                            <TicketItem ticket={item} onPress={handleTicketPress} />
                        )}
                    />
                )}
            </View>
        </View>
    );
};

const styles = StyleSheet.create({
    container: {
        flex: 1,
        padding: 15,
    },
    centeredContainer: {
        flex: 1,
        justifyContent: 'center',
        alignItems: 'center',
        padding: 20,
        minHeight: 200,
    },
    errorText: {
        color: 'red',
        fontSize: 16,
    },
    whiteBlock: {
        borderRadius: 10,
        overflow: 'hidden',
        flex: 1,
    },
    header: {
        flexDirection: 'row',
        justifyContent: 'space-between',
        paddingHorizontal: 20,
        marginBottom: 10,
    },
    headerTitle: {
        fontSize: 16,
        fontWeight: 'bold',
        color: '#333',
        width: '60%',
    },
    headerStatus: {
        fontSize: 16,
        fontWeight: 'bold',
        color: '#333',
        width: '40%',
        textAlign: 'right',
    },
    itemContainer: {
        flexDirection: 'row',
        justifyContent: 'space-between',
        alignItems: 'center',
        paddingVertical: 15,
        paddingHorizontal: 15,
        backgroundColor: '#f0f0f0',
        borderBottomWidth: 1,
        borderBottomColor: '#ccc',
    },
    titleColumn: {
        width: '60%',
        paddingRight: 10,
    },
    itemTitle: {
        fontSize: 16,
        color: '#333',
        fontWeight: '500',
    },
    statusColumn: {
        width: '40%',
        flexDirection: 'row',
        alignItems: 'center',
        justifyContent: 'flex-end',
    },
    statusCircle: {
        width: 10,
        height: 10,
        borderRadius: 5,
        marginRight: 8,
    },
    itemStatus: {
        fontSize: 16,
        color: '#333',
        fontWeight: '600',
    },
});

export default Ticket;