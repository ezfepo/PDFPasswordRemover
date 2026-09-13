# PDFPasswordRemover

WinForms desktop utility (.NET, `net10.0-windows`) that batch-removes passwords from PDF files in a folder using [iText7](https://itextpdf.com/).

## Build & run

```sh
dotnet build
dotnet run --project PDFPasswordRemover
```

Windows only (WinForms). Requires the .NET 10 SDK.

## Project structure

Single project, no test project currently exists.

- `PDFPasswordRemover/Program.cs` — WinForms entry point.
- `PDFPasswordRemover/frmMain.cs` / `frmMain.Designer.cs` — the only UI: pick a folder, enter a password, click Process. Persists the last-used folder via `Properties.Settings` (the password is never persisted).
- `PDFPasswordRemover/modiTextSharp.cs` — core logic. For every `*.pdf` under the selected folder (recursive), tries to open it with iText7; if it's not password-protected, it's left untouched; if `BadPasswordException` is thrown, it reopens with the supplied password and writes a decrypted copy.
- `PDFPasswordRemover/LocalEnv.cs` — reads an optional gitignored `.env` file (`DEFAULT_PASSWORD=...`) to prefill the password field for local dev/testing. See `.env.example`. Never required.

## Important behavior

Processing a password-protected PDF is **destructive**: the original file is renamed to `<name>.pwd` and the decrypted copy takes over the original filename. There is no undo — the `.pwd` file is the only backup of the original. Keep this in mind when changing `modiTextSharp.cs`.

There is no test project. If you add tests, extract the file-processing logic to be testable independently of the WinForms UI first — it currently reads/writes real files and isn't unit-test-friendly.
