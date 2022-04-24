using System.Linq;
using Godot;
using Godot.Collections;
using SchemaEditor;
using SchemaEditor.SchemaModel;

public class ObjectValueEditor : ElementsContainerBase
{
	private static readonly PackedScene ItemEditor = ResourceLoader.Load<PackedScene>("res://scenes/ObjectItemValueEditor.tscn");

	public Schema ItemsSchema { private get; set; }
	private Node AddItemButton { get; set; }

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		this.AddItemButton = this.GetChild<Node>(0);
	}

	public override void SetValue(object value)
	{
		Array valueList = (Array)value;
		foreach (object item in valueList)
		{
			IValueEditor subcontainerValueEditor = this.AddItem();
			subcontainerValueEditor.SetValue(item);
		}
	}

	public override object GetValue()
	{
		object[] outValue = this.GetChildren()
			.OfType<IValueEditor>()
			.Select(ve => ve.GetValue())
			.ToArray();

		return outValue;
	}

	public override Control GetControlNode() => this;

	private IValueEditor AddItem()
	{
		ObjectItemValueEditor itemEditor = ItemEditor.Instance<ObjectItemValueEditor>();
		itemEditor.Connect(nameof(ObjectItemValueEditor.RemoveTriggered), this, nameof(_on_RemoveItem_triggered));

		itemEditor.SetChildEditor(base.CreateNewItem(this.ItemsSchema));

		this.AddChild(itemEditor.GetControlNode());

		this.MoveChild(this.AddItemButton, this.GetChildCount()-1);

		return itemEditor;
	}

	private void _on_AddItemButton_pressed() => this.AddItem();

	private void _on_RemoveItem_triggered(Node source)
	{
		this.RemoveChild(source);
	}
}
