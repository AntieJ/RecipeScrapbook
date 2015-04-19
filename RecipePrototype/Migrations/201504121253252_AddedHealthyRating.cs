namespace RecipePrototype.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedHealthyRating : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Recipes", "HealthyRating", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Recipes", "HealthyRating");
        }
    }
}
