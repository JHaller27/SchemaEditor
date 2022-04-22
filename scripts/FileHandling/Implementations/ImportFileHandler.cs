using Godot;
using File = System.IO.File;

namespace SchemaEditor.FileHandling.Implementations
{
	public class ImportFileHandler : IFileHandler
	{
		private IValueEditor Context { get; }

		public ImportFileHandler(IValueEditor context)
		{
			this.Context = context;
		}

		public void Handle(string path)
		{
			string text = File.ReadAllText(path);
			JSONParseResult jsonParseResult = JSON.Parse(text);
			this.Context.SetValue(jsonParseResult.Result);
		}
	}
}
