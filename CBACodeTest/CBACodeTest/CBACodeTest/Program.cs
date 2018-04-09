using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace CBACodeTest
{
    class Program
    {
        static void Main(string[] args)
        {
            string rule1InputFilePath = @"../../../input/rule1.txt";
            string rule2InputFilePath = @"../../../input/rule2.txt";
            string rule3InputFilePath = @"../../../input/rule3.txt";
            string rule4InputFilePath = @"../../../input/rule4.txt";

            string rule1outputFilePath = @"../../../output/average_length_of_words_starting_with_a.txt";
            string rule2outputFilePath = @"../../../output/count_of_e_in_words_starting_with_b.txt";
            string rule3outputFilePath = @"../../../output/longest_words_starting_with_abc.txt";
            string rule4outputFilePath = @"../../../output/count_of_sequence_of_words_starting_withs_c_and_a.txt";

            string yesNo;

            do
            {
                if (File.Exists(rule1InputFilePath))
                {
                    if (!File.Exists(rule1outputFilePath))
                    {
                        File.Create(rule1outputFilePath);
                    }
                    int total = 0;
                    int numOfWords = 0;
                    double average = 0.0;

                    string rule = ConfigurationManager.AppSettings.Get("rule1");
                    string ruleFromFile = File.ReadLines(rule1InputFilePath).First();

                    using (StreamReader sr = new StreamReader(rule1InputFilePath))
                    {
                        String line;

                        if (ruleFromFile.StartsWith("-"))
                        {
                            rule = ruleFromFile[1].ToString();
                        }

                        while ((line = sr.ReadLine()) != null)
                        {
                            string[] words = line.Split(' ');
                            foreach (var word in words)
                            {
                                for (int i = 0; i < rule.Length; i++)
                                {
                                    if (word.StartsWith(rule, StringComparison.CurrentCultureIgnoreCase))
                                    {
                                        total += word.Length;
                                        numOfWords++;
                                    }
                                }

                            }
                        }
                    }

                    if (numOfWords > 0)
                    {
                        average = total / numOfWords;
                    }

                    File.WriteAllLines(rule1outputFilePath, new string[] { "Average length of word-" + average + "\r\n" });


                }

                if (File.Exists(rule2InputFilePath))
                {
                    if (!File.Exists(rule2outputFilePath))
                    {
                        File.Create(rule2outputFilePath);
                    }

                    string[] ruleCharacter = ConfigurationManager.AppSettings.Get("rule2").Split(',');
                    string result = string.Empty;
                    int total = 0;

                    string ruleFromFile = File.ReadLines(rule2InputFilePath).First();

                    
                    using (StreamReader sr = new StreamReader(rule2InputFilePath))
                    {
                        String line;
                        if (ruleFromFile.StartsWith("-"))
                        {
                            ruleCharacter = ruleFromFile.Substring(1).Split(',');
                            sr.ReadLine();
                        }

                        while ((line = sr.ReadLine()) != null)
                        {
                            string[] words = line.Split(' ');
                            foreach (var word in words)
                            {
                                if (word[0].ToString().Equals(ruleCharacter[0].ToString(), StringComparison.InvariantCultureIgnoreCase))
                                {
                                    foreach (char c in word)
                                    {
                                        if (c.ToString().Equals(ruleCharacter[1].ToString(), StringComparison.CurrentCultureIgnoreCase))
                                        {
                                            total++;
                                        }
                                    }

                                    result += word + "-" + total + "\r\n";
                                }
                                total = 0;

                            }
                        }
                    }

                    File.WriteAllLines(rule2outputFilePath, new string[] { result });

                }

                if (File.Exists(rule3InputFilePath))
                {
                    if (!File.Exists(rule3outputFilePath))
                    {
                        File.Create(rule3outputFilePath);
                    }
                    string[] rule = ConfigurationManager.AppSettings.Get("rule3").Split(',');
                    int[] ruleLengthCount = new int[rule.Length];

                    string ruleFromFile = File.ReadLines(rule3InputFilePath).First();
               
                    using (StreamReader sr = new StreamReader(rule3InputFilePath))
                    {
                        String line;

                        if (ruleFromFile.StartsWith("-"))
                        {
                            rule = ruleFromFile.Substring(1).Split(',');
                            ruleLengthCount = new int[rule.Length];
                            sr.ReadLine();
                        }
                        
                        while ((line = sr.ReadLine()) != null)
                        {
                            string[] words = line.Split(' ');
                            foreach (var word in words)
                            {
                                for (int i = 0; i < rule.Length; i++)
                                {
                                    if (word.StartsWith(rule[i],StringComparison.CurrentCultureIgnoreCase) && word.Length > ruleLengthCount[i])
                                    {
                                        ruleLengthCount[i] = word.Length;
                                    }
                                }

                            }
                        }
                    }

                    string result = string.Empty;
                    for (int i = 0; i < rule.Length; i++)
                    {
                        result += "Total for " + rule[i] + "-" + ruleLengthCount[i] + "\r\n";
                    }
                    File.WriteAllLines(rule3outputFilePath, new string[] { result });
                }

                if (File.Exists(rule4InputFilePath))
                {
                    if (!File.Exists(rule4outputFilePath))
                    {
                        File.Create(rule4outputFilePath);
                    }
                    string[] rule = ConfigurationManager.AppSettings.Get("rule4").Split(',');
                    int total = 0;

                    string ruleFromFile = File.ReadLines(rule4InputFilePath).First();

                    

                    using (StreamReader sr = new StreamReader(rule4InputFilePath))
                    {
                        String line;

                        if (ruleFromFile.StartsWith("-"))
                        {
                            rule = ruleFromFile.Substring(1).Split(',');
                            sr.ReadLine();
                        }

                        while ((line = sr.ReadLine()) != null)
                        {
                            string[] words = line.Split(' ');
                            for(int i=0; i< words.Length; i++)
                            {
                                if (words[i].StartsWith(rule[0],StringComparison.CurrentCultureIgnoreCase) && words[i+1].StartsWith(rule[1], StringComparison.CurrentCultureIgnoreCase))
                                {
                                    total++;
                                }
                            }
                        }
                    }
                    File.WriteAllLines(rule4outputFilePath, new string[] { "Count of sequence of words starting withs c and a - " + total });
                }

                Console.WriteLine("Press 'e' to exit or 'y' to perform another operation");
                yesNo = Console.ReadLine().ToLower();
            } while (yesNo.Equals("y"));
        }
    }
}
