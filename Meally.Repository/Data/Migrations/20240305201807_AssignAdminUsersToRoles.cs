using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Meally.Repository.Migrations
{
    public partial class AssignAdminUsersToRoles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("Insert INTO [dbo].[AspNetUserRoles] (UserId,RoleId) SELECT '1650ee49-9ac6-4294-ae75-706f5d0efd56',Id FROM [dbo].[AspNetRoles]\r\n");
            migrationBuilder.Sql("Insert INTO [dbo].[AspNetUserRoles] (UserId,RoleId) SELECT '4561ab73-c13f-498a-ab26-827347ba0ea3',Id FROM [dbo].[AspNetRoles]\r\n");
            migrationBuilder.Sql("Insert INTO [dbo].[AspNetUserRoles] (UserId,RoleId) SELECT 'e87ea933-ac6f-4f08-a7c9-5fd1dc3c1789',Id FROM [dbo].[AspNetRoles]\r\n");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM [dbo].[AspNetUserRoles] WHERE UserId = '1650ee49-9ac6-4294-ae75-706f5d0efd56'");
            migrationBuilder.Sql("DELETE FROM [dbo].[AspNetUserRoles] WHERE UserId = '4561ab73-c13f-498a-ab26-827347ba0ea3'");
            migrationBuilder.Sql("DELETE FROM [dbo].[AspNetUserRoles] WHERE UserId = 'e87ea933-ac6f-4f08-a7c9-5fd1dc3c1789'");
        }
    }
}
