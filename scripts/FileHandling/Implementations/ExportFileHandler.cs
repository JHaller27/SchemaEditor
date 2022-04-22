using Godot;
using File = System.IO.File;

namespace SchemaEditor.FileHandling.Implementations
{
	public class ExportFileHandler : IFileHandler
	{
		private IContext Context { get; }

		public ExportFileHandler(IContext context)
		{
			this.Context = context;
		}

		public void Handle(string path)
		{
			object obj = this.Context.ExportData();
			string json = JSON.Print(obj);
			File.WriteAllText(path, json);
		}
	}
}
