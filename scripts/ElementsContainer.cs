using System;
using Godot;
using SchemaEditor.scripts;

public class ElementsContainer : VBoxContainer
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{

	}

	public void AddItem(SchemaDataType dataType)
	{
		switch (dataType)
		{
			case SchemaDataType.Boolean:
				this.AddBoolean();
				break;

			case SchemaDataType.String:
				this.AddString();
				break;

			case SchemaDataType.Number:
				this.AddNumber();
				break;

			case SchemaDataType.Array:
				this.AddArray();
				break;

			case SchemaDataType.Object:
				this.AddObject();
				break;
		}
	}

	private void AddBoolean()
	{
		throw new NotImplementedException();
	}

	private void AddString()
	{
		throw new NotImplementedException();
	}

	private void AddNumber()
	{
		throw new NotImplementedException();
	}

	private void AddArray()
	{
		throw new NotImplementedException();
	}

	private void AddObject()
	{
		throw new NotImplementedException();
	}
}
