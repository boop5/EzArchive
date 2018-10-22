namespace SchadLucas.EzArchive.Domain.Messages
{
    public static partial class Messages
    {
        public class FolderSelected
        {
            public FolderSelected(EzFolder folder)
            {
                Folder = folder;
            }

            public EzFolder Folder { get; }
        }
    }
}