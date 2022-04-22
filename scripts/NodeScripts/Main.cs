using Godot;
using SchemaEditor;
using SchemaEditor.FileHandling;
using SchemaEditor.FileHandling.Implementations;
using Environment = System.Environment;

public class Main : Control, IValueEditor
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

		this.ElementsContainer = this.GetNode("MarginContainer").GetNode("VBoxContainer").GetNode("Elements").GetNode<ElementsContainer>("SubcontainerValueEditor");
	}

	public void SetValue(object data) => this.ElementsContainer.SetValue(data);

	public object GetValue() => this.ElementsContainer.GetValue();

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
