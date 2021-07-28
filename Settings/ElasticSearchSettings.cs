namespace Job.Advertisement.Service.Settings

{
    public class ElasticSearchSettings
    {
        public string Host { get; init; }

        public string Port { get; init; }

        public string IndexName { get; init; }

        public string ElasticSearchUrl => $"http://{Host}:{Port}";

        //$"http://{Host}:{Port}";
    }

}