using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LightningPT.EntityFrameworkCore.Migrations
{
    public partial class _20181223172101 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PtBitTorrentCateGories",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    DisplayName = table.Column<string>(nullable: true),
                    Code = table.Column<string>(nullable: true),
                    IconUrl = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PtBitTorrentCateGories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PtBitTorrents",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Uploader = table.Column<long>(nullable: false),
                    Size = table.Column<long>(nullable: false),
                    CateGory = table.Column<int>(nullable: false),
                    IsAnonymous = table.Column<bool>(nullable: false),
                    InfoHash = table.Column<string>(nullable: true),
                    UploadTime = table.Column<DateTime>(nullable: false),
                    DownLoadCount = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PtBitTorrents", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PtUserLevels",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    DisplayName = table.Column<string>(maxLength: 50, nullable: true),
                    Code = table.Column<string>(maxLength: 10, nullable: true),
                    IconUrl = table.Column<string>(maxLength: 200, nullable: true),
                    Description = table.Column<string>(maxLength: 1000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PtUserLevels", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PtUsers",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    UserName = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true),
                    DisplayName = table.Column<string>(nullable: true),
                    Level = table.Column<int>(nullable: false),
                    MagicValue = table.Column<long>(nullable: false),
                    LastLoginIp = table.Column<string>(nullable: true),
                    LasLoginDateTime = table.Column<DateTime>(nullable: true),
                    IsBanned = table.Column<bool>(nullable: false),
                    BannedTime = table.Column<DateTime>(nullable: true),
                    PassKey = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PtUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PtUserStatisticses",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    UserId = table.Column<long>(nullable: false),
                    UploadTotalCount = table.Column<long>(nullable: false),
                    DownloadTotalCount = table.Column<long>(nullable: false),
                    SharingRate = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PtUserStatisticses", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PtBitTorrentCateGories");

            migrationBuilder.DropTable(
                name: "PtBitTorrents");

            migrationBuilder.DropTable(
                name: "PtUserLevels");

            migrationBuilder.DropTable(
                name: "PtUsers");

            migrationBuilder.DropTable(
                name: "PtUserStatisticses");
        }
    }
}
