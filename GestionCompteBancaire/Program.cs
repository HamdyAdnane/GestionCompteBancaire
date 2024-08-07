using GestionCompteBancaire.ConsoleApp;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using ReadValeur;
using System.Data;


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
            SqlConnection connection = new SqlConnection(configuration.GetSection("ConnectionString").Value);
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
                    var insertion = "INSERT INTO CompteBancaire (Reference,Libelle,Societe) VALUES" +
                        $"(@Reference,@Libelle,@Societe);" +
                        $"SELECT CAST (scope_identity() AS int)";
                    SqlParameter ReferenceParameter = new SqlParameter
                    {
                        ParameterName = "@Reference",
                        SqlDbType = SqlDbType.VarChar,
                        Direction = ParameterDirection.Input,
                        Value = CompteBancaireToInsert.Reference
                    };
                    SqlParameter LibelleParameter = new SqlParameter
                    {
                        ParameterName = "@Libelle",
                        SqlDbType = SqlDbType.VarChar,
                        Direction = ParameterDirection.Input,
                        Value = CompteBancaireToInsert.Libelle
                    };
                    SqlParameter SocieteParameter = new SqlParameter
                    {
                        ParameterName = "@Societe",
                        SqlDbType = SqlDbType.VarChar,
                        Direction = ParameterDirection.Input,
                        Value = CompteBancaireToInsert.Societe
                    };
                    SqlCommand command = new SqlCommand(insertion, connection);
                    command.Parameters.Add(ReferenceParameter);
                    command.Parameters.Add(LibelleParameter);
                    command.Parameters.Add(SocieteParameter);
                    command.CommandType = CommandType.Text;
                    connection.Open();
                    CompteBancaireToInsert.Id = (int)command.ExecuteScalar();
                    Console.WriteLine($"Compte Bancaire {CompteBancaireToInsert.Id} ajouté avec succès ");
                    connection.Close();
                }
                else if (choix == 2)
                {
                    var affichage = "SELECT * FROM CompteBancaire";
                    SqlCommand command = new SqlCommand(affichage, connection);
                    command.CommandType = CommandType.Text;
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    InformationCompteBancaire compteBancaire;
                    while (reader.Read())
                    {
                        compteBancaire = new InformationCompteBancaire
                        {
                            Id = reader.GetInt32("Id"),
                            Reference = reader.GetString("Reference"),
                            Libelle = reader.GetString("Libelle"),
                            Societe = reader.GetString("Societe")
                        };
                        Console.WriteLine(compteBancaire);
                    }
                    connection.Close();
                }
                else if (choix == 3)
                {
                    var CompteBancaireToDelete = new InformationCompteBancaire
                    {
                        Id = Read.ReadInt("Entre L'ID Pour Delete :")
                    };
                    var supprimer = "DELETE FROM CompteBancaire WHERE Id = @Id";
                    SqlParameter IdParameter = new SqlParameter
                    {
                        ParameterName = "@Id",
                        SqlDbType = SqlDbType.Int,
                        Direction = ParameterDirection.Input,
                        Value = CompteBancaireToDelete.Id
                    };
                    SqlCommand command = new SqlCommand(supprimer, connection);
                    command.Parameters.Add(IdParameter);
                    command.CommandType = CommandType.Text;
                    connection.Open();
                    if (command.ExecuteNonQuery() > 0)
                        Console.WriteLine($"Compte bancaire Supprimé avec succès");
                    connection.Close();
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
                    var modification = "UPDATE CompteBancaire SET Reference = @Reference, Libelle = @Libelle ,Societe = @Societe WHERE Id = @Id";
                    SqlParameter IdParameter = new SqlParameter
                    {
                        ParameterName = "@Id",
                        SqlDbType = SqlDbType.Int,
                        Direction = ParameterDirection.Input,
                        Value = CompteBancaireToUpdate.Id
                    };
                    SqlParameter ReferenceParameter = new SqlParameter
                    {
                        ParameterName = "@Reference",
                        SqlDbType = SqlDbType.VarChar,
                        Direction = ParameterDirection.Input,
                        Value = CompteBancaireToUpdate.Reference
                    };
                    SqlParameter LibelleParameter = new SqlParameter
                    {
                        ParameterName = "@Libelle",
                        SqlDbType = SqlDbType.VarChar,
                        Direction = ParameterDirection.Input,
                        Value = CompteBancaireToUpdate.Libelle
                    };
                    SqlParameter SocieteParameter = new SqlParameter
                    {
                        ParameterName = "@Societe",
                        SqlDbType = SqlDbType.VarChar,
                        Direction = ParameterDirection.Input,
                        Value = CompteBancaireToUpdate.Societe
                    };
                    SqlCommand command = new SqlCommand(modification, connection);
                    command.Parameters.Add(IdParameter);
                    command.Parameters.Add(ReferenceParameter);
                    command.Parameters.Add(LibelleParameter);
                    command.Parameters.Add(SocieteParameter);
                    command.CommandType = CommandType.Text;
                    connection.Open();
                    if (command.ExecuteNonQuery() > 0)
                        Console.WriteLine($"Compte bancaire mis à jour avec succès.");
                    connection.Close();
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