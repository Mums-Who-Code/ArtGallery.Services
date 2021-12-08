// -----------------------------------------------------------------------
// Copyright (c) MumsWhoCode. All rights reserved.
// -----------------------------------------------------------------------

using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ArtGallery.Services.Api.Migrations
{
    public partial class UpdateArtists : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Artists_Users_CreatedByUserId",
                table: "Artists");

            migrationBuilder.DropForeignKey(
                name: "FK_Artists_Users_UpdatedByUserId",
                table: "Artists");

            migrationBuilder.DropIndex(
                name: "IX_Artists_CreatedByUserId",
                table: "Artists");

            migrationBuilder.DropIndex(
                name: "IX_Artists_UpdatedByUserId",
                table: "Artists");

            migrationBuilder.DropColumn(
                name: "CreatedByUserId",
                table: "Artists");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserId",
                table: "Artists");

            migrationBuilder.CreateIndex(
                name: "IX_Artists_CreatedBy",
                table: "Artists",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Artists_UpdatedBy",
                table: "Artists",
                column: "UpdatedBy");

            migrationBuilder.AddForeignKey(
                name: "FK_Artists_Users_CreatedBy",
                table: "Artists",
                column: "CreatedBy",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Artists_Users_UpdatedBy",
                table: "Artists",
                column: "UpdatedBy",
                principalTable: "Users",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Artists_Users_CreatedBy",
                table: "Artists");

            migrationBuilder.DropForeignKey(
                name: "FK_Artists_Users_UpdatedBy",
                table: "Artists");

            migrationBuilder.DropIndex(
                name: "IX_Artists_CreatedBy",
                table: "Artists");

            migrationBuilder.DropIndex(
                name: "IX_Artists_UpdatedBy",
                table: "Artists");

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedByUserId",
                table: "Artists",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedByUserId",
                table: "Artists",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Artists_CreatedByUserId",
                table: "Artists",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Artists_UpdatedByUserId",
                table: "Artists",
                column: "UpdatedByUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Artists_Users_CreatedByUserId",
                table: "Artists",
                column: "CreatedByUserId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Artists_Users_UpdatedByUserId",
                table: "Artists",
                column: "UpdatedByUserId",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}
