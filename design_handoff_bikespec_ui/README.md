# Handoff: Eurocycles BikeSpec — WinForms UI Restyle

## Overview
Restyled 4 screens of the Eurocycles BikeSpec CRUD app (Nomenclatures list, New, Edit, Preview) to match Eurocycles company branding (navy + brand yellow, sampled from their logo).

## About the design files
The bundled file (`Eurocycles BikeSpec Mockups.dc.html`) is an **HTML design reference** — it shows the intended look, layout, and colors. It is NOT code to copy into the WinForms project. The task for Claude Code is to **recreate this look using WinForms controls** (Panel, TableLayoutPanel, DataGridView, Button, TextBox, ComboBox, PictureBox, Label) in the existing .NET project, applying the same colors/fonts/spacing via WinForms styling APIs.

## Fidelity
High-fidelity for **colors, spacing, and layout structure**. Treat exact pixel values as a strong guide, not a pixel-perfect contract — WinForms rendering (DPI scaling, native controls) will differ slightly from HTML/CSS.

## Design tokens

Colors:
- Navy (primary / title bars / table headers / selected row / primary text): #14213D
- Navy hover/secondary: #1F2E52
- Muted navy text: #3B4A63
- Brand yellow (primary action buttons, fill; use with navy text): #F5D300
- Yellow hover: #E0C000
- Danger / delete accent: #C0392B
- Success text (status counts): #2E7D46
- Background (window/desktop): #EEF0F3
- Card / panel background: #FFFFFF
- Borders: #E1E4EA
- Input border: #CBD2DE
- Muted label text: #6B7686
- Read-only field background (Aperçu screen): #F4F5F7

Typography: Segoe UI (matches native Windows/WinForms default — no custom font needed).
- Section headers ("Identification", "Caractéristiques techniques"): 11-12px, bold, uppercase, letter-spacing, navy (#14213D)
- Field labels: 12px, #6B7686
- Field values / body text: 13-14px, navy or dark gray
- Buttons: 13px, semibold

Spacing: 16-22px padding inside cards/panels, 10-12px gaps between fields, 8-10px between buttons.

Radius: 6-8px on buttons, inputs, cards, table containers (use FlatStyle + custom borders in WinForms; native controls don't support radius directly — consider owner-drawn buttons or a UI library like MetroFramework/Krypton if rounded corners matter).

## Screens

### 1. Liste (Nomenclatures list)
- Title bar: navy (#14213D) background, Eurocycles logo (left), window title, window controls (right).
- Toolbar row: single search TextBox with placeholder-style hint text "Rechercher : code, nom, marque…", plus buttons: [Rechercher] (outlined navy), [Réinitialiser] (outlined gray — resets the search field), [+ Nouveau] (filled yellow, navy text, bold).
- DataGridView: navy header row (white bold text), columns Code/Nom/Date/Marque/Modèle/Taille cadre/Taille roue/Couleur. Selected row: navy background, white text (inverted highlight, not a light tint).
- Footer bar: green (#2E7D46) status text left ("N nomenclatures trouvées."), buttons right: [Modifier] outlined navy, [Supprimer] outlined red (#C0392B), [Aperçu] filled yellow.

### 2. Nouvelle fiche / Modifier (form, same layout)
- 3-column grid: "Identification" card (Code*, Nom*, Date*, Ref. client), "Caractéristiques techniques" card (Marque, Modèle, GenCode, Couleur, NW/GW kg, Taille cadre/roue dropdowns, Type décor dropdown), "Photo" card (square preview placeholder, Choisir photo…, Supprimer buttons).
- Below: "Lignes de la nomenclature" section — Ajouter ligne / Supprimer ligne buttons, DataGridView with Code/Désignation/Qté/Prix/Fabricant/Imprimé/Observation/Devise columns. Selected row same navy/white inversion.
- Modifier screen is identical to Nouvelle fiche but pre-filled with the record's data and title "Modifier nomenclature — <code>".
- Footer: [Aperçu] outlined navy, [Annuler] outlined gray, [Enregistrer] filled yellow (primary, right-most).

### 3. Aperçu (read-only preview)
- Same 3-column layout as the form, but fields are read-only (light gray #F4F5F7 fill, no border emphasis) instead of editable inputs.
- Lines table populated, read-only.
- Footer: total line ("Total : X Euro · Y USD/TND") left, buttons right: [Modifier] outlined navy (opens the Modifier screen for this record), [Imprimer] filled yellow, [Fermer] outlined gray.

## Interactions
- Tabs/buttons across screens are static references only — actual navigation (list → new/edit/preview, Modifier button on Aperçu → edit screen) should be wired to the existing WinForms form-opening logic already in the project.
- No new business logic implied — this is a visual restyle of existing forms and fields.

## Assets
- Eurocycles logo: sample provided in the HTML file as `uploads/logo-...png` (also copied into this folder as `eurocycles-logo.png`). Use the project's real logo asset if a higher-res version exists in the app's resources.

## Files in this bundle
- `Eurocycles BikeSpec Mockups.dc.html` — full HTML design reference (open in any browser; use the "Liste" / "Nouvelle fiche" / "Modifier" / "Aperçu" tabs to see each screen)
- `eurocycles-logo.png` — extracted logo asset
