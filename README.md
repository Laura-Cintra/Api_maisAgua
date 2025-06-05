# 🌎 Global Solutions - 2025 - 1º Semestre

## 💧 +Água API REST: Solução para Gestão de Recursos Hídricos

A **+Água API REST** é uma solução desenvolvida em **.NET 8** para o **monitoramento inteligente de recursos hídricos**. Permite o **registro, consulta e gestão** de dispositivos de medição e suas respectivas leituras.

A aplicação visa **facilitar a integração com sistemas externos**, promovendo **eficiência no controle e análise de dados** relacionados ao consumo e à qualidade da água, alinhando-se aos requisitos do projeto **Global Solutions**.

---

## 🧱 Arquitetura e Tecnologias

* Arquitetura RESTful
* Persistência via **Entity Framework Core** com **banco Oracle**
* Relacionamento **1\:N** entre `Device` e `Reading`
* Documentação automática com **Swagger**
* Validações robustas com **FluentValidation**
* Tratamento de exceções customizadas
* Uso de **Migrations** para versionamento do banco
* **Testes unitários e de integração**

---

## 📋 Funcionalidades Principais

### 📿 Dispositivos (Device)

* `GET /api/device` — Listar dispositivos
* `GET /api/device/{id}` — Consultar dispositivo por ID
* `POST /api/device` — Cadastrar novo dispositivo
* `PATCH /api/device/{id}` — Atualizar parcialmente
* `DELETE /api/device/{id}` — Remover por ID

### 📊 Leituras (Reading)

* `GET /api/reading` — Listar leituras
* `GET /api/reading/{id}` — Consultar leitura por ID
* `POST /api/reading` — Cadastrar leitura associada a um dispositivo
* `PATCH /api/reading/{id}` — Atualizar parcialmente
* `DELETE /api/reading/{id}` — Remover por ID

---

## 🛠️ Tecnologias Utilizadas

* [.NET 8](https://dotnet.microsoft.com/) / ASP.NET Core
* C# 12
* Entity Framework Core 9
* Oracle (via `Oracle.EntityFrameworkCore`)
* Swagger (Swashbuckle)
* FluentValidation
* DotNetEnv (gerenciamento de variáveis de ambiente)

---

## 📂 Estrutura da Solução

```bash
.
├── maisAgua.Domain/           # Entidades de domínio e exceções
├── maisAgua.Application/      # Serviços, DTOs, validações (FluentValidation)
├── maisAgua.Infrastructure/   # Contexto EF Core, Migrations, repositórios
├── maisAgua.Presentation/     # Controllers, filtros, endpoints REST (API)
└── tests/                     # Testes unitários e de integração
```

---

## 📊 Diagrama de Entidades

Relacionamento **1\:N** entre `Device` e `Reading`:

```
┌─────────────────┐ 1     N ┌─────────────────┐
│   Device        │ ──────> │     Reading     │
└─────────────────┘         └─────────────────┘
    Id (PK)                Id (PK)
    Name                   LevelPct
    InstallationDate       TurbidityNtu
                           PhLevel
                           ReadingDatetime
                           IdDevice (FK)
```

> **Device**: Representa um dispositivo de medição de água.
> **Reading**: Representa uma leitura específica de um dispositivo, contendo nível, pH e turbidez.

---

## 🚀 Como Executar o Projeto

### 1. Configurar a String de Conexão Oracle

No arquivo `appsettings.json` ou via variável de ambiente:

```json
"ConnectionStrings": {
  "Oracle": "Data Source=oracle.fiap.com.br:1521/orcl;User Id=seu_usuario;Password=sua_senha;"
}
```

---

### 2. Restaurar Pacotes

```bash
dotnet restore
```

---

### 3. Aplicar Migrations

No **Console do Gerenciador de Pacotes**:

```powershell
Update-Database maisAgua-migrations-v2
```

---

### 4. Executar a Aplicação

```bash
dotnet run
```

---

### 5. Acessar a Documentação Swagger

Acesse: [http://localhost:5222/swagger](http://localhost:5222/swagger)
*(Ou a porta configurada no seu projeto)*

---

## 📚 Documentação Adicional

A interface Swagger é a principal fonte para entender, explorar e testar os endpoints da API.
Ela facilita o uso tanto em ambiente de desenvolvimento quanto em integração com outras aplicações.

---

## 👥 Equipe — Prisma.Code

* **Laura de Oliveira Cintra** — RM 558843
* **Maria Eduarda Alves da Paixão** — RM 558832
* **Vinícius Saes de Souza** — RM 554456

> *“Faça o teu melhor, na condição que você tem, enquanto você não tem condições melhores, para fazer melhor ainda.”*
> — **Mario Sergio Cortella**
