namespace Job.Advertisement.Service.Settings

{
    public class MongoDbSettings
    {
        public string Host { get; init; }

        public string Port { get; init; }

        public string ConnectionName => $"mongodb://{Host}:{Port}";
    }

}