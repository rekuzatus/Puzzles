using Microsoft.EntityFrameworkCore.Migrations;

namespace Puzzles.Migrations
{
    public partial class NameFixIC : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ingredient_Cocktail_Cocktails_CocktailId",
                table: "Ingredient_Cocktail");

            migrationBuilder.DropForeignKey(
                name: "FK_Ingredient_Cocktail_Ingredients_IngredientId",
                table: "Ingredient_Cocktail");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Ingredient_Cocktail",
                table: "Ingredient_Cocktail");

            migrationBuilder.RenameTable(
                name: "Ingredient_Cocktail",
                newName: "Ingredients_Cocktails");

            migrationBuilder.RenameIndex(
                name: "IX_Ingredient_Cocktail_CocktailId",
                table: "Ingredients_Cocktails",
                newName: "IX_Ingredients_Cocktails_CocktailId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Ingredients_Cocktails",
                table: "Ingredients_Cocktails",
                columns: new[] { "IngredientId", "CocktailId" });

            migrationBuilder.AddForeignKey(
                name: "FK_Ingredients_Cocktails_Cocktails_CocktailId",
                table: "Ingredients_Cocktails",
                column: "CocktailId",
                principalTable: "Cocktails",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Ingredients_Cocktails_Ingredients_IngredientId",
                table: "Ingredients_Cocktails",
                column: "IngredientId",
                principalTable: "Ingredients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ingredients_Cocktails_Cocktails_CocktailId",
                table: "Ingredients_Cocktails");

            migrationBuilder.DropForeignKey(
                name: "FK_Ingredients_Cocktails_Ingredients_IngredientId",
                table: "Ingredients_Cocktails");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Ingredients_Cocktails",
                table: "Ingredients_Cocktails");

            migrationBuilder.RenameTable(
                name: "Ingredients_Cocktails",
                newName: "Ingredient_Cocktail");

            migrationBuilder.RenameIndex(
                name: "IX_Ingredients_Cocktails_CocktailId",
                table: "Ingredient_Cocktail",
                newName: "IX_Ingredient_Cocktail_CocktailId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Ingredient_Cocktail",
                table: "Ingredient_Cocktail",
                columns: new[] { "IngredientId", "CocktailId" });

            migrationBuilder.AddForeignKey(
                name: "FK_Ingredient_Cocktail_Cocktails_CocktailId",
                table: "Ingredient_Cocktail",
                column: "CocktailId",
                principalTable: "Cocktails",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Ingredient_Cocktail_Ingredients_IngredientId",
                table: "Ingredient_Cocktail",
                column: "IngredientId",
                principalTable: "Ingredients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
