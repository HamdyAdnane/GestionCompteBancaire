using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionCompteBancaire.ConsoleApp
{
    public class CompteBancaire
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
