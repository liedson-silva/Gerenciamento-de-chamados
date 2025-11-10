import { useState } from 'react';
import { View, Text, TextInput, StyleSheet, TouchableOpacity, ScrollView, Platform } from 'react-native';
import { Picker } from '@react-native-picker/picker';
import api from '../services/api.js'

const CreateTicket = ({ setActiveTab, user }) => {
    const [title, setTitle] = useState('');
    const [category, setCategory] = useState('');
    const [description, setDescription] = useState('');
    const [affectedPeople, setAffectedPeople] = useState('');
    const [stopWork, setStopWork] = useState('');
    const [happenedBefore, setHappenedBefore] = useState('');
    const [showImpactSection, setShowImpactSection] = useState(false);
    const [showCreateSection, setShowCreateSection] = useState(true);
    const [error, setError] = useState("")

    const handleContinue = () => {
        setError("");
        if (title.trim() === "" || category === "" || description.trim() === "") {
            setError("Preencha todos os campos obrigatórios (*).");
            setTimeout(() => setError(""), 3000);
            return;
        }
        setShowImpactSection(true);
        setShowCreateSection(false);
        setError("");
    };

    const handleBack = () => {
        setShowCreateSection(true);
        setShowImpactSection(false);
    };

    const handleSuccessTicket = async () => {
        setError("");
        if (affectedPeople === "" || stopWork === "" || happenedBefore === "") {
            setError("Preencha todos os campos obrigatórios (*).");
            setTimeout(() => setError(""), 3000);
            return;
        }

        if (!user || !user.IdUsuario) {
            setError("Erro: Usuário não encontrado. Tente fazer login novamente.");
            setTimeout(() => setError(""), 3000);
            return;
        }

        try {
            const { data } = await api.post("/create-ticket", {
                title, description, category, userId: user.IdUsuario, affectedPeople, stopWork, happenedBefore
            });

            if (data.success) {
                setActiveTab("SuccessTicket", { user: user, ticket: data.ticket });
            } else {
                setError(data.message || "Erro ao criar chamado");
                setTimeout(() => setError(""), 3000);
            }
        } catch (err) {
            console.error(err);
            setError(err.response?.data?.message || "Erro de rede ao criar chamado.");
            setTimeout(() => setError(""), 3000);
        }
    };

    return (<>
        {showCreateSection && (
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
                            <Picker.Item label="Outros" value="Outros" />
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

                    {error && <Text style={styles.errorText}>{error}</Text>}

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
        )}

        {showImpactSection && (
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

                    {error && <Text style={styles.errorText}>{error}</Text>}

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
        )}

    </>);
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
        height: Platform.OS === 'ios' ? 90 : 50,
    },
    picker: {
        height: 50,
        justifyContent: 'center',
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
        flexDirection: 'row',
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
        backgroundColor: '#aaa',
        opacity: 0.8,
    },
    continuarButton: {
        backgroundColor: '#333',
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
    errorText: {
        color: 'red',
        textAlign: 'center',
        marginTop: 15,
        fontSize: 14,
        fontWeight: 'bold',
    },
    aiText: {
        fontSize: 12,
        fontStyle: 'italic',
        color: '#777',
        textAlign: 'center',
        marginTop: 25,
    }
});

export default CreateTicket;