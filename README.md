# Outsera - Projeto de Avaliação

## Como configurar e executar a solução

### 1. Verificar a versão do .NET Core

 O projeto necessita da versão 8 ou superior do .net core. Para verificar a versão instalada digite o comando no terminal:
   ```bash
   dotnet --version
   ```   

### 2. Configura o arquivo appsettings.json
O arquivo appsettings.json se encontra dentro da pasta \src\OutseraMovies.WebAPI\
Valide os de configuração do arquivo csv:
  ```bash
  "CsvFileSettings": {
    "FilePath": "movielist.csv",
    "Delimiter": ";"
  },
  ```
### 3. Execute os testes:
Dentro da pasta principal execute o seguinte arquivo para executar o teste de integração:
  ```bash
run_tests.bat
  ```

  Ou vá até a pasta \src\OutseraMovies.Tests e no terminal execute os comandos:
```bash
dotnet restore
dotnet test
  ```

### 4. Execute a API
Dentro da pasta principal execute o seguinte arquivo para iniciar a API:
  ```bash
run_api.bat
  ```
Aguarde até que os registros do CSV sejam carregados, após isso é possível acessar os endpoints via Swagger pela URL:
```bash
http://localhost:5001/swagger/
  ```

O endpoint para a validação do teste se encontra na URL:
```bash
http://localhost:5001/api/ProducersWinners
  ```

### 5. Execute a API (Versão alternativa)
  
  Se não foi possível executar o arquivo no passo anterior, entre na pasta \src\OutseraMovies.WebAPI e no terminal execute os comandos:
```bash
dotnet restore
dotnet run --urls "http://localhost:5001" 
  ```

#### Health Check:
A API possui um endpoint para verificação de saúde da aplicação:
  ```bash
  http://localhost:5001/health
  ```
