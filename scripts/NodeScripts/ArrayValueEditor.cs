using System.Linq;
using Godot;
using SchemaEditor;
using SchemaEditor.scripts;
using GDArray = Godot.Collections.Array;

public class ArrayValueEditor : ElementsContainer
{
	public SchemaDataType ItemsType { private get; set; }
	private Node AddItemButton { get; set; }

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		this.AddItemButton = this.GetChild<Node>(0);
	}

	public void SetValue(object value)
	{
		this.ClearEditors();

		GDArray valueList = (GDArray)value;
		foreach (object item in valueList)
		{
			IValueEditor subcontainerValueEditor = this.AddItem();
			subcontainerValueEditor.SetValue(item);
		}
	}

	public object GetValue()
	{
		object[] outValue = this.GetChildren()
			.OfType<IValueEditor>()
			.Select(ve => ve.GetValue())
			.ToArray();

		return outValue;
	}

	protected override void ClearEditors()
	{
		// nop
	}

	private IValueEditor AddItem()
	{
		IValueEditor childEditor = this.SetSchema(this.ItemsType);
		this.MoveChild(this.AddItemButton, this.GetChildCount()-1);

		return childEditor;
	}

	private void _on_AddItemButton_pressed() => this.AddItem();
}
