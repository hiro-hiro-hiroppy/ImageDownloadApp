using System;
using System.Collections.Generic;
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
        public int EndPageNo { get; set; }

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

            Console.WriteLine("ダウンロード元のソースフォルダーは?");
            var downloadSourceFolder = Console.ReadLine();
            (bool, string) check1 = InputCheck.TryParseString(downloadSourceFolder, null);
            if(check1.Item1)
            {
                ir.DownloadSourceFolder = check1.Item2;
            }
            else
            {
                throw new Exception(Message.Message1);
            }

            Console.WriteLine("開始ページ番号は?");
            var startPageNo = Console.ReadLine();
            (bool, int) check2 = InputCheck.TryParseInt(startPageNo, 0);
            if (check2.Item1)
            {
                ir.StartPageNo = check2.Item2;
            }
            else
            {
                throw new Exception(Message.Message2);
            }

            Console.WriteLine("終了ページ番号は?");
            var endPageNo = Console.ReadLine();
            (bool, int) check3 = InputCheck.TryParseInt(endPageNo, 0);
            if(check3.Item1)
            {
                ir.EndPageNo = check3.Item2;
            }
            else
            {
                throw new Exception(Message.Message3);
            }

            Console.WriteLine("ダウンロード画像のファイル拡張子は?");
            var fileExtension = Console.ReadLine();
            (bool, string) check4 = InputCheck.CheckImageExtension(fileExtension);
            if (check4.Item1)
            {
                ir.FileExtension = check4.Item2;
            }
            else
            {
                throw new Exception(Message.Message4);
            }

            Console.WriteLine("ダウンロード先のフォルダーは?");
            var downloadFolder = Console.ReadLine();
            (bool, string) check5 = InputCheck.TryParseString(downloadFolder, null);
            if (check5.Item1)
            {
                ir.DownloadFolder = check5.Item2;
            }
            else
            {
                throw new Exception(Message.Message5);
            }

            Console.WriteLine("ダウンロード画像のファイル名は?");
            var downLoadFileStartName = Console.ReadLine();
            (bool, string) check6 = InputCheck.TryParseString(downLoadFileStartName, "");
            if (check6.Item1)
            {
                ir.DownloadFileStartName = check6.Item2;
            }
            else if(downLoadFileStartName == null)
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
