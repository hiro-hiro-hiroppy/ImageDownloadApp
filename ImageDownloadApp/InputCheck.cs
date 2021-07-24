using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ImageDownloadApp
{
    class InputCheck
    {

        /// <summary>
        /// string型に変換可能か調べる
        /// </summary>
        /// <param name="value">trueなら変換可能</param>
        /// <param name="defaultValue">変換不可の場合の既定値</param>
        /// <returns>(変換可能か否か、変換後の値or既定値)</returns>
        public static (bool, string) TryParseString(dynamic value, string defaultValue)
        {
            string parseValue = null;
            bool parseResult = false;

            try
            {
                parseValue = value.ToString();
                parseResult = true;
            }
            catch (Exception ex)
            {
                parseValue = defaultValue;
            }

            return (parseResult, parseValue);
        }

        /// <summary>
        /// int型に変換可能か調べる
        /// </summary>
        /// <param name="value">trueなら変換可能</param>
        /// <param name="defaultValue">変換不可の場合の既定値</param>
        /// <returns>(変換可能か否か、変換後の値or既定値)</returns>
        public static (bool, int) TryParseInt(dynamic value, int defaultValue)
        {
            int parseValue = 0;
            bool parseResult = false;

            if (int.TryParse(value, out parseValue))
            {
                parseResult = true;
            }
            else
            {
                parseValue = defaultValue;
            }

            return (parseResult, parseValue);
        }

        /// <summary>
        /// 画像拡張子かどうか調べる
        /// </summary>
        /// <param name="value"></param>
        /// <returns>(画像拡張子か否か、画像拡張子)</returns>
        public static (bool, string) CheckImageExtension(string value)
        {
            string valueExtension = null;
            bool result = false;

            string[] imgExtension = new string[] { ".png", ".jpg", ".jpeg" };

            if(!value.StartsWith("."))
            {
                value = "." + value;
            }

            if(imgExtension.Contains(value))
            {
                result = true;
                valueExtension = value;
            }

            return (result, valueExtension);
        }




    }
}
