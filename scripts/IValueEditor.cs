namespace SchemaEditor
{
	public interface IValueEditor
	{
		void SetValue(object value);
		object GetValue();
	}
}
