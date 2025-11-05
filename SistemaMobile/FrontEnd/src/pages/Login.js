import { useState } from 'react';
import { View, Text, TextInput, StyleSheet, TouchableOpacity, Image, } from 'react-native';
import { FontAwesome6 } from '@expo/vector-icons';

const Login = ({ setActiveTab }) => {
    const [username, setUsername] = useState('');
    const [password, setPassword] = useState('');

    const handleLogin = () => {
        setActiveTab('Home');
    };

    const handleForgotPassword = () => {
        console.log('Esqueceu a senha?');
    };

    return (

        <View style={styles.scrollContainer}>
            <View style={styles.contentContainer}>
                <Image
                    source={require('../../assets/logo.png')}
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
                        value={username}
                        onChangeText={setUsername}
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

                <TouchableOpacity onPress={handleForgotPassword} style={styles.forgotPasswordButton}>
                    <Text style={styles.forgotPasswordText}>Esqueceu a senha?</Text>
                </TouchableOpacity>

                <TouchableOpacity style={styles.loginButton} onPress={handleLogin}>
                    <Text style={styles.loginButtonText}>Entrar</Text>
                </TouchableOpacity>
            </View>
        </View>
    );
};

const styles = StyleSheet.create({
    scrollContainer: {
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
        color: '#fff',
        marginBottom: 40,
        textShadowColor: 'rgba(0, 0, 0, 0.75)',
        textShadowOffset: { width: 1, height: 1 },
        textShadowRadius: 2,
    },
    inputContainer: {
        flexDirection: 'row',
        alignItems: 'center',
        backgroundColor: 'rgba(255, 255, 255, 0.9)',
        borderRadius: 8,
        width: '100%',
        marginBottom: 15,
        paddingHorizontal: 10,
        shadowColor: '#000',
        shadowOffset: { width: 0, height: 2 },
        shadowOpacity: 0.2,
        shadowRadius: 3,
        elevation: 3,
    },
    icon: {
        marginRight: 10,
    },
    input: {
        flex: 1,
        height: 50,
        fontSize: 16,
        color: '#333',
    },
    forgotPasswordButton: {
        alignSelf: 'flex-start',
        marginLeft: '5%',
        marginBottom: 30,
        marginTop: 5,
    },
    forgotPasswordText: {
        color: '#eee',
        fontSize: 14,
        textDecorationLine: 'underline',
        textShadowColor: 'rgba(0, 0, 0, 0.75)',
        textShadowOffset: { width: 0.5, height: 0.5 },
        textShadowRadius: 1,
    },
    loginButton: {
        backgroundColor: '#02356c',
        borderRadius: 25,
        paddingVertical: 12,
        width: '40%',
        alignItems: 'center',
        shadowColor: '#000',
        shadowOffset: { width: 0, height: 2 },
        shadowOpacity: 0.25,
        shadowRadius: 3.84,
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