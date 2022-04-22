using Godot;using SchemaEditor.Interfaces;

public class StringValueEditor : Control, IValueEditor<string>
{
	private LineEdit EditNode { get; set; }

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		this.EditNode = this.GetChild<LineEdit>(0);
	}

	public void SetValue(string value)
	{
		this.EditNode.Text = value;
	}

	public string GetValue()
	{
		throw new System.NotImplementedException();
	}
}
