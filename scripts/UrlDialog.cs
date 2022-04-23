using Godot;

public class UrlDialog : ConfirmationDialog
{
	private LineEdit LineEdit { get; set; }
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		this.LineEdit = this.GetNode("MarginContainer").GetNode("VBoxContainer").GetNode<LineEdit>("LineEdit");
	}

	[Signal] public delegate void UrlSelected(string url);

	private void _on_UrlDialog_confirmed()
	{
		EmitSignal(nameof(UrlSelected), this.LineEdit.Text);
	}
}
