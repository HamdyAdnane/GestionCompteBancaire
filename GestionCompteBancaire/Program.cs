using Dapper;
using GestionCompteBancaire.ConsoleApp;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using ReadValeur;
using System.Data;
using System.Net.Sockets;


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
            var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            IDbConnection dbConnection = new SqlConnection(configuration.GetSection("ConnectionString").Value);
            do
            {
                int choix = ChoixMenu();
                if (choix == 1)
                {
                    var CompteBancaireToInsert = new InformationCompteBancaire
                    {
                        Reference = Read.ReadString("Entrer un Reference : "),
                        Libelle = Read.ReadString("Entrer un Libelle : "),
                        Societe = Read.ReadString("Entrer un Societe : ")
                    };
                    var sql = "INSERT INTO CompteBancaire (Reference,Libelle,Societe) VALUES" +
                        $"(@Reference,@Libelle,@Societe)" +
                        $"SELECT CAST (scope_identity() AS int);";
                    var parameters = new
                    {
                        Reference = CompteBancaireToInsert.Reference,
                        Libelle = CompteBancaireToInsert.Libelle,
                        Societe = CompteBancaireToInsert.Societe
                    };
                    CompteBancaireToInsert.Id = dbConnection.Query<int>(sql, parameters).Single();
                    Console.WriteLine($"Compte Bancaire {CompteBancaireToInsert} ajouté avec succès ");
                    Console.WriteLine();
                }
                else if (choix == 2)
                {
                    var affichage = "SELECT * FROM CompteBancaire";
                    var result = dbConnection.Query<InformationCompteBancaire>(affichage);
                    foreach(var item in result)
                        Console.WriteLine(item);
                    Console.WriteLine();
                }
                else if (choix == 3)
                {
                    var CompteBancaireToDelete = new InformationCompteBancaire
                    {
                        Id = Read.ReadInt("Entre L'ID Pour Supprimer :")
                    };
                    var supprimer = "DELETE FROM CompteBancaire WHERE Id = @Id;";
                    var parameter =
                        new
                        {
                            Id = CompteBancaireToDelete.Id
                        };
                    dbConnection.Execute(supprimer, parameter);
                    Console.WriteLine($"Compte bancaire Supprimé avec succès");
                    Console.WriteLine();
                }
                else if (choix == 4)
                {
                    var CompteBancaireToUpdate = new InformationCompteBancaire
                    {
                        Id = Read.ReadInt("Donnez-moi l'ID dont vous modifiez les propriétés : "),
                        Reference = Read.ReadString("Entrer Nouveau Reference : "),
                        Libelle = Read.ReadString("Entrer Nouveau Libelle : "),
                        Societe = Read.ReadString("Entrer Nouveau Societe : ")
                    };
                    var modification = "UPDATE CompteBancaire SET Reference = @Reference, Libelle = @Libelle ,Societe = @Societe WHERE Id = @Id;";
                    var parameter =
                        new
                        {
                            Id = CompteBancaireToUpdate.Id,
                            Reference = CompteBancaireToUpdate.Reference,
                            Libelle = CompteBancaireToUpdate.Libelle,
                            Societe = CompteBancaireToUpdate.Societe
                        };
                    dbConnection.Execute(modification, parameter);
                    Console.WriteLine($"Compte bancaire mis à jour avec succès.");
                    Console.WriteLine();
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