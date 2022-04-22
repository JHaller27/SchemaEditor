using System;
using Godot;
using SchemaEditor;
using SchemaEditor.scripts;

public class ElementsContainer : Control, IValueEditor
{
	private static readonly PackedScene StringEditor = ResourceLoader.Load<PackedScene>("res://scenes/StringValueEditor.tscn");
	private static readonly PackedScene NumberEditor = ResourceLoader.Load<PackedScene>("res://scenes/NumberValueEditor.tscn");
	private static readonly PackedScene ArrayEditor = ResourceLoader.Load<PackedScene>("res://scenes/ArrayValueEditor.tscn");
	public static readonly PackedScene SubcontainerEditor = ResourceLoader.Load<PackedScene>("res://scenes/SubcontainerValueEditor.tscn");

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

	public void SetValue(object value) => this.ChildEditor.SetValue(value);

	public object GetValue() => this.ChildEditor.GetValue();

	protected  void ClearEditors()
	{
		foreach (Node child in this.GetChildren())
		{
			this.RemoveChild(child);
		}
	}

	protected  void SetArray(SchemaDataType itemsType)
	{
		this.ClearEditors();

		ArrayValueEditor editorNode = ArrayEditor.Instance<ArrayValueEditor>();

		editorNode.ItemsType = itemsType;

		editorNode.SizeFlagsHorizontal = (int)SizeFlags.ExpandFill;
		editorNode.SizeFlagsVertical = (int)SizeFlags.ExpandFill;

		this.AddChild(editorNode);
	}

	protected  void SetObject(SchemaDataType valuesType)
	{
		throw new NotImplementedException();
	}

	private IValueEditor ChildEditor => this.GetChild<IValueEditor>(0);

	protected void SetBoolean()
	{
		throw new NotImplementedException();
	}

	protected void SetString()
	{
		this.ClearEditors();

		StringValueEditor editorNode = StringEditor.Instance<StringValueEditor>();

		editorNode.SizeFlagsHorizontal = (int)SizeFlags.ExpandFill;
		editorNode.SizeFlagsVertical = (int)SizeFlags.ExpandFill;

		this.AddChild(editorNode);
	}

	protected void SetNumber()
	{
		this.ClearEditors();

		NumberValueEditor editorNode = NumberEditor.Instance<NumberValueEditor>();

		editorNode.SizeFlagsHorizontal = (int)SizeFlags.ExpandFill;
		editorNode.SizeFlagsVertical = (int)SizeFlags.ExpandFill;

		this.AddChild(editorNode);
	}
}
