import React from 'react';
import { View, Text, StyleSheet, TouchableOpacity, ScrollView } from 'react-native';

const data = {
    id: 234,
    userId: 31,
    title: 'problema na internet',
    date: '05/11/2025',
    status: 'Pendente',
    priority: 'Análise',
    description: 'internet não esta funcionando',
    affectedPeople: 'Empresa inteira',
    impedingWork: 'Sim',
    occurredBefore: 'Sim',
    solution: 'O ticket ainda não foi resolvido.',
};

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

const ShowTicket = ({ setActiveTab }) => {

    const handleGoHome = () => {
        setActiveTab('Home');
    };


    return (
        <ScrollView style={styles.container}>
            <View style={styles.contentWrapper}>
                <View style={styles.greenBlock}>
                    <Text style={styles.sectionHeader}>Dados do formulário</Text>

                    <DetailRow label="Id do chamado" value={data.id} />
                    <DetailRow label="Id do usuário" value={data.userId} />
                    <DetailRow label="Título" value={data.title} />
                    <DetailRow label="Data" value={data.date} />
                    <DetailRow label="Status" value={data.status} />
                    <DetailRow label="Prioridade" value={data.priority} />
                    <DetailRow label="Descrição do problema" value={data.description} />
                    <DetailRow label="Solução Sugerida/Aplicada" value={data.solution} />

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