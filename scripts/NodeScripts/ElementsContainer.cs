using System;
using Godot;
using SchemaEditor.scripts;

public class ElementsContainer : ElementsContainerBase
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		this.SetSchema(SchemaDataType.Array);
	}

	public void SetSchema(SchemaDataType dataType)
	{
		switch (dataType)
		{
			case SchemaDataType.Boolean:
				this.SetBoolean();
				break;

			case SchemaDataType.String:
				this.SetString();
				break;

			case SchemaDataType.Number:
				this.SetNumber();
				break;

			case SchemaDataType.Array:
				this.SetArray(SchemaDataType.String);  // TODO Set itemsType dynamically
				break;

			case SchemaDataType.Object:
				this.SetObject(SchemaDataType.String);  // TODO Set valuesType dynamically
				break;
		}
	}

	protected override void ClearEditors()
	{
		foreach (Node child in this.GetChildren())
		{
			this.RemoveChild(child);
		}
	}

	protected override void SetArray(SchemaDataType itemsType)
	{
		this.ClearEditors();

		ArrayValueEditor editorNode = ArrayEditor.Instance<ArrayValueEditor>();
		this.ValueEditor = editorNode;

		editorNode.ItemsType = itemsType;

		editorNode.SizeFlagsHorizontal = (int)SizeFlags.ExpandFill;
		editorNode.SizeFlagsVertical = (int)SizeFlags.ExpandFill;

		this.AddChild(editorNode);
	}

	protected override void SetObject(SchemaDataType valuesType)
	{
		throw new NotImplementedException();
	}
}
