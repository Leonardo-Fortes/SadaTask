# 📌 Desafio - API de Tarefas

Para este desafio, decidi implementar uma **arquitetura em camadas**, separando responsabilidades de forma clara e escalável:

### 🧱 Estrutura de Pastas

- **.API**  
  Contém os _Endpoints_, _Handlers_ e _Contexto de dados_ (Data), organizando a entrada das requisições HTTP. Optei por utilizar **Handlers** no lugar de Controllers tradicionais, promovendo maior separação de responsabilidades e deixando o código mais próximo de um padrão **CQRS leve**.

- **.CORE**  
  Contém:
  - **Entities** com _construtores ricos_, garantindo que os objetos sejam criados sempre em estado válido.
  - **DTOs** (Requests e Responses) padronizados.
  - **Enums** para representar com clareza os status.
  - **Responses genéricos**, permitindo reutilização de código e consistência nas respostas da API.

- **.TESTS**  
  Aplicação de testes com MSTest para cobrir todos os fluxos principais, usando um banco de dados **InMemory** para simulações isoladas e confiáveis.

---

### ⚙️ Boas práticas aplicadas

S — Single Responsibility Principle
Cada classe do sistema possui uma responsabilidade única e bem definida. Por exemplo, os Handlers lidam exclusivamente com a lógica de requisição, enquanto as entidades encapsulam regras de negócio.

O — Open/Closed Principle
O código está aberto para extensão, mas fechado para modificação. Isso é evidenciado no uso de Responses genéricos e estruturas reutilizáveis como Requests e Enums, que facilitam adaptações futuras sem alterações diretas nas implementações existentes.

L — Liskov Substitution Principle
Esse princípio não foi diretamente aplicável no contexto do projeto, já que não houve uso de herança entre classes. Porém, a estrutura adotada está preparada para futura extensão com heranças respeitando esse princípio.

I — Interface Segregation Principle
As interfaces foram criadas para definir contratos claros entre camadas, como a ITaskHandler, garantindo que cada interface exponha apenas o necessário, evitando implementações obrigatórias de métodos desnecessários.

D — Dependency Inversion Principle
As dependências (como DbContext e ILogger) são injetadas via construtor nos Handlers, o que desacopla as classes concretas e facilita a criação de testes unitários e a manutenção do código.

- **Design Patterns**
  - **Handler Pattern** – Organiza os fluxos por operações específicas, desacoplado da camada de roteamento.
  - **DTO Pattern** – Define objetos de entrada e saída claros e enxutos.
  - **Repository-like** (implícito) – Embora o acesso seja feito diretamente pelo contexto, a lógica está encapsulada, podendo evoluir para um repositório formal.

---

### 📌 Endpoints

#### POST `/v1/tarefas`
Criação de uma nova tarefa:
```json
{
  "title": "string",
  "description": "string",
  "expirationDate": "2025-04-30T18:57:17.864Z",
  "status": 0
}
```

#### GET `/v1/tarefas`
Busca todas as tarefas registradas.

#### GET `/v1/tarefas/task-period`
Busca por tarefas filtrando por status e/ou data de expiração:

**Query Parameters:**
- `status`:  
  - 0 = Pendente  
  - 1 = Em Progresso  
  - 2 = Concluído  
- `expirationDate`: data máxima para expiração

#### GET `/v1/tarefas/{id}`
Consulta uma tarefa específica por ID.

#### PUT `/v1/tarefas/{id}`
Atualiza os dados de uma tarefa:
```json
{
  "title": "string",
  "description": "string",
  "expirationDate": "2025-04-30T18:57:17.864Z",
  "status": 0
}
```

#### DELETE `/v1/tarefas/{id}`
Remove uma tarefa específica pelo ID.

---

### 🥮 Regras de Validação

- `status` deve estar entre 0 e 2.
- `expirationDate` **não pode ser menor** que a data atual.

---

Projeto preparado para evoluir com integrações, autenticação/autorizacão e persistência definitiva se necessário.

![Capturar](https://github.com/user-attachments/assets/6ecab6de-32b9-4778-ae3e-e17e10aa0e59)
