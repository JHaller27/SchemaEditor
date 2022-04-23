using Godot;
using File = System.IO.File;

namespace SchemaEditor.FileHandling.Implementations
{
	public class ExportSchemaFileHandler : IFileHandler
	{
		private ISchemaEditor SchemaEditor { get; }

		public ExportSchemaFileHandler(ISchemaEditor schemaEditor)
		{
			this.SchemaEditor = schemaEditor;
		}

		public void Handle(string path)
		{
			object schema = this.SchemaEditor.ExportSchema();
			string json = JSON.Print(schema);
			File.WriteAllText(path, json);
		}
	}
}
