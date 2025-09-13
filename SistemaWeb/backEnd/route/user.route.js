import { Router } from "express"
import { LoginController, TicketsController } from "../controllers/user.controller.js"

export default function userRouter(pool, sql) {
    const router = Router()
    const loginController = new LoginController(pool, sql)
    const ticketsController = new TicketsController(pool, sql)

    router.post("/login", (req, res) => loginController.login(req, res))
    router.get("/tickets/:userId", (req, res) => ticketsController.getTicket(req, res))
    router.post("/create-ticket", (req, res) => ticketsController.createTicket(req, res))

    return router
}
