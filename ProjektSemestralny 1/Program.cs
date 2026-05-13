using System;
using System.Collections.Generic;

namespace ProjektSemestralny_1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<Zajecia> grafik = new List<Zajecia>();

            grafik.Add(new Zajecia
            {
                Nazwa = "Programowanie",
                Rodzaj = "Laboratorium",
                Data = new DateTime(2026, 5, 20, 10, 0, 0),
                LimitMiejsc = 20,
                Zapisani = 0
            });

            Console.WriteLine("Pierwsze zajęcia:");
            Console.WriteLine(grafik[0].Nazwa);
            Console.WriteLine(grafik[0].Rodzaj);
            Console.WriteLine(grafik[0].Data);
            Console.WriteLine("Wolne miejsca: " + (grafik[0].LimitMiejsc - grafik[0].Zapisani));

            grafik[0].ZarezerwujMiejsce();

            Console.WriteLine("Zapisani po rezerwacji: " + grafik[0].Zapisani);

            Console.ReadLine();
        }
    }
}