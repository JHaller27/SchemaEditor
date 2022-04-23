namespace SchemaEditor
{
	public interface ISchemaEditor
	{
		void ArrangeSchema(object schema);
		object ExportSchema();
	}
}
