using Microsoft.VisualBasic.FileIO;
using System;
using System.Net.Mail;

namespace _2d_array_string
{
    internal class Program
    {


        static int[][] createJugged(int row, int col)
        {
            int[][] tmp = new int[row][];
            for (int i = 0; i < row; i++)
            {
                tmp[i] = new int[col++];
            }
            return tmp;
        }

        static void FillJugged(int[][] arr, int min = 1, int max = 20)
        {
            var rnd = new Random();
            for (int i = 0; i < arr.Length; i++)
            {
                for (int j = 0; j < arr[i].Length; j++)
                {
                    arr[i][j] = rnd.Next(min, max + 1);
                }
            }
        }



        static void ShiftDown(int[][] arr, int shift)
        {
            if (shift == arr.Length || shift == 0)
            {
                return;
            }

            var newarr = createJugged(5,2);
            for (int i = 0; i < arr.Length; i++)
            {
                int newRow = (i + shift) % arr.Length;
                newarr[newRow] = arr[i];
            }
            for (int i = 0; i < arr.Length; i++)
            {
                arr[i] = newarr[i];
            }
        }
        static void ShiftUp(int[][] arr, int shift)
        {
            if (shift == arr.Length || shift == 0)
            {
                return;
            }

            var newarr = createJugged(5, 2);
            for (int i = 0; i < arr.Length; i++)
            {
                int newRow = (arr.Length + i - shift) % arr.Length;
                newarr[newRow] = arr[i];
            }
            for (int i = 0; i < arr.Length; i++)
            {
                arr[i] = newarr[i];
            }
        }

