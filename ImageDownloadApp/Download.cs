using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace ImageDownloadApp
{
    class Download
    {
        /// <summary>
        /// 画像ファイル群をダウンロードする
        /// </summary>
        /// <param name="ir"></param>
        public static void DownLoadImages(InputReadModel ir)
        {
            using (WebClient client = new WebClient())
            {
                if(ir.EndPageNo == null)
                {
                    ir.EndPageNo = int.MaxValue;
                }
                string[] fileExtensions = new string[] { ".png", ".jpg", ".jpeg", ".avif" };

                for(int i = ir.StartPageNo; i <= ir.EndPageNo; i++)
                {
                    bool isExistImageUrl = false;
                    string imageName = "";
                    string imageUrl = "";

                    foreach(string fileExtension in fileExtensions)
                    {
                        imageName = i.ToString() + fileExtension;
                        imageUrl = ir.DownloadSourceFolder + imageName;

                        if (UrlLExists(imageUrl))
                        {
                            isExistImageUrl = true;
                            ir.FileExtension = fileExtension;
                            break;
                        }
                    }

                    if(isExistImageUrl == false)
                    {
                        throw new Exception("ファイルが存在しません。");
                    }

                    string downloadImageName = ir.DownloadFileStartName + i.ToString() + ir.FileExtension;
                    string downloadImageUrl = @Path.Combine(ir.DownloadFolder, downloadImageName);
                    client.DownloadFile(new Uri(imageUrl), downloadImageUrl);
                    Console.WriteLine($"{i}ページ目 ダウンロード成功");
                }
            }
        }

        public static bool UrlLExists(string url)
        {
            url = url.Replace("\\", "/");
            bool result = false;
            HttpWebResponse response = null;
            var request = (HttpWebRequest)WebRequest.Create(url);
            request.Timeout = 1200; // miliseconds
            request.Method = "HEAD";
            try
            {
                response = (HttpWebResponse)request.GetResponse();
                result = true;
            }
            catch (WebException ex)
            {
                /* A WebException will be thrown if the status of the response is not `200 OK` */
            }
            finally
            {
                // Don't forget to close your response.
                if (response != null)
                {
                    response.Close();
                }
            }

            return result;
        }
    }
}
