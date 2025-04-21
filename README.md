# Localize.Company

Projeto .NET 9 para gerenciamento de contas e autenticação com arquitetura em camadas.
O projeto foi desenvolvido com:

DDD
- EntityFramework
- NotificationPattern
- Padronização de Response
- Testes Unitários
- CleanCode
- Conceitos REST
- Authentication via JWT
- Criptografia com Argon2
- Injeção de dependências
e mais
---

## 🚀 Rodando o Projeto Localmente

### 📦 Requisitos

- [.NET 9 SDK](https://dotnet.microsoft.com/en-us/download)
- SQL Server (ou outro banco configurado no `appsettings`)
- EF Core CLI (opcional) – já incluído no SDK do .NET 9

---

### ⚙️ Configuração

1. Clone o repositório:

```bash
git clone [https://github.com/mrs-matheus/Localize](https://github.com/mrs-matheus/Localize.git)

cd Localize
cd Localize.Company
cd Localize.Company.Infrastructure
dotnet ef database update


Localize
├─ Localize.Company
│   ├── Localize.Company.Api/              # Camada de apresentação (Controllers)
│   ├── Localize.Company.Application/      # Aplicação (DTOs, Services, Interfaces)
│   ├── Localize.Company.Domain/           # Domínio (Entidades, Services, Interfaces, Notificações)
│   └── Localize.Company.Infrastructure/   # Infraestrutura (EF, Repositórios, External(Api ReceitaWS)
└── tests/
    └── Localize.Company.Tests/            # Projeto de testes (xUnit, Moq)


