namespace SchemaEditor.Interfaces
{
	public interface IValueEditor<T>
	{
		void SetValue(T value);
		T GetValue();
	}
}
