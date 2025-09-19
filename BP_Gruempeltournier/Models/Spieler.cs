namespace BP_Gruempeltournier.Models
{
   public class Spieler
    {
        public int SpielerID { get; set; }
        public string Vorname { get; set; } = null!;
        public string Nachname { get; set; } = null!;
        public DateOnly Geburtstag { get; set; }
        public string Wohnort { get; set; } = null!;
    }
}

