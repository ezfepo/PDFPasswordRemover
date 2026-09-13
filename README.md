# PDFPasswordRemover

A small Windows desktop utility that batch-removes passwords from PDF files in a folder (recursively), using [iText7](https://itextpdf.com/).

## Requirements

- Windows
- [.NET 10 SDK](https://dotnet.microsoft.com/download)

## Build & run

```sh
dotnet build
dotnet run --project PDFPasswordRemover
```

## Local password prefill (optional)

To prefill the password field for your own local testing without committing a password, create a `PDFPasswordRemover/.env` file (gitignored) based on `.env.example`:

```sh
DEFAULT_PASSWORD=your-test-password
```

This is never required and never committed — the field is blank by default.

## Usage

1. Click **Select** and choose a folder. It's scanned recursively for `.pdf` files.
2. Enter the password shared by the protected PDFs in that folder.
3. Click **Process**.

Each password-protected PDF found is decrypted in place. The original encrypted file is kept alongside it with a `.pwd` extension as a backup; PDFs that aren't password-protected are left untouched.

> **Note:** This modifies files in place. Keep a backup of the folder before processing if you want to be safe — the `.pwd` file is the only copy of the original kept by the tool.

## License

[MIT](LICENSE)
