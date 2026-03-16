# campanha



## 📦 Requisitos
- Node.js >= 18
- npm ou yarn
- Nest CLI (opcional): `npm i -g @nestjs/cli`

## 🚀 Instalação
```bash
npm install
# ou
yarn
```

## ⚙️ Configuração
Crie um arquivo `.env` na raiz do projeto. Exemplo em [.env.example](.env.example).

## ▶️ Execução
- Desenvolvimento:
```bash
npm run start:dev
```
- Produção (build):
```bash
npm run build
npm run start:prod
```

## 📜 Scripts Disponíveis
- `build` → `nest build`
- `format` → `prettier --write "src/**/*.ts" "test/**/*.ts"`
- `start` → `nest start`
- `start:dev` → `nest start --watch`
- `start:debug` → `nest start --debug --watch`
- `start:prod` → `node dist/main`
- `lint` → `eslint "{src,apps,libs,test}/**/*.ts" --fix`
- `test` → `jest`
- `test:watch` → `jest --watch`
- `test:cov` → `jest --coverage`
- `test:debug` → `node --inspect-brk -r tsconfig-paths/register -r ts-node/register node_modules/.bin/jest --runInBand`
- `test:e2e` → `jest --config ./test/jest-e2e.json`

## 🌐 API (resumo)
- **AppController** (`/`): GET /
- **AppController** (`/`): GET /
- **fornecedorController** (`fornecedor`): GET /fornecedor
- **UsuariosController** (`usuarios`): GET /usuarios, GET /usuarios/nome/:cpf, POST /usuarios/login, POST /usuarios/registrar, PUT /usuarios/atualizar-senhas
- **DiarioController** (`diario`): GET /diario
- **GeralController** (`geral`): POST /geral/acesso, GET /geral, POST /geral/simular-pontos
- **ResumoVendasController** (`resumo`): GET /resumo
- **VendasController** (`vendas`): GET /vendas, GET /vendas/:dtmov/:rcacode/:codprod/:manager
- **VendasRCAController** (`vendasRCA`): GET /vendasRCA

Documentação completa em [docs/api-reference.md](docs/api-reference.md).

## 🗄️ Banco de Dados
- `RelationType.d.ts` (tabela `RelationType.d.ts`)
- `EntityMetadata.d.ts` (tabela `EntityMetadata.d.ts`)
- `DefaultNamingStrategy.d.ts` (tabela `name`)
- `NamingStrategyInterface.d.ts` (tabela `name`)
- `RelationType.d.ts` (tabela `RelationType.d.ts`)
- `EntityMetadata.d.ts` (tabela `EntityMetadata.d.ts`)
- `DefaultNamingStrategy.d.ts` (tabela `name`)
- `NamingStrategyInterface.d.ts` (tabela `name`)
- `fornecedor` (tabela `fornecedor`)
- `Usuario` (tabela `usuario`)
- `Diario` (tabela `diario`)
- `Geral` (tabela `Geral`)
- `ResumoVendas` (tabela `resumo_vendas`)
- `Venda` (tabela `vendas`)
- `vendaRCA` (tabela `resumo_RCA`)

Mais detalhes em [docs/database.md](docs/database.md).

## 📂 Estrutura do Projeto
```
src/
├── modules/...
├── controllers/...
├── services/...
```

Diagramas e arquitetura em [docs/architecture.md](docs/architecture.md).

## 🛠️ Deploy
Instruções completas em [docs/deployment.md](docs/deployment.md).

Exemplo com PM2:
```bash
npm run build
pm2 start dist/main.js --name my-nest-app
```

## 🤝 Contribuindo
Consulte [docs/contributing.md](docs/contributing.md)

## 📌 Changelog
Veja [docs/changelog.md](docs/changelog.md)
