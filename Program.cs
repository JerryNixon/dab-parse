using System.Text;
using System.Text.Json;

internal class Program
{
	private static void Main(string[] args)
	{
		var path = "samples/one.json";
		var encodedSettings = EncodeSettings(path);

		var expectedEncodings = "M11MD0S1MMM0011001";
		Console.WriteLine($"Ordinal 01: Actual/Expected = {encodedSettings[00]}/{expectedEncodings[00]} | Match: {(encodedSettings[00] == expectedEncodings[00] ? 1 : 0)} | 'data-source-files'");
		Console.WriteLine($"Ordinal 02: Actual/Expected = {encodedSettings[01]}/{expectedEncodings[01]} | Match: {(encodedSettings[01] == expectedEncodings[01] ? 1 : 0)} | 'runtime.rest.enabled'");
		Console.WriteLine($"Ordinal 03: Actual/Expected = {encodedSettings[02]}/{expectedEncodings[02]} | Match: {(encodedSettings[02] == expectedEncodings[02] ? 1 : 0)} | 'runtime.graphql.enabled'");
		Console.WriteLine($"Ordinal 04: Actual/Expected = {encodedSettings[03]}/{expectedEncodings[03]} | Match: {(encodedSettings[03] == expectedEncodings[03] ? 1 : 0)} | 'runtime.graphql.multiple-mutations.create.enabled'");
		Console.WriteLine($"Ordinal 05: Actual/Expected = {encodedSettings[04]}/{expectedEncodings[04]} | Match: {(encodedSettings[04] == expectedEncodings[04] ? 1 : 0)} | 'runtime.host.mode'");
		Console.WriteLine($"Ordinal 06: Actual/Expected = {encodedSettings[05]}/{expectedEncodings[05]} | Match: {(encodedSettings[05] == expectedEncodings[05] ? 1 : 0)} | 'runtime.host.cors.allow-credentials'");
		Console.WriteLine($"Ordinal 07: Actual/Expected = {encodedSettings[06]}/{expectedEncodings[06]} | Match: {(encodedSettings[06] == expectedEncodings[06] ? 1 : 0)} | 'runtime.host.authentication.provider'");
		Console.WriteLine($"Ordinal 08: Actual/Expected = {encodedSettings[07]}/{expectedEncodings[07]} | Match: {(encodedSettings[07] == expectedEncodings[07] ? 1 : 0)} | 'cache.enabled'");
		Console.WriteLine($"Ordinal 09: Actual/Expected = {encodedSettings[08]}/{expectedEncodings[08]} | Match: {(encodedSettings[08] == expectedEncodings[08] ? 1 : 0)} | 'pagination.max-page-size'");
		Console.WriteLine($"Ordinal 10: Actual/Expected = {encodedSettings[09]}/{expectedEncodings[09]} | Match: {(encodedSettings[09] == expectedEncodings[09] ? 1 : 0)} | 'pagination.default-page-size'");
		Console.WriteLine($"Ordinal 11: Actual/Expected = {encodedSettings[10]}/{expectedEncodings[10]} | Match: {(encodedSettings[10] == expectedEncodings[10] ? 1 : 0)} | 'runtime.host.max-response-size-mb'");
		Console.WriteLine($"Ordinal 12: Actual/Expected = {encodedSettings[11]}/{expectedEncodings[11]} | Match: {(encodedSettings[11] == expectedEncodings[11] ? 1 : 0)} | 'telemetry.application-insights.enabled'");
		Console.WriteLine($"Ordinal 13: Actual/Expected = {encodedSettings[12]}/{expectedEncodings[12]} | Match: {(encodedSettings[12] == expectedEncodings[12] ? 1 : 0)} | 'Entities: count'");
		Console.WriteLine($"Ordinal 14: Actual/Expected = {encodedSettings[13]}/{expectedEncodings[13]} | Match: {(encodedSettings[13] == expectedEncodings[13] ? 1 : 0)} | 'Entities: any use table'");
		Console.WriteLine($"Ordinal 15: Actual/Expected = {encodedSettings[14]}/{expectedEncodings[14]} | Match: {(encodedSettings[14] == expectedEncodings[14] ? 1 : 0)} | 'Entities: any use view'");
		Console.WriteLine($"Ordinal 16: Actual/Expected = {encodedSettings[15]}/{expectedEncodings[15]} | Match: {(encodedSettings[15] == expectedEncodings[15] ? 1 : 0)} | 'Entities: any use stored procedures'");
		Console.WriteLine($"Ordinal 17: Actual/Expected = {encodedSettings[16]}/{expectedEncodings[16]} | Match: {(encodedSettings[16] == expectedEncodings[16] ? 1 : 0)} | 'Entities: any use policies'");
		Console.WriteLine($"Ordinal 18: Actual/Expected = {encodedSettings[17]}/{expectedEncodings[17]} | Match: {(encodedSettings[17] == expectedEncodings[17] ? 1 : 0)} | 'Entities: any use cache'");
		Console.Read();

		static string EncodeSettings(string path)
		{
			var text = File.ReadAllText(path);

			// Parse the JSON and get the root element
			var jsonDocument = JsonDocument.Parse(text);
			JsonElement root = jsonDocument.RootElement;

			var defaults = new Dictionary<string, object>
			{
				{"pagination.max-page-size", 100000},
				{"pagination.default-page-size", 100},
				{"runtime.host.max-response-size-mb", 158}
			};

			var ordinal01 = root.GetConfigurationProperty("data-source-files").EncodeToSmallCount();

			var ordinal02 = root.GetConfigurationProperty("runtime.rest.enabled").EncodeToBoolean();

			var ordinal03 = root.GetConfigurationProperty("runtime.graphql.enabled").EncodeToBoolean();

			var ordinal04 = root.GetConfigurationProperty("runtime.graphql.multiple-mutations.create.enabled").EncodeToBoolean();

			var ordinal05 = root.GetConfigurationProperty("runtime.host.mode").GetString() switch
			{
				"production" => 'P',
				"development" => 'D',
				_ => '!' // Return '!' for any unexpected values
			};

			var ordinal06 = root.GetConfigurationProperty("runtime.host.cors.allow-credentials").EncodeToBoolean();

			var ordinal07 = root.GetConfigurationProperty("runtime.host.authentication.provider").GetString() switch
			{
				"StaticWebApps" => 'S',
				"AppService" => 'A',
				"AzureId" => 'Z',
				"Simulator" => 'D',
				"EntraId" => 'E',
				"Oauth" => 'O',
				"None" => 'N',
				null => 'M',
				_ => '!'
			};

			var ordinal08 = root.GetConfigurationProperty("runtime.cache.enabled").EncodeToBoolean();

			var ordinal09 = root.GetConfigurationProperty("runtime.pagination.max-page-size").EncodeToCustom(defaults["pagination.max-page-size"]);

			var ordinal10 = root.GetConfigurationProperty("runtime.pagination.default-page-size").EncodeToCustom(defaults["pagination.default-page-size"]);

			var ordinal11 = root.GetConfigurationProperty("runtime.host.max-response-size-mb").EncodeToCustom(defaults["runtime.host.max-response-size-mb"]);

			var ordinal12 = root.GetConfigurationProperty("runtime.telemetry.application-insights.enabled").EncodeToBoolean();

			var ordinal13 = root.GetProperty("entities").EnumerateObject().Count() switch
			{
				0 => '0', // No entities
				1 => '1', // One entity
				2 => '2',
				3 => '3',
				4 => '4',
				5 => '5',
				6 => '6',
				7 => '7',
				8 => '8',
				9 => '9',
				_ => 'A' // More than 9
			};

			var entities = root.GetAllEntities();

			var ordinal14 = entities.FindAny("source.type", "table") ? '1' : '0';

			var ordinal15 = entities.FindAny("source.type", "view") ? '1' : '0';

			var ordinal16 = entities.FindAny("source.type", "stored-procedure") ? '1' : '0';

			var ordinal17 = entities.GetAllPolicies().Length != 0 ? '1' : '0';

			var ordinal18 = entities.FindAny("cache.enabled", "true") ? '1' : '0';

			var settingsBuilder = new StringBuilder();
			settingsBuilder.Append(ordinal01);
			settingsBuilder.Append(ordinal02);
			settingsBuilder.Append(ordinal03);
			settingsBuilder.Append(ordinal04);
			settingsBuilder.Append(ordinal05);
			settingsBuilder.Append(ordinal06);
			settingsBuilder.Append(ordinal07);
			settingsBuilder.Append(ordinal08);
			settingsBuilder.Append(ordinal09);
			settingsBuilder.Append(ordinal10);
			settingsBuilder.Append(ordinal11);
			settingsBuilder.Append(ordinal12);
			settingsBuilder.Append(ordinal13);
			settingsBuilder.Append(ordinal14);
			settingsBuilder.Append(ordinal15);
			settingsBuilder.Append(ordinal16);
			settingsBuilder.Append(ordinal17);
			settingsBuilder.Append(ordinal18);
			return settingsBuilder.ToString();
		}
	}
}

