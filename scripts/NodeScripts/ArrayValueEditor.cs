using System;
using System.Linq;
using Godot;
using SchemaEditor;
using SchemaEditor.scripts;
using GDArray = Godot.Collections.Array;

public class ArrayValueEditor : Control, IValueEditor
{
	public SchemaDataType ItemsType { private get; set; }
	private Node AddItemButton { get; set; }

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		this.AddItemButton = this.GetChild<Node>(0);
	}

	public void SetValue(object value)
	{
		this.ClearEditors();

		GDArray valueList = (GDArray)value;
		foreach (object item in valueList)
		{
			ElementsSubcontainerValueEditor subcontainerValueEditor = this.AddItem();
			subcontainerValueEditor.SetValue(item);
		}
	}

	public object GetValue()
	{
		object[] outValue = this.GetChildren()
			.OfType<IValueEditor>()
			.Select(ve => ve.GetValue())
			.ToArray();

		return outValue;
	}

	private void ClearEditors()
	{
		foreach (Node child in this.GetChildren())
		{
			if (child != this.AddItemButton)
			{
				this.RemoveChild(child);
			}
		}
	}

	private ElementsSubcontainerValueEditor AddItem()
	{
		ElementsSubcontainerValueEditor subEditorNode = ElementsContainerBase.SubcontainerEditor.Instance<ElementsSubcontainerValueEditor>();
		subEditorNode.SetSchema(this.ItemsType);

		this.AddChild(subEditorNode);

		this.MoveChild(this.AddItemButton, this.GetChildCount()-1);

		return subEditorNode;
	}

	private void _on_AddItemButton_pressed() => this.AddItem();
}
