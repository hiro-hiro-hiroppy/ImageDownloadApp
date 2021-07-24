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
                int downloadStartPageNo = 1;

                for(int i = ir.StartPageNo; i <= ir.EndPageNo; i++)
                {
                    string imageName = i.ToString() + ir.FileExtension;
                    string imageUrl = @Path.Combine(ir.DownloadSourceFolder, imageName);

                    string downloadImageName = ir.DownloadFileStartName + downloadStartPageNo.ToString() + ir.FileExtension;
                    string downloadImageUrl = @Path.Combine(ir.DownloadFolder, downloadImageName);

                    client.DownloadFile(new Uri(imageUrl), downloadImageUrl);

                    downloadStartPageNo++;
                }
            }

        }
    }
}
