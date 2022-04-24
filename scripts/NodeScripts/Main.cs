using System.Linq;
using System.Text;
using Godot;
using SchemaEditor;
using SchemaEditor.FileHandling;
using SchemaEditor.FileHandling.Implementations;
using Environment = System.Environment;

public class Main : Control, IValueEditor, ISchemaEditor
{
	private ImportFileHandler ImportFileHandler { get; set; }
	private ExportFileHandler ExportFileHandler { get; set; }
	private ImportSchemaFileHandler ImportSchemaFileHandler { get; set; }
	private ExportSchemaFileHandler ExportSchemaFileHandler { get; set; }

	private ElementsContainer ElementsContainer { get; set; }
	private HTTPRequest HttpRequest { get; set; }

	private UrlDialog UrlDialog { get; set; }
	private FileDialog FileDialog { get; set; }
	private IFileHandler FileHandler { get; set; }

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		this.UrlDialog = this.GetNode<UrlDialog>("UrlDialog");
		this.FileDialog = this.GetNode<FileDialog>("FileDialog");
		this.FileDialog.CurrentDir = GetHomePath();

		this.ImportFileHandler = new(this);
		this.ExportFileHandler = new(this);
		this.ImportSchemaFileHandler = new(this);
		this.ExportSchemaFileHandler = new(this);

		this.ElementsContainer = this.GetNode("MarginContainer").GetNode("VBoxContainer").GetNode("Elements").GetNode<ElementsContainer>("ElementsContainerEditor");
		this.HttpRequest = this.GetNode<HTTPRequest>("HTTPRequest");
	}

	public void SetValue(object data) => this.ElementsContainer.SetValue(data);

	public object GetValue() => this.ElementsContainer.GetValue();

	public void ArrangeSchema(object schema) => this.ElementsContainer.ArrangeSchema(schema);

	public object ExportSchema() => this.ElementsContainer.ExportSchema();

	private void _on_ImportButton_pressed()
	{
		this.FileDialog.Mode = FileDialog.ModeEnum.OpenFile;
		this.FileHandler = this.ImportFileHandler;
		this.FileDialog.Show();
	}

	private void _on_ExportButton_pressed()
	{
		this.FileDialog.Mode = FileDialog.ModeEnum.SaveFile;
		this.FileHandler = this.ExportFileHandler;
		this.FileDialog.Show();
	}

	private void _on_SchemaFileImportButton_pressed()
	{
		this.FileDialog.Mode = FileDialog.ModeEnum.OpenFile;
		this.FileHandler = this.ImportSchemaFileHandler;
		this.FileDialog.Show();
	}

	private void _on_SchemaWebImportButton_pressed()
	{
		this.UrlDialog.Show();
	}

	private void _on_SchemaExportButton_pressed()
	{
		this.FileDialog.Mode = FileDialog.ModeEnum.SaveFile;
		this.FileHandler = this.ExportSchemaFileHandler;
		this.FileDialog.Show();
	}

	private void _on_FileDialog_file_selected(string path)
	{
		this.FileDialog.Hide();
		this.FileHandler.Handle(path);
	}

	private void _on_UrlDialog_UrlSelected(string url)
	{
		this.HttpRequest.Request(url);
	}

	private void _on_HTTPRequest_request_completed(int result, int response_code, string[] headers, byte[] body)
	{
		if (result != (int)HTTPRequest.Result.Success) return;

		JSONParseResult json = JSON.Parse(Encoding.UTF8.GetString(body));
		if (json.Error != Error.Ok) return;

		this.ArrangeSchema(json.Result);
	}

	private static string GetHomePath() => CoalesceEnvVar("HOME", "USERPROFILE");

	private static string CoalesceEnvVar(params string[] keys)
	{
		return keys
			.Select(Environment.GetEnvironmentVariable)
			.FirstOrDefault(val => !string.IsNullOrEmpty(val));
	}
}
