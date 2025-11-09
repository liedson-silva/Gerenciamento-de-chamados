import { jest, describe, it, expect, beforeEach, afterAll } from '@jest/globals'
import { UserController } from '../controllers/userController.js'

// --- Mock do MSSQL ---
const mockRequest = { input: jest.fn().mockReturnThis(), query: jest.fn() }
const mockPool = { request: jest.fn(() => mockRequest) }
const mockSql = {
  VarChar: (size) => `VarChar(${size})`,
  Int: 'Int',
  Date: 'Date',
}

// --- Helper para simular resposta Express ---
const makeRes = () => ({
  status: jest.fn().mockReturnThis(),
  json: jest.fn(),
})

// --- Dados base para os testes ---
const mockBody = {
  name: 'Liedson Silva',
  cpf: '123.456.789-00',
  rg: '12.345.678-9',
  functionUser: 'Analista',
  sex: 'Masculino',
  sector: 'TI',
  date: '2000-01-01',
  email: 'liedson@example.com',
  password: 'senha123',
  login: 'liedson',
}

const mockUserRecord = {
  IdUsuario: 1,
  Nome: 'Liedson Silva',
  Email: 'liedson@example.com',
  FuncaoUsuario: 'Analista',
}

describe('UserController', () => {
  let controller, res

  beforeEach(() => {
    jest.clearAllMocks()
    jest.spyOn(console, 'error').mockImplementation(() => {})
    controller = new UserController(mockPool, mockSql)
    res = makeRes()
  })

  afterAll(() => jest.restoreAllMocks())

  // --- TESTE 1: Criar usuário com sucesso ---
  it('cria usuário com sucesso e retorna 201', async () => {
    const req = { body: mockBody }

    mockRequest.query.mockResolvedValueOnce({ recordset: [mockUserRecord] })

    await controller.createUser(req, res)

    expect(mockPool.request).toHaveBeenCalled()
    expect(mockRequest.input).toHaveBeenCalledWith('name', 'VarChar(50)', 'Liedson Silva')
    expect(mockRequest.query).toHaveBeenCalled()
    expect(res.status).toHaveBeenCalledWith(201)
    expect(res.json).toHaveBeenCalledWith({
      success: true,
      user: mockUserRecord,
    })
  })

  // --- TESTE 2: Campos obrigatórios faltando ---
  it('retorna 400 se campos obrigatórios faltarem', async () => {
    const req = { body: { name: 'Incompleto' } }

    await controller.createUser(req, res)

    expect(res.status).toHaveBeenCalledWith(400)
    expect(res.json).toHaveBeenCalledWith({
      success: false,
      message: 'Campos obrigatórios faltando',
    })
  })

  // --- TESTE 3: Erro no banco de dados ---
  it('retorna 500 se ocorrer erro no banco', async () => {
    const req = { body: mockBody }
    mockRequest.query.mockRejectedValueOnce(new Error('Erro de conexão'))

    await controller.createUser(req, res)

    expect(res.status).toHaveBeenCalledWith(500)
    expect(res.json).toHaveBeenCalledWith({
      success: false,
      message: 'Erro ao criar usuário',
    })
  })
})
