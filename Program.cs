using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Snooker
{
    class Program
    {
        struct SnookerPlayer
        {
            public int Rajtszam;
            public string Nev;
            public string Orszag;
            public int Penz;
        }

        static void Pause() => Console.ReadKey();
        static void Print(string message) => Console.WriteLine(message);
        static void TaskNumber(int taskNumber) => Console.WriteLine($"{taskNumber}. Feladat.");

        static void Main(string[] args)
        {
            List<SnookerPlayer> playersList = new List<SnookerPlayer>();
            string[] fileLines = File.ReadAllLines("snooker.txt", Encoding.UTF8);

            for (int i = 1; i < fileLines.Length; i++)
            {
                if (fileLines[i].Length > 0)
                {
                    string[] columns = fileLines[i].Split(';');
                    SnookerPlayer newPlayer = new SnookerPlayer
                    {
                        Rajtszam = Convert.ToInt32(columns[0]),
                        Nev = columns[1],
                        Orszag = columns[2],
                        Penz = Convert.ToInt32(columns[3])
                    };

                    playersList.Add(newPlayer);
                }
            }

            TaskNumber(1);
            TaskNumber(2);
            Print("Adatok beolvasva és tárolva.");
            TaskNumber(3);
            Print($"{playersList.Count} versenyző adatait tartalmazza az állomány.");
            TaskNumber(4);

            int totalPrize = 0;
            int highestEarnerIndex = 0;
            bool isNorwegianPlayer = false;
            int[] countryStats = new int[5];

            for (int index = 0; index < playersList.Count; index++)
            {
                totalPrize += playersList[index].Penz;

                if ((playersList[index].Orszag == "Kína") && (playersList[index].Penz > playersList[highestEarnerIndex].Penz))
                {
                    highestEarnerIndex = index;
                }

                if (playersList[index].Orszag == "Norvégia")
                {
                    isNorwegianPlayer = true;
                }

                switch (playersList[index].Orszag)
                {
                    case "Skócia":
                        countryStats[4]++;
                        break;
                    case "Wales":
                        countryStats[3]++;
                        break;
                    case "Anglia":
                        countryStats[2]++;
                        break;
                    case "Kína":
                        countryStats[1]++;
                        break;
                }
            }

            Console.WriteLine($"Az átlag díjérték: {Convert.ToDouble(totalPrize / playersList.Count)}");
            TaskNumber(5);

            Console.WriteLine("A legjobban kereső Kínai versenyző:");
            Console.WriteLine($"	Helyzés: {playersList[highestEarnerIndex].Rajtszam}");
            Console.WriteLine($"	Név: {playersList[highestEarnerIndex].Nev}");
            Console.WriteLine($"	Orszag: {playersList[highestEarnerIndex].Orszag}");
            Console.WriteLine($"	Nyeremény összeg forintban: {playersList[highestEarnerIndex].Penz * 380}");

            TaskNumber(6);
            if (isNorwegianPlayer)
            {
                Print("Van Norvég versenyző.");
            }

            TaskNumber(7);
            Console.WriteLine("Statisztika:");
            Console.WriteLine($"	Kína - {countryStats[1]} fő.");
            Console.WriteLine($"	Anglia - {countryStats[2]} fő.");
            Console.WriteLine($"	Wales - {countryStats[3]} fő.");
            Console.WriteLine($"	Skócia - {countryStats[4]} fő.");

            Pause();
        }
    }
}
