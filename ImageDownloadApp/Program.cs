using System;
using System.Net;

namespace ImageDownloadApp
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("画像の保存を開始します。");

                InputReadModel ir = InputRead.SetInputRead();
                Download.DownLoadImages(ir);

                Console.WriteLine("ダウンロード成功");
            }
            catch(Exception ex)
            {
                Console.WriteLine("エラー発生：");
                Console.WriteLine(ex.ToString());
                Console.WriteLine("ダウンロード失敗");
                Console.ReadKey();
            }

        }
    }
}
