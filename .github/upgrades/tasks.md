# JLOrdaz.DapperDataMSSQL .NET 10 Upgrade Tasks

## Overview

This document tracks the atomic upgrade of the `JLOrdaz.DapperDataMSSQL` library from .NET 9.0 to .NET 10.0. All project, package, and test validation steps are performed in a single coordinated operation, with a single commit at the end.

**Progress**: 1/1 tasks complete (100%) ![100%](https://progress-bar.xyz/100)

---

## Tasks

### [x] TASK-001: Atomic upgrade to .NET 10, update packages, and validate tests *(Completed: 2026-01-20 21:16)*
**References**: Plan §Actions, Plan §Package updates, Plan §Verification

- [x] (1) Update `TargetFramework` in `JLOrdaz.DapperDataMSSQL.csproj` to `net10.0` per Plan §Actions
- [x] (2) Update `PackageReference` versions in the project file as specified in Plan §Package updates
- [x] (3) If `global.json` exists, update SDK `version` to `10.0.100` per Plan §Actions (not present)
- [x] (4) Run `dotnet restore` and `dotnet build` for the solution
- [x] (5) Solution builds with 0 errors (**Verify**)
- [x] (6) Run tests using `dotnet test` for all test projects if present (no test projects found)
- [x] (7) All tests pass with 0 failures (**Verify**)
- [x] (8) Commit all changes to branch `upgrade-to-NET10` with message: "TASK-001: Upgrade project to .NET 10 and update packages"

**Execution notes**:

- Build: succeeded
- Tests: none found
- Branch: `upgrade-to-NET10`

---



