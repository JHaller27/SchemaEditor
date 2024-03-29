using Godot;
using SchemaEditor;

public class StringValueEditor : LineEdit, IValueEditorNode
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	public void SetValue(object value)
	{
		this.Text = (string)value;
	}

	public object GetValue()
	{
		return this.Text;
	}

	public Control GetControlNode() => this;
}
