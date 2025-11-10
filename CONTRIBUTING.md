# Contributing to Clinic Management System

Thank you for considering contributing to this project! ??

## How to Contribute

### Reporting Bugs

If you find a bug, please create an issue with:
- Clear title and description
- Steps to reproduce
- Expected vs actual behavior
- Screenshots if applicable
- Your environment (Windows version, .NET version, MySQL version)

### Suggesting Enhancements

We welcome feature suggestions! Please:
- Check if the feature has already been requested
- Provide a clear use case
- Explain how it would benefit users

### Pull Requests

1. **Fork the Repository**
   ```bash
   git clone https://github.com/YOUR_USERNAME/clinic-management-system.git
   ```

2. **Create a Branch**
   ```bash
   git checkout -b feature/your-feature-name
   ```

3. **Make Your Changes**
   - Follow the existing code style
   - Add comments for complex logic
   - Update documentation if needed

4. **Test Your Changes**
   - Ensure the application builds successfully
   - Test all affected features
   - Verify database operations work correctly

5. **Commit Your Changes**
   ```bash
   git add .
   git commit -m "Add: Brief description of your changes"
   ```

6. **Push to Your Fork**
   ```bash
   git push origin feature/your-feature-name
   ```

7. **Create a Pull Request**
   - Provide a clear description of changes
   - Reference any related issues
   - Include screenshots if UI changes

## Code Style Guidelines

### VB.NET Conventions
- Use meaningful variable and function names
- Add XML comments for public methods
- Use Try-Catch blocks for error handling
- Close database connections in Finally blocks

### Example:
```vb
''' <summary>
''' Loads patient data from database
''' </summary>
Private Sub LoadPatients()
    Try
        OpenConnection()
        ' Your code here
    Catch ex As Exception
        MessageBox.Show("Error: " & ex.Message)
    Finally
        CloseConnection()
    End Try
End Sub
```

### Database Guidelines
- Always use parameterized queries
- Handle NULL values properly
- Use transactions for multiple operations
- Add appropriate indexes

## What We're Looking For

### High Priority
- Bug fixes
- Performance improvements
- Security enhancements
- Documentation improvements

### Medium Priority
- New features (see Roadmap in README)
- UI/UX improvements
- Code refactoring

### Low Priority
- Minor text changes
- Code formatting only

## Questions?

Feel free to:
- Open an issue for discussion
- Comment on existing issues
- Reach out to maintainers

## Code of Conduct

- Be respectful and inclusive
- Provide constructive feedback
- Focus on the issue, not the person
- Help newcomers

Thank you for contributing! ??
