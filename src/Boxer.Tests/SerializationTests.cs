using System;
using NUnit.Framework;
using SpriteUtility.Data;
using JsonSerializer = SpriteUtility.JsonSerializer;

namespace Boxer.Tests
{
    [TestFixture]
    public class SerializationTests
    {
        [Test]
        public void Can_serialize_nested_folder()
        {
            var parent = new Folder {Name = "Parent"};
            var child = new Folder {Name = "Child"};
            child.Add(new Folder { Name = "Grandchild" });
            parent.Add(child);
            
            var json = JsonSerializer.Serialize(parent);
            Assert.IsNotNull(json);
            Console.WriteLine(json);
        }
    }
}
