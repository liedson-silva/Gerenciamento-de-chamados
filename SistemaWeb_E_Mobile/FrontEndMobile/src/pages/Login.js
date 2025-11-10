import React, { useState } from 'react';
import { View, Text, TextInput, StyleSheet, TouchableOpacity, Alert, Image, ActivityIndicator } from 'react-native';
import { FontAwesome6 } from '@expo/vector-icons';
import api from '../services/api.js';

const Login = ({ setActiveTab, setUser }) => {
    const [login, setLogin] = useState('');
    const [password, setPassword] = useState('');
    const [error, setError] = useState('');
    const [forgetPassword, setForgetPassword] = useState("");

    const handleLogin = async () => {
        if (!login || !password) {
            setError("Preencha todos os campos.");
            setTimeout(() => setError(""), 2000);
            return;
        }

        try {
            const response = await api.post("/login", { login, password });

            if (response.data.success) {
                const user = response.data.user;
                setUser(user);
                setActiveTab('Home');

            } else {
                setError(response.data.message || "Usuário ou senha inválidos!");
                setLogin("");
                setPassword("");
                setTimeout(() => setError(""), 2000);
            }

        } catch (err) {
            const errorMessage = err.response?.data?.message || "Usuário ou senha inválidos!";
            console.error("Erro na requisição de login:", err.message);
            setError(errorMessage);
            setLogin("");
            setPassword("");
            setTimeout(() => setError(""), 3000);
        }
    };

    const textForgetPassword = () => {
        setForgetPassword("Entre em contato com o administrador para recuperar a senha");
        setTimeout(() => setForgetPassword(""), 3000);
    };

    return (
        <View style={styles.container}>
            <View style={styles.contentContainer}>
                <Image
                    source={require('../assets/logo.png')}
                    style={styles.logo}
                    resizeMode="contain"
                />

                <Text style={styles.loginTitle}>Login</Text>

                <View style={styles.inputContainer}>
                    <FontAwesome6 name="user" size={20} color="#777" style={styles.icon} />
                    <TextInput
                        style={styles.input}
                        placeholder="Usuário"
                        placeholderTextColor="#888"
                        value={login}
                        onChangeText={setLogin}
                        autoCapitalize="none"
                    />
                </View>

                <View style={styles.inputContainer}>
                    <FontAwesome6 name="lock" size={20} color="#777" style={styles.icon} />
                    <TextInput
                        style={styles.input}
                        placeholder="Senha"
                        placeholderTextColor="#888"
                        value={password}
                        onChangeText={setPassword}
                        secureTextEntry
                    />
                </View>

                {(error || forgetPassword) && (
                    <Text style={[styles.notice, error ? styles.errorText : styles.forgetPasswordText]}>
                        {error || forgetPassword}
                    </Text>
                )}

                <TouchableOpacity onPress={textForgetPassword} style={styles.forgotPasswordButton}>
                    <Text style={styles.forgotPasswordLink}>Esqueceu a senha?</Text>
                </TouchableOpacity>

                <TouchableOpacity
                    style={styles.loginButton}
                    onPress={handleLogin}
                >
                    <Text style={styles.loginButtonText}>Entrar</Text>
                </TouchableOpacity>
            </View>
        </View>
    );
};

const styles = StyleSheet.create({
    container: {
        flex: 1,
        justifyContent: 'center',
        alignItems: 'center',
        paddingVertical: 20,
    },
    contentContainer: {
        width: '85%',
        alignItems: 'center',
    },
    logo: {
        width: 250,
        height: 100,
        marginBottom: 50,
        marginTop: -50,
    },
    loginTitle: {
        fontSize: 32,
        fontWeight: 'bold',
        color: '#02356c',
        marginBottom: 40,
    },
    inputContainer: {
        flexDirection: 'row',
        alignItems: 'center',
        width: '100%',
        marginBottom: 15,
        paddingHorizontal: 10,
    },
    icon: {
        marginRight: 10,
    },
    input: {
        flex: 1,
        height: 50,
        fontSize: 16,
        borderColor: '#ccc',
        borderWidth: 1,
        borderRadius: 8,
        paddingHorizontal: 10,
        color: '#333',
    },
    notice: {
        fontSize: 14,
        fontWeight: 'bold',
        textAlign: 'center',
        marginBottom: 10,
    },
    errorText: {
        color: 'red',
    },
    forgetPasswordText: {
        color: 'gold',
    },

    forgotPasswordButton: {
        alignSelf: 'flex-start',
        marginLeft: '5%',
        marginBottom: 30,
        marginTop: 5,
    },
    forgotPasswordLink: {
        color: '#02356c',
        fontSize: 14,
        textDecorationLine: 'underline',
    },
    loginButton: {
        backgroundColor: '#02356c',
        borderRadius: 25,
        paddingVertical: 12,
        width: '40%',
        alignItems: 'center',
        elevation: 5,
        alignSelf: 'flex-end',
        marginRight: '5%',
    },
    loginButtonText: {
        color: '#fff',
        fontSize: 18,
        fontWeight: 'bold',
    },
});

export default Login;