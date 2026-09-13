using iText.Kernel.Exceptions;
using iText.Kernel.Pdf;
using System.Text;

namespace PDFPasswordRemover;

public static class modiTextSharp
{
    public static void ProcessFile(string oFile, string oPassword)
    {
        string oDirectory = Path.GetDirectoryName(oFile) ?? string.Empty;
        string oFileName = Path.GetFileNameWithoutExtension(oFile);
        string oFileTmp = Path.Combine(oDirectory, oFileName + ".tmp");
        string oFileWithPwd = Path.Combine(oDirectory, oFileName + ".pwd");

        try
        {
            using (PdfReader pdfFileReader = new(oFile))
            using (PdfDocument pdfFileDocument = new(pdfFileReader))
            {
                // Opens without a password, so nothing to do.
            }
            Console.WriteLine(oFile + " not converted.");
        }
        catch (BadPasswordException)
        {
            try
            {
                using (PdfReader pdfFileReader = new(oFile, new ReaderProperties().SetPassword(Encoding.UTF8.GetBytes(oPassword))))
                {
                    pdfFileReader.SetUnethicalReading(true);
                    using PdfDocument pdfFileDocument = new(pdfFileReader, new PdfWriter(oFileTmp));
                }
                Console.WriteLine(oFile + " converted.");

                File.Move(oFile, oFileWithPwd);
                File.Move(oFileTmp, oFile);
            }
            catch (Exception ex)
            {
                Console.WriteLine(oFile + " " + ex.Message);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(oFile + " " + ex.Message);
        }
    }
}