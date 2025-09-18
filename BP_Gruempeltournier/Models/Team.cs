

namespace BP_Gruempeltournier.Models
{
    public class Team
    {
        public int TeamID { get; set; }
        public string Teamname { get; set; } = null!;
        public List<Spieler> Spieler { get; set; } = new();
        public override string ToString() => $"{TeamID}: {Teamname} (Spieler: {Spieler.Count})";
    }
}

