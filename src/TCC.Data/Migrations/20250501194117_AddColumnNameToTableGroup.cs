﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TCC.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddColumnNameToTableGroup : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "groups",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "groups");
        }
    }
}
