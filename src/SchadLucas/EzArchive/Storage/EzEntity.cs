using System;

namespace SchadLucas.EzArchive.Storage
{
    public abstract class EzEntity
    {
        protected EzEntity()
        {
            Id = Guid.NewGuid();
            Created = DateTime.Now;
            Modified = DateTime.Now;
        }

        public Guid Id { get; set; }
        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }
    }
}