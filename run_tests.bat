@echo off

:: Navegar até o diretório do projeto de testes
cd src\OutseraMovies.Tests

:: Restaurar dependências (opcional, caso necessário)
echo Restaurando dependências...
dotnet restore

:: Executar os testes
echo Executando os testes...
dotnet test --no-build --verbosity normal

:: Manter o terminal aberto
pause

