import { Router } from "express"
import { LoginController } from "../controllers/user.controller.js"

export default function userRouter(pool, sql) {
    const router = Router()
    const loginController = new LoginController(pool, sql)

    router.post("/login", (req, res) => loginController.login(req, res))

    return router
}
