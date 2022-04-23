using Godot;
using SchemaEditor;
using SchemaEditor.SchemaModel;

public class ElementsContainer : ElementsContainerBase
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	public override void SetValue(object value) => this.ChildEditor.SetValue(value);
	public override object GetValue() => this.ChildEditor.GetValue();
	public override Control GetControlNode() => this;

	private IValueEditor ChildEditor => this.GetChild<IValueEditor>(0);

	protected override IValueEditorNode CreateNewItem(Schema schema)
	{
		this.ClearEditors();
		IValueEditorNode editorNode = base.CreateNewItem(schema);

		this.AddChild(editorNode.GetControlNode());

		return editorNode;
	}

	private void ClearEditors()
	{
		foreach (Node child in this.GetChildren())
		{
			this.RemoveChild(child);
		}
	}
}
