# JLOrdaz.DapperDataMSSQL .NET 10 Upgrade Tasks

## Overview

This document tracks the atomic upgrade of the `JLOrdaz.DapperDataMSSQL` library from .NET 9.0 to .NET 10.0. All project, package, and test validation steps are performed in a single coordinated operation, with a single commit at the end.

**Progress**: 0/1 tasks complete (0%) ![0%](https://progress-bar.xyz/0)

---

## Tasks

### [▶] TASK-001: Atomic upgrade to .NET 10, update packages, and validate tests
**References**: Plan §Actions, Plan §Package updates, Plan §Verification

- [✓] (1) Update `TargetFramework` in `JLOrdaz.DapperDataMSSQL.csproj` to `net10.0` per Plan §Actions
- [▶] (2) Update `PackageReference` versions in the project file as specified in Plan §Package updates
- [ ] (3) If `global.json` exists, update SDK `version` to `10.0.100` per Plan §Actions
- [ ] (4) Run `dotnet restore` and `dotnet build` for the solution
- [ ] (5) Solution builds with 0 errors (**Verify**)
- [ ] (6) Run tests using `dotnet test` for all test projects if present (see Plan §Actions)
- [ ] (7) All tests pass with 0 failures (**Verify**)
- [ ] (8) Commit all changes to branch `upgrade-to-NET10` with message: "TASK-001: Upgrade project to .NET 10 and update packages"

---

