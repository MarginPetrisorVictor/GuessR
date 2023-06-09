﻿using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace GuessR.Models
{
	public class Player
	{
		[Key]
		public int Id { get; set; }
		[StringLength(256)]
		public string? Name { get; set; }
		[StringLength(450)]
		public string? UserId { get; set; }

		public DateTime CreatedDateTime { get; set; } = DateTime.Now;
		[AllowNull]
		public int Score { get; set; }
		[AllowNull]
		public int Rank { get; set; }
		[AllowNull]
		public int TotalScore { get; set; }
		[AllowNull]
		public int TotalPlayedGames { get; set; }
		[AllowNull]
		public double SuccessPercentage { get; set; }
		[AllowNull]
		[StringLength(500)]
		public string? ImageUrl { get; set; } //daca nu declaram ca 'nullable' cu '?', atunci va da eroare pentru ca gaseste null in baza de date.

		[ForeignKey(nameof(UserId))]
		public IdentityUser? User { get; set; }

		[Display(Name="Profile Picture")]
		[NotMapped]
		public IFormFile? ProfilePicture { get; set; }

	}
}
