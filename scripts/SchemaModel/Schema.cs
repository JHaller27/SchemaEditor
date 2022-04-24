using System;
using System.Collections;
using System.Collections.Generic;
using GDC = Godot.Collections;

namespace SchemaEditor.SchemaModel
{
	public class Schema
	{
		public Schema(object jsonResult)
		{
			GDC.Dictionary jsonDict = (GDC.Dictionary)jsonResult;

			this.Type = jsonDict["type"] as string;

			if (this.TypeEnum == SchemaDataType.Array)
			{
				this.Items = new(jsonDict["items"]);
			}
			else if (this.TypeEnum == SchemaDataType.Object)
			{
				this.Properties = new();
				GDC.Dictionary properties = (GDC.Dictionary)jsonDict["properties"];
				foreach (DictionaryEntry kvp in properties)
				{
					this.Properties[(string)kvp.Key] = new(kvp.Value);
				}
			}
		}

		public string Type { get; set; }
		public Schema Items { get; }
		public Dictionary<string, Schema> Properties { get; }

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
