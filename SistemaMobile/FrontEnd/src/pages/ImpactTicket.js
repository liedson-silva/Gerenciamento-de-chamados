import { useState } from 'react';
import { View, Text, StyleSheet, TouchableOpacity, ScrollView } from 'react-native';
import { Picker } from '@react-native-picker/picker';

const ImpactTicket = ({ setActiveTab }) => {
    const [affectedPeople, setAffectedPeople] = useState('');
    const [stopWork, setStopWork] = useState('');
    const [happenedBefore, setHappenedBefore] = useState('');

    const handleBack = () => {
        setActiveTab('CreateTicket');
    };

    const handleSuccessTicket = () => {
        setActiveTab('SuccessTicket');
    };

    return (
        <ScrollView style={styles.container}>
            <View style={styles.whiteBlock}>

                <Text style={styles.label}>Quais pessoas são afetadas ? <Text style={styles.required}>*</Text></Text>
                <View style={styles.pickerContainer}>
                    <Picker
                        selectedValue={affectedPeople}
                        onValueChange={(itemValue) => setAffectedPeople(itemValue)}
                        style={styles.picker}
                        dropdownIconColor="#000"
                        mode="dropdown"
                    >
                        <Picker.Item label="Selecione" value="" />
                        <Picker.Item label="Somente eu" value="Somente eu" />
                        <Picker.Item label="Meu setor" value="Meu setor" />
                        <Picker.Item label="Empresa inteira" value="Empresa inteira" />
                    </Picker>
                </View>

                <Text style={styles.label}>Esse problema está impedindo meu trabalho ? <Text style={styles.required}>*</Text></Text>
                <View style={styles.pickerContainer}>
                    <Picker
                        selectedValue={stopWork}
                        onValueChange={(itemValue) => setStopWork(itemValue)}
                        style={styles.picker}
                        dropdownIconColor="#000"
                        mode="dropdown"
                    >
                        <Picker.Item label="Selecione" value="" />
                        <Picker.Item label="Sim" value="Sim" />
                        <Picker.Item label="Não" value="Não" />
                        <Picker.Item label="Parcialmente" value="Parcialmente" />
                    </Picker>
                </View>

                <Text style={styles.label}>Já ocorreu anteriormente ? <Text style={styles.required}>*</Text></Text>
                <View style={styles.pickerContainer}>
                    <Picker
                        selectedValue={happenedBefore}
                        onValueChange={(itemValue) => setHappenedBefore(itemValue)}
                        style={styles.picker}
                        dropdownIconColor="#000"
                        mode="dropdown"
                    >
                        <Picker.Item label="Selecione" value="" />
                        <Picker.Item label="Sim" value="Sim" />
                        <Picker.Item label="Não" value="Não" />
                        <Picker.Item label="Não sei" value="Não sei" />
                    </Picker>
                </View>

                <Text style={styles.aiText}>
                    *Esta seção está sendo preenchida com apoio da inteligência artificial*
                </Text>

                <View style={styles.buttonRow}>
                    <TouchableOpacity
                        style={[styles.button, styles.voltarButton]}
                        onPress={handleBack}
                    >
                        <Text style={styles.buttonText}>Voltar</Text>
                    </TouchableOpacity>

                    <TouchableOpacity
                        style={[styles.button, styles.enviarButton]}
                        onPress={handleSuccessTicket}
                    >
                        <Text style={styles.buttonText}>Enviar</Text>
                    </TouchableOpacity>
                </View>
            </View>
        </ScrollView>
    );
};

const styles = StyleSheet.create({
    container: {
        flexGrow: 1,
    },
    whiteBlock: {
        borderRadius: 10,
        padding: 20,
    },
    label: {
        fontSize: 16,
        fontWeight: '600',
        marginTop: 15,
        marginBottom: 15,
        color: '#333',
    },
    required: {
        color: 'red',
    },
    pickerContainer: {
        borderWidth: 1,
        borderColor: '#ccc',
        borderRadius: 5,
        overflow: 'hidden',
        backgroundColor: '#fff',
        justifyContent: 'center',
        height: 50,
    },
    picker: {
        height: 50,
        width: '100%',
        color: '#333',
    },
    aiText: {
        fontSize: 12,
        color: '#777',
        textAlign: 'center',
        marginTop: 30,
    },
    buttonRow: {
        flexDirection: 'row',
        justifyContent: 'space-between',
        marginTop: 20,
        marginBottom: 10,
    },
    button: {
        flex: 1,
        paddingVertical: 12,
        borderRadius: 8,
        marginHorizontal: 10,
        alignItems: 'center',
        shadowColor: '#000',
        shadowOffset: { width: 0, height: 2 },
        shadowOpacity: 0.15,
        shadowRadius: 3,
        elevation: 2,
    },
    voltarButton: {
        backgroundColor: '#ccc',
        opacity: 0.8,
    },
    enviarButton: {
        backgroundColor: '#333',
        opacity: 0.8,
    },
    buttonText: {
        fontSize: 16,
        fontWeight: 'bold',
        color: '#fff',
    },
});

export default ImpactTicket;