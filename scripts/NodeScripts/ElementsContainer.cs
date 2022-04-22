using System;
using Godot;
using SchemaEditor;
using SchemaEditor.scripts;

public class ElementsContainer : Control, IValueEditor
{
	private static readonly PackedScene StringEditor = ResourceLoader.Load<PackedScene>("res://scenes/StringValueEditor.tscn");
	private static readonly PackedScene NumberEditor = ResourceLoader.Load<PackedScene>("res://scenes/NumberValueEditor.tscn");
	private static readonly PackedScene ArrayEditor = ResourceLoader.Load<PackedScene>("res://scenes/ArrayValueEditor.tscn");

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		this.SetSchema(SchemaDataType.Array);
	}

	public IValueEditor SetSchema(SchemaDataType dataType)
	{
		switch (dataType)
		{
			case SchemaDataType.Boolean:
				return this.SetBoolean();

			case SchemaDataType.String:
				return this.SetString();

			case SchemaDataType.Number:
				return this.SetNumber();

			case SchemaDataType.Array:
				return this.SetArray(SchemaDataType.String);  // TODO Set itemsType dynamically

			case SchemaDataType.Object:
				return this.SetObject(SchemaDataType.String);  // TODO Set valuesType dynamically

			case SchemaDataType.Integer:
				throw new NotImplementedException();

			case SchemaDataType.Null:
				throw new NotImplementedException();

			default:
				throw new ArgumentOutOfRangeException(nameof(dataType), dataType, null);
		}
	}

	public void SetValue(object value) => this.ChildEditor.SetValue(value);

	public object GetValue() => this.ChildEditor.GetValue();

	private IValueEditor ChildEditor => this.GetChild<IValueEditor>(0);

	protected virtual void ClearEditors()
	{
		foreach (Node child in this.GetChildren())
		{
			this.RemoveChild(child);
		}
	}

	protected IValueEditor SetBoolean()
	{
		throw new NotImplementedException();
	}

	protected IValueEditor SetString()
	{
		this.ClearEditors();

		StringValueEditor editorNode = StringEditor.Instance<StringValueEditor>();

		editorNode.SizeFlagsHorizontal = (int)SizeFlags.ExpandFill;
		editorNode.SizeFlagsVertical = (int)SizeFlags.ExpandFill;

		this.AddChild(editorNode);

		return editorNode;
	}

	protected IValueEditor SetNumber()
	{
		this.ClearEditors();

		NumberValueEditor editorNode = NumberEditor.Instance<NumberValueEditor>();

		editorNode.SizeFlagsHorizontal = (int)SizeFlags.ExpandFill;
		editorNode.SizeFlagsVertical = (int)SizeFlags.ExpandFill;

		this.AddChild(editorNode);

		return editorNode;
	}

	protected IValueEditor SetArray(SchemaDataType itemsType)
	{
		this.ClearEditors();

		ArrayValueEditor editorNode = ArrayEditor.Instance<ArrayValueEditor>();

		editorNode.ItemsType = itemsType;

		editorNode.SizeFlagsHorizontal = (int)SizeFlags.ExpandFill;
		editorNode.SizeFlagsVertical = (int)SizeFlags.ExpandFill;

		this.AddChild(editorNode);

		return editorNode;
	}

	protected IValueEditor SetObject(SchemaDataType valuesType)
	{
		throw new NotImplementedException();
	}
}
