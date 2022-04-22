using Godot;
using SchemaEditor.FileHandling;
using SchemaEditor.FileHandling.Implementations;
using Environment = System.Environment;

public class Main : Control, IContext
{
	private ImportFileHandler ImportFileHandler { get; set; }
	private ExportFileHandler ExportFileHandler { get; set; }

	private ElementsContainer ElementsContainer { get; set; }

	private FileDialog FileDialog { get; set; }
	private IFileHandler FileHandler { get; set; }

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		this.FileDialog = this.GetNode<FileDialog>("FileDialog");
		this.FileDialog.CurrentDir = Environment.GetEnvironmentVariable("HOME");

		this.ImportFileHandler = new(this);
		this.ExportFileHandler = new(this);

		this.ElementsContainer = this.GetNode("MarginContainer").GetNode("VBoxContainer").GetNode<ElementsContainer>("Elements");
	}

	public void ImportData(object data) => this.ElementsContainer.ImportData(data);

	public object ExportData() => this.ElementsContainer.ExportData();

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

	private void _on_FileDialog_file_selected(string path)
	{
		this.FileDialog.Hide();
		this.FileHandler.Handle(path);
	}
}
