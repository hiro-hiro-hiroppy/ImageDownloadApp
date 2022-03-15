using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ImageDownloadApp
{
    class InputReadModel
    {
        /// <summary>
        /// ダウンロード元ソースフォルダー
        /// </summary>
        public string DownloadSourceFolder { get; set; }

        /// <summary>
        /// 開始ページ番号
        /// </summary>
        public int StartPageNo { get; set; }

        /// <summary>
        /// 終了ページ番号
        /// </summary>
        public int? EndPageNo { get; set; }

        /// <summary>
        /// ファイル拡張子
        /// </summary>
        public string FileExtension { get; set; }

        /// <summary>
        /// ダウンロード先フォルダー
        /// </summary>
        public string DownloadFolder { get; set; }

        /// <summary>
        /// ダウンロード先フォイル名
        /// </summary>
        public string DownloadFileStartName { get; set; }

    }

    class InputRead
    {
        /// <summary>
        /// 入力した値を読み取る
        /// </summary>
        public static InputReadModel SetInputRead()
        {
            InputReadModel ir = new InputReadModel();

            Console.WriteLine("ダウンロードする元画像のフォルダーは?(画像ファイルのパスでもOK)");
            var downloadSourceFolder = Console.ReadLine();
            (bool, string) check1 = InputCheck.TryParseString(downloadSourceFolder, null);
            if(check1.Item1 && downloadSourceFolder != null)
            {
                if(System.IO.Path.HasExtension(check1.Item2))
                {
                    int endSlashIndex = check1.Item2.LastIndexOf("/");
                    ir.DownloadSourceFolder = check1.Item2.Remove(endSlashIndex + 1);
                }
                else
                {
                    ir.DownloadSourceFolder = check1.Item2;
                }
            }
            else
            {
                throw new Exception(Message.Message1);
            }

            Console.WriteLine("開始ページ番号は?(デフォルトは1ページ目から)");
            var startPageNo = Console.ReadLine();
            (bool, int) check2 = InputCheck.TryParseInt(startPageNo, 0);
            
            if(check2.Item2 == 0 && startPageNo == "")
            {
                ir.StartPageNo = 1;
            }
            else
            {
                if (check2.Item1)
                {
                    ir.StartPageNo = check2.Item2;
                }
                else
                {
                    throw new Exception(Message.Message2);
                }
            }

            Console.WriteLine("終了ページ番号は?");
            var endPageNo = Console.ReadLine();
            (bool, int) check3 = InputCheck.TryParseInt(endPageNo, 0);
            if(check3.Item2 == 0 && endPageNo == "")
            {
                //ir.EndPageNo = null;
                throw new Exception("ちゃんと入力してください");
            }
            else
            {
                if (check3.Item1)
                {
                    ir.EndPageNo = check3.Item2;
                }
                else
                {
                    throw new Exception(Message.Message3);
                }
            }

            //Console.WriteLine("ダウンロード画像のファイル拡張子は?");
            //var fileExtension = Console.ReadLine();
            //(bool, string) check4 = InputCheck.CheckImageExtension(fileExtension);
            //if (check4.Item1)
            //{
            //    ir.FileExtension = check4.Item2;
            //}
            //else
            //{
            //    throw new Exception(Message.Message4);
            //}

            Console.WriteLine("保存するフォルダーパスは?");
            var downloadFolder = Console.ReadLine();
            (bool, string) check5 = InputCheck.TryParseString(downloadFolder, null);
            if (check5.Item1 && downloadFolder != "")
            {
                ir.DownloadFolder = check5.Item2;
            }
            else
            {
                throw new Exception(Message.Message5);
            }

            Console.WriteLine("ダウンロード画像の先頭につける名称は?");
            var downLoadFileStartName = Console.ReadLine();
            (bool, string) check6 = InputCheck.TryParseString(downLoadFileStartName, "");
            if (check6.Item1)
            {
                ir.DownloadFileStartName = check6.Item2;
            }
            else if(downLoadFileStartName == "")
            {
                ir.DownloadFileStartName = "";
            }
            else
            {
                throw new Exception(Message.Message6);
            }

            return ir;
        }

    }
}
