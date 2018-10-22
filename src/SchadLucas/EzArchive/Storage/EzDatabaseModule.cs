using System;
using Autofac;
using LiteDB;

namespace SchadLucas.EzArchive.Storage
{
    public class EzDatabaseModule : Module
    {
        private static readonly string DatabasePath = $"./tmp_{Guid.NewGuid():N}.db";

        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder.Register<Func<LiteDatabase>>(c => () => new LiteDatabase(DatabasePath));
        }
    }
}