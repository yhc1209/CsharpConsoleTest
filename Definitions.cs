using System;
using System.IO;
using System.Text.Json;

namespace testCons
{
    public class Globals
    {
        /// <summary>祕密的ReadLine()。</summary>
        /// <param name="secretChar">要輸出的加密charector，預設為*。</param>
        /// <returns>輸出使用者輸入的字串。</returns>
        public static string GetSecretInput(char secretChar = '*')
        {
            string input = string.Empty;
            bool over = false;
            Console.Write("請輸入密碼：");
            while (!over)
            {
                ConsoleKeyInfo k = Console.ReadKey(true);
                switch (k.Key)
                {
                    case ConsoleKey.Enter:
                        over = true;
                        break;
                    case ConsoleKey.Backspace:
                        Console.Write("\b \b");
                        input = input.Substring(0, input.Length - 1);
                        break;
                    default:
                        if (Char.IsLetterOrDigit(k.KeyChar) || Char.IsPunctuation(k.KeyChar))
                        {
                            Console.Write(secretChar);
                            input += k.KeyChar;
                        }
                        break;
                }
            }
            Console.WriteLine();

            return input;
        }

        /// <summary>OTP驗證輸入，只有輸入數字才為有效。輸入R鍵進行重新傳送，輸入C鍵則取消並結束輸入。</summary>
        /// <param name="resendCD">重新傳送的冷卻時間。</param>
        /// <param name="timebased">是否為TOTP驗證輸入，是的話則不會有R鍵選項。</param>
        /// <returns>輸出使用者輸入的字串。</returns>
        public static string GetOTPInput(int resendCD = 15, bool timebased = false)
        {
            string desc = (timebased ? "請輸入OTP驗證碼：":"請輸入TOTP驗證碼：");
            int ts = Environment.TickCount;
            string input = string.Empty;
            bool over = false;
        
            Console.Write(desc);
            while (!over)
            {
                ConsoleKeyInfo k = Console.ReadKey(true);
                switch (k.Key)
                {
                    case ConsoleKey.C:
                    {
                        Console.Write(" 取消");
                        input = "c";
                        over = true;
                        break;
                    }
                    case ConsoleKey.R:
                    {
                        int recent = (1000 * resendCD) - (Environment.TickCount - ts);
                        if (recent <= 0)
                        {
                            input = "r";
                            over = true;
                        }
                        else
                        {
                            Console.WriteLine($" 需再等待{((float)recent/1000):F}秒後才能重新發送。");
                            Console.Write($"{desc}{input}");
                        }
                        break;
                    }
                    case ConsoleKey.Enter:
                        over = true;
                        break;
                    case ConsoleKey.Backspace:
                        Console.Write("\b \b");
                        input = input.Substring(0, input.Length - 1);
                        break;
                    case ConsoleKey.D0:
                    case ConsoleKey.D1:
                    case ConsoleKey.D2:
                    case ConsoleKey.D3:
                    case ConsoleKey.D4:
                    case ConsoleKey.D5:
                    case ConsoleKey.D6:
                    case ConsoleKey.D7:
                    case ConsoleKey.D8:
                    case ConsoleKey.D9:
                        Console.Write(k.KeyChar);
                        input += k.KeyChar;
                        break;
                }
            }
            Console.WriteLine();

            return input;
        }
    }
}