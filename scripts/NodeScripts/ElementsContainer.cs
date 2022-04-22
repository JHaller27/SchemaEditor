using System;
using Godot;
using SchemaEditor;
using SchemaEditor.scripts;

public class ElementsContainer : ElementsContainerBase
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		this.AddItem(SchemaDataType.Array);
	}

	public override void SetValue(object value) => this.ChildEditor.SetValue(value);

	public override object GetValue() => this.ChildEditor.GetValue();

	private IValueEditor ChildEditor => this.GetChild<IValueEditor>(0);

	protected override IValueEditor AddItem(SchemaDataType dataType)
	{
		this.ClearEditors();
		return base.AddItem(dataType);
	}

	private void ClearEditors()
	{
		foreach (Node child in this.GetChildren())
		{
			this.RemoveChild(child);
		}
	}
}
