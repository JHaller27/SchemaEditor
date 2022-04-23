using Godot;
using File = System.IO.File;

namespace SchemaEditor.FileHandling.Implementations
{
	public class ImportSchemaFileHandler : IFileHandler
	{
		private ISchemaEditor SchemaEditor { get; }

		public ImportSchemaFileHandler(ISchemaEditor schemaEditor)
		{
			this.SchemaEditor = schemaEditor;
		}

		public void Handle(string path)
		{
			string schemaStr = File.ReadAllText(path);
			JSONParseResult schema = JSON.Parse(schemaStr);
			this.SchemaEditor.ArrangeSchema(schema.Result);
		}
	}
}
