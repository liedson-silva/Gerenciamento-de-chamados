import { BrowserRouter as Router, Routes, Route } from "react-router-dom";
import Login from "./pages/Login";
import Home from "./pages/Home";
import Layout from "./pages/Layout"
import UserConfig from "./pages/UserConfig"
import CreateTicket from "./pages/CreateTicket";
import ImpactTicket from "./pages/ImpactTicket";
import SuccesTicket from "./pages/SuccesTicket";
import ViewTicketForm from "./pages/ViewTicketForm";
import Tickets from "./pages/Tickets";
import PendingTicket from "./pages/PendingTicket";
import TicketInProgress from "./pages/TicketInProgress";
import TicketResolved from "./pages/TicketResolved";

function App() {
  return (
    <Router>
      <Routes>
        <Route path="/" element={<Login />} />
        <Route element={<Layout />}>
          <Route path="/home" element={<Home />} />
          <Route path="/user-configuration" element={<UserConfig />} />
          <Route path="/create-ticket" element={<CreateTicket />} />
          <Route path="/create-ticket/impact" element={<ImpactTicket />} />
          <Route path="/create-ticket/impact/success" element={<SuccesTicket />} />
          <Route path="/view-ticket-form" element={<ViewTicketForm />} />
          <Route path="/tickets" element={<Tickets />} />
          <Route path="/pending-ticket" element={<PendingTicket />} />
          <Route path="/ticket-in-progress" element={<TicketInProgress />} />
          <Route path="/ticket-resolved" element={<TicketResolved />} />
        </Route>
      </Routes>
    </Router>
  );
}

export default App;
