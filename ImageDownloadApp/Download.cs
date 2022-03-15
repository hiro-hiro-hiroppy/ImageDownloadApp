using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ImageDownloadApp
{
    class Download
    {
        /// <summary>
        /// 画像ファイル群をダウンロードする
        /// </summary>
        /// <param name="ir"></param>
        public static async Task DownLoadImages(InputReadModel ir)
        {

            if (ir.EndPageNo == null)
            {
                ir.EndPageNo = int.MaxValue;
            }

            Dictionary<string, string> urlFileNames = new Dictionary<string, string>();
            for (int i = ir.StartPageNo; i <= ir.EndPageNo; i++)
            {
                string imageName = i.ToString();
                string url = ir.DownloadSourceFolder + imageName;
                urlFileNames.Add(url, imageName);
            }

            var sem = new SemaphoreSlim(16);
            var downloadTasks = urlFileNames.Select(async x =>
            {
                await sem.WaitAsync();
                try
                {
                    await ReadFromUrlAsync(ir, x);
                    Console.WriteLine($"{x.Key} ダウンロード");
                    sem.Release();
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
                //finally
                //{
                //    sem.Release();
                //}
            });
            await Task.WhenAll(downloadTasks);




            //for(int i = ir.StartPageNo; i <= ir.EndPageNo; i++)
            //{
            //    bool isExistImageUrl = false;
            //    string imageName = "";
            //    string imageUrl = "";

            //    foreach(string fileExtension in fileExtensions)
            //    {
            //        imageName = i.ToString() + fileExtension;
            //        imageUrl = ir.DownloadSourceFolder + imageName;

            //        if (UrlLExists(imageUrl))
            //        {
            //            isExistImageUrl = true;
            //            ir.FileExtension = fileExtension;
            //            break;
            //        }
            //    }

            //    if(isExistImageUrl == false)
            //    {
            //        throw new Exception("ファイルが存在しません。");
            //    }

            //    string downloadImageName = ir.DownloadFileStartName + i.ToString() + ir.FileExtension;
            //    string downloadImageUrl = @Path.Combine(ir.DownloadFolder, downloadImageName);
            //    client.DownloadFile(new Uri(imageUrl), downloadImageUrl);
            //    Console.WriteLine($"{i}ページ目 ダウンロード成功");
            //}
        }

        static async Task ReadFromUrlAsync(InputReadModel ir, KeyValuePair<string, string> x)
        {
            string[] fileExtensions = new string[] { ".jpg", ".png", ".jpeg", ".avif" };

            using (WebClient client = new WebClient())
            {
                bool isExistImageUrl = false;
                string imageName = "";
                string imageUrl = "";

                foreach (string fileExtension in fileExtensions)
                {
                    imageUrl = x.Key + fileExtension;
                    imageName = x.Value + fileExtension;

                    if (UrlLExists(imageUrl))
                    {
                        isExistImageUrl = true;
                        ir.FileExtension = fileExtension;
                        break;
                    }
                }

                if (isExistImageUrl == false)
                {
                    throw new Exception("ファイルが存在しません。");
                }

                string downloadImageName = ir.DownloadFileStartName + x.Value + ir.FileExtension;
                string downloadImageUrl = @Path.Combine(ir.DownloadFolder, downloadImageName);
                await client.DownloadFileTaskAsync(new Uri(imageUrl), downloadImageUrl);
            }
        }

        public static bool UrlLExists(string url)
        {
            url = url.Replace("\\", "/");
            bool result = false;
            HttpWebResponse response = null;
            var request = (HttpWebRequest)WebRequest.Create(url);
            request.Timeout = 30000; // miliseconds
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
