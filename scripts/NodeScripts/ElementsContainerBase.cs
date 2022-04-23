using System;
using Godot;
using SchemaEditor;
using SchemaEditor.SchemaModel;
using SchemaEditor.scripts;

public abstract class ElementsContainerBase : Control, IValueEditorNode, ISchemaEditor
{
	private static readonly PackedScene StringEditor = ResourceLoader.Load<PackedScene>("res://scenes/StringValueEditor.tscn");
	private static readonly PackedScene NumberEditor = ResourceLoader.Load<PackedScene>("res://scenes/NumberValueEditor.tscn");
	private static readonly PackedScene ArrayEditor = ResourceLoader.Load<PackedScene>("res://scenes/ArrayValueEditor.tscn");

	public abstract void SetValue(object value);
	public abstract object GetValue();
	public abstract Control GetControlNode();

	public void ArrangeSchema(object schema)
	{
		Schema schemaModel = new(schema);
		this.CreateNewItem(schemaModel.TypeEnum);
	}

	public object ExportSchema()
	{
		throw new NotImplementedException();
	}

	protected virtual IValueEditorNode CreateNewItem(SchemaDataType dataType)
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

	private IValueEditorNode SetBoolean()
	{
		throw new NotImplementedException();
	}

	private IValueEditorNode SetString()
	{
		StringValueEditor editorNode = StringEditor.Instance<StringValueEditor>();

		editorNode.SizeFlagsHorizontal = (int)SizeFlags.ExpandFill;
		editorNode.SizeFlagsVertical = (int)SizeFlags.ExpandFill;

		return editorNode;
	}

	private IValueEditorNode SetNumber()
	{
		NumberValueEditor editorNode = NumberEditor.Instance<NumberValueEditor>();

		editorNode.SizeFlagsHorizontal = (int)SizeFlags.ExpandFill;
		editorNode.SizeFlagsVertical = (int)SizeFlags.ExpandFill;

		return editorNode;
	}

	private IValueEditorNode SetArray(SchemaDataType itemsType)
	{
		ArrayValueEditor editorNode = ArrayEditor.Instance<ArrayValueEditor>();

		editorNode.ItemsType = itemsType;

		editorNode.SizeFlagsHorizontal = (int)SizeFlags.ExpandFill;
		editorNode.SizeFlagsVertical = (int)SizeFlags.ExpandFill;

		return editorNode;
	}

	private IValueEditorNode SetObject(SchemaDataType valuesType)
	{
		throw new NotImplementedException();
	}
}
