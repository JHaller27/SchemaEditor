using Godot;
using SchemaEditor;

public class ObjectItemValueEditor : HBoxContainer, IValueEditorNode
{
	private IValueEditorNode ChildValueEditor { get; set; }

	public void SetValue(object value) => this.ChildValueEditor.SetValue(value);

	public object GetValue() => this.ChildValueEditor.GetValue();

	public Control GetControlNode() => this;

	public void SetChildEditor(IValueEditorNode child)
	{
		this.ChildValueEditor = child;
		this.RemoveChild(this.GetChild(2));
		this.AddChild(child.GetControlNode());
	}

	[Signal]
	public delegate void RemoveTriggered(Node source);

	private void _on_RemoveButton_pressed()
	{
		EmitSignal(nameof(RemoveTriggered), this);
	}
}
