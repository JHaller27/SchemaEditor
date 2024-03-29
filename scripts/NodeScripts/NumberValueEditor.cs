using Godot;
using SchemaEditor;

public class NumberValueEditor : SpinBox, IValueEditorNode
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	public void SetValue(object value)
	{
		this.Value = (float)value;
	}

	public object GetValue()
	{
		return this.Value;
	}

	public Control GetControlNode() => this;
}
