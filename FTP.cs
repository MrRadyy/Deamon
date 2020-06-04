using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentFTP;
using System.IO;
using System.Threading;
using System.Net;

namespace Deamon
{
    class FTP
    {

        public static void FTPBackup()
        {

            var temp = Api_Helper.Temp_Get().Result;  
            foreach (var item in temp)
            {

                string Filename = @"/asd/";
                string server = "ftp://endora.endora.cz";
                string username = "swombikcekujnet";
                string Password = "Aa123456";
                
                FtpClient ftpClient = new FtpClient(server, new NetworkCredential(username, Password));
                ftpClient.Connect();
                ftpClient.UploadDirectory(item.Source, Filename);

            }

            
            //FtpWebRequest request = (FtpWebRequest)WebRequest.Create(new Uri(string.Format("{0}/{1}", server, Filename)));
            //request.Method = WebRequestMethods.Ftp.UploadFile;

            //request.Credentials = new NetworkCredential(username, Password);

            //StreamReader sourceStream = new StreamReader(temp);
            //byte[] fileContents = Encoding.UTF8.GetBytes(sourceStream.ReadToEnd());
            //sourceStream.Close();

            //Stream requestStream = request.GetRequestStream();
            //requestStream.Write(fileContents, 0, fileContents.Length);
            //requestStream.Close();
            //FtpWebRequest request = (FtpWebRequest)WebRequest.Create(new Uri(string.Format("{0}/{1}", server, Filename)));
            //request.Method = WebRequestMethods.Ftp.UploadFile;
            //request.Credentials = new NetworkCredential(username, Password);
            //Stream FTPstream = request.GetRequestStream();
            //FileStream fs = File.OpenRead(Fullname);
        }



    }
}
