using Godot;using SchemaEditor.Interfaces;

public class StringValueEditor : Control, IValueEditor
{
	private LineEdit EditNode { get; set; }

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		this.EditNode = this.GetChild<LineEdit>(0);
	}

	public void SetValue(object value)
	{
		this.EditNode.Text = (string)value;
	}

	public object GetValue()
	{
		return this.EditNode.Text;
	}
}
