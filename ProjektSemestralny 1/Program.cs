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
                Console.WriteLine("4. Anuluj rezerwacje");
                Console.WriteLine("5. Modyfikuj rezerwacje");
                Console.WriteLine("6. Dodaj nowe zajecia");
                Console.WriteLine("7. Usun zajecia");
                Console.WriteLine("8. Raport zajetosci zasobu");
                Console.WriteLine("9. Wyjscie");
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
                        AnulujRezerwacje(grafik, rezerwacje);
                        break;

                    case "5":
                        ModyfikujRezerwacje(grafik, rezerwacje);
                        break;

                    case "6":
                        DodajZajecia(grafik);
                        break;

                    case "7":
                        UsunZajecia(grafik, rezerwacje);
                        break;

                    case "8":
                        RaportZajetosciZasobu(grafik, rezerwacje);
                        break;

                    case "9":
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

        static void ZapiszGrafikDoPliku(List<Zajecia> grafik)
        {
            List<string> linie = new List<string>();

            linie.Add("Id;Nazwa;Poziom;Data;CzasTrwaniaMinuty;LimitMiejsc;Prowadzacy;Miejsce");

            foreach (Zajecia zajecia in grafik)
            {
                string linia = $"{zajecia.Id};{zajecia.Nazwa};{zajecia.Poziom};{zajecia.Data:yyyy-MM-dd HH:mm};{zajecia.CzasTrwaniaMinuty};{zajecia.LimitMiejsc};{zajecia.Prowadzacy};{zajecia.Miejsce}";
                linie.Add(linia);
            }

            File.WriteAllLines("grafik.csv", linie);
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

            int idZajec;

            if (!int.TryParse(Console.ReadLine(), out idZajec))
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

            for (int i = 0; i < rezerwacje.Count; i++)
            {
                Rezerwacja rezerwacja = rezerwacje[i];
                Zajecia zajecia = grafik.FirstOrDefault(z => z.Id == rezerwacja.IdZajec);

                if (zajecia != null)
                {
                    Console.WriteLine($"{i + 1}. {rezerwacja.ImieNazwisko} -> {zajecia.Nazwa}");
                    Console.WriteLine($"   Data zajec: {zajecia.Data:yyyy-MM-dd HH:mm}");
                    Console.WriteLine($"   Miejsce: {zajecia.Miejsce}");
                    Console.WriteLine($"   Data rezerwacji: {rezerwacja.DataRezerwacji:yyyy-MM-dd HH:mm}");
                    Console.WriteLine();
                }
            }

            Console.WriteLine("Nacisnij Enter, aby wrocic do menu.");
            Console.ReadLine();
        }

        static void AnulujRezerwacje(List<Zajecia> grafik, List<Rezerwacja> rezerwacje)
        {
            Console.Clear();
            Console.WriteLine("=== ANULOWANIE REZERWACJI ===");
            Console.WriteLine();

            if (rezerwacje.Count == 0)
            {
                Console.WriteLine("Brak rezerwacji do anulowania.");
                Console.ReadLine();
                return;
            }

            for (int i = 0; i < rezerwacje.Count; i++)
            {
                Rezerwacja rezerwacja = rezerwacje[i];
                Zajecia zajecia = grafik.FirstOrDefault(z => z.Id == rezerwacja.IdZajec);

                if (zajecia != null)
                {
                    Console.WriteLine($"{i + 1}. {rezerwacja.ImieNazwisko} -> {zajecia.Nazwa}, {zajecia.Data:yyyy-MM-dd HH:mm}, {zajecia.Miejsce}");
                }
            }

            Console.WriteLine();
            Console.Write("Podaj numer rezerwacji do anulowania: ");

            int numer;

            if (!int.TryParse(Console.ReadLine(), out numer))
            {
                Console.WriteLine("Nieprawidlowy numer.");
                Console.ReadLine();
                return;
            }

            int indeks = numer - 1;

            if (indeks < 0 || indeks >= rezerwacje.Count)
            {
                Console.WriteLine("Nie ma rezerwacji o takim numerze.");
                Console.ReadLine();
                return;
            }

            rezerwacje.RemoveAt(indeks);
            ZapiszRezerwacjeDoPliku(rezerwacje);

            Console.WriteLine("Rezerwacja zostala anulowana.");
            Console.ReadLine();
        }

        static void ModyfikujRezerwacje(List<Zajecia> grafik, List<Rezerwacja> rezerwacje)
        {
            Console.Clear();
            Console.WriteLine("=== MODYFIKOWANIE REZERWACJI ===");
            Console.WriteLine();

            if (rezerwacje.Count == 0)
            {
                Console.WriteLine("Brak rezerwacji do modyfikacji.");
                Console.ReadLine();
                return;
            }

            for (int i = 0; i < rezerwacje.Count; i++)
            {
                Rezerwacja rezerwacja = rezerwacje[i];
                Zajecia zajecia = grafik.FirstOrDefault(z => z.Id == rezerwacja.IdZajec);

                if (zajecia != null)
                {
                    Console.WriteLine($"{i + 1}. {rezerwacja.ImieNazwisko} -> {zajecia.Nazwa}, {zajecia.Data:yyyy-MM-dd HH:mm}, {zajecia.Miejsce}");
                }
            }

            Console.WriteLine();
            Console.Write("Podaj numer rezerwacji do modyfikacji: ");

            int numer;

            if (!int.TryParse(Console.ReadLine(), out numer))
            {
                Console.WriteLine("Nieprawidlowy numer.");
                Console.ReadLine();
                return;
            }

            int indeks = numer - 1;

            if (indeks < 0 || indeks >= rezerwacje.Count)
            {
                Console.WriteLine("Nie ma rezerwacji o takim numerze.");
                Console.ReadLine();
                return;
            }

            Rezerwacja wybranaRezerwacja = rezerwacje[indeks];

            Console.WriteLine();
            Console.WriteLine("Co chcesz zmienic?");
            Console.WriteLine("1. Imie i nazwisko");
            Console.WriteLine("2. Zajecia");
            Console.Write("Wybierz opcje: ");

            string wybor = Console.ReadLine();

            if (wybor == "1")
            {
                Console.Write("Podaj nowe imie i nazwisko: ");
                string noweImieNazwisko = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(noweImieNazwisko))
                {
                    Console.WriteLine("Imie i nazwisko nie moze byc puste.");
                    Console.ReadLine();
                    return;
                }

                wybranaRezerwacja.ImieNazwisko = noweImieNazwisko;
                ZapiszRezerwacjeDoPliku(rezerwacje);

                Console.WriteLine("Zmieniono dane rezerwacji.");
                Console.ReadLine();
            }
            else if (wybor == "2")
            {
                Console.Clear();
                Console.WriteLine("=== WYBIERZ NOWE ZAJECIA ===");
                Console.WriteLine();

                foreach (Zajecia zajecia in grafik)
                {
                    int liczbaRezerwacji = rezerwacje.Count(r => r.IdZajec == zajecia.Id);
                    Console.WriteLine($"{zajecia.Id}. {zajecia.Nazwa} | {zajecia.Data:yyyy-MM-dd HH:mm} | miejsca: {liczbaRezerwacji}/{zajecia.LimitMiejsc} | {zajecia.Miejsce}");
                }

                Console.WriteLine();
                Console.Write("Podaj ID nowych zajec: ");

                int noweIdZajec;

                if (!int.TryParse(Console.ReadLine(), out noweIdZajec))
                {
                    Console.WriteLine("Nieprawidlowe ID.");
                    Console.ReadLine();
                    return;
                }

                Zajecia noweZajecia = grafik.FirstOrDefault(z => z.Id == noweIdZajec);

                if (noweZajecia == null)
                {
                    Console.WriteLine("Nie istnieja zajecia o takim ID.");
                    Console.ReadLine();
                    return;
                }

                int aktualnieZapisani = rezerwacje.Count(r => r.IdZajec == noweIdZajec);

                if (noweIdZajec != wybranaRezerwacja.IdZajec && aktualnieZapisani >= noweZajecia.LimitMiejsc)
                {
                    Console.WriteLine("Brak wolnych miejsc na wybranych zajeciach.");
                    Console.ReadLine();
                    return;
                }

                wybranaRezerwacja.IdZajec = noweIdZajec;
                ZapiszRezerwacjeDoPliku(rezerwacje);

                Console.WriteLine("Zmieniono zajecia w rezerwacji.");
                Console.ReadLine();
            }
            else
            {
                Console.WriteLine("Nieprawidlowa opcja.");
                Console.ReadLine();
            }
        }

        static void DodajZajecia(List<Zajecia> grafik)
        {
            Console.Clear();
            Console.WriteLine("=== DODAWANIE NOWYCH ZAJEC ===");

            Console.Write("Nazwa zajec: ");
            string nazwa = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(nazwa))
            {
                Console.WriteLine("Nazwa nie moze byc pusta.");
                Console.ReadLine();
                return;
            }

            Console.Write("Poziom: ");
            string poziom = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(poziom))
            {
                Console.WriteLine("Poziom nie moze byc pusty.");
                Console.ReadLine();
                return;
            }

            Console.Write("Data (w formacie YYYY-MM-DD HH:MM): ");
            DateTime data;

            while (!DateTime.TryParseExact(Console.ReadLine(), "yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out data))
            {
                Console.Write("Bledny format! Podaj date jeszcze raz (YYYY-MM-DD HH:MM): ");
            }

            Console.Write("Czas trwania (minuty): ");
            int czas;

            while (!int.TryParse(Console.ReadLine(), out czas) || czas <= 0)
            {
                Console.Write("Bledna wartosc. Podaj liczbe wieksza od 0: ");
            }

            Console.Write("Limit miejsc: ");
            int limit;

            while (!int.TryParse(Console.ReadLine(), out limit) || limit <= 0)
            {
                Console.Write("Bledna wartosc. Podaj liczbe wieksza od 0: ");
            }

            Console.Write("Prowadzacy: ");
            string prowadzacy = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(prowadzacy))
            {
                Console.WriteLine("Prowadzacy nie moze byc pusty.");
                Console.ReadLine();
                return;
            }

            Console.Write("Miejsce: ");
            string miejsce = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(miejsce))
            {
                Console.WriteLine("Miejsce nie moze byc puste.");
                Console.ReadLine();
                return;
            }

            bool konflikt = grafik.Any(z =>
                z.Miejsce.ToLower() == miejsce.ToLower()
                && z.Data == data
            );

            if (konflikt)
            {
                Console.WriteLine("Istnieja juz zajecia w tym samym miejscu o tej samej godzinie.");
                Console.ReadLine();
                return;
            }

            int noweId = grafik.Count > 0 ? grafik.Max(z => z.Id) + 1 : 1;

            Zajecia noweZajecia = new Zajecia
            {
                Id = noweId,
                Nazwa = nazwa,
                Poziom = poziom,
                Data = data,
                CzasTrwaniaMinuty = czas,
                LimitMiejsc = limit,
                Prowadzacy = prowadzacy,
                Miejsce = miejsce
            };

            grafik.Add(noweZajecia);
            ZapiszGrafikDoPliku(grafik);

            Console.WriteLine();
            Console.WriteLine("Pomyslnie dodano zajecia i zaktualizowano plik CSV.");
            Console.ReadLine();
        }

        static void UsunZajecia(List<Zajecia> grafik, List<Rezerwacja> rezerwacje)
        {
            Console.Clear();
            Console.WriteLine("=== USUWANIE ZAJEC ===");
            Console.WriteLine();

            foreach (Zajecia z in grafik)
            {
                Console.WriteLine($"{z.Id}. {z.Nazwa} | {z.Data:yyyy-MM-dd HH:mm} | {z.Prowadzacy}");
            }

            Console.WriteLine();
            Console.Write("Podaj ID zajec do usuniecia: ");

            int idZajec;

            if (!int.TryParse(Console.ReadLine(), out idZajec))
            {
                Console.WriteLine("Nieprawidlowe ID.");
                Console.ReadLine();
                return;
            }

            Zajecia doUsuniecia = grafik.FirstOrDefault(z => z.Id == idZajec);

            if (doUsuniecia == null)
            {
                Console.WriteLine("Nie znaleziono zajec o takim ID.");
                Console.ReadLine();
                return;
            }

            if (rezerwacje.Any(r => r.IdZajec == idZajec))
            {
                Console.WriteLine("Nie mozna usunac tych zajec, poniewaz sa juz na nie zapisane osoby.");
                Console.ReadLine();
                return;
            }

            grafik.Remove(doUsuniecia);
            ZapiszGrafikDoPliku(grafik);

            Console.WriteLine("Zajecia zostaly usuniete.");
            Console.ReadLine();
        }

        static void RaportZajetosciZasobu(List<Zajecia> grafik, List<Rezerwacja> rezerwacje)
        {
            Console.Clear();
            Console.WriteLine("=== RAPORT ZAJETOSCI ZASOBU ===");
            Console.WriteLine();

            Console.Write("Podaj nazwe zasobu / miejsca, np. Sala fitness, Kort 1, Boisko: ");
            string miejsce = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(miejsce))
            {
                Console.WriteLine("Nazwa zasobu nie moze byc pusta.");
                Console.ReadLine();
                return;
            }

            Console.Write("Podaj rok, np. 2026: ");
            int rok;

            if (!int.TryParse(Console.ReadLine(), out rok))
            {
                Console.WriteLine("Nieprawidlowy rok.");
                Console.ReadLine();
                return;
            }

            Console.Write("Podaj miesiac, np. 5: ");
            int miesiac;

            if (!int.TryParse(Console.ReadLine(), out miesiac) || miesiac < 1 || miesiac > 12)
            {
                Console.WriteLine("Nieprawidlowy miesiac.");
                Console.ReadLine();
                return;
            }

            List<Zajecia> zajeciaWZasobie = grafik
                .Where(z => z.Miejsce.ToLower() == miejsce.ToLower()
                            && z.Data.Year == rok
                            && z.Data.Month == miesiac)
                .ToList();

            if (zajeciaWZasobie.Count == 0)
            {
                Console.WriteLine("Brak zajec dla podanego zasobu w wybranym miesiacu.");
                Console.ReadLine();
                return;
            }

            int sumaMiejsc = 0;
            int sumaRezerwacji = 0;

            Console.WriteLine();
            Console.WriteLine($"Raport dla zasobu: {miejsce}");
            Console.WriteLine($"Okres: {miesiac}/{rok}");
            Console.WriteLine();

            foreach (Zajecia zajecia in zajeciaWZasobie)
            {
                int liczbaRezerwacji = rezerwacje.Count(r => r.IdZajec == zajecia.Id);

                sumaMiejsc += zajecia.LimitMiejsc;
                sumaRezerwacji += liczbaRezerwacji;

                Console.WriteLine($"{zajecia.Nazwa} | {zajecia.Data:yyyy-MM-dd HH:mm}");
                Console.WriteLine($"Zajetosc: {liczbaRezerwacji}/{zajecia.LimitMiejsc}");
                Console.WriteLine();
            }

            double procentZajetosci = 0;

            if (sumaMiejsc > 0)
            {
                procentZajetosci = (double)sumaRezerwacji / sumaMiejsc * 100;
            }

            Console.WriteLine("=== PODSUMOWANIE ===");
            Console.WriteLine($"Liczba zajec w miesiacu: {zajeciaWZasobie.Count}");
            Console.WriteLine($"Liczba wszystkich miejsc: {sumaMiejsc}");
            Console.WriteLine($"Liczba rezerwacji: {sumaRezerwacji}");
            Console.WriteLine($"Zajetosc procentowa: {procentZajetosci:F2}%");

            Console.WriteLine();
            Console.WriteLine("Nacisnij Enter, aby wrocic do menu.");
            Console.ReadLine();
        }
    }
}