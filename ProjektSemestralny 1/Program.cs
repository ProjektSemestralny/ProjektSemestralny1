using System;
using System.Collections.Generic;
using System.IO;
namespace ProjektSemestralny_1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<Zajecia> grafik = WczytajZPliku();

            if (grafik.Count == 0)
            {
                grafik.Add(new Zajecia
                {
                    Nazwa = "Programowanie",
                    Rodzaj = "Laboratorium",
                    Data = new DateTime(2026, 5, 20, 10, 0, 0),
                    LimitMiejsc = 20,
                    Zapisani = 0
                });
            }

            bool dziala = true;

            while (dziala)
            {
                Console.Clear();
                Console.WriteLine("=== SYSTEM REZERWACJI ZAJEC ===");
                Console.WriteLine("1. Pokaz zajecia");
                Console.WriteLine("2. Zarezerwuj miejsce");
                Console.WriteLine("3. Dodaj zajecia");
                Console.WriteLine("4. Usun zajecia");
                Console.WriteLine("5. Zapisz do pliku CSV");
                Console.WriteLine("6. Wyjscie");
                Console.Write("Wybierz opcje: ");

                string wybor = Console.ReadLine();

                switch (wybor)
                {
                    case "1":
                        PokazZajecia(grafik);
                        break;

                    case "2":
                        Zarezerwuj(grafik);
                        break;

                    case "3":
                        DodajZajecia(grafik);
                        break;

                    case "4":
                        UsunZajecia(grafik);
                        break;

                    case "5":
                        ZapiszDoPliku(grafik);
                        Console.WriteLine("Zapisano grafik do pliku CSV.");
                        Console.ReadLine();
                        break;

                    case "6":
                        dziala = false;
                        break;
                }
            }
        }

        static void PokazZajecia(List<Zajecia> grafik)
        {
            Console.Clear();
            Console.WriteLine("=== LISTA ZAJEC ===");

            for (int i = 0; i < grafik.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {grafik[i].Nazwa} - {grafik[i].Rodzaj}");
                Console.WriteLine($"   Data: {grafik[i].Data}");
                Console.WriteLine($"   Miejsca: {grafik[i].Zapisani}/{grafik[i].LimitMiejsc}");
                Console.WriteLine();
            }

            Console.WriteLine("Nacisnij Enter, aby wrocic do menu.");
            Console.ReadLine();
        }

        static void Zarezerwuj(List<Zajecia> grafik)
        {
            Console.Clear();
            PokazZajeciaBezPauzy(grafik);

            Console.Write("Podaj numer zajec do rezerwacji: ");
            string tekst = Console.ReadLine();

            if (int.TryParse(tekst, out int numer))
            {
                int indeks = numer - 1;

                if (indeks >= 0 && indeks < grafik.Count)
                {
                    grafik[indeks].ZarezerwujMiejsce();
                }
                else
                {
                    Console.WriteLine("Nie ma zajec o takim numerze.");
                }
            }
            else
            {
                Console.WriteLine("Podano nieprawidlowa liczbe.");
            }

            Console.WriteLine("Nacisnij Enter, aby wrocic do menu.");
            Console.ReadLine();
        }

        static void DodajZajecia(List<Zajecia> grafik)
        {
            Console.Clear();

            Console.Write("Podaj nazwe zajec: ");
            string nazwa = Console.ReadLine();

            Console.Write("Podaj rodzaj zajec: ");
            string rodzaj = Console.ReadLine();

            Console.Write("Podaj grupe zajec: ");
            string grupa = Console.ReadLine();

            Console.Write("Podaj poziom zajec: ");
            string poziom = Console.ReadLine();

            Console.Write("Podaj prowadzacego: ");
            string prowadzacy = Console.ReadLine();

            Console.Write("Podaj date i godzine zajec, np. 2026-05-20 10:00: ");
            string dataTekst = Console.ReadLine();

            if (!DateTime.TryParse(dataTekst, out DateTime data))
            {
                Console.WriteLine("Nieprawidlowa data.");
                Console.ReadLine();
                return;
            }

            Console.Write("Podaj limit miejsc: ");
            string limitTekst = Console.ReadLine();

            if (!int.TryParse(limitTekst, out int limit) || limit <= 0)
            {
                Console.WriteLine("Nieprawidlowy limit miejsc.");
                Console.ReadLine();
                return;
            }

            Zajecia noweZajecia = new Zajecia
            {
                Nazwa = nazwa,
                Rodzaj = rodzaj,
                Grupa = grupa,
                Poziom = poziom,
                Data = data,
                LimitMiejsc = limit,
                Zapisani = 0,
                Prowadzacy = prowadzacy
            };

            grafik.Add(noweZajecia);

            Console.WriteLine("Dodano zajecia.");
            Console.ReadLine();
        }

        static void PokazZajeciaBezPauzy(List<Zajecia> grafik)
        {
            Console.WriteLine("=== LISTA ZAJEC ===");

            for (int i = 0; i < grafik.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {grafik[i].Nazwa} - {grafik[i].Rodzaj}, miejsca: {grafik[i].Zapisani}/{grafik[i].LimitMiejsc}");
            }

            Console.WriteLine();
        }



        static void ZapiszDoPliku(List<Zajecia> grafik)
        {
            List<string> linie = new List<string>();

            foreach (Zajecia zajecia in grafik)
            {
                string linia = $"{zajecia.Nazwa};{zajecia.Rodzaj};{zajecia.Grupa};{zajecia.Poziom};{zajecia.Data};{zajecia.LimitMiejsc};{zajecia.Zapisani};{zajecia.Prowadzacy}";
                linie.Add(linia);
            }

            File.WriteAllLines("grafik.csv", linie);
        }

        static List<Zajecia> WczytajZPliku()
        {
            List<Zajecia> grafik = new List<Zajecia>();

            if (!File.Exists("grafik.csv"))
            {
                return grafik;
            }

            string[] linie = File.ReadAllLines("grafik.csv");

            foreach (string linia in linie)
            {
                string[] dane = linia.Split(';');

                if (dane.Length == 8)
                {
                    Zajecia zajecia = new Zajecia
                    {
                        Nazwa = dane[0],
                        Rodzaj = dane[1],
                        Grupa = dane[2],
                        Poziom = dane[3],
                        Data = DateTime.Parse(dane[4]),
                        LimitMiejsc = int.Parse(dane[5]),
                        Zapisani = int.Parse(dane[6]),
                        Prowadzacy = dane[7]
                    };

                    grafik.Add(zajecia);
                }
            }

            return grafik;
        }
        static void UsunZajecia(List<Zajecia> grafik)
        {
            Console.Clear();
            PokazZajeciaBezPauzy(grafik);

            Console.Write("Podaj numer zajec do usuniecia: ");
            string tekst = Console.ReadLine();

            if (int.TryParse(tekst, out int numer))
            {
                int indeks = numer - 1;

                if (indeks >= 0 && indeks < grafik.Count)
                {
                    grafik.RemoveAt(indeks);
                    Console.WriteLine("Usunieto zajecia.");
                }
                else
                {
                    Console.WriteLine("Nie ma zajec o takim numerze.");
                }
            }
            else
            {
                Console.WriteLine("Podano nieprawidlowa liczbe.");
            }

            Console.ReadLine();
        }
    }
}