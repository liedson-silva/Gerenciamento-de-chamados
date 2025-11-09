import { jest, describe, it, expect, beforeEach, afterAll } from '@jest/globals'

// --- Mock dinâmico de módulos externos ---
const mockGemini = jest.fn()
const mockSendEmail = jest.fn()

jest.unstable_mockModule('../config/connectIA.js', () => ({
  __esModule: true,
  default: mockGemini,
}))

jest.unstable_mockModule('../config/sendEmail.js', () => ({
  __esModule: true,
  default: mockSendEmail,
}))

// --- Importações assíncronas (ESM + Jest) ---
const { TicketsController } = await import('../controllers/ticketController.js')

// --- Mocks padrão MSSQL ---
const mockRequest = { input: jest.fn().mockReturnThis(), query: jest.fn() }
const mockPool = { request: jest.fn(() => mockRequest) }
const mockSql = { VarChar: (s) => `VarChar(${s})`, Int: 'Int', Date: 'Date' }

// --- Helpers para facilitar leitura ---
const makeRes = () => ({
  status: jest.fn().mockReturnThis(),
  json: jest.fn(),
})

const mockBody = {
  title: 'PC não liga',
  description: 'Meu computador não está ligando.',
  category: 'Hardware',
  userId: 1,
  affectedPeople: 'Somente eu',
  stopWork: 'Sim',
  happenedBefore: 'Não',
}

const mockTicket = {
  IdChamado: 123,
  Titulo: 'PC não liga',
  DataChamado: new Date('2024-01-01T12:00:00.000Z'),
  PrioridadeChamado: 'Análise',
  StatusChamado: 'Pendente',
}

const mockUser = { Nome: 'Usuário Teste', Email: 'teste@exemplo.com' }

// --- Test Suite ---
describe('TicketsController', () => {
  let controller, res

  beforeEach(() => {
    jest.clearAllMocks()
    jest.spyOn(console, 'error').mockImplementation(() => {})
    jest.useFakeTimers().setSystemTime(new Date('2024-01-01T12:00:00.000Z'))

    controller = new TicketsController(mockPool, mockSql)
    res = makeRes()
  })

  afterAll(() => jest.useRealTimers())

  // --- TESTE 1 ---
  it('cria ticket com sucesso, chama IA e envia email', async () => {
    const req = { body: mockBody }
    mockRequest.query
      .mockResolvedValueOnce({ recordset: [mockTicket] })
      .mockResolvedValueOnce({ recordset: [mockUser] })
      .mockResolvedValue({}) // Reuso automático pros UPDATE/INSERT
    mockGemini
      .mockResolvedValueOnce('Alta')
      .mockResolvedValueOnce('Verificar fonte de alimentação')

    await controller.createTicket(req, res)

    expect(mockGemini).toHaveBeenCalledTimes(2)
    expect(mockSendEmail).toHaveBeenCalled()
    expect(res.json).toHaveBeenCalledWith({ success: true, ticket: mockTicket })
  })

  // --- TESTE 2 ---
  it('retorna 400 se campos obrigatórios faltarem', async () => {
    await controller.createTicket({ body: { title: 'Incompleto' } }, res)
    expect(res.status).toHaveBeenCalledWith(400)
  })

  // --- TESTE 3 ---
  it('retorna 500 se o banco falhar', async () => {
    mockRequest.query.mockRejectedValueOnce(new Error('Falha DB'))
    await controller.createTicket({ body: mockBody }, res)
    expect(res.status).toHaveBeenCalledWith(500)
  })
})
