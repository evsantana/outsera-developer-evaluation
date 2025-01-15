@echo off

:: Diretório do projeto WebAPI
cd src\OutseraMovies.WebAPI

:: Restaurar dependências
echo Restaurando dependências...
dotnet restore

:: Instruções
echo Iniciando o projeto na porta 5001...
echo API pode ser acessada em: http://localhost:5001/swagger

:: Especificar porta
dotnet run --urls "http://localhost:5001" 


echo Pressione Ctrl+C para encerrar a execução.