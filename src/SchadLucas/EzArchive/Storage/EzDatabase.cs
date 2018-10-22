using System;
using LiteDB;
using SchadLucas.Wpf.InversionOfControl;

namespace SchadLucas.EzArchive.Storage
{
    public class EzDatabase : ITransientService
    {
        private readonly Func<LiteDatabase> _dbFactory;

        public EzDatabase(Func<LiteDatabase> dbFactory)
        {
            _dbFactory = dbFactory;
        }

        public TOut Query<TOut>(Func<LiteDatabase, TOut> func)
        {
            using (var db = _dbFactory())
            {
                return func(db);
            }
        }

        public TOut QueryCollection<TCollection, TOut>(Func<LiteCollection<TCollection>, TOut> func) where TCollection : EzEntity
        {
            using (var db = _dbFactory())
            {
                var collection = db.GetCollection<TCollection>();
                return func(collection);
            }
        }


        #region AsAction

        public void Query(Action<LiteDatabase> action)
        {
            Query<object>(ctx =>
            {
                action(ctx);
                return null;
            });
        }


        public void QueryCollection<TCollection>(Action<LiteCollection<TCollection>> action) where TCollection : EzEntity
        {
            QueryCollection<TCollection, object>(ctx =>
            {
                action(ctx);
                return null;
            });
        }

        #endregion
    }
}