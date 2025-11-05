import { View, Text, StyleSheet, FlatList, TouchableOpacity } from 'react-native';

const tickets = [
    { id: 1, title: 'problema na int...', status: 'Resolvido' },
    { id: 2, title: 'Mouse', status: 'Resolvido' },
    { id: 3, title: 'problema na int...', status: 'Pendente' },
    { id: 4, title: 'Falta de acesso', status: 'Pendente' },
    { id: 5, title: 'Solicitação de software', status: 'Em Andamento' },
];

const statusColors = {
    'Resolvido': '#50eb89ff',
    'Pendente': 'yellow',  
    'Em Andamento': 'orange', 
};

const TicketItem = ({ ticket, onPress }) => {
    const statusColor = statusColors[ticket.status] || '#6c757d'; 

    return (
        <TouchableOpacity style={styles.itemContainer} onPress={() => onPress(ticket.id)}>
            <View style={styles.titleColumn}>
                <Text style={styles.itemTitle} numberOfLines={1}>{ticket.title}</Text>
            </View>

            <View style={styles.statusColumn}>
                <View style={[styles.statusCircle, { backgroundColor: statusColor }]} />
                <Text style={styles.itemStatus}>{ticket.status}</Text>
            </View>
        </TouchableOpacity>
    );
};

const Ticket = ({ setActiveTicket }) => {
    
    const handleTicketPress = () => {
        setActiveTicket('ShowTicket');
    };

    return (
        <View style={styles.container}>
            <View style={styles.header}>
                <Text style={styles.headerTitle}>TÍTULO</Text>
                <Text style={styles.headerStatus}>STATUS</Text>
            </View>

            <View style={styles.whiteBlock}>
                <FlatList
                    data={tickets}
                    keyExtractor={(item) => item.id.toString()}
                    renderItem={({ item }) => (
                        <TicketItem ticket={item} onPress={handleTicketPress} />
                    )}
                />
            </View>
        </View>
    );
};

const styles = StyleSheet.create({
    container: {
        flex: 1,
        padding: 15,
    },
    whiteBlock: {
        borderRadius: 10,
        overflow: 'hidden',
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