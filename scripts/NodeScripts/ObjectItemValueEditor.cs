using System.Collections.Generic;
using Godot;
using SchemaEditor;

public class ObjectItemValueEditor : HBoxContainer, IValueEditorNode
{
	private IValueEditorNode ChildValueEditor { get; set; }

	private string KeyText
	{
		get => this.GetNode<LineEdit>("KeyLineEdit").Text;
		set => this.GetNode<LineEdit>("KeyLineEdit").Text = value;
	}

	public void SetValue(object value)
	{
		KeyValuePair<string, object> kvp = (KeyValuePair<string, object>)value;
		this.KeyText = kvp.Key;
		this.ChildValueEditor.SetValue(kvp.Value);
	}

	public object GetValue() => new KeyValuePair<string, object>(this.KeyText, this.ChildValueEditor.GetValue());

	public Control GetControlNode() => this;

	public void SetChildEditor(string key, IValueEditorNode child)
	{
		this.KeyText = key;
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
