namespace SchemaEditor.FileHandling
{
	public interface IContext
	{
		void ImportData(object data);
		object ExportData();
	}
}
