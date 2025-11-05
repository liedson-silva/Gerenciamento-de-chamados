import { StyleSheet, Text, View, TouchableOpacity, ScrollView } from 'react-native'
import { formatDate } from '../components/FormatDate'

const DUMMY_USER_DATA = {
    Nome: "Liedson Silva",
    Email: "liedson@example.com",
    FuncaoUsuario: "Admin",
    Setor: "TI / Suporte",
    Login: "liedson.ti",
    Sexo: "Masculino",
    DataDeNascimento: "1990-05-15T00:00:00"
};

export default function Profile({ user = DUMMY_USER_DATA, onNavigate }) {

    if (!user) {
        return <View style={styles.loadingContainer}><Text>Carregando dados do usuário...</Text></View>;
    }

    return (
        <ScrollView style={styles.container} contentContainerStyle={styles.scrollContent}>

            <Text style={styles.title}>Minhas Configurações</Text>

            <View style={styles.dataSection}>

                <View>
                    <Text style={styles.userConfig}>Nome:</Text>
                    <Text style={styles.userConfig}>Email:</Text>
                    <Text style={styles.userConfig}>Função:</Text>
                    <Text style={styles.userConfig}>Setor:</Text>
                    <Text style={styles.userConfig}>Login:</Text>
                    <Text style={styles.userConfig}>Sexo:</Text>
                    <Text style={styles.userConfig}>Data de Nasc:</Text>
                </View>

                <View style={styles.dataColumn}>
                    <Text style={styles.userData}>{user.Nome}</Text>
                    <Text style={styles.userData}>{user.Email}</Text>
                    <Text style={styles.userData}>{user.FuncaoUsuario}</Text>
                    <Text style={styles.userData}>{user.Setor}</Text>
                    <Text style={styles.userData}>{user.Login}</Text>
                    <Text style={styles.userData}>{user.Sexo}</Text>
                    <Text style={styles.userData}>{formatDate(user.DataDeNascimento)}</Text>
                </View>

            </View>

        </ScrollView>
    );
}

const styles = StyleSheet.create({
    container: {
        flex: 1,
    },
    scrollContent: {
        padding: 20,
        alignItems: 'center',
    },
    title: {
        fontSize: 26,
        fontWeight: 'bold',
        color: '#02356c',
        marginBottom: 50,
    },
    dataSection: {
        flexDirection: 'row',
        backgroundColor: 'white',
        borderRadius: 10,
        padding: 15,
        width: '100%',
        maxWidth: 400,
        shadowColor: '#000',
        shadowOffset: { width: 0, height: 2 },
        shadowOpacity: 0.1,
        shadowRadius: 3,
        elevation: 5,
        marginBottom: 20,
    },
    dataColumn: {
        marginLeft: 20,
    },
    userConfig: {
        fontSize: 16,
        fontWeight: '500',
        color: '#666',
        marginBottom: 32,
        paddingVertical: 2,
    },
    userData: {
        fontSize: 16,
        fontWeight: 'bold',
        color: '#333',
        marginBottom: 32,
        paddingVertical: 2,
    },
    loadingContainer: {
        flex: 1,
        justifyContent: 'center',
        alignItems: 'center',
    }
});