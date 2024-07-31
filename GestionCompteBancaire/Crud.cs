using System;
using GestionCompteBancaire.ConsoleApp;
using ReadValeur;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mehode.Crud
{
    public static class Crud
    {
        public static List<CompteBancaire> Insertion(this List<CompteBancaire> compte, int[] index)
        {
            compte.Add(new CompteBancaire()
            {
                Id = index[7],
                Reference = Read.ReadString("Entrer Une Réference : "),
                Libelle = Read.ReadString("Entrer Une Libelle : "),
                Societe = Read.ReadString("Entrer la Societe : ")
            });
            return compte;
        }
        public static List<CompteBancaire> Affichage(this List<CompteBancaire> compte)
        {
            List<CompteBancaire> Afficher = new List<CompteBancaire>(compte);
            return Afficher;
        }
        public static List<CompteBancaire> Modification(this List<CompteBancaire> compte)
        {
            List<CompteBancaire> Update = new List<CompteBancaire>(compte);
            int indexToUpdate = Read.ReadInt("Donnez l'ID de la sélection que vous allez modifier :");
            foreach (var item in Update.Where(x => x.Id == indexToUpdate))
            {
                item.Reference = Read.ReadString("Entrer Une Réference : ");
                item.Libelle = Read.ReadString("Entrer Une Libelle : ");
                item.Societe = Read.ReadString("Entrer la Societe : ");
            }
            return Update;
        }
        public static List<CompteBancaire> Supprimer(this List<CompteBancaire> compte)
        {
            List<CompteBancaire> Delete = new List<CompteBancaire>(compte);
            int indexToRemove = Read.ReadInt("Donnez l'ID de la sélection que vous allez supprimer :");
            Delete.RemoveAt(indexToRemove - 1);
            return Delete;
        }
    }
}