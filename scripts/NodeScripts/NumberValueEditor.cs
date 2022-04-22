using Godot;
using SchemaEditor;

public class NumberValueEditor : Control, IValueEditor
{
	private SpinBox EditNode { get; set; }

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		this.EditNode = this.GetChild<SpinBox>(0);
	}

	public void SetValue(object value)
	{
		this.EditNode.Value = (float)value;
	}

	public object GetValue()
	{
		return this.EditNode.Value;
	}
}