        static void PrintJugged(int[][] arr)
        {

            for (int i = 0; i < arr.Length; i++)
            {
                for (int j = 0; j < arr[i].Length; j++)
                {
                    Console.Write($"{arr[i][j],10}");
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }
        static void PushRow(ref int[][] arr, int[] row)
        {
            Array.Resize(ref arr, arr.Length + 1);
            arr[arr.Length - 1] = row;
        }
        static void DelRow(ref int[][] arr, int index)
        {
            int[][] tmp = new int[arr.Length - 1][];
            for (int i = 0; i < arr.Length - 1; i++)
            {
                if (i < index)
                {
                    tmp[i] = arr[i];
                }
                else tmp[i] = arr[i + 1];
            }
            Array.Clear(arr);
            Array.Resize(ref arr, tmp.Length);
            for (int i = 0; i < tmp.Length; i++)
            {
                arr[i] = tmp[i];
            }
        }
        static void MinMax(int[][] arr, out int min, out int max) {
            min = arr[0][0];
            max = arr[0][0];
            foreach (var row in arr)
            {
                foreach (var col in row)
                {
                    if (col < min)
                    {
                        min = col;
                    }
                    if (col > max)
                    {
                        max = col;
                    }
                }
            }
        }
        static void DelChars(string line, out string edited, params char[] chars)
        {
            do
            {
                line = line.Remove(line.IndexOfAny(chars), 1);
            } while (line.IndexOfAny(chars) > 0);
            edited = line;
        }
        static void Statistic(string line) {
            string alphabet = "abcdefghijklmnopqrstuvwxyz";
            foreach (var letter in alphabet)
            {
                int amount = line.Count(l => l == letter);
                if (amount > 0)
                {
                    Console.Write(letter + " [" + amount + "] ");
                    for (int i = 0; i < amount; i++)
                    {
                        Console.Write("*");
                    }
                    Console.WriteLine();
                }
            }
        }
        static void KeyWordsStat(string prog)
        {
            string[] csharpKeywords = new string[]
            {
            "abstract", "as", "base", "bool", "break", "byte", "case", "catch", "char", "checked", "class", "const",
            "continue", "decimal", "default", "delegate", "do", "double", "else", "enum", "event", "explicit",
            "extern", "false", "finally", "fixed", "float", "for", "foreach", "goto", "if", "implicit", "in",
            "int", "interface", "internal", "is", "lock", "long", "namespace", "new", "null", "object", "operator",
            "out", "override", "params", "private", "protected", "public", "readonly", "ref", "return", "sbyte",
            "sealed", "short", "sizeof", "stackalloc", "static", "string", "struct", "switch", "this", "throw",
            "true", "try", "typeof", "uint", "ulong", "unchecked", "unsafe", "ushort", "using", "virtual", "void",
            "volatile", "while"
            };

            var wordCount = new Dictionary<string, int>();
            string[] keywords = prog.Split("\n\r();{}:<=][\",*.=>=++   ".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
            foreach (var word in keywords)
            {
                if (csharpKeywords.Contains(word))
                {
                    if (wordCount.ContainsKey(word))
                    {
                        wordCount[word]++;
                    }
                    else wordCount[word] = 1;
                }
            }

            
            foreach (var word in wordCount.OrderByDescending(e => e.Value))
            {
                Console.WriteLine(word);
            }
        }

        static void Main(string[] args)
        {
            var arr2d = createJugged(5, 2);
            FillJugged(arr2d);
            PrintJugged(arr2d);
            ShiftDown(arr2d, 2);
            PrintJugged(arr2d);
            ShiftUp(arr2d, 2);
            PrintJugged(arr2d);
            PushRow(ref arr2d, new int[] { 2, 3, 4 });
            PrintJugged(arr2d);
            DelRow(ref arr2d, 3);
            PrintJugged(arr2d);

            int min = 0, max = 0;
            MinMax(arr2d, out min, out max);
            Console.Write(min + "\t" + max);
            Console.WriteLine();

            string line = Console.ReadLine();
            char symbol = (char)Console.Read();
            int start = 0;
            char[] chars = line.ToCharArray();
            int index = 0;
            int lastIndex = 0;
            while (true)
            {
                lastIndex = index;
                index = line.IndexOf(symbol, start);
                if (index < 0)
                {
                    break;
                }
                chars[index] = Char.ToUpper(symbol);
                start = index + 1;
            }
            Console.WriteLine(lastIndex);
            string nline = new string(chars);
            string dline = nline.Remove(lastIndex + 1);
            Console.WriteLine(dline);

            string tline = "There are many variations of passages of Lorem Ipsum available, but the majority have suffered alteration in some form, by injected humour, or randomised words which don't look even slightly believable. If you are going to use a passage of Lorem Ipsum, you need to be sure there isn't anything embarrassing hidden in the middle of text. All the Lorem Ipsum generators on the Internet tend to repeat predefined chunks as necessary, making this the first true generator on the Internet. It uses a dictionary of over 200 Latin words, combined with a handful of model sentence structures, to generate Lorem Ipsum which looks reasonable. The generated Lorem Ipsum is therefore always free from repetition, injected humour, or non-characteristic words etc.";
            string res = "";
            DelChars(tline,out res, ".,' ".ToCharArray());
            Console.WriteLine(res);

            Console.WriteLine(tline);
            Statistic(tline);


            string prog = "using System;\r\nclass Multipication\r\n{\r\n    static void Main()\r\n    {\r\n        int no;\r\n \r\n        Console.Write(\"Enter a no : \");\r\n        no = Convert.ToInt32(Console.ReadLine());\r\n        while (no <= 0)\r\n        {\r\n            Console.WriteLine(\"You entered an invalid no\");\r\n \r\n            Console.Write(\"Enter a no great than 0: \");\r\n            no = Convert.ToInt32(Console.ReadLine());\r\n        }\r\n        Console.WriteLine(\"Multiplication Table :\");\r\n        for (int i = 1; i <= no; i++)\r\n        {\r\n            Console.WriteLine(\"\\n\");\r\n \r\n            for (int j = 1; j <= no; j++)\r\n            {\r\n                Console.Write(\"{0,6}\", i * j);\r\n            }\r\n \r\n        }\r\n        Console.Read();\r\n    }\r\n}";
            KeyWordsStat(prog);
        }

    }
}