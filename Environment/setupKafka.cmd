@echo off
cls

echo Installing and setting up Kafka environment
powershell docker-compose up -d --build

echo.
echo. Starting consumer console window ...
start "CONSUMER" C:\Users\user\Desktop\Kafka\src\kafka.exe consume

echo.
echo. Starting producer console window + message generation to topic cars ...
start "PRODUCER" C:\Users\user\Desktop\Kafka\src\kafka.exe produce 100

echo.
echo Press any key to kill Docker ...
pause > nul
powershell docker-compose down

echo.
echo Press any key to exit
pause > nul

