# API de Consulta CADIFA

![.NET](https://img.shields.io/badge/.NET-8.0-purple)
![ASP.NET Core](https://img.shields.io/badge/ASP.NET_Core-API-blue)
![PostgreSQL](https://img.shields.io/badge/PostgreSQL-Neon-336791)
![Python](https://img.shields.io/badge/Python-ETL-yellow)
![Swagger](https://img.shields.io/badge/Swagger-Documented-green)
![License](https://img.shields.io/badge/License-MIT-lightgrey)

API REST desenvolvida em ASP.NET Core 8 para monitoramento e consulta pública dos registros CADIFA da ANVISA.

Os dados são coletados automaticamente a partir do Power BI público da ANVISA, armazenados em PostgreSQL e disponibilizados através de uma API REST documentada com Swagger.

## Objetivo

O projeto disponibiliza uma API para consulta dos registros CADIFA extraídos de uma fonte pública da ANVISA: [Portal CADIFA](https://app.powerbi.com/view?r=eyJrIjoiOTQwZDZjZWEtNzUwNy00MTdhLTk3ZDEtN2VhNDM2ZDNhMTEzIiwidCI6ImI2N2FmMjNmLWMzZjMtNGQzNS04MGM3LWI3MDg1ZjVlZGQ4MSJ9)

A coleta e atualização dos dados é feita por um processo ETL em Python, enquanto esta API em C# expõe os dados para consulta.

## Arquitetura

```text
ANVISA Power BI
        ↓
ETL Python
        ↓
PostgreSQL (Neon)
        ↓
ASP.NET Core 8 API
        ↓
Swagger / Usuários
```

## Tecnologias

- C#
- ASP.NET Core 8
- Entity Framework Core
- PostgreSQL
- Neon
- Swagger
- Python ETL

## Funcionalidades

- Consulta pública dos registros CADIFA
- Filtro por empresa
- Filtro por processo
- Paginação
- Histórico de alterações
- Monitoramento de inclusões
- Monitoramento de exclusões
- Endpoint de resumo
- Documentação Swagger

## Endpoints

### Status

```http
GET /api/status
```

## Resumo

```http
GET /api/status/resumo
```
### Exemplo de retorno

```json
{
  "totalCadifa": 530,
  "totalInclusoes": 3,
  "totalExclusoes": 3,
  "totalAlteracoes": 6,
  "ultimaAlteracao": "2026-06-22T20:05:00"
}
```
## Consultar CADIFA
```http
GET /api/cadifa
```
## Filtrar por empresa (exemplo)
```http
GET /api/cadifa?empresa=aarti
```
## Filtrar por processo (exemplo)
```http
GET /api/cadifa?processo=25351
```
## Paginação
```http
GET /api/cadifa?page=1&pageSize=50
```
## Alterações
```http
GET /api/alteracoes
```
## Configuração local
Crie um arquivo appsettings.json com base em appsettings.example.json.
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=HOST;Port=5432;Database=BANCO;Username=USUARIO;Password=SENHA;SSL Mode=Require;Trust Server Certificate=true"
  }
}
```
## Executar localmente
```bash
dotnet restore
dotnet run
```
## Acesse
```http
http://localhost:5229/swagger
```
## Segurança
O arquivo appsettings.json não deve ser versionado, pois contém dados sensíveis de conexão com o banco.

## Roadmap

### Versão 1.0

- [x] Integração com PostgreSQL Neon
- [x] API ASP.NET Core 8
- [x] Swagger
- [x] Consulta de registros CADIFA
- [x] Filtros por empresa
- [x] Filtros por processo
- [x] Paginação
- [x] Monitoramento de inclusões
- [x] Monitoramento de exclusões
- [x] Histórico de alterações
- [x] Endpoint de resumo

### Próximas versões

- [ ] Deploy público no Render
- [ ] Agendamento automático do ETL
- [ ] Notificação por e-mail quando houver alterações
- [ ] Exportação para Excel
- [ ] Dashboard web para consulta
- [ ] CI/CD com GitHub Actions


### Autor
Lucas Albuquerque

Projeto desenvolvido como estudo de desenvolvimento backend utilizando ASP.NET Core, PostgreSQL, Python e integração com dados públicos da ANVISA.
