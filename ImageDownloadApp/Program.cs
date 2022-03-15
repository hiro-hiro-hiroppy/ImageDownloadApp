using System;
using System.Net;
using System.Threading.Tasks;

namespace ImageDownloadApp
{
    class Program
    {
        static async Task Main(string[] args)
        {
            try
            {
                Console.WriteLine("画像の保存を開始します。");

                InputReadModel ir = InputRead.SetInputRead();
                await Download.DownLoadImages(ir);

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
