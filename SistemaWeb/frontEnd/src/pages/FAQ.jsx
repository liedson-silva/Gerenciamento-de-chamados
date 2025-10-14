import React, { useState } from 'react';

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

const FAQ = () => {
  const [openIndex, setOpenIndex] = useState(null);

  const toggleFAQ = (index) => {
    setOpenIndex(openIndex === index ? null : index);
  };

  return (
    <main className="scroll-list">
      <h1>FAQ - Perguntas Frequentes</h1>
      <section className="faq-section">
        {faqData.map((item, index) => (
          <div
            key={index}
            className={`faq-item ${openIndex === index ? 'open' : ''}`}
            onClick={() => toggleFAQ(index)}
          >
            <div className="faq-question">
              <span>{item.question}</span>
              <span className="arrow">{openIndex === index ? '▲' : '▼'}</span>
            </div>
            {openIndex === index && (
              <div className="faq-answer">{item.answer}</div>
            )}
          </div>
        ))}
      </section>
    </main>
  );
}; 

export default FAQ;