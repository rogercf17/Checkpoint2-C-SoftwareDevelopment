# 🏦 Projeto Banco API

## 📌 1. Integrantes

* Fernanda Rocha — RM 554673
* Luiza Macena — RM 556237
* Roger Cardoso — RM 557230

---

## 💳 2. Produto Bancário Escolhido

O produto implementado foi **Empréstimo** e **Receber Salário**.

### ✔ Justificativa

O empréstimo e receber salário permite aplicar regras de negócio mais completas, como validação de valor, número de parcelas e taxa de juros, simulando um cenário real de análise de crédito.

---

## 🔄 3. Modelagem de Processamento

O enunciado propõe o uso de filas para processamento assíncrono.

Neste projeto, optamos por **simular o processamento de forma síncrona**, conforme orientação do professor (sem uso de RabbitMQ).

### ✔ Funcionamento:

* A contratação é criada com status **PendenteProcessamento**
* Em seguida, é processada internamente
* O status é atualizado para:

  * **Aprovado**
  * **Recusado**

---

## 🧩 4. Diagrama de Classes

![Diagrama UML](Docs/Print%20Diagrama.png)

---

## ⚙️ 5. Como rodar o projeto

### ✔ Pré-requisitos

* .NET 8 SDK
* Oracle Database
* EF Core CLI

---

### ✔ Configuração

No arquivo `appsettings.json`, configure sua conexão:

```json
"ConnectionStrings": {
  "Oracle": "User Id=SEU_RM;Password=SUA_SENHA;Data Source=oracle.fiap.com.br:1521/ORCL"
}
```

---

### ✔ Comandos

```bash
dotnet ef migrations add InitialCreate
dotnet ef database update
dotnet run
```

---

## 🌐 6. Endpoints da API

### 👤 Clientes

#### ➤ Criar Pessoa Física

POST `/api/clientes/pf`

```json
{
  "nome": "Roger",
  "cpf": "12345678901",
  "dataNascimento": "2000-01-01",
  "agenciaId": 1
}
```

---

#### ➤ Criar Pessoa Jurídica

POST `/api/clientes/pj`

```json
{
  "nome": "Empresa X",
  "cnpj": "12345678000199",
  "razaoSocial": "Empresa X LTDA",
  "agenciaId": 1
}
```

---

#### ➤ Buscar Cliente

GET `/api/clientes/{id}`

---

### 🏦 Agência

#### ➤ Criar Agência

POST `/api/agencias`

```json
{
  "numero": "1234",
  "nome": "Agência Central"
}
```

---

#### ➤ Buscar Agência

GET `/api/agencias/{id}`

---

### 📄 Contratação

#### ➤ Solicitar Contratação

POST `/api/contratacoes`

```json
{
  "idCliente": 1,
  "tipoProduto": 1,
  "valor": 12000.00,
  "parcelas": 10,
  "taxaJuros": 2.50
}
```

---

#### ➤ Consultar Status

GET `/api/contratacoes/{id}`

---

## 🧪 7. Testes

Para executar os testes:

```bash
dotnet test
```

![Testes](Docs/Print%20Testes.png)

---

## 📊 8. Evidência de Processamento

![Contratação](Docs/Print%20Contratação.png)

---

## 🚀 9. Execução no Swagger

Após rodar o projeto:

```
https://localhost:xxxx/swagger
```

![Swagger](Docs/Print%20Swagger.png)

---

## 📝 Observações

* Foi utilizada herança para Cliente (PF/PJ) e Produto
* Utilizado Entity Framework Core com Oracle
* Processamento de contratação simulado sem uso de filas externas
* Sistema preparado para expansão com novos produtos

---
