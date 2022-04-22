using System;
using Godot;
using SchemaEditor.scripts;

public class ElementsContainer : Control
{
	private PackedScene StringEditor { get; set; }

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		this.StringEditor = ResourceLoader.Load<PackedScene>("res://scenes/StringValueEditor.tscn");

		this.SetItem(SchemaDataType.String);
	}

	public void SetItem(SchemaDataType dataType)
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
				this.SetArray();
				break;

			case SchemaDataType.Object:
				this.SetObject();
				break;
		}
	}

	private void ClearEditors()
	{
		foreach (Node child in this.GetChildren())
		{
			this.RemoveChild(child);
		}
	}

	private void SetBoolean()
	{
		throw new NotImplementedException();
	}

	private void SetString()
	{
		this.ClearEditors();
		Control editorNode = this.StringEditor.Instance<Control>();
		editorNode.SizeFlagsHorizontal = (int)SizeFlags.ExpandFill;
		editorNode.SizeFlagsVertical = (int)SizeFlags.ExpandFill;
		this.AddChild(editorNode);
	}

	private void SetNumber()
	{
		throw new NotImplementedException();
	}

	private void SetArray()
	{
		throw new NotImplementedException();
	}

	private void SetObject()
	{
		throw new NotImplementedException();
	}
}
