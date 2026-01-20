# Plan: Upgrade project to .NET 10

## Objective
Upgrade `JLOrdaz.DapperDataMSSQL` library from `net9.0` to `net10.0` and update package references to versions compatible with .NET 10.

## Scope
- Project: `JLOrdaz.DapperDataMSSQL.csproj`
- Files potentially changed: project file, optional `global.json` if present
- Validate: build solution, run tests (if any)

## Target framework
- New TFM: `net10.0`

## Package updates
Update package versions in the project file to the following recommended versions:
- `Dapper`: 2.1.66 (no change required)
- `Microsoft.Data.SqlClient`: 6.1.4
- `Microsoft.Extensions.Configuration`: 10.0.2
- `Microsoft.Extensions.DependencyInjection`: 10.0.2

## Actions
1. Update `TargetFramework` in `JLOrdaz.DapperDataMSSQL.csproj` to `net10.0`.
2. Update the `PackageReference` versions as listed in "Package updates".
3. If `global.json` exists, update SDK `version` to `10.0.100` (if applicable).
4. Run `dotnet restore` and `dotnet build` for the solution and verify successful build.
5. Run tests (`dotnet test`) if test projects exist.
6. Commit changes to branch `upgrade-to-NET10` with message `TASK-001: Upgrade project to .NET 10 and update packages`.

## Verification
- Build completes with 0 errors.
- No runtime-critical breaking changes introduced by project code (report any compiler errors/warnings referencing breaking changes).

## Rollback
- All changes will be committed to branch `upgrade-to-NET10`. Use Git to revert or create PR for review.

---

Plan authored by GitHub Copilot App Modernization Agent.