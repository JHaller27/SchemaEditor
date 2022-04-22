using System;
using Godot;
using SchemaEditor.Interfaces;
using SchemaEditor.scripts;

public class ElementsContainer : Control
{
	private static readonly PackedScene StringEditor = ResourceLoader.Load<PackedScene>("res://scenes/StringValueEditor.tscn");

	private IValueEditor ValueEditor { get; set; }

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		this.SetSchema(SchemaDataType.String);
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
		StringValueEditor editorNode = StringEditor.Instance<StringValueEditor>();
		this.ValueEditor = editorNode;

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

	private void PrintValue()
	{
		Console.WriteLine(this.ValueEditor.Stringify());
	}

	private void _on_ExportButton_pressed()
	{
		this.PrintValue();
	}
}
