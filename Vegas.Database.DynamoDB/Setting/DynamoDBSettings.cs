using System.Reflection;

namespace Vegas.Database.DynamoDB.Setting
{
    public class DynamoDBSettings : IDynamoDBSettings
    {
        public string AccessKey { get; set; }
        public string SecretKey { get; set; }
        public string Region { get; set; }
    }

    public interface IDynamoDBSettings
    {
        string AccessKey { get; set; }
        string SecretKey { get; set; }
        string Region { get; set; }
    }
}
