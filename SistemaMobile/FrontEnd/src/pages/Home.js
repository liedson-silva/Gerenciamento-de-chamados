import { Dimensions, StyleSheet, Text, TouchableOpacity, View } from 'react-native'
import { PieChart } from 'react-native-chart-kit'

const screenWidth = Dimensions.get('window').width

const chartData = [
    {
        name: "Baixa",
        population: 35,
        color: "#28a745",
        legendFontColor: "#7F7F7F",
        legendFontSize: 14
    },
    {
        name: "Média",
        population: 65,
        color: "#007bff",
        legendFontColor: "#7F7F7F",
        legendFontSize: 14
    },
    {
        name: "Alta",
        population: 140,
        color: "#dc3545",
        legendFontColor: "#7F7F7F",
        legendFontSize: 14
    }
]

const summaryCards = [
    { title: "Total de Chamados", value: 240, color: "#02356c" },
    { title: "Chamados Pendentes", value: 60, color: "#ffc107" },
    { title: "Chamados Em Andamento", value: 60, color: "#c97c00ff" },
    { title: "Chamados Resolvidos", value: 60, color: "#28a745" },
]

const SummaryCard = ({ title, value, color }) => (
    <View style={[styles.card, { borderLeftColor: color, borderLeftWidth: 5 }]}>
        <Text style={styles.cardValue}>{value}</Text>
        <Text style={styles.cardTitle}>{title}</Text>
    </View>
)

export default function HomeScreen({ setActiveTab }) {

    const handleNewTicket = () => {
        setActiveTab('CreateTicket');
    };

    return (
        <View>
            <View style={styles.screen}>
                <Text style={styles.title}>Olá! Liedson Silva</Text>
            </View>

            <TouchableOpacity style={styles.newTicketButton} onPress={handleNewTicket}>
                <Text style={styles.newTicketButtonText}>+ NOVO CHAMADO</Text>
            </TouchableOpacity>

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
        </View>
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