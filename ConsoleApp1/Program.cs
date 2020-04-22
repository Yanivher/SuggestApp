using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class Program
    {
        static Dictionary<string, Dictionary<string, int>> dic = new Dictionary<string, Dictionary<string, int>>();
        static void Main(string[] args)
        {
            //load csv
            var items = LoadCsv();
            LoadValuesToDic(items);

            //read user input and get results
            string word = GetUserInput();
            while (!string.IsNullOrEmpty(word))
            {
                Stopwatch sw = new Stopwatch();
                sw.Start();

                var result = Suggest(word);

                sw.Stop();

                Console.WriteLine($"Time:{sw.Elapsed}{Environment.NewLine}" +
                    $"Results:{Environment.NewLine}" +
                    $"{string.Join(Environment.NewLine, result)}{Environment.NewLine}");

                //read next word
                word = GetUserInput();
            }


            //Console.WriteLine(string.Join(Environment.NewLine, result.Select(x => new { x.Key, x.Value })));
        }

        private static string GetUserInput()
        {
            Console.WriteLine("Enter Search Term:");
            var word = Console.ReadLine();
            return word;
        }

        public static void LoadValuesToDic(List<Tuple<string, int>> items)
        {
            foreach (var item in items)
            {
                for (int i = 0; i < item.Item2; i++)
                {
                    AddWord(item.Item1);
                }
            }
        }

        public static List<Tuple<string, int>> LoadCsv()
        {
            List<Tuple<string, int>> list = new List<Tuple<string, int>>();
            string dir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            using (var reader = new StreamReader(dir + @"\SearchTermsDB.csv"))
            {
                //skip first line header
                reader.ReadLine();
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();

                    if (line == null)
                    {
                        break;
                    }

                    var values = line.Split(',');

                    list.Add(new Tuple<string, int>(values[0], int.Parse(values[1])));
                }
            }
            return list;
        }

        public static string[] Suggest(string startingWith)
        {
            return dic[startingWith].Keys.Take(10).ToArray();
            //return  dic[startingWith];
        }

        private static void AddWord(string word)
        {
            //iterate sub strings. "abc" => "a", "ab", "abc"
            for (var i = 1; i <= word.Length; i++)
            {
                var subString = word.Substring(0, i);

                //is existing or new sub string
                if (dic.ContainsKey(subString))
                {
                    //is word exist
                    if (dic[subString].ContainsKey(word))
                    {
                        //exists - add 1 to current frequency
                        dic[subString][word] = dic[subString][word] + 1;

                        //sort the list                        
                        var orderedList = dic[subString].OrderByDescending(w => w.Value);
                        dic[subString] = orderedList.ToDictionary(k => k.Key, v => v.Value);
                    }
                    else
                    {
                        //not exists - create list and init frequency to 1
                        dic[subString].Add(word, 1);
                    }
                }
                else
                {
                    //new substring - create dicitionary with new word and init frequency 
                    var list = new Dictionary<string, int>();
                    list.Add(word, 1);
                    dic.Add(subString, list);
                }
            }
        }



    }


}
