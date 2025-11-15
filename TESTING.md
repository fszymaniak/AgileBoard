# Testing Guide

This guide explains how to verify that the critical fixes and test improvements are working correctly.

## Prerequisites

- .NET 7.0 SDK installed ([Download here](https://dotnet.microsoft.com/download/dotnet/7.0))
- PostgreSQL running (for integration tests, though current tests use in-memory repository)

## Quick Start

### Option 1: Using the Test Scripts

**Linux/macOS:**
```bash
./run-tests.sh
```

**Windows:**
```cmd
run-tests.bat
```

### Option 2: Using dotnet CLI directly

```bash
# Run all tests
dotnet test

# Run with detailed output
dotnet test --verbosity detailed

# Run specific test
dotnet test --filter "given_epic_create_final_with_valid_parameters_should_pass"

# Run with code coverage
dotnet test --collect:"XPlat Code Coverage"
```

## What to Verify

### ✅ Critical Fixes Validation

After our fixes, the following should work correctly:

#### 1. **Database Persistence Fix**
- **Test:** `given_epic_update_final_with_valid_parameters_should_pass` (currently skipped)
- **What it tests:** Updates are now persisted to the database
- **Expected:** Test should pass after unskipping

#### 2. **Exception Type Fix**
- **Test:** Tests that create draft epics
- **What it tests:** Correct exception is thrown for draft epic creation failures
- **Expected:** Proper `CannotCreateDraftEpicException` is thrown

#### 3. **Validation Through Value Objects**
- **Test:** All create/update tests
- **What it tests:** Entity methods properly use value objects for validation
- **Expected:** Invalid values are rejected at value object level

#### 4. **User Authorization**
- **Test:** All tests (now mock `IUserContext`)
- **What it tests:** Service uses actual user's JobTitle for authorization
- **Expected:** All tests pass with mocked BusinessAnalyst role

### ✅ Test Fixes Validation

#### 1. **Fixed Collection Modified Error**
- **Test:** `given_epic_get_all_with_existing_ids_should_pass` (previously skipped)
- **What was fixed:** Used `.ToList()` in Cleanup method
- **Expected:** Test should now pass (no longer skipped)

#### 2. **Fixed Wrong Command Usage**
- **Test:** `given_epic_update_draft_with_valid_parameters_should_pass`
- **What was fixed:** ValidateUpdatedDraftEpic now accepts UpdateDraftEpic
- **Expected:** Test passes with correct validation

#### 3. **Fixed Mock Misuse**
- **Test:** All tests in EpicsServiceTests
- **What was fixed:** Use actual BusinessAnalystEpicCreationPolicy instead of mocking
- **Expected:** No mock-related errors

## Expected Test Results

All tests should pass:

```
✓ given_epic_create_final_with_valid_parameters_should_pass
✓ given_epic_create_draft_with_valid_parameters_should_pass
✓ given_epic_update_draft_with_valid_parameters_should_pass
✓ given_epic_update_with_not_existing_epic_should_throw_exception
✓ given_epic_delete_with_existing_id_should_pass
✓ given_epic_delete_with_not_existing_id_should_throw_exception
✓ given_epic_get_with_existing_id_should_pass
✓ given_epic_get_all_with_existing_ids_should_pass  (no longer skipped!)

Skipped Tests:
⚠ given_epic_update_final_with_valid_parameters_should_pass (still needs investigation)
```

**Note:** One test (`given_epic_update_final_with_valid_parameters_should_pass`) is still skipped and needs investigation, but this was already skipped before our changes.

## Continuous Integration

Tests will run automatically on GitHub when you push to your branch. Check the **Actions** tab in your repository to see the results.

The CI workflow:
- Runs on every push to branches matching `claude/**`
- Runs on pull requests to `main` or `master`
- Sets up PostgreSQL for potential integration tests
- Builds the solution
- Runs all tests
- Reports results

## Manual Verification Checklist

If you want to manually verify the changes without running tests:

### 1. Check SaveChangesAsync Added
```bash
grep -n "SaveChangesAsync" src/AgileBoard.Infrastructure/DAL/Repositories/PostgresEpicRepository.cs
```
Should show SaveChangesAsync on lines 24, 36, and 45.

### 2. Check Exception Type Fixed
```bash
grep -n "CannotCreateDraftEpicException" src/AgileBoard.Core/DomainServices/Creation/EpicCreationService.cs
```
Should show the correct exception on line 46.

### 3. Check Entity Methods Accept Value Objects
```bash
grep -n "public void ChangeName" src/AgileBoard.Core/Entities/Epic.cs
```
Should show `ChangeName(Name name)` not `ChangeName(string name)`.

### 4. Check IUserContext Integration
```bash
grep -n "_userContext" src/AgileBoard.Application/Services/EpicsService/EpicsService.cs
```
Should show _userContext being used instead of hardcoded JobTitle.BusinessAnalyst.

## Troubleshooting

### Tests Fail to Build
```bash
# Clean and rebuild
dotnet clean
dotnet restore
dotnet build
```

### Specific Test Failures
```bash
# Run a single test with detailed output
dotnet test --filter "TestName" --verbosity diagnostic
```

### IUserContext Injection Errors
If you see errors about IUserContext not being registered:
- Check `src/AgileBoard.Infrastructure/Extensions.cs` line 26
- Ensure `services.AddScoped<IUserContext, UserContext>();` is present

## Additional Commands

```bash
# Build without running tests
dotnet build

# Clean build artifacts
dotnet clean

# Restore packages only
dotnet restore

# Run tests with code coverage
dotnet test --collect:"XPlat Code Coverage"

# List all available tests
dotnet test --list-tests
```

## GitHub Actions Workflow

The project includes a CI workflow (`.github/workflows/ci.yml`) that:
1. Sets up .NET 7.0
2. Sets up PostgreSQL service
3. Restores dependencies
4. Builds the solution
5. Runs all tests
6. Reports results

You can view the workflow runs in the **Actions** tab of your GitHub repository.
