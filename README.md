# +Água API REST

A +Água API REST é uma solução desenvolvida em .NET 8 para o monitoramento inteligente de recursos hídricos, permitindo o registro, consulta e gestão de dispositivos de medição e suas leituras. O sistema foi projetado para facilitar a integração com diferentes aplicações e promover a eficiência no controle e análise de dados relacionados ao consumo e à qualidade da água.

A arquitetura da solução segue boas práticas de desenvolvimento, utilizando Entity Framework Core para persistência em banco de dados Oracle e o padrão RESTful para os endpoints. A API oferece um relacionamento 1:N entre dispositivos e leituras, possibilitando o acompanhamento detalhado do histórico de medições de cada sensor instalado. A documentação automática via Swagger proporciona facilidade na exploração e teste dos recursos disponíveis.

---

## 📚 Requisitos Atendidos

- API REST com boas práticas de arquitetura e programação
- Persistência em banco de dados relacional (Oracle)
- Relacionamento 1:N entre Dispositivo e Leituras
- Documentação automática via Swagger
- Migrations com Entity Framework Core

---

## 🗂️ Estrutura da Solução

- **Domain**: Entidades de domínio e exceções
- **Application**: Serviços, DTOs, validações (FluentValidation)
- **Infrastructure**: Contexto EF Core, Migrations
- **Presentation**: Controllers, filtros, endpoints REST

---

## 📊 Diagrama de Entidades
```
┌────────────┐ 1    N ┌─────────────┐
│  Device    │ ──────>│  Reading    │
└────────────┘        └─────────────┘
   Id (PK)                Id (PK)
   Name                   LevelPct
   InstallationDate       TurbidityNtu
   ...                    ...
```
---

## 🚀 Funcionalidades

- CRUD de Dispositivos (Device)
- CRUD de Leituras (Reading)
- Validações de domínio e tratamento de exceções customizadas
- Documentação automática via Swagger
- Respostas HTTP padronizadas (200, 201, 204, 400, 404, 500, 503)

---

## 🔗 Principais Endpoints

### Dispositivos

- `GET /api/device` — Lista todos os dispositivos
- `GET /api/device/{id}` — Consulta um dispositivo por ID
- `POST /api/device` — Cadastra um novo dispositivo
- `PATCH /api/device/{id}` — Atualiza um dispositivo
- `DELETE /api/device/{id}` — Remove um dispositivo

### Leituras

- `GET /api/reading` — Lista todas as leituras
- `GET /api/reading/{id}` — Consulta uma leitura por ID
- `POST /api/reading` — Cadastra uma nova leitura
- `PATCH /api/reading/{id}` — Atualiza uma leitura
- `DELETE /api/reading/{id}` — Remove uma leitura

---

## ⚙️ Tecnologias Utilizadas

- .NET 8 / ASP.NET Core
- C# 12
- Entity Framework Core 9
- Oracle (Oracle.EntityFrameworkCore)
- Swagger (Swashbuckle)
- FluentValidation

---

## 🏗️ Como Executar

1. **Configurar a string de conexão Oracle**
   - No arquivo `appsettings.json` ou via variável de ambiente:
     ```
     "ConnectionStrings": {
       "Oracle": "Data Source=oracle.fiap.com.br:1521/orcl;User Id=seu_usuario;Password=sua_senha;"
     }
     ```

2. **Restaurar pacotes e aplicar migrations**
``` shell
dotnet restore
```
**Console do Gerenciador de Pacotes**
``` shell
Update-Database maisAgua-migration-v2
```

3. **Executar a aplicação**

``` shell
dotnet run
```

4. **Acessar a documentação Swagger**
   [http://localhost:5222/swagger](http://localhost:5222/swagger)

---

## 👥 Equipe - Prisma.Code
- Laura de Oliveira Cintra - RM 558843
- Maria Eduarda Alves da Paixão - RM 558832
- Vinícius Saes de Souza - RM 554456

> “Faça o teu melhor, na condição que você tem, enquanto você não tem condições melhores, para fazer melhor ainda.” — Mario Sergio Cortella

---