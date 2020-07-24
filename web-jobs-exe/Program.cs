using System;
using System.IO;
using System.Net;
using System.Text;

namespace web_jobs_exe
{
    class Program
    {
        static void Main(string[] args)
        {

            try
            {
                Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
                Encoding tr_encoding = Encoding.GetEncoding(28599); // codepage:  28599	iso-8859-9	ISO 8859-9 Turkish https://docs.microsoft.com/en-us/windows/win32/intl/code-page-identifiers
                StreamWriter sw = new StreamWriter("C:\\log-path\\web-jobs-exe.log", true, tr_encoding);

                sw.WriteLine("=========================================================");
                sw.WriteLine("Begin\n");

                WebRequest request = WebRequest.Create("http://localhost/monthly-data"); // web page encoding iso-8859-9 Turkish

                request.Credentials = CredentialCache.DefaultCredentials;
                request.ContentType = "text/html; charset=iso-8859-9";// content type set encoding iso-8859-9 Turkish
                WebResponse response = request.GetResponse();

                sw.WriteLine("Server Statu => " + ((HttpWebResponse)response).StatusDescription);

                using (Stream dataStream = response.GetResponseStream())
                {
                    StreamReader reader = new StreamReader(dataStream, tr_encoding);
                    string responseFromServer = reader.ReadToEnd();
                    sw.WriteLine(responseFromServer);
                }

                response.Close();

                sw.WriteLine("End\n");
                sw.WriteLine("=========================================================\n\n");

                sw.Close();

            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
            }
            finally
            {
                Console.WriteLine("Log write");
            }

        }
    }
}
