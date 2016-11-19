// Created by berkin on 18.11.2016.
// Finished by berkin on 20.11.2016 at 01:18

using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Collections.Generic;

namespace Huffman_Coding
{
    class Program
    {
        private static Stack<List<HufmannLetter>> BuildStack(HufmannLetter[] letters)
        {
            List<HufmannLetter> letterList = new List<HufmannLetter>();
            List<HufmannLetter> dropLetterList = new List<HufmannLetter>();
            Stack<List<HufmannLetter>> steps = new Stack<List<HufmannLetter>>();

            foreach (var letter in letters)
            {
                letterList.Add(letter);
            }

            while (true)
            {
                var lowestChar = letterList.Where(c => c.LetterFrequency == letterList.Min(x => x.LetterFrequency)).ElementAt(0);
                dropLetterList.Add(lowestChar);
                letterList.Remove(lowestChar);

                if (dropLetterList.Count == 2)
                {
                    string letterName = dropLetterList.ElementAt(0).LetterName + dropLetterList.ElementAt(1).LetterName;
                    int frequency = dropLetterList.ElementAt(0).LetterFrequency + dropLetterList.ElementAt(1).LetterFrequency;
                    HufmannLetter newLetter = new HufmannLetter(letterName, frequency);

                    dropLetterList.Add(newLetter);
                    letterList.Add(newLetter);

                    List<HufmannLetter> tempLetterLink = new List<HufmannLetter>(dropLetterList);
                    steps.Push(tempLetterLink);
                    dropLetterList.Clear();
                    if (letterList.Count == 1)
                        break;
                }
            }
            return steps;
        }

        private static void BuildTree(Stack<List<HufmannLetter>> letters)
        {
            HuffmanTree tree = new HuffmanTree();
            tree.Insert(letters.Peek()[2], 0, null);
            while (letters.Count != 0)
            {
                HufmannLetter parent = letters.Peek()[2];

                for (int i = letters.Peek().Count - 2; i >= 0; i--)
                {
                    tree.Insert(letters.Peek()[i], i, parent);
                }
                letters.Pop();
            }
        }

        private static double CodeLength(HufmannLetter[] letters)
        {
            double length = 0;
            foreach (var letter in letters)
            {
                int pathLength = (letter.LetterPath).Length;
                length += pathLength * letter.LetterProbability;
            }
            return length;
        }

        private static double ExpectedCodeLength(int letterCount)
        {
            return Math.Ceiling(Math.Log(letterCount+1, 2));
        }

        private static double HufmannEntropy(HufmannLetter[] letters)
        {
            double entropy = 0;
            foreach (var letter in letters)
            {
                double probabilty = letter.LetterProbability;
                entropy += probabilty * (-Math.Log(probabilty, 2));
            }
            return entropy;
        }

        private static void CalculateProbabilty(HufmannLetter[] letters)
        {
            int charSize = 20;
            foreach (var letter in letters)
            {
                letter.LetterProbability = (double)letter.LetterFrequency / charSize;
            }
        }

        private static HufmannLetter[] ReadFile(string filePath, HufmannLetter[] letter)
        {
            string message = File.ReadAllText(filePath, Encoding.UTF8);
            int differentCharNumber = 5;
            var letters = message.ToCharArray().GroupBy(x => x).Where(y => y.Any()).Select(z => new { charName = z.Key, charCount = z.Count() });

            for (int i = 0; i < differentCharNumber; i++)
            {
                // ReSharper disable once PossibleMultipleEnumeration
                var lettersAndFrequency = letters.ElementAt(i);
                letter[i] = new HufmannLetter(lettersAndFrequency.charName + "", lettersAndFrequency.charCount);
            }

            return letter;
        }

        private static bool FileExist(string filePath)
        {
            return File.Exists(filePath);
        }

        private static void CreateFile(string filePath)
        {
            string message = null;
            const int letterNumber = 20;
            Random random = new Random();
            var letters = new[] { 'A', 'B', 'C', 'D', 'E' };

            for (int i = 0; i < letterNumber; i++)
            {
                int selector = random.Next(0, 5);
                message += letters[selector];
            }

            using (TextWriter tw = new StreamWriter(filePath))
            {
                tw.WriteLine(message);
                tw.Close();
            }
        }

        private static void Main(string[] args)
        {
            string filePath = "message.txt";

            HufmannLetter[] letter = new HufmannLetter[5];
            if (FileExist(filePath))
            {
                letter = ReadFile(filePath, letter);
            }
            else
            {
                CreateFile(filePath);
                letter = ReadFile(filePath, letter);
            }

            CalculateProbabilty(letter);
            double entropy = HufmannEntropy(letter);

            Stack<List<HufmannLetter>> steps = BuildStack(letter);
            BuildTree(steps);

            double codeLength = CodeLength(letter);

            double expectedCodeLength = ExpectedCodeLength(letter.Length);

            foreach (var chars in letter)
            {
                Console.WriteLine("Char > {0} {1} {2} {3}", chars.LetterName, chars.LetterFrequency, chars.LetterProbability, chars.LetterPath);
            }

            Console.WriteLine("Entropy > " + entropy);
            Console.WriteLine("L > " + codeLength);
            Console.WriteLine("Expected Code Length > " + expectedCodeLength);

            Console.ReadLine();
        }
    }
}