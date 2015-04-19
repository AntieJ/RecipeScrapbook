namespace RecipePrototype.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Recipes",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Method = c.String(),
                        WeightWatchersPoints = c.Int(nullable: false),
                        MealType = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.RecipeIngredients",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Quantity = c.String(),
                        Name = c.String(),
                        Recipe_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Recipes", t => t.Recipe_ID)
                .Index(t => t.Recipe_ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.RecipeIngredients", "Recipe_ID", "dbo.Recipes");
            DropIndex("dbo.RecipeIngredients", new[] { "Recipe_ID" });
            DropTable("dbo.RecipeIngredients");
            DropTable("dbo.Recipes");
        }
    }
}
