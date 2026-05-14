using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Globalization;

namespace ProjektSemestralny_1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            UtworzPlikRezerwacjiJesliNieIstnieje();

            List<Zajecia> grafik = WczytajGrafikZPliku();
            List<Rezerwacja> rezerwacje = WczytajRezerwacjeZPliku();

            if (grafik.Count == 0)
            {
                UtworzPrzykladowyGrafik();
                grafik = WczytajGrafikZPliku();
            }

            if (grafik.Count == 0)
            {
                Console.WriteLine("Nie udalo sie wczytac grafiku.");
                Console.WriteLine("Folder programu:");
                Console.WriteLine(Directory.GetCurrentDirectory());
                Console.ReadLine();
                return;
            }

            bool dziala = true;

            while (dziala)
            {
                Console.Clear();
                Console.WriteLine("=== SYSTEM REZERWACJI ZAJEC SPORTOWYCH ===");
                Console.WriteLine("1. Pokaz grafik");
                Console.WriteLine("2. Zarezerwuj miejsce");
                Console.WriteLine("3. Pokaz rezerwacje");
                Console.WriteLine("4. Zapisz rezerwacje do CSV");
                Console.WriteLine("5. Wyjscie");
                Console.Write("Wybierz opcje: ");

                string wybor = Console.ReadLine();

                switch (wybor)
                {
                    case "1":
                        PokazGrafik(grafik, rezerwacje);
                        break;

                    case "2":
                        ZarezerwujMiejsce(grafik, rezerwacje);
                        break;

                    case "3":
                        PokazRezerwacje(grafik, rezerwacje);
                        break;

                    case "4":
                        ZapiszRezerwacjeDoPliku(rezerwacje);
                        Console.WriteLine("Zapisano rezerwacje do pliku CSV.");
                        Console.ReadLine();
                        break;

                    case "5":
                        dziala = false;
                        break;

                    default:
                        Console.WriteLine("Nieprawidlowa opcja.");
                        Console.ReadLine();
                        break;
                }
            }
        }

        static void UtworzPrzykladowyGrafik()
        {
            List<string> linie = new List<string>();

            linie.Add("Id;Nazwa;Poziom;Data;CzasTrwaniaMinuty;LimitMiejsc;Prowadzacy;Miejsce");
            linie.Add("1;Tenis;Poczatkujacy;2026-05-20 10:00;60;2;Jan Kowalski;Kort 1");
            linie.Add("2;Tenis;Sredni;2026-05-20 11:00;60;2;Jan Kowalski;Kort 2");
            linie.Add("3;Badminton;Dla wszystkich;2026-05-20 12:00;60;2;Anna Nowak;Hala A");
            linie.Add("4;Squash;Poczatkujacy;2026-05-20 13:30;45;2;Piotr Zielinski;Kort squash 1");
            linie.Add("5;Tenis stolowy;Dla wszystkich;2026-05-20 15:00;60;4;Marek Wisniewski;Sala rekreacyjna");
            linie.Add("6;Zdrowe plecy;Dla wszystkich;2026-05-20 16:30;60;15;Katarzyna Wozniak;Sala fitness");
            linie.Add("7;Yoga;Dla wszystkich;2026-05-20 17:30;60;15;Ewa Zielinska;Sala fitness");
            linie.Add("8;Boks;Sredni;2026-05-20 18:30;90;20;Tomasz Lewandowski;Sala walk");
            linie.Add("9;Judo;Poczatkujacy;2026-05-20 20:00;90;20;Pawel Kaminski;Sala walk");
            linie.Add("10;Gimnastyka;Dla dzieci;2026-05-21 16:00;60;12;Monika Kowal;Sala gimnastyczna");
            linie.Add("11;Pilka nozna;Dla wszystkich;2026-05-21 18:00;90;14;Adam Mazur;Boisko");
            linie.Add("12;Koszykowka;Dla wszystkich;2026-05-21 19:30;90;10;Michal Nowicki;Hala glowna");

            File.WriteAllLines("grafik.csv", linie);
        }

        static void UtworzPlikRezerwacjiJesliNieIstnieje()
        {
            if (!File.Exists("rezerwacje.csv"))
            {
                File.WriteAllText("rezerwacje.csv", "IdZajec;ImieNazwisko;DataRezerwacji");
            }
        }

        static List<Zajecia> WczytajGrafikZPliku()
        {
            List<Zajecia> grafik = new List<Zajecia>();

            if (!File.Exists("grafik.csv"))
            {
                return grafik;
            }

            string[] linie = File.ReadAllLines("grafik.csv");

            for (int i = 1; i < linie.Length; i++)
            {
                string linia = linie[i];

                if (string.IsNullOrWhiteSpace(linia))
                {
                    continue;
                }

                string[] dane = linia.Split(';');

                if (dane.Length == 8)
                {
                    int id;
                    int czasTrwania;
                    int limitMiejsc;
                    DateTime data;

                    bool poprawneId = int.TryParse(dane[0].Trim(), out id);
                    bool poprawnaData = DateTime.TryParseExact(
                        dane[3].Trim(),
                        "yyyy-MM-dd HH:mm",
                        CultureInfo.InvariantCulture,
                        DateTimeStyles.None,
                        out data
                    );
                    bool poprawnyCzas = int.TryParse(dane[4].Trim(), out czasTrwania);
                    bool poprawnyLimit = int.TryParse(dane[5].Trim(), out limitMiejsc);

                    if (poprawneId && poprawnaData && poprawnyCzas && poprawnyLimit)
                    {
                        Zajecia zajecia = new Zajecia
                        {
                            Id = id,
                            Nazwa = dane[1].Trim(),
                            Poziom = dane[2].Trim(),
                            Data = data,
                            CzasTrwaniaMinuty = czasTrwania,
                            LimitMiejsc = limitMiejsc,
                            Prowadzacy = dane[6].Trim(),
                            Miejsce = dane[7].Trim()
                        };

                        grafik.Add(zajecia);
                    }
                }
            }

            return grafik;
        }

        static List<Rezerwacja> WczytajRezerwacjeZPliku()
        {
            List<Rezerwacja> rezerwacje = new List<Rezerwacja>();

            if (!File.Exists("rezerwacje.csv"))
            {
                return rezerwacje;
            }

            string[] linie = File.ReadAllLines("rezerwacje.csv");

            for (int i = 1; i < linie.Length; i++)
            {
                string linia = linie[i];

                if (string.IsNullOrWhiteSpace(linia))
                {
                    continue;
                }

                string[] dane = linia.Split(';');

                if (dane.Length == 3)
                {
                    int idZajec;
                    DateTime dataRezerwacji;

                    bool poprawneId = int.TryParse(dane[0].Trim(), out idZajec);
                    bool poprawnaData = DateTime.TryParseExact(
                        dane[2].Trim(),
                        "yyyy-MM-dd HH:mm",
                        CultureInfo.InvariantCulture,
                        DateTimeStyles.None,
                        out dataRezerwacji
                    );

                    if (poprawneId && poprawnaData)
                    {
                        Rezerwacja rezerwacja = new Rezerwacja
                        {
                            IdZajec = idZajec,
                            ImieNazwisko = dane[1].Trim(),
                            DataRezerwacji = dataRezerwacji
                        };

                        rezerwacje.Add(rezerwacja);
                    }
                }
            }

            return rezerwacje;
        }

        static void ZapiszRezerwacjeDoPliku(List<Rezerwacja> rezerwacje)
        {
            List<string> linie = new List<string>();

            linie.Add("IdZajec;ImieNazwisko;DataRezerwacji");

            foreach (Rezerwacja rezerwacja in rezerwacje)
            {
                string linia = $"{rezerwacja.IdZajec};{rezerwacja.ImieNazwisko};{rezerwacja.DataRezerwacji:yyyy-MM-dd HH:mm}";
                linie.Add(linia);
            }

            File.WriteAllLines("rezerwacje.csv", linie);
        }

        static void PokazGrafik(List<Zajecia> grafik, List<Rezerwacja> rezerwacje)
        {
            Console.Clear();
            Console.WriteLine("=== GRAFIK ZAJEC SPORTOWYCH ===");
            Console.WriteLine();

            foreach (Zajecia zajecia in grafik)
            {
                int liczbaRezerwacji = rezerwacje.Count(r => r.IdZajec == zajecia.Id);

                Console.WriteLine($"{zajecia.Id}. {zajecia.Nazwa}");
                Console.WriteLine($"   Poziom: {zajecia.Poziom}");
                Console.WriteLine($"   Data: {zajecia.Data:yyyy-MM-dd HH:mm}");
                Console.WriteLine($"   Czas trwania: {zajecia.CzasTrwaniaMinuty} min");
                Console.WriteLine($"   Miejsca: {liczbaRezerwacji}/{zajecia.LimitMiejsc}");
                Console.WriteLine($"   Prowadzacy: {zajecia.Prowadzacy}");
                Console.WriteLine($"   Miejsce: {zajecia.Miejsce}");
                Console.WriteLine();
            }

            Console.WriteLine("Nacisnij Enter, aby wrocic do menu.");
            Console.ReadLine();
        }

        static void ZarezerwujMiejsce(List<Zajecia> grafik, List<Rezerwacja> rezerwacje)
        {
            Console.Clear();
            Console.WriteLine("=== REZERWACJA MIEJSCA ===");
            Console.WriteLine();

            foreach (Zajecia zajecia in grafik)
            {
                int liczbaRezerwacji = rezerwacje.Count(r => r.IdZajec == zajecia.Id);

                Console.WriteLine($"{zajecia.Id}. {zajecia.Nazwa} | {zajecia.Data:yyyy-MM-dd HH:mm} | miejsca: {liczbaRezerwacji}/{zajecia.LimitMiejsc} | {zajecia.Miejsce}");
            }

            Console.WriteLine();
            Console.Write("Podaj ID zajec: ");
            string idTekst = Console.ReadLine();

            int idZajec;

            if (!int.TryParse(idTekst, out idZajec))
            {
                Console.WriteLine("Nieprawidlowe ID.");
                Console.ReadLine();
                return;
            }

            Zajecia wybraneZajecia = grafik.FirstOrDefault(z => z.Id == idZajec);

            if (wybraneZajecia == null)
            {
                Console.WriteLine("Nie istnieja zajecia o takim ID.");
                Console.ReadLine();
                return;
            }

            int aktualnieZapisani = rezerwacje.Count(r => r.IdZajec == idZajec);

            if (aktualnieZapisani >= wybraneZajecia.LimitMiejsc)
            {
                Console.WriteLine("Brak wolnych miejsc.");
                Console.ReadLine();
                return;
            }

            Console.Write("Podaj imie i nazwisko: ");
            string imieNazwisko = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(imieNazwisko))
            {
                Console.WriteLine("Imie i nazwisko nie moze byc puste.");
                Console.ReadLine();
                return;
            }

            Rezerwacja nowaRezerwacja = new Rezerwacja
            {
                IdZajec = idZajec,
                ImieNazwisko = imieNazwisko,
                DataRezerwacji = DateTime.Now
            };

            rezerwacje.Add(nowaRezerwacja);
            ZapiszRezerwacjeDoPliku(rezerwacje);

            Console.WriteLine("Zarezerwowano miejsce.");
            Console.ReadLine();
        }

        static void PokazRezerwacje(List<Zajecia> grafik, List<Rezerwacja> rezerwacje)
        {
            Console.Clear();
            Console.WriteLine("=== LISTA REZERWACJI ===");
            Console.WriteLine();

            if (rezerwacje.Count == 0)
            {
                Console.WriteLine("Brak rezerwacji.");
            }

            foreach (Rezerwacja rezerwacja in rezerwacje)
            {
                Zajecia zajecia = grafik.FirstOrDefault(z => z.Id == rezerwacja.IdZajec);

                if (zajecia != null)
                {
                    Console.WriteLine($"{rezerwacja.ImieNazwisko} -> {zajecia.Nazwa}");
                    Console.WriteLine($"Data zajec: {zajecia.Data:yyyy-MM-dd HH:mm}");
                    Console.WriteLine($"Miejsce: {zajecia.Miejsce}");
                    Console.WriteLine($"Data rezerwacji: {rezerwacja.DataRezerwacji:yyyy-MM-dd HH:mm}");
                    Console.WriteLine();
                }
            }

            Console.WriteLine("Nacisnij Enter, aby wrocic do menu.");
            Console.ReadLine();
        }
    }
}