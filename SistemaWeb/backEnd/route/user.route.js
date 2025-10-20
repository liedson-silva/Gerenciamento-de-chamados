import { Router } from "express"
import { TicketsController } from "../controllers/ticketController.js"
import { LoginController } from "../controllers/loginController.js"
import { UserController } from "../controllers/userController.js"
import { ManageTicketsController } from "../controllers/manageTicketsController.js"

export default function userRouter(pool, sql) {
    const router = Router()
    const loginController = new LoginController(pool, sql)
    const ticketsController = new TicketsController(pool, sql)
    const userController = new UserController(pool, sql)
    const manageTicketsController = new ManageTicketsController(pool, sql)

    router.post("/login", (req, res) => loginController.login(req, res))
    router.get("/tickets/:userId", (req, res) => ticketsController.getTicket(req, res))
    router.post("/create-ticket", (req, res) => ticketsController.createTicket(req, res))
    router.post("/create-user", (req, res) => userController.createUser(req, res))
    router.get('/users', (req, res) => userController.getAllUsers(req, res));
    router.put("/update-user/:id", (req, res) => userController.updateUser(req, res))    
    router.get("/All-tickets", (req, res) => ticketsController.getAllTickets(req, res))
    router.get("/manage-ticket/:id", (req, res) => manageTicketsController.getTicketById(req, res))
    router.put("/manage-ticket/:id", (req, res) => manageTicketsController.updateTicket(req, res))
    router.get("/manage-tickets", (req, res) => manageTicketsController.getAllTickets(req, res))
    router.post("/manage-ticket", (req, res) => manageTicketsController.createTicket(req, res))

    return router
}

