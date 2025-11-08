import { useState } from 'react';
import { View, Text, TextInput, StyleSheet, TouchableOpacity, ScrollView } from 'react-native';
import { Picker } from '@react-native-picker/picker';

const CreateTicket = ({ setActiveTab }) => {
    const [title, setTitle] = useState('');
    const [category, setCategory] = useState('');
    const [description, setDescription] = useState('');

    const handleContinue = () => {
        setActiveTab('ImpactTicket');
    };

    return (
        <ScrollView style={styles.container}>

            <View style={styles.whiteBlock}>
                <Text style={styles.label}>Título <Text style={styles.required}>*</Text></Text>
                <TextInput
                    style={styles.input}
                    value={title}
                    onChangeText={setTitle}
                />

                <Text style={styles.label}>Categoria <Text style={styles.required}>*</Text></Text>
                <View style={styles.pickerContainer}>
                    <Picker
                        selectedValue={category}
                        onValueChange={(itemValue) => setCategory(itemValue)}
                        style={styles.picker}
                        dropdownIconColor="#000"
                        mode="dropdown"
                    >
                        <Picker.Item label="Selecione" value="" />
                        <Picker.Item label="Hardware" value="Hardware" />
                        <Picker.Item label="Software" value="Software" />
                        <Picker.Item label="Segurança" value="Segurança" />
                        <Picker.Item label="Serviços" value="Serviços" />
                        <Picker.Item label="Rede" value="Rede" />
                        <Picker.Item label="Infraestrutura" value="Infraestrutura" />
                    </Picker>
                </View>

                <Text style={styles.label}>Descrição do problema <Text style={styles.required}>*</Text></Text>
                <TextInput
                    style={styles.textArea}
                    value={description}
                    onChangeText={setDescription}
                    multiline
                    numberOfLines={4}
                />

                <View style={styles.buttonRow}>

                    <TouchableOpacity
                        style={[styles.button, styles.continuarButton]}
                        onPress={handleContinue}
                    >
                        <Text style={styles.buttonText}>Continuar</Text>
                    </TouchableOpacity>
                </View>
            </View>
        </ScrollView>
    );
};

const styles = StyleSheet.create({
    container: {
        flex: 1,        
        padding: 20,
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
    input: {
        borderWidth: 1,
        borderColor: '#ccc',
        borderRadius: 5,
        padding: 10,
        fontSize: 16,
        backgroundColor: '#fff',
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
    textArea: {
        borderWidth: 1,
        borderColor: '#ccc',
        borderRadius: 5,
        padding: 10,
        fontSize: 16,
        height: 100,
        textAlignVertical: 'top',
        backgroundColor: '#fff',
    },
    buttonRow: {
        marginTop: 30,
    },
    button: {
        flex: 1,
        paddingVertical: 12,
        borderRadius: 25,
        marginHorizontal: 5,
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
    continuarButton: {
        backgroundColor: '#333',
        opacity: 0.8,
    },
    buttonText: {
        fontSize: 16,
        fontWeight: 'bold',
        color: '#fff',
    },
});

export default CreateTicket;