using Microsoft.AspNet.Identity;

namespace NPress.Core.Identity
{
    public class LowerInvariantLookupNormalizer : ILookupNormalizer
    {
        public string Normalize(string key)
        {
            return key?.Normalize().ToLowerInvariant();
        }
    }
}
