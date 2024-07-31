using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using Mehode.Crud;
using GestionCompteBancaire.ConsoleApp;
using ReadValeur;
using System.IO;

namespace GestionCompteBancaire
{
    class Compte
    {
        public int ChoixMenu()
        {
            Console.WriteLine("MENU PRINCIPAL");
            Console.WriteLine("Qu'est-ce que tu aimerais faire?");
            Console.WriteLine("Tapez 1 pour ajouter des enregistrements.");
            Console.WriteLine("Tapez 2 pour afficher tous les enregistrements.");
            Console.WriteLine("Tapez 3 pour supprimer les enregistrements.");
            Console.WriteLine("Tapez 4 pour mettre à jour les enregistrements.");
            Console.WriteLine("Tapez 5 pour fermer l’application.");
            return Read.ReadIntBetween("Votre choix : ", 1, 5);
        }
        public void UserCommand()
        {
            do
            {
                int choix = ChoixMenu();
                List<CompteBancaire> compte = new List<CompteBancaire>();
                if (choix == 1)
                {
                    int[] index = new int[100];
                    Crud.Insertion(compte, index);
                    return;
                }
                else if (choix == 2)
                {
                    var affichage = Crud.Affichage(ListData.GetCompteBancaire());
                    foreach (var affiche in affichage)
                        Console.WriteLine("ID :" + affiche.Id + " Reference :" + affiche.Reference + " Libelle :" + affiche.Libelle + " Societe :" + affiche.Societe);
                    return;
                }
                else if (choix == 3)
                {
                    var supprimer = Crud.Supprimer(ListData.GetCompteBancaire());
                    foreach (var affiche in supprimer)
                        Console.WriteLine("ID :" + affiche.Id + " Reference :" + affiche.Reference + " Libelle :" + affiche.Libelle + " Societe :" + affiche.Societe);
                    return;
                }
                else if (choix == 4)
                {
                    var modifier = Crud.Modification(ListData.GetCompteBancaire());
                    foreach (var affiche in modifier)
                        Console.WriteLine("ID :" + affiche.Id + " Reference :" + affiche.Reference + " Libelle :" + affiche.Libelle + " Societe :" + affiche.Societe);
                    return;
                }
                else if (choix == 5)
                {
                    Console.WriteLine("Merci d'avoir utilisé notre application");
                    return;
                }
            } while (true);
        }
        static void Main(string[] args)
        {
            Compte compte = new Compte();
            compte.UserCommand();
        }
    }
}