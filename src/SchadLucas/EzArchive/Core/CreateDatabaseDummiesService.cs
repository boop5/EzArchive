using System;
using System.Collections.Generic;
using SchadLucas.EzArchive.Domain;
using SchadLucas.EzArchive.Storage;
using SchadLucas.Wpf.InversionOfControl;

namespace SchadLucas.EzArchive.Core
{
    public class CreateDatabaseDummiesService : IStandaloneService
    {
        private readonly Random _random = new Random();

        public CreateDatabaseDummiesService(EzDatabase db)
        {
            var root = new[]
            {
                new EzFolder {Name = "Folder A"},
                new EzFolder {Name = "Folder B"},
                new EzFolder {Name = "Folder C"},
                new EzFolder {Name = "Folder D"}
            };
            var nodes = new List<EzFolder>();
            var documents = new List<EzDocument>();

            foreach (var folder in root)
            {
                for (var j = 0; j < _random.Next(8, 97); j++)
                {
                    var fileName = $"{Guid.NewGuid().ToString("D").Substring(0, _random.Next(4, 32))}.docx";
                    documents.Add(new EzDocument { Folder = folder.Id, FileName = fileName });
                }

                for (var i = 0; i < 2; i++)
                {
                    var subFolder = new EzFolder { Name = $"Subfolder {i}", Parent = folder.Id };
                    nodes.Add(subFolder);

                    for (var i1 = 0; i1 < 4; i1++)
                    {
                        var subSubFolder = new EzFolder { Name = $"SubSubfolder {i1}", Parent = subFolder.Id };
                        nodes.Add(subSubFolder);

                        for (var i2 = 0; i2 < 6; i2++)
                        {
                            var subSubSubFolder = new EzFolder { Name = $"SubSubSubfolder {i2}", Parent = subSubFolder.Id };
                            nodes.Add(subSubSubFolder);

                            for (var i3 = 0; i3 < 8; i3++)
                            {
                                var subSubSubSubFolder = new EzFolder { Name = $"SubSubSubSubfolder {i3}", Parent = subSubSubFolder.Id };
                                nodes.Add(subSubSubSubFolder);
                            }
                        }
                    }
                }
            }

            db.QueryCollection<EzFolder>(ctx =>
            {
                ctx.InsertBulk(root);
                ctx.InsertBulk(nodes);
            });

            db.QueryCollection<EzDocument>(ctx => ctx.InsertBulk(documents));
        }
    }
}