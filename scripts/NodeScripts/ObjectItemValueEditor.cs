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

		Control oldChild = this.GetChild<Control>(2);
		Control newChild = child.GetControlNode();

		float stretchRatio = oldChild.SizeFlagsStretchRatio;
		newChild.SizeFlagsStretchRatio = stretchRatio;

		this.RemoveChild(oldChild);
		this.AddChild(newChild);
	}

	[Signal]
	public delegate void RemoveTriggered(Node source);

	private void _on_RemoveButton_pressed()
	{
		EmitSignal(nameof(RemoveTriggered), this);
	}
}
