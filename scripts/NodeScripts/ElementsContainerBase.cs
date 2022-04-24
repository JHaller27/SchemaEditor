using System;
using System.Collections.Generic;
using Godot;
using SchemaEditor;
using SchemaEditor.SchemaModel;

public abstract class ElementsContainerBase : Control, IValueEditorNode, ISchemaEditor
{
	private static readonly PackedScene StringEditor = ResourceLoader.Load<PackedScene>("res://scenes/StringValueEditor.tscn");
	private static readonly PackedScene NumberEditor = ResourceLoader.Load<PackedScene>("res://scenes/NumberValueEditor.tscn");
	private static readonly PackedScene ArrayEditor = ResourceLoader.Load<PackedScene>("res://scenes/ArrayValueEditor.tscn");
	private static readonly PackedScene ObjectEditor = ResourceLoader.Load<PackedScene>("res://scenes/ObjectValueEditor.tscn");

	public abstract void SetValue(object value);
	public abstract object GetValue();
	public abstract Control GetControlNode();

	public void ArrangeSchema(object schema)
	{
		Schema schemaModel = new(schema);
		this.CreateNewItem(schemaModel);
	}

	public object ExportSchema()
	{
		throw new NotImplementedException();
	}

	protected virtual IValueEditorNode CreateNewItem(Schema schema)
	{
		switch (schema.TypeEnum)
		{
			case SchemaDataType.Boolean:
				return this.SetBoolean();

			case SchemaDataType.String:
				return this.SetString();

			case SchemaDataType.Number:
				return this.SetNumber();

			case SchemaDataType.Array:
				return this.SetArray(schema.Items);

			case SchemaDataType.Object:
				return this.SetObject(schema.Properties);

			case SchemaDataType.Integer:
				throw new NotImplementedException();

			case SchemaDataType.Null:
				throw new NotImplementedException();

			default:
				throw new ArgumentOutOfRangeException(nameof(schema), schema, null);
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

	private IValueEditorNode SetArray(Schema itemsSchema)
	{
		ArrayValueEditor editorNode = ArrayEditor.Instance<ArrayValueEditor>();

		editorNode.ItemsSchema = itemsSchema;

		editorNode.SizeFlagsHorizontal = (int)SizeFlags.ExpandFill;
		editorNode.SizeFlagsVertical = (int)SizeFlags.ExpandFill;

		return editorNode;
	}

	private IValueEditorNode SetObject(IDictionary<string, Schema> properties)
	{
		ObjectValueEditor editorNode = ObjectEditor.Instance<ObjectValueEditor>();

		editorNode.SetProperties(properties);

		editorNode.SizeFlagsHorizontal = (int)SizeFlags.ExpandFill;
		editorNode.SizeFlagsVertical = (int)SizeFlags.ExpandFill;

		return editorNode;
	}
}
