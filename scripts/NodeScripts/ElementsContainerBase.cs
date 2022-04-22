using System;
using Godot;
using SchemaEditor;
using SchemaEditor.scripts;

public abstract class ElementsContainerBase : Control, IValueEditor
{
	private static readonly PackedScene StringEditor = ResourceLoader.Load<PackedScene>("res://scenes/StringValueEditor.tscn");
	private static readonly PackedScene NumberEditor = ResourceLoader.Load<PackedScene>("res://scenes/NumberValueEditor.tscn");
	private static readonly PackedScene ArrayEditor = ResourceLoader.Load<PackedScene>("res://scenes/ArrayValueEditor.tscn");

	public abstract void SetValue(object value);
	public abstract object GetValue();

	protected virtual IValueEditor AddItem(SchemaDataType dataType)
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

	private IValueEditor SetBoolean()
	{
		throw new NotImplementedException();
	}

	private IValueEditor SetString()
	{
		StringValueEditor editorNode = StringEditor.Instance<StringValueEditor>();

		editorNode.SizeFlagsHorizontal = (int)SizeFlags.ExpandFill;
		editorNode.SizeFlagsVertical = (int)SizeFlags.ExpandFill;

		this.AddChild(editorNode);

		return editorNode;
	}

	private IValueEditor SetNumber()
	{
		NumberValueEditor editorNode = NumberEditor.Instance<NumberValueEditor>();

		editorNode.SizeFlagsHorizontal = (int)SizeFlags.ExpandFill;
		editorNode.SizeFlagsVertical = (int)SizeFlags.ExpandFill;

		this.AddChild(editorNode);

		return editorNode;
	}

	private IValueEditor SetArray(SchemaDataType itemsType)
	{
		ArrayValueEditor editorNode = ArrayEditor.Instance<ArrayValueEditor>();

		editorNode.ItemsType = itemsType;

		editorNode.SizeFlagsHorizontal = (int)SizeFlags.ExpandFill;
		editorNode.SizeFlagsVertical = (int)SizeFlags.ExpandFill;

		this.AddChild(editorNode);

		return editorNode;
	}

	private IValueEditor SetObject(SchemaDataType valuesType)
	{
		throw new NotImplementedException();
	}
}
