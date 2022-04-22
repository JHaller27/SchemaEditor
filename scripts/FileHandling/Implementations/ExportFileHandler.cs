using Godot;
using File = System.IO.File;

namespace SchemaEditor.FileHandling.Implementations
{
	public class ExportFileHandler : IFileHandler
	{
		private IValueEditor Context { get; }

		public ExportFileHandler(IValueEditor context)
		{
			this.Context = context;
		}

		public void Handle(string path)
		{
			object obj = this.Context.GetValue();
			string json = JSON.Print(obj);
			File.WriteAllText(path, json);
		}
	}
}
