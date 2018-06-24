namespace Poetry.UI.ResourceSupport
{
    public class Resource
    {
        public string Path { get; }
        public string LocalPath { get; }

        public Resource(string componentId, string path)
        {
            Path = componentId + "/" + path;
            LocalPath = path;
        }
    }
}