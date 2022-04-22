using Godot;
using File = System.IO.File;

namespace SchemaEditor.FileHandling.Implementations
{
	public class ImportFileHandler : IFileHandler
	{
		private IContext Context { get; }

		public ImportFileHandler(IContext context)
		{
			this.Context = context;
		}

		public void Handle(string path)
		{
			string text = File.ReadAllText(path);
			JSONParseResult jsonParseResult = JSON.Parse(text);
			this.Context.ImportData(jsonParseResult.Result);
		}
	}
}
