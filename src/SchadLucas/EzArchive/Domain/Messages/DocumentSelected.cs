namespace SchadLucas.EzArchive.Domain.Messages
{
    public static partial class Messages
    {
        public class DocumentSelected
        {
            public DocumentSelected(EzDocument document)
            {
                Document = document;
            }

            public EzDocument Document { get; }
        }
    }
}