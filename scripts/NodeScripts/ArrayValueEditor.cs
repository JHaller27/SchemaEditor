using System.Linq;
using Godot;
using SchemaEditor;
using SchemaEditor.SchemaModel;
using GDArray = Godot.Collections.Array;

public class ArrayValueEditor : ElementsContainerBase
{
	private static readonly PackedScene ItemEditor = ResourceLoader.Load<PackedScene>("res://scenes/ArrayItemValueEditor.tscn");

	public Schema ItemsSchema { private get; set; }
	private Node AddItemButton { get; set; }

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		this.AddItemButton = this.GetChild<Node>(0);
	}

	public override void SetValue(object value)
	{
		GDArray valueList = (GDArray)value;
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

	private IValueEditorNode AddItem()
	{
		ArrayItemValueEditor itemEditor = ItemEditor.Instance<ArrayItemValueEditor>();
		itemEditor.Connect(nameof(ArrayItemValueEditor.RemoveTriggered), this, nameof(_on_RemoveItem_triggered));

		itemEditor.SetChildEditor(base.CreateNewItem(this.ItemsSchema));

		this.AddChild(itemEditor.GetControlNode());  // TODO Stuff into a container with a remove button

		this.MoveChild(this.AddItemButton, this.GetChildCount()-1);

		return itemEditor;
	}

	private void _on_AddItemButton_pressed() => this.AddItem();

	private void _on_RemoveItem_triggered(Node source)
	{
		this.RemoveChild(source);
	}
}
