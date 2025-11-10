@echo off
echo ========================================
echo  Uploading Clinic Management System
echo  to GitHub: toxicalchemist/OOP-CRUD
echo ========================================
echo.

REM Navigate to the correct directory
cd /d "C:\Users\Josh Cabunoc\source\repos\CRUD APP Clinic Management System\CRUD APP Clinic Management System"

echo [1/7] Initializing Git repository...
git init
if errorlevel 1 (
    echo ERROR: Git initialization failed. Is Git installed?
    pause
    exit /b 1
)

echo.
echo [2/7] Configuring Git user...
git config user.name "toxicalchemist"
git config user.email "your.email@example.com"

echo.
echo [3/7] Adding all files...
git add .
if errorlevel 1 (
    echo ERROR: Failed to add files
    pause
    exit /b 1
)

echo.
echo [4/7] Creating initial commit...
git commit -m "Initial commit: Complete Clinic Management System - Patient management, Appointments, Staff management, Billing, Reports with VB.NET and MySQL"
if errorlevel 1 (
    echo ERROR: Commit failed
    pause
    exit /b 1
)

echo.
echo [5/7] Setting branch to main...
git branch -M main

echo.
echo [6/7] Adding remote repository...
git remote add origin https://github.com/toxicalchemist/OOP-CRUD.git
if errorlevel 1 (
    echo Note: Remote might already exist, trying to set URL...
    git remote set-url origin https://github.com/toxicalchemist/OOP-CRUD.git
)

echo.
echo [7/7] Pushing to GitHub...
echo You will be prompted for your GitHub credentials:
echo - Username: toxicalchemist
echo - Password: Use your Personal Access Token (NOT your password!)
echo.
git push -u origin main

if errorlevel 1 (
    echo.
    echo ========================================
    echo  PUSH FAILED - Possible Solutions:
    echo ========================================
    echo 1. Authentication Error:
    echo    - Use Personal Access Token instead of password
    echo    - Create at: github.com/settings/tokens
    echo.
    echo 2. Repository has existing content:
    echo    - Run: git pull origin main --allow-unrelated-histories
    echo    - Then: git push -u origin main
    echo.
    pause
    exit /b 1
)

echo.
echo ========================================
echo  SUCCESS! Project uploaded to GitHub!
echo ========================================
echo.
echo Your repository: https://github.com/toxicalchemist/OOP-CRUD
echo.
echo Next steps:
echo 1. Visit your repository
echo 2. Add topics/tags for better discoverability
echo 3. Star your own repository
echo 4. Share it on LinkedIn!
echo.
pause
