using Godot;

namespace SchemaEditor
{
	public interface IValueEditor
	{
		void SetValue(object value);
		object GetValue();
	}

	public interface IValueEditorNode : IValueEditor
	{
		Control GetControlNode();
	}
}
