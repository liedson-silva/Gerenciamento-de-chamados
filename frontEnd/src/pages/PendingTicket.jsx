

const PendingTicket = () => {
  return (
    <section>

        <div className="pending-ticket">
            <ul className="info-pending-ticket">
                <li>ID</li>
                <li>TÍTULO</li>
                <li>STATUS</li>
                <li>DATA</li>
                <li>PRIORIDADE</li>
                <li>CATEGORIA</li>
                <li>DESCRIÇÃO</li>
            </ul>
        </div>
        
        <div className="box-pending-ticket">
            <ul className="info-pending-ticket">
                <li>4856</li>
                <li>Impressora não liga</li>
                <li> <span class="circle-orange">ㅤ</span> Pendente</li>
                <li>05/07/2004</li>
                <li> <span class="circle-green">ㅤ</span> Baixa</li>
                <li>Hardware</li>
                <li>A impressora está ligada e corretamente conectada ao computador/rede, porém não esta realizando impressões. Os documentos estão em fila.</li>
            </ul>
        </div>

    </section>
  )
}

export default PendingTicket