@echo off
echo ========================================
echo  Uploading to GitHub
echo  Repository: toxicalchemist/OOP-CRUD
echo ========================================
echo.

cd /d "C:\Users\Josh Cabunoc\source\repos\CRUD APP Clinic Management System\CRUD APP Clinic Management System"

echo Initializing Git...
git init

echo Adding files...
git add .

echo Creating commit...
git commit -m "Initial commit: Clinic Management System - VB.NET CRUD app with MySQL"

echo Setting branch...
git branch -M main

echo Adding remote...
git remote add origin https://github.com/toxicalchemist/OOP-CRUD.git 2>nul || git remote set-url origin https://github.com/toxicalchemist/OOP-CRUD.git

echo.
echo Pushing to GitHub...
echo Username: toxicalchemist
echo Password: Use your Personal Access Token
echo.
git push -u origin main

if errorlevel 1 (
    echo.
    echo ERROR: Push failed!
    echo - Make sure you use Personal Access Token as password
    echo - Create token at: github.com/settings/tokens
    pause
    exit /b 1
)

echo.
echo ========================================
echo  SUCCESS!
echo ========================================
echo.
echo View at: https://github.com/toxicalchemist/OOP-CRUD
echo.
pause
