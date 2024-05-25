﻿using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using Microsoft.AspNetCore.DataProtection.AuthenticatedEncryption;
using Middleware;

namespace IdentityAPI.Model
{
	public class User
	{
		[BsonId]
		[BsonRepresentation(BsonType.ObjectId)]
		public string? Id { get; set; }
		public string? Email { get; set; }
		public string? Password { get; set; }
		public string? Salt { get; set; }
		public string? FirstName { get; set; }
		public string? LastName { get; set; }

		public void SetPassword(string password, IEncryptor encryptor)
		{
			Salt = encryptor.GetSalt();
			Password = encryptor.GetHash(password, Salt);
		}

		public bool ValidatePassword(string password, IEncryptor encryptor)
		{
			return Password == encryptor.GetHash(password, Salt);
		}
	}
}
