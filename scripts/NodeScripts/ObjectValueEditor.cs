using System;
using System.Collections.Generic;
using System.Linq;
using Godot;
using SchemaEditor;
using SchemaEditor.SchemaModel;

public class ObjectValueEditor : ElementsContainerBase
{
	private static readonly PackedScene ItemEditor = ResourceLoader.Load<PackedScene>("res://scenes/ObjectItemValueEditor.tscn");

	private Dictionary<string, IValueEditor> PropertiesMap { get; } = new();
	private Node AddItemButton { get; set; }

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		this.AddItemButton = this.GetChild<Node>(0);
	}

	public void SetProperties(IDictionary<string, Schema> properties)
	{
		foreach (KeyValuePair<string, Schema> kvp in properties)
		{
			IValueEditor subcontainerValueEditor = this.AddItem(kvp.Key, kvp.Value);
			this.PropertiesMap[kvp.Key] = subcontainerValueEditor;
		}
	}

	public override void SetValue(object value)
	{
		Dictionary<string, object> valueList = (Dictionary<string, object>)value;
		foreach (KeyValuePair<string, object> kvp in valueList)
		{
			IValueEditor subcontainerValueEditor = this.PropertiesMap[kvp.Key];
			subcontainerValueEditor.SetValue(kvp);
		}
	}

	public override object GetValue()
	{
		object outValue = this.GetChildren()
			.OfType<IValueEditor>()
			.Select(ve => ve.GetValue())
			.Cast<KeyValuePair<string, object>>()
			.ToDictionary(kvp => kvp.Key, kvp => kvp.Value);

		return outValue;
	}

	public override Control GetControlNode() => this;

	private IValueEditor AddItem(string key, Schema valueSchema)
	{
		ObjectItemValueEditor itemEditor = ItemEditor.Instance<ObjectItemValueEditor>();
		itemEditor.Connect(nameof(ObjectItemValueEditor.RemoveTriggered), this, nameof(_on_RemoveItem_triggered));

		// itemEditor.
		itemEditor.SetChildEditor(key, base.CreateNewItem(valueSchema));

		this.AddChild(itemEditor.GetControlNode());

		this.MoveChild(this.AddItemButton, this.GetChildCount()-1);

		return itemEditor;
	}

	private void _on_AddItemButton_pressed() => throw new NotImplementedException("TODO: Implement schema editing");

	private void _on_RemoveItem_triggered(Node source)
	{
		this.RemoveChild(source);
	}
}
