using System;
using SchadLucas.EzArchive.Storage;

namespace SchadLucas.EzArchive.Domain
{
    public class EzFolder : EzEntity
    {
        public string Name { get; set; } = string.Empty;
        public Guid Parent { get; set; } = Guid.Empty;
    }
}