using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace NinetyNine
{
    class Program
    {
        static int points = 0;
        static List<string> hand1 = new List<string>(), hand2 = new List<string>();
        static Dictionary<string, int> values = new Dictionary<string, int>
        {
            { "2" , 2 },
            { "3" , 3 },
            { "4" , 4 },
            { "5" , 5 },
            { "6" , 6 },
            { "7" , 7 },
            { "8" , 8 },
            { "9" , 9 },
            { "T" , 10 },
            { "J" , 11 },
            { "Q" , 12 },
            { "K" , 13 },
            { "A" , 14 },
        };

        static void Main(string[] args)
        {
            string[] lines = File.ReadAllLines(@"data.txt");
            lines = FormatText(lines);

            string[] hands = lines[0].Split(',');

            for (int i = 1; i < lines.Length; i++)
            {
                string[] line = lines[i].Split(',');
                points = int.Parse(line[0]);
                DealHands(hands);

                string winner = "";

                bool p1turn = true;
                for (int j = 1; j <= line.Length; j++)
                {
                    if(p1turn)
                    {
                        PlayCard(hand1);
                        if (j < line.Length)
                            hand1.Add(line[j]);
                    }
                    else
                    {
                        PlayCard(hand2);
                        if (j < line.Length)
                            hand2.Add(line[j]);
                    }
                    if (points > 99)
                    {
                        if (p1turn) winner = "Player #2";
                        else winner = "Player #1";
                        break;
                    }
                    p1turn = !p1turn;
                }

                Console.WriteLine(i + ".  " + points + ", " + winner);
            }
        }
        
        static void DealHands(string[] hands)
        {
            hand1.Clear();
            hand2.Clear();
            for (int i = 0; i < 5; i++)
            {
                hand1.Add(hands[i]);
                hand2.Add(hands[i + 5]);
            }
        }
        static void PlayCard(List<string> hand)
        {
            string card = PickCard(hand);
            hand.Remove(card);
            UpdatePoints(card);
        }

        static string PickCard(List<string> hand)
        {
            string card = "";
            List<int> handVals = new List<int>();
            for (int i = 0; i < hand.Count; i++)
            {
                handVals.Add(values[hand[i]]);
            }
            handVals.Sort();
            for (int i = 0; i < hand.Count; i++)
            {
                if (values[hand[i]] == handVals[2])
                    card = hand[i];
            }
            return card;
        }

        static void UpdatePoints(string card)
        {
            int points1 = points;
            if (card == "9")
            {
                
            }
            else if (card == "T")
            {
                points -= 10;
            }
            else if (card == "7")
            {
                if (points + 7 <= 99)
                    points += 7;
                else
                    points++;
            }
            else
            {
                points += values[card];
            }

            if ((points >= 34 && points1 < 34) || (points < 34 && points1 >= 34) ||
                (points >= 56 && points1 < 56) || (points < 56 && points1 >= 56) ||
                (points >= 78 && points1 < 78) || (points < 78 && points1 >= 78))
            {
                points += 5;
            }
        }

        static string[] FormatText(string[] lines)
        {
            for (int i = 0; i < lines.Length; i++)
            {
                lines[i] = new string(lines[i].Where(c => c != ' ').ToArray()).Substring(2);
            }
            return lines;
        }
    }
}
