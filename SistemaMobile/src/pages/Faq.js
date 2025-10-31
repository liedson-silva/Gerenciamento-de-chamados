import { useState } from 'react'
import { StyleSheet, Text, View, TouchableOpacity, ScrollView } from 'react-native'

const faqData = [
  {
    question: "Quem pode abrir um chamado?",
    answer: "Qualquer usuário autorizado dentro do sistema pode abrir um chamado para reportar problemas técnicos ou solicitar suporte.",
  },
  {
    question: "Como crio um novo chamado?",
    answer: "Clique no botão 'Criar chamado' na tela inicial. Preencha os campos obrigatórios e responda às perguntas importantes para detalhar seu chamado, e, se necessário, anexe arquivos relevante. Após preencher tudo, envie o chamado e a equipe de suporte técnico será notificada.",
  },
  {
    question: "Como acompanho o status do meu chamado?",
    answer: "Vá até 'Chamados Pendentes', 'Em andamento' ou 'Resolvidos' para ver os detalhes do seu chamado.",
  },
  {
    question: "Posso editar um chamado depois de enviado?",
    answer: "Não, o conteúdo de um chamado não pode ser alterado após o envio. Somente a equipe de suporte (para o status) e os administradores (para o conteúdo) podem fazer mudanças, para garantir o controle e a integridade do registro.",
  },
  {
    question: "Quem atende meu chamado?",
    answer: "Os chamados são atendidos pela equipe de suporte técnico, que avalia e resolve cada problema conforme a categoria e prioridade.",
  },
  {
    question: "Como o sistema define a prioridade do chamado?",
    answer: "A prioridade é determinada automaticamente com base nas informações preenchidas.",
  },
  {
    question: "Quanto tempo leva para um chamado ser resolvido?",
    answer: "O tempo para resolver o chamado depende da complexidade do problema e da sua prioridade. Chamados com prioridade alta são tratados com maior urgência.",
  },
  {
    question: "Esqueci minha senha. E agora?",
    answer: "Entre em contato com o administrador para recuperar a senha.",
  }
];

export default function FAQ() {
  const [openIndex, setOpenIndex] = useState(null);

  const toggleFAQ = (index) => {
    setOpenIndex(openIndex === index ? null : index);
  };

  return (
    <ScrollView style={styles.container}>
      <Text style={styles.title}>FAQ - Perguntas Frequentes</Text>
      
      <View style={styles.faqSection}>
        {faqData.map((item, index) => (
          <View key={index} style={styles.faqItem}>
            <TouchableOpacity
              onPress={() => toggleFAQ(index)}
              style={styles.faqQuestion}
            >
              <Text style={styles.questionText}>{item.question}</Text>
              <Text style={styles.arrow}>{openIndex === index ? '▲' : '▼'}</Text>
            </TouchableOpacity>

            {openIndex === index && (
              <View style={styles.faqAnswerContainer}>
                <Text style={styles.faqAnswerText}>{item.answer}</Text>
              </View>
            )}
          </View>
        ))}
      </View>
    </ScrollView>
  );
}

const styles = StyleSheet.create({
  container: {
    flex: 1,
    paddingHorizontal: 20,
    paddingTop: 10,
  },
  title: {
    fontSize: 26,
    fontWeight: '700',
    color: '#02356c',
    marginBottom: 20,
    textAlign: 'center',
  },
  faqSection: {
    marginBottom: 40,
  },
  faqItem: {
    backgroundColor: '#fff',
    borderRadius: 10,
    marginBottom: 10,
    overflow: 'hidden',
    shadowColor: '#000',
    shadowOffset: { width: 0, height: 1 },
    shadowOpacity: 0.1,
    shadowRadius: 2,
    elevation: 3,
  },
  faqQuestion: {
    flexDirection: 'row',
    justifyContent: 'space-between',
    alignItems: 'center',
    padding: 15,
    borderBottomWidth: 1,
    borderBottomColor: '#eee',
  },
  questionText: {
    flex: 1,
    fontSize: 16,
    fontWeight: '600',
    color: '#333',
    paddingRight: 10,
  },
  arrow: {
    fontSize: 16,
    color: '#02356c',
  },
  faqAnswerContainer: {
    paddingHorizontal: 15,
    paddingBottom: 15,
    backgroundColor: '#f0f4f7',
  },
  faqAnswerText: {
    fontSize: 14,
    color: '#555',
    lineHeight: 20,
  },
});
