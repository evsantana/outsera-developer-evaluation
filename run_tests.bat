@echo off

:: Navegar at� o diret�rio do projeto de testes
cd src\OutseraMovies.Tests

:: Restaurar depend�ncias (opcional, caso necess�rio)
echo Restaurando depend�ncias...
dotnet restore

:: Executar os testes
echo Executando os testes...
dotnet test --no-build --verbosity normal

:: Manter o terminal aberto
pause

