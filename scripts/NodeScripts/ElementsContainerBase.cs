using System;
using Godot;
using SchemaEditor;
using SchemaEditor.scripts;

public abstract class ElementsContainerBase : Control
{
	private static readonly PackedScene StringEditor = ResourceLoader.Load<PackedScene>("res://scenes/StringValueEditor.tscn");
	private static readonly PackedScene NumberEditor = ResourceLoader.Load<PackedScene>("res://scenes/NumberValueEditor.tscn");
	protected static readonly PackedScene ArrayEditor = ResourceLoader.Load<PackedScene>("res://scenes/ArrayValueEditor.tscn");
	public static readonly PackedScene SubcontainerEditor = ResourceLoader.Load<PackedScene>("res://scenes/SubcontainerValueEditor.tscn");

	protected IValueEditor ValueEditor { get; set; }

	public void ImportData(object data) => this.ValueEditor.SetValue(data);

	public object ExportData() => this.ValueEditor.GetValue();

	protected abstract void ClearEditors();

	protected void SetBoolean()
	{
		throw new NotImplementedException();
	}

	protected void SetString()
	{
		this.ClearEditors();

		StringValueEditor editorNode = StringEditor.Instance<StringValueEditor>();
		this.ValueEditor = editorNode;

		editorNode.SizeFlagsHorizontal = (int)SizeFlags.ExpandFill;
		editorNode.SizeFlagsVertical = (int)SizeFlags.ExpandFill;

		this.AddChild(editorNode);
	}

	protected void SetNumber()
	{
		this.ClearEditors();

		NumberValueEditor editorNode = NumberEditor.Instance<NumberValueEditor>();
		this.ValueEditor = editorNode;

		editorNode.SizeFlagsHorizontal = (int)SizeFlags.ExpandFill;
		editorNode.SizeFlagsVertical = (int)SizeFlags.ExpandFill;

		this.AddChild(editorNode);
	}

	protected abstract void SetArray(SchemaDataType itemsType);
	protected abstract void SetObject(SchemaDataType valuesType);
}
