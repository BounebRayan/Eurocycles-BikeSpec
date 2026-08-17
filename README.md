# Eurocycles BikeSpec

A Windows desktop app (VB.NET / WinForms) for managing bike **Nomenclature**
records — bills of materials for Eurocycles bikes — backed by SQL Server
LocalDB.

## Prerequisites

- Windows 10/11
- [.NET 10 SDK](https://dotnet.microsoft.com/download) (or Visual Studio 2022+
  with the ".NET desktop development" workload, which includes it)
- SQL Server LocalDB — installed automatically with Visual Studio's ".NET
  desktop development" or "Data storage and processing" workloads, or
  standalone via the [SQL Server Express LocalDB installer](https://learn.microsoft.com/sql/database-engine/configure-windows/sql-server-express-localdb)

No other services, containers, or accounts are needed — everything runs
locally.

## 1. Get the database

The app expects a database named `Eurocycles-BikeSpec` on
`(localdb)\MSSQLLocalDB`. Pick one of these:

### Option A — restore the included backup (fastest, includes sample data)

```powershell
sqlcmd -S "(localdb)\MSSQLLocalDB" -Q "RESTORE DATABASE [Eurocycles-BikeSpec] FROM DISK = '$PWD\Database\Eurocycles-BikeSpec.bak' WITH REPLACE"
```

Or in SQL Server Management Studio / Azure Data Studio: connect to
`(localdb)\MSSQLLocalDB` → right-click **Databases** → **Restore Database…** →
**Device** → select `Database/Eurocycles-BikeSpec.bak`.

### Option B — build it from scratch

```powershell
sqlcmd -S "(localdb)\MSSQLLocalDB" -i Database/Schema.sql
sqlcmd -S "(localdb)\MSSQLLocalDB" -i Database/SeedData.sql   # optional sample data
```

`Schema.sql` creates the schema (`Nomenclature`, `LigneNomenclature`,
constraints, indexes). `SeedData.sql` is idempotent — safe to re-run any time
you want to reset back to the sample rows (it deletes existing rows first).

The app's connection string lives in
`Eurocycles-BikeSpec/App.config`:

```xml
<add name="EurocyclesBikeSpec"
     connectionString="Server=(localdb)\MSSQLLocalDB;Database=Eurocycles-BikeSpec;Integrated Security=True;TrustServerCertificate=True;" />
```

Edit that if your database lives somewhere other than the default LocalDB
instance.

## 2. Build & run

```powershell
dotnet build Eurocycles-BikeSpec/Eurocycles-BikeSpec.vbproj
dotnet run --project Eurocycles-BikeSpec/Eurocycles-BikeSpec.vbproj
```

Or open `Eurocycles-BikeSpec.slnx` in Visual Studio and press **F5**.

## Project structure

```
Eurocycles-BikeSpec/                  the app project
├── Models/                           Nomenclature, LigneNomenclature (POCOs)
├── Data/                             ADO.NET data access, no ORM
│   ├── DatabaseHelper.vb             connection factory (reads App.config)
│   ├── NomenclatureRepository.vb     GetAll/Search/GetByCode/Insert/Update/Delete
│   └── DataAccessException.vb        wraps SqlException with a user-facing message
├── Utils/                            shared helpers used across the forms
│   ├── Theme.vb                      brand palette + control styling helpers
│   ├── AllowedValues.vb              dropdown values - MUST mirror the DB CHECK constraints
│   ├── GenCodeValidator.vb           GenCode format validation
│   ├── LigneCodeGenerator.vb         surrogate codes for new BOM lines
│   ├── LigneTotalsCalculator.vb      per-Devise line totals
│   ├── NullableConverter.vb          blank-string <-> Nothing conversions
│   └── PhotoHelper.vb                safe image decode (never throws on bad data)
├── Assets/                           embedded logo resource
├── Forms/
│   ├── FormListe                     list/search screen (the only top-level window)
│   ├── FormNomenclature              create/edit screen
│   └── FormApercu                    read-only preview + print screen
└── App.config                        connection string

Database/
├── Schema.sql                       database schema
├── SeedData.sql                     sample data (idempotent)
└── Eurocycles-BikeSpec.bak          full DB backup (schema + sample data)
```

## Architecture notes

- **Data layer**: plain ADO.NET, parameterized queries throughout (no string-built
  SQL, no ORM). `NomenclatureRepository.Insert`/`Update` write the header and
  all of its BOM lines inside a single `SqlTransaction`, so the two tables
  never end up out of sync with each other.
- **Single-window navigation**: `FormListe` is the only real top-level window.
  `FormNomenclature` and `FormApercu` are embedded into it (not shown as
  separate windows), so New/Edit/Aperçu swap the window's content in place
  instead of opening more windows. Navigating from Edit to its Aperçu preview
  only detaches the edit form (doesn't close it), so unsaved in-progress edits
  survive the round trip.
- **Validation**: header fields use `ErrorProvider` (icons on the field, not a
  MessageBox); BOM line errors highlight the specific grid cell via
  `DataGridViewCell.ErrorText`. Nothing is saved to the database until both
  the header and every line pass.
- **`Option Strict On` / `Option Explicit On`** are enabled project-wide.
- **Styling**: `Utils/Theme.vb` centralizes the app's navy/yellow brand
  palette and all control styling (buttons, grid, cards, header strip) so
  colors and fonts aren't duplicated across the three forms.

## Known limitations

- Enter-to-save / Esc-to-cancel don't reliably work on the Edit/Aperçu screens
  — a consequence of them being embedded (non-top-level) forms rather than
  real dialogs; WinForms' `AcceptButton`/`CancelButton` wiring is only fully
  honored on top-level windows.
- No automated test suite yet — verification so far has been manual/UI-driven.
- `LigneNomenclature.Code` is a user-editable surrogate key (not
  auto-hidden); duplicate/blank codes are caught by client-side validation
  before save, but there's no server-side uniqueness check beyond the
  table's own primary key.
