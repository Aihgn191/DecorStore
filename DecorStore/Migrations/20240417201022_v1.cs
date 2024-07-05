using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DecorStore.Migrations
{
    /// <inheritdoc />
    public partial class v1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Category",
                columns: table => new
                {
                    ID_CATE = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CATE_NAME = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    LINK = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    HIDE = table.Column<int>(type: "int", nullable: true),
                    DESCRIPTION = table.Column<string>(type: "ntext", nullable: true),
                    IMG = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Category__7A169070B143E042", x => x.ID_CATE);
                });

            migrationBuilder.CreateTable(
                name: "Favorite",
                columns: table => new
                {
                    ID_FAVO = table.Column<int>(type: "int", nullable: false),
                    ID_USER = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    ID_PROD = table.Column<int>(type: "int", nullable: false),
                    DATE = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Favorite__6CC98A524F17FB87", x => x.ID_FAVO);
                });

            migrationBuilder.CreateTable(
                name: "Producer",
                columns: table => new
                {
                    ID_PRODUCER = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PRODUCER_NAME = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    EMAIL = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    PHONE_NUMS = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ADDRESS = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    IMG = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    HIDE = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Producer__88BD7532785392CC", x => x.ID_PRODUCER);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Order",
                columns: table => new
                {
                    ID_ORDER = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ID_USER = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    ORDER_DATE = table.Column<DateTime>(type: "datetime", nullable: true),
                    ADDRESS = table.Column<string>(type: "ntext", nullable: true),
                    TOTAL_PRICE = table.Column<decimal>(type: "decimal(18,0)", nullable: true),
                    NOTES = table.Column<string>(type: "ntext", nullable: true),
                    IS_PAY = table.Column<bool>(type: "bit", nullable: true),
                    IS_COMPLETE = table.Column<bool>(type: "bit", nullable: true),
                    PT_THANHTOAN = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    UserCustomId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Order__D23A856506D6AA6F", x => x.ID_ORDER);
                    table.ForeignKey(
                        name: "FK_Order_AspNetUsers_UserCustomId",
                        column: x => x.UserCustomId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Product",
                columns: table => new
                {
                    ID_PROD = table.Column<int>(type: "int", nullable: false),
                    PROD_NAME = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    ALIAS_NAME = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    ID_CATE = table.Column<int>(type: "int", nullable: false),
                    ID_PRODUCER = table.Column<int>(type: "int", nullable: false),
                    UNIT = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    PRICE = table.Column<decimal>(type: "decimal(18,0)", nullable: true),
                    IMG1 = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    IMG2 = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    IMG3 = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    NSX = table.Column<DateTime>(type: "datetime", nullable: true),
                    SALE = table.Column<decimal>(type: "decimal(18,0)", nullable: true),
                    DESCRIPTION = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    LINK = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    HIDE = table.Column<int>(type: "int", nullable: true),
                    NUMS = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Product__BA497EFEC97E67E7", x => x.ID_PROD);
                    table.ForeignKey(
                        name: "FK_Product_Category",
                        column: x => x.ID_CATE,
                        principalTable: "Category",
                        principalColumn: "ID_CATE");
                    table.ForeignKey(
                        name: "FK_Product_Producer",
                        column: x => x.ID_PRODUCER,
                        principalTable: "Producer",
                        principalColumn: "ID_PRODUCER");
                });

            migrationBuilder.CreateTable(
                name: "OrderDetail",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ID_ORDER = table.Column<int>(type: "int", nullable: false),
                    ID_PROD = table.Column<int>(type: "int", nullable: false),
                    PRICE = table.Column<decimal>(type: "decimal(18,0)", nullable: true),
                    QUANTITY = table.Column<int>(type: "int", nullable: true),
                    SALE = table.Column<decimal>(type: "decimal(18,0)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__OrderDet__3214EC2781B8D563", x => x.ID);
                    table.ForeignKey(
                        name: "FK_OrderDetail_Order",
                        column: x => x.ID_ORDER,
                        principalTable: "Order",
                        principalColumn: "ID_ORDER");
                    table.ForeignKey(
                        name: "FK_OrderDetail_Product",
                        column: x => x.ID_ORDER,
                        principalTable: "Product",
                        principalColumn: "ID_PROD");
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Order_UserCustomId",
                table: "Order",
                column: "UserCustomId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetail_ID_ORDER",
                table: "OrderDetail",
                column: "ID_ORDER");

            migrationBuilder.CreateIndex(
                name: "IX_Product_ID_CATE",
                table: "Product",
                column: "ID_CATE");

            migrationBuilder.CreateIndex(
                name: "IX_Product_ID_PRODUCER",
                table: "Product",
                column: "ID_PRODUCER");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "Favorite");

            migrationBuilder.DropTable(
                name: "OrderDetail");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Order");

            migrationBuilder.DropTable(
                name: "Product");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Category");

            migrationBuilder.DropTable(
                name: "Producer");
        }
    }
}
