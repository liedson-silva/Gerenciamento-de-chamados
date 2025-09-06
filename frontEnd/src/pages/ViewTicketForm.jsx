import { useLocation } from "react-router-dom"
 
const ViewTicketForm = () => {
    const location = useLocation()
    const user = location.state?.user

  return (
    <section>

        <div>
            <p>criado em: X horas atrás por {user?.name}</p>
            <p>Título: Impressora não liga</p>
            <p>Categoria: Hardware</p>
            <p>Descrição do problema: A impressora está ligada e corretamente conectada ao computador/rede, porém não esta realizando impressões. Os documentos estão em fila.</p>
            <p></p>
        </div>
        
    </section>
  )
}

export default ViewTicketForm