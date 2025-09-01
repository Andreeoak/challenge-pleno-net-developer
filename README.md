📝 Desafio Livecoding – Desenvolvedor Pleno .NET
🎯 Objetivo

Avaliar sua capacidade de evoluir uma aplicação existente em .NET 8, aplicando boas práticas de arquitetura, SOLID, mensageria e testes unitários.

Você terá um código-base pronto (API simples + regra inicial + teste unitário), e seu desafio será implementar novas funcionalidades e evoluir a solução.

🔹 O que já está pronto

API .NET 8 com endpoint de agendamentos.

Regra inicial: não permitir agendamento em datas passadas.

Repositório em memória.

1 teste unitário de exemplo.

Dockerfile básico configurado.

🔹 O que você deve implementar
1. Regras de negócio

Não permitir agendamentos duplicados no mesmo horário/endereço.

Validar que o nome do cliente é obrigatório.

2. Endpoints

Criar um endpoint para buscar agendamento por Id (GET /agendamentos/{id}).

3. Mensageria

Criar uma interface IMessageBus e uma implementação fake (InMemoryMessageBus).

Ao criar um agendamento válido, publicar o evento AgendamentoCriado.

4. Testes unitários

Adicionar pelo menos 2 novos testes:

Agendamento duplicado não deve ser permitido.

Agendamento válido deve retornar sucesso.

5. (Opcional, se der tempo)

Executar a API usando Docker (docker build / docker run).

Explicar como publicaria essa aplicação no Azure.

Explicar como trocaria o repositório em memória por MongoDB.

🔹 O que vamos avaliar

Clareza e qualidade do código.

Aplicação de princípios SOLID e boas práticas.

Capacidade de estruturar regras de negócio.

Escrita de testes unitários.

Raciocínio sobre mensageria, Docker e Cloud.

Clareza ao explicar suas decisões técnicas.

🚀 Como rodar o projeto

Clonar o repositório.

Rodar a API:

dotnet run --project src/Agendamento.Api


Testar endpoints via Swagger em https://localhost:5001/swagger.

Rodar testes:

dotnet test


(Opcional) Rodar com Docker:

docker build -t agendamento-api .
docker run -p 8080:8080 agendamento-api


👉 O mínimo esperado para aprovação é implementar corretamente o Core (itens 1 a 4).
Os itens opcionais diferenciam candidatos que vão além do básico.
