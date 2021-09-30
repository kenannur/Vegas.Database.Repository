using System.Text.Json.Serialization;
using Amazon.DynamoDBv2.DataModel;
using Vegas.Database.DynamoDB.Entity;

namespace Vegas.Database.ApiTest.Entities
{
    public class User : DynamoDefaultEntity
    {
        public bool IsActive { get; set; }

        [DynamoDBGlobalSecondaryIndexHashKey]
        public string Email { get; set; }

        [JsonIgnore]
        public string Password { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public Role Role { get; set; }
    }

    public enum Role
    {
        Unknown = 0,
        Admin = 1,
        Guest = 2,
        Standard = 3,
        Premium = 4
    }
}
