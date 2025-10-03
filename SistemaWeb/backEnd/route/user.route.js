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
    router.get("/tickets/:userId", (req, res) => ticketsController.getTicket(req, res))
    router.post("/create-ticket", (req, res) => ticketsController.createTicket(req, res))
    router.post("/create-user", (req, res) => userController.createUser(req, res))

    return router
}
