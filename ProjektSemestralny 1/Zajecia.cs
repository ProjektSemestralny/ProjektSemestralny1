using System;

namespace ProjektSemestralny_1
{
    internal class Zajecia
    {
        public int Id { get; set; }
        public string Nazwa { get; set; }
        public string Poziom { get; set; }
        public DateTime Data { get; set; }
        public int CzasTrwaniaMinuty { get; set; }
        public int LimitMiejsc { get; set; }
        public string Prowadzacy { get; set; }
        public string Miejsce { get; set; }
    }
}