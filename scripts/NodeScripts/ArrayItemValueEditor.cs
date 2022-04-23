using Godot;
using SchemaEditor;

public class ArrayItemValueEditor : HBoxContainer, IValueEditorNode
{
	private IValueEditorNode ChildValueEditor { get; set; }

	public void SetValue(object value) => this.ChildValueEditor.SetValue(value);

	public object GetValue() => this.ChildValueEditor.GetValue();

	public Control GetControlNode() => this;

	public void SetChildEditor(IValueEditorNode child)
	{
		this.ChildValueEditor = child;
		this.AddChild(child.GetControlNode());
	}

	[Signal]
	public delegate void RemoveTriggered(Node source);

	private void _on_RemoveButton_pressed()
	{
		EmitSignal(nameof(RemoveTriggered), this);
	}
}
