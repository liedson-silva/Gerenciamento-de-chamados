import { Router } from "express"
import { TicketsController } from "../controllers/ticketController.js"
import { LoginController } from "../controllers/loginController.js"
import { UserController } from "../controllers/userController.js"

export default function userRouter(pool, sql) {
    const router = Router()
    const loginController = new LoginController(pool, sql)
    const ticketsController = new TicketsController(pool, sql)
    const userController = new UserController(pool, sql)

    router.post("/login", (req, res) => loginController.login(req, res))

    router.post("/create-ticket", (req, res) => ticketsController.createTicket(req, res))
    router.get("/tickets/:userId", (req, res) => ticketsController.getTicketById(req, res))
    router.get("/all-tickets", (req, res) => ticketsController.getAllTickets(req, res))
    router.put("/update-ticket/:id", (req, res) => ticketsController.updateTicket(req, res))
    router.get("/report-ticket", (req, res) => ticketsController.createReport(req, res))
    router.get("/get-reply-ticket/:id", (req, res) => ticketsController.getSolution(req, res))
    router.post("/reply-ticket", (req, res) => ticketsController.Solution(req, res))
    
    router.post("/create-user", (req, res) => userController.createUser(req, res))
    router.get('/users', (req, res) => userController.getAllUsers(req, res));
    router.put("/update-user/:id", (req, res) => userController.updateUser(req, res))

    return router
}

