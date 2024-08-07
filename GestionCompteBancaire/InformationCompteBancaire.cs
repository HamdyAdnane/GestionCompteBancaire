namespace GestionCompteBancaire.ConsoleApp
{
    public class InformationCompteBancaire
    {
        public int Id { get; set; }
        public string? Reference { get; set; }
        public string? Libelle { get; set; }
        public string? Societe { get; set; }
        public override string ToString()
        {
            return $"[{Id}] {Reference} {Libelle} {Societe}";
        }
    }
}
