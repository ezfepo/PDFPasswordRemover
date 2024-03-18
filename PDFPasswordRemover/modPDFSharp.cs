using PdfSharp.Pdf;
using PdfSharp.Pdf.IO;
using System;
using System.IO;

namespace PDFPasswordRemover
{
    public static class modPDFSharp
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
                using (PdfDocument pdfFileTest = PdfReader.Open(oFile, PdfDocumentOpenMode.Import))
                {
                    //if pdf don't need pwd, we close it
                    Console.WriteLine(oFile + " not converted.");
                    pdfFileTest.Dispose();
                }
            }
            catch (PdfReaderException)
            {
                try
                {
                    using (PdfDocument pdfFileWithPwd = PdfReader.Open(oFile, oPassword, PdfDocumentOpenMode.Import))
                    {
                        using (PdfDocument pdfFileWithoutPwd = new PdfDocument())
                        {
                            //Copy over pages from original document
                            foreach (PdfPage page in pdfFileWithPwd.Pages)
                            {
                                pdfFileWithoutPwd.AddPage(page);
                            }

                            pdfFileWithoutPwd.Save(oFileTmp);
                            pdfFileWithoutPwd.Dispose();
                        }

                        Console.WriteLine(oFile + " converted.");
                        pdfFileWithPwd.Dispose();
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