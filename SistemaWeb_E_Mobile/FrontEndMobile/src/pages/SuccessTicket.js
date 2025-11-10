import { View, Text, StyleSheet, TouchableOpacity, ScrollView } from 'react-native';
import { FontAwesome6 } from '@expo/vector-icons';
import { formatDate } from '../components/FormatDate';

const SuccessTicket = ({ setActiveTab, state }) => {

    const { user, ticket } = state;

    const handleViewTicket = () => {
        setActiveTab('ShowTicket', { user: user, ticket: ticket });
    };

    return (
        <ScrollView style={styles.container}>
            <View style={styles.whiteBlock}>

                <Text style={styles.title}>Chamado enviado com sucesso!</Text>
                <Text style={styles.subtitle}>
                    Seu chamado foi registrado e será analisado em breve
                </Text>

                <View style={styles.iconContainer}>
                    <FontAwesome6 name="circle-check" size={120} color="#28a745" />
                </View>

                <View style={styles.infoContainer}>
                    <Text style={styles.infoLine}>
                        <Text style={styles.infoLabel}>ID do chamado:</Text> {ticket?.IdChamado}
                    </Text>
                    <Text style={styles.infoLine}>
                        <Text style={styles.infoLabel}>Data chamado:</Text> {formatDate(ticket?.DataChamado)}
                    </Text>
                    <Text style={styles.infoLine}>
                        <Text style={styles.infoLabel}>Email enviado para:</Text> {user?.Email}
                    </Text>
                </View>

                <TouchableOpacity
                    style={styles.viewTicketButton}
                    onPress={handleViewTicket}
                >
                    <Text style={styles.viewTicketButtonText}>Visualizar chamado</Text>
                </TouchableOpacity>

            </View>
        </ScrollView>
    );
};

const styles = StyleSheet.create({
    container: {
        flex: 1,
    },
    whiteBlock: {
        borderRadius: 10,
        padding: 20,
        alignItems: 'center',

    },
    title: {
        fontSize: 26,
        fontWeight: 'bold',
        color: '#333',
        marginTop: 20,
        textAlign: 'center',
    },
    subtitle: {
        fontSize: 14,
        color: '#777',
        marginTop: 5,
        marginBottom: 40,
        textAlign: 'center',
    },
    iconContainer: {
        marginVertical: 20,
    },
    infoContainer: {
        width: '100%',
        paddingHorizontal: 20,
        marginTop: 30,
        marginBottom: 40,
    },
    infoLine: {
        fontSize: 16,
        color: '#333',
        marginBottom: 8,
    },
    infoLabel: {
        fontWeight: 'bold',
    },
    viewTicketButton: {
        backgroundColor: '#333',
        opacity: 0.8,
        borderRadius: 8,
        paddingVertical: 12,
        paddingHorizontal: 30,
        width: '70%',
        alignItems: 'center',
        marginTop: 20,
        shadowColor: '#000',
        shadowOffset: { width: 0, height: 2 },
        shadowOpacity: 0.15,
        shadowRadius: 3,
        elevation: 2,
    },
    viewTicketButtonText: {
        fontSize: 16,
        fontWeight: 'bold',
        color: '#fff',
    },
});

export default SuccessTicket;