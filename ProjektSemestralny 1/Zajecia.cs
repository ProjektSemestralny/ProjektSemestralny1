using System;

namespace ProjektSemestralny_1
{
    internal class Zajecia
    {
        public string Nazwa { get; set; }
        public string Rodzaj { get; set; }
        public DateTime Data { get; set; }
        public int LimitMiejsc { get; set; }
        public int Zapisani { get; set; }

        public bool CzySaWolneMiejsca()
        {
            return Zapisani < LimitMiejsc;
        }

        public void ZarezerwujMiejsce()
        {
            if (CzySaWolneMiejsca())
            {
                Zapisani++;
                Console.WriteLine("Zarezerwowano miejsce.");
            }
            else
            {
                Console.WriteLine("Brak wolnych miejsc.");
            }
        }
    }
}