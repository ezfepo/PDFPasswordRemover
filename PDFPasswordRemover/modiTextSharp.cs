using iText.Kernel.Crypto;
using iText.Kernel.Pdf;
using System;
using System.IO;
using System.Text;

namespace PDFPasswordRemover
{
    public static class modiTextSharp
    {
        public static void ProcessFile(string oFile, string oPassword)
        {
            string oDirectory = System.IO.Path.GetDirectoryName(oFile);
            string oFileName = System.IO.Path.GetFileNameWithoutExtension(oFile);
            string oExtension = System.IO.Path.GetExtension(oFile).ToLower();
            string oFileTmp = Path.Combine(oDirectory, oFileName + ".tmp");
            string oFileWithPwd = Path.Combine(oDirectory, oFileName + ".pwd");

            try
            {
                using (PdfReader pdfFileReader = new PdfReader(oFile))
                {
                    //if pdf don't need pwd, we close it
                    using (PdfDocument pdfFileDocument = new PdfDocument(pdfFileReader))
                    { 
                        pdfFileDocument.Close();
                    }
                    pdfFileReader.Close();
                    Console.WriteLine(oFile + " not converted.");
                }
            }
            catch (BadPasswordException)
            {
                try
                {
                    using (PdfReader pdfFileReader = new PdfReader(oFile, new ReaderProperties().SetPassword(Encoding.ASCII.GetBytes(oPassword))))
                    {
                        pdfFileReader.SetUnethicalReading(true);
                        PdfDocument pdfFileDocument = new PdfDocument(pdfFileReader, new PdfWriter(oFileTmp));
                        pdfFileDocument.Close();
                        pdfFileReader.Close();
                        Console.WriteLine(oFile + " converted.");
                    }

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
}