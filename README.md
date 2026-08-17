# Eurocycles BikeSpec

A Windows desktop app (VB.NET, WinForms) for managing bike Nomenclature records,
basically bills of materials for Eurocycles bikes. Backed by SQL Server LocalDB.

## Demo

[Demo.mp4](Demo.mp4) is a short video showing the app in use: browsing and
searching the list, opening Aperçu, and creating/editing a Nomenclature.

## What you need

- Windows 10 or 11
- [.NET 10 SDK](https://dotnet.microsoft.com/download), or Visual Studio 2022+
  with the ".NET desktop development" workload (that includes the SDK already)
- SQL Server LocalDB. Visual Studio installs this for you with the ".NET desktop
  development" or "Data storage and processing" workloads. You can also grab it
  standalone from the [SQL Server Express LocalDB installer](https://learn.microsoft.com/sql/database-engine/configure-windows/sql-server-express-localdb)

That's it. No containers, no extra accounts, everything runs locally.

## 1. Set up the database

The app expects a database called `Eurocycles-BikeSpec` on `(localdb)\MSSQLLocalDB`.
You have two options.

### Option A: restore the included backup

This is the fastest way, and it already has sample data in it.

```powershell
sqlcmd -S "(localdb)\MSSQLLocalDB" -Q "RESTORE DATABASE [Eurocycles-BikeSpec] FROM DISK = '$PWD\Database\Eurocycles-BikeSpec.bak' WITH REPLACE"
```

Or do it through SQL Server Management Studio or Azure Data Studio: connect to
`(localdb)\MSSQLLocalDB`, right-click Databases, Restore Database, Device, then
pick `Database/Eurocycles-BikeSpec.bak`.

### Option B: build it from scratch

```powershell
sqlcmd -S "(localdb)\MSSQLLocalDB" -i Database/Schema.sql
sqlcmd -S "(localdb)\MSSQLLocalDB" -i Database/SeedData.sql
```

`Schema.sql` creates the tables, constraints and indexes. `SeedData.sql` adds
some sample rows, it's optional but useful for trying the app out. It's also
safe to run again later if you want to reset the data, it clears existing rows
first.

The connection string lives in `Eurocycles-BikeSpec/App.config`:

```xml
<add name="EurocyclesBikeSpec"
     connectionString="Server=(localdb)\MSSQLLocalDB;Database=Eurocycles-BikeSpec;Integrated Security=True;TrustServerCertificate=True;" />
```

Change that if your database isn't on the default LocalDB instance.

## 2. Build and run

```powershell
dotnet build Eurocycles-BikeSpec/Eurocycles-BikeSpec.vbproj
dotnet run --project Eurocycles-BikeSpec/Eurocycles-BikeSpec.vbproj
```

Or just open `Eurocycles-BikeSpec.slnx` in Visual Studio and hit F5.

## Project layout

```
Eurocycles-BikeSpec/                  the app project
├── Models/                           Nomenclature, LigneNomenclature (plain data classes)
├── Data/                             ADO.NET data access, no ORM
│   ├── DatabaseHelper.vb             opens connections, reads App.config
│   ├── NomenclatureRepository.vb     GetPage, SearchPage, GetByCode, Insert, Update, Delete
│   └── DataAccessException.vb        wraps SqlException with a friendly message
├── Utils/                            shared helpers used across the forms
│   ├── Theme.vb                      brand colors and control styling
│   ├── AllowedValues.vb              dropdown values, must match the DB CHECK constraints
│   ├── CurrencyFormatter.vb          formats amounts with each Devise's symbol/separators
│   ├── GenCodeValidator.vb           GenCode format check
│   ├── LigneCodeGenerator.vb         generates codes for new BOM lines
│   ├── LigneTotalsCalculator.vb      totals per currency
│   ├── NullableConverter.vb          converts blank strings to Nothing and back
│   └── PhotoHelper.vb                loads photo bytes safely, never throws
├── Assets/                           the embedded logo
├── Forms/
│   ├── FormListe                     the list and search screen, the only real window
│   ├── FormNomenclature              create and edit screen
│   └── FormApercu                    read-only preview and print screen
└── App.config                        connection string

Database/
├── Schema.sql                        database schema
├── SeedData.sql                      sample data
└── Eurocycles-BikeSpec.bak           full backup (schema plus sample data)
```

## How it's built

A few decisions worth knowing about before you start changing things:

**Data layer.** Plain ADO.NET with parameterized queries, no ORM, no string-built
SQL anywhere. Saving a Nomenclature (`Insert`/`Update`) writes the header and all
of its BOM lines in one SQL transaction, so you never end up with a header and
lines out of sync.

**One window.** `FormListe` is the only top-level window in the app.
`FormNomenclature` and `FormApercu` get embedded into it instead of opening as
separate windows, so clicking around between New, Edit and Aperçu just swaps
what's shown, it doesn't stack up new windows. If you go from editing a record
straight to its Aperçu preview, the edit form isn't closed, just hidden, so any
unsaved changes are still there when you come back to it.

**Validation.** Header fields show errors with `ErrorProvider` (a small icon next
to the field, not a MessageBox). Problems in the BOM lines grid highlight the
actual cell that's wrong. Nothing gets saved until the whole form is valid.

**Option Strict and Option Explicit are both on**, project-wide.

**Styling.** `Utils/Theme.vb` holds the app's colors and all the shared control
styling (buttons, grid, cards, header bar), so none of that is duplicated across
the three forms.

## Known limitations

- Enter to save and Escape to cancel don't reliably work on the Edit and Aperçu
  screens. That's a side effect of them being embedded forms rather than real
  dialogs, WinForms only fully supports those shortcuts on top-level windows.
- No automated tests yet. Everything's been checked manually so far.
- `LigneNomenclature.Code` is a code the user can edit, not a hidden ID. The
  form checks for blank or duplicate codes before saving, but there's no
  server-side uniqueness check beyond the table's own primary key.
