namespace RecipePrototype.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedFillingAndHealthy : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Recipes", "FillingAndHealthy", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Recipes", "FillingAndHealthy");
        }
    }
}
