using System;
using Godot;
using SchemaEditor;
using SchemaEditor.scripts;

public class ElementsSubcontainerValueEditor : ElementsContainerBase, IValueEditor
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	public void SetSchema(SchemaDataType dataType)
	{
		switch (dataType)
		{
			case SchemaDataType.Boolean:
				this.SetBoolean();
				break;

			case SchemaDataType.String:
				this.SetString();
				break;

			case SchemaDataType.Number:
				this.SetNumber();
				break;

			case SchemaDataType.Array:
				this.SetArray(SchemaDataType.String);  // TODO Set itemsType dynamically
				break;

			case SchemaDataType.Object:
				this.SetObject(SchemaDataType.String);  // TODO Set valuesType dynamically
				break;
		}
	}

	protected override void ClearEditors()
	{
		foreach (Node child in this.GetChildren())
		{
			this.RemoveChild(child);
		}
	}

	protected override void SetArray(SchemaDataType itemsType)
	{
		throw new NotImplementedException();
	}

	protected override void SetObject(SchemaDataType valuesType)
	{
		throw new NotImplementedException();
	}

	private IValueEditor ChildEditor => this.GetChild<IValueEditor>(0);

	public void SetValue(object value) => this.ChildEditor.SetValue(value);

	public object GetValue() => this.ChildEditor.GetValue();
}
