namespace NPress.Core.Domains
{
    public class Role : IDataObject
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string NormalizedName { get; set; }
    }
}
