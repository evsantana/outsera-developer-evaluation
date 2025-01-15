@echo off

:: Navegar até o diretório do projeto de testes
cd src\OutseraMovies.Tests

:: Restaurar dependências
echo Restaurando dependências...
dotnet restore

:: Compilar o projeto de testes
echo Compilando o projeto de testes...
dotnet build

:: Executar os testes (sem rebuild)
echo Executando os testes...
dotnet test --no-build --verbosity normal

:: Manter o terminal aberto
pause