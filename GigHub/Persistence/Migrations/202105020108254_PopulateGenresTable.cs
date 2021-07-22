namespace GigHub.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PopulateGenresTable : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO Genres (Id, Name) VALUES (1, 'Pop')");
            Sql("INSERT INTO Genres (Id, Name) VALUES (2, 'Rock')");
            Sql("INSERT INTO Genres (Id, Name) VALUES (3, 'Hip-hop')");
            Sql("INSERT INTO Genres (Id, Name) VALUES (4, 'Reggaeton')");
        }
        
        public override void Down()
        {
            Sql("DELETE FROM Genres WHERE Id IN (1, 2, 3, 4)");
        }
    }
}
