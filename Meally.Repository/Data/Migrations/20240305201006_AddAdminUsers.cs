using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Meally.Repository.Migrations
{
    public partial class AddAdminUsers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO [dbo].[AspNetUsers] ([Id], [DisplayName], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'1650ee49-9ac6-4294-ae75-706f5d0efd56', N'Ahmed Hamed', N'Ahmed_hamed', N'AHMED_HAMED', N'ahmedhamed20042003@gmail.com', N'AHMEDHAMED20042003@GMAIL.COM', 0, N'AQAAAAEAACcQAAAAEA1NPxq8XnkfDaOxVwC+MGVynW9lTfpboaehAU/+D7SvxrqsT/OD38crrMYMnvrh/w==', N'UIUGDGNOBYHUXPOGA4VXJGG4ZIYWNWKE', N'47a30f62-3a8e-4bd2-a329-bdf684fe06e1', N'01055481277', 0, 0, NULL, 1, 0)\r\n");
            migrationBuilder.Sql("INSERT INTO [dbo].[AspNetUsers] ([Id], [DisplayName], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'4561ab73-c13f-498a-ab26-827347ba0ea3', N'Mohamed_mamdoh', N'mohamedmamdoh', N'MOHAMEDMAMDOH', N'mohamedmamdoh@gmail.com', N'MOHAMEDMAMDOH@GMAIL.COM', 0, N'AQAAAAEAACcQAAAAEOweOyBofd0hhtokiM7mh3hShFvPjAvTPJQ3/rHNSp2W5YEUg4qDH2wkH4KAz/TNVg==', N'4GB26QNATSIPNURH3EDBW2TPQZ45OZAP', N'fffb09e4-f6d6-42ad-a532-c5fa2c9204d1', N'01521521845', 0, 0, NULL, 1, 0)\r\n");
            migrationBuilder.Sql("INSERT INTO [dbo].[AspNetUsers] ([Id], [DisplayName], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'e87ea933-ac6f-4f08-a7c9-5fd1dc3c1789', N'Rahma Hamed', N'rahmahamed', N'RAHMAHAMED', N'rahmahamed@gmail.com', N'RAHMAHAMED@GMAIL.COM', 0, N'AQAAAAEAACcQAAAAEM347WCXVfpG4904numlUcusBjaM4kB5Ix/NnVZtE9V5V+JJnTh/xqKoyetnq9SQ7Q==', N'4XOAKCOXLOA5MVYYFEA4SDEVYIBYD2ZJ', N'9a1d97c8-4f41-452b-a374-8ba5cbde52cf', N'01521521845', 0, 0, NULL, 1, 0)\r\n");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM [dbo].[AspNetUsers] WHERE Id = '1650ee49-9ac6-4294-ae75-706f5d0efd56'");
            migrationBuilder.Sql("DELETE FROM [dbo].[AspNetUsers] WHERE Id = '4561ab73-c13f-498a-ab26-827347ba0ea3'");
            migrationBuilder.Sql("DELETE FROM [dbo].[AspNetUsers] WHERE Id = 'e87ea933-ac6f-4f08-a7c9-5fd1dc3c1789'");
        }
    }
}