file static class Extensions
{
	public static JsonElement GetConfigurationProperty(this JsonElement element, string propertyPath)
	{
		var properties = propertyPath.Split('.');
		foreach (var property in properties)
		{
			if (element.TryGetProperty(property, out JsonElement nestedElement))
			{
				element = nestedElement;
			}
			else
			{
				using var jsonDocument = JsonDocument.Parse("null");
				return jsonDocument.RootElement.Clone();
			}
		}
		return element;
	}

	public static JsonElement[] GetAllEntities(this JsonElement root)
	{
		if (root.TryGetProperty("entities", out JsonElement entities))
		{
			if (entities.ValueKind == JsonValueKind.Object)
			{
				return entities.EnumerateObject()
							   .Select(entity => JsonDocument.Parse(entity.Value.GetRawText()).RootElement)
							   .ToArray(); // Return an array of JsonElement for each entity
			}
			else if (entities.ValueKind == JsonValueKind.Array)
			{
				// Handle the case where "entities" is an array; return empty array
				return Array.Empty<JsonElement>();
			}
		}

		// Return an empty array if "entities" is missing or not an object
		return Array.Empty<JsonElement>();
	}

	public static JsonElement[] GetAllPolicies(this JsonElement[] entities)
	{
		var policies = new List<JsonElement>();

		foreach (var entity in entities)
		{
			// Check if the entity is an object before accessing properties
			if (entity.ValueKind == JsonValueKind.Object &&
				entity.TryGetProperty("permissions", out JsonElement permissionsElement) &&
				permissionsElement.ValueKind == JsonValueKind.Array)
			{
				foreach (var permission in permissionsElement.EnumerateArray())
				{
					if (permission.TryGetProperty("policy", out JsonElement policyElement))
					{
						policies.Add(policyElement);
					}
				}
			}
		}

		return policies.ToArray();
	}

	public static bool FindAny(this JsonElement[] entities, string complexProperty, string expectedValue = null)
	{
		foreach (var entity in entities)
		{
			if (entity.ValueKind == JsonValueKind.Object)
			{
				var currentElement = entity;
				var parts = complexProperty.Split('.');

				foreach (var part in parts)
				{
					if (currentElement.TryGetProperty(part, out JsonElement nextElement))
					{
						currentElement = nextElement;
					}
					else
					{
						currentElement = default; // Set to default when property is missing
						break;
					}
				}

				if (currentElement.ValueKind != JsonValueKind.Null)
				{
					// Check against expected value
					if (expectedValue == null)
					{
						return true; // Found a match if expectedValue is null
					}
					else if (currentElement.ValueKind == JsonValueKind.String)
					{
						if (currentElement.GetString() == expectedValue)
						{
							return true; // Found a match
						}
					}
					else if (currentElement.ValueKind == JsonValueKind.True || currentElement.ValueKind == JsonValueKind.False)
					{
						if (expectedValue == "true" && currentElement.GetBoolean() ||
							expectedValue == "false" && !currentElement.GetBoolean())
						{
							return true; // Found a match for boolean values
						}
					}
				}
			}
		}

		return false; // No matches found
	}

	public static char EncodeToSmallCount(this JsonElement element)
	{
		if (element.ValueKind == JsonValueKind.Array)
		{
			var count = element.GetArrayLength();
			if (count <= 9) return count.ToString()[0];
			return 'Z'; // More than 9
		}
		if (element.ValueKind == JsonValueKind.Number)
		{
			var value = element.GetInt32();
			if (value <= 9) return value.ToString()[0];
			return 'Z'; // More than 9
		}
		return 'M'; // Missing or invalid type
	}

	public static char EncodeToBigCount(this JsonElement element)
	{
		int count = 0;

		if (element.ValueKind == JsonValueKind.Array)
		{
			count = element.GetArrayLength();
		}
		else if (element.ValueKind == JsonValueKind.Number)
		{
			count = element.GetInt32();
		}
		else if (element.ValueKind == JsonValueKind.Null || element.ValueKind == JsonValueKind.Undefined)
		{
			return 'M'; // Missing
		}
		else
		{
			return '!'; // Error or unknown type
		}

		return count switch
		{
			0 => 'M',
			_ when count.Between(1, 9) => '0',
			_ when count.Between(10, 19) => '1',
			_ when count.Between(20, 29) => '2',
			_ when count.Between(30, 39) => '3',
			_ when count.Between(40, 49) => '4',
			_ when count.Between(50, 59) => '5',
			_ when count.Between(60, 69) => '6',
			_ when count.Between(70, 79) => '7',
			_ when count.Between(80, 89) => '8',
			_ when count.Between(90, 99) => '9',
			_ when count.Between(100, 199) => 'A',
			_ when count.Between(200, 299) => 'B',
			_ when count.Between(300, 399) => 'C',
			_ when count.Between(400, 499) => 'D',
			_ when count.Between(500, 599) => 'E',
			_ when count.Between(600, 699) => 'F',
			_ when count.Between(700, 799) => 'G',
			_ when count.Between(800, 899) => 'H',
			_ when count.Between(900, 999) => 'I',
			_ => 'Z' // More than 999
		};
	}

	public static char EncodeToBoolean(this JsonElement element)
	{
		if (element.ValueKind == JsonValueKind.True)
		{
			return '1';
		}
		else if (element.ValueKind == JsonValueKind.False)
		{
			return '0';
		}
		else if (element.ValueKind == JsonValueKind.Null || element.ValueKind == JsonValueKind.Undefined)
		{
			return 'M'; // Missing
		}

		return '!'; // Error or unknown type
	}

	public static char EncodeToCustom(this JsonElement element, object defaultValue)
	{
		if (element.ValueKind == JsonValueKind.Number)
		{
			double value = element.GetDouble();
			if (!Equals(value, Convert.ToDouble(defaultValue))) return 'C';
			return 'D';
		}
		else if (element.ValueKind == JsonValueKind.String)
		{
			string? value = element.GetString();
			if (!Equals(value, defaultValue)) return 'C';
			return 'D';
		}
		else if (element.ValueKind == JsonValueKind.True || element.ValueKind == JsonValueKind.False)
		{
			bool value = element.GetBoolean();
			if (!Equals(value, defaultValue)) return 'C';
			return 'D';
		}
		else if (element.ValueKind == JsonValueKind.Null || element.ValueKind == JsonValueKind.Undefined)
		{
			return 'M'; // Missing
		}

		return '!'; // Error or unknown type
	}

	public static bool Between<T>(this T value, T min, T max)
		where T : System.Numerics.INumber<T>
	{
		return value >= min && value <= max;
	}
}
