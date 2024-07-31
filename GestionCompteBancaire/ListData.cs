using System;
using GestionCompteBancaire.ConsoleApp;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace GestionCompteBancaire.ConsoleApp
{
    public class ListData
    {
        public static List<CompteBancaire> GetCompteBancaire()
        {
            List<CompteBancaire> compte = new List<CompteBancaire>();
            int[] index = new int[100];
            int i = 0;
            while (i >= 0)
            {
                index[i] = i + 1;
            }
            compte.Add(new CompteBancaire() { Id = index[0], Reference = "AAA", Libelle = "LIB 1", Societe = "BMCE" });
            compte.Add(new CompteBancaire() { Id = index[1], Reference = "AAB", Libelle = "LIB 2", Societe = "BMCI" });
            compte.Add(new CompteBancaire() { Id = index[2], Reference = "ABA", Libelle = "LIB 3", Societe = "SOCIETE GENERALE" });
            compte.Add(new CompteBancaire() { Id = index[3], Reference = "AZE", Libelle = "LIB 4", Societe = "ATTIJARI WAFABANK" });
            compte.Add(new CompteBancaire() { Id = index[4], Reference = "ATY", Libelle = "LIB 5", Societe = "BARID BANK" });
            compte.Add(new CompteBancaire() { Id = index[5], Reference = "BME", Libelle = "LIB 6", Societe = "OMNIA BANK" });
            compte.Add(new CompteBancaire() { Id = index[6], Reference = "EBM", Libelle = "LIB 7", Societe = "BANK CHAABI" });
            return compte;
        }
    }
}