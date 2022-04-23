using System;
using Godot.Collections;
using SchemaEditor.scripts;

namespace SchemaEditor.SchemaModel
{
	public class Schema
	{
		public Schema(object jsonResult)
		{
			Dictionary jsonDict = (Dictionary)jsonResult;

			this.Type = jsonDict["type"] as string;
		}

		public string Type { get; set; }

		public SchemaDataType TypeEnum => this.Type.ToLower() switch
		{
			"boolean" => SchemaDataType.Boolean,
			"string" => SchemaDataType.String,
			"number" => SchemaDataType.Number,
			"integer" => SchemaDataType.Integer,
			"array" => SchemaDataType.Array,
			"object" => SchemaDataType.Object,
			"null" => SchemaDataType.Null,
			_ => throw new ArgumentOutOfRangeException(nameof(this.Type), this.Type, null),
		};
	}
}