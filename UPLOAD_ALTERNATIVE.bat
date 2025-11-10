@echo off
echo ========================================
echo  Alternative Upload Script
echo  (Use if repository has existing files)
echo ========================================
echo.

cd /d "C:\Users\Josh Cabunoc\source\repos\CRUD APP Clinic Management System\CRUD APP Clinic Management System"

echo Pulling existing content from GitHub...
git pull origin main --allow-unrelated-histories --no-edit

echo.
echo Pushing your code...
git push -u origin main

if errorlevel 1 (
    echo.
    echo ERROR: Push failed!
    echo Try running UPLOAD_TO_GITHUB.bat first
    pause
    exit /b 1
)

echo.
echo ========================================
echo  SUCCESS!
echo ========================================
echo.
echo Visit: https://github.com/toxicalchemist/OOP-CRUD
echo.
pause
