using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

	
public class MapReader
{
	public List<string> ReadTextFile(TextAsset filePath)
	{
		string pathFile = AssetDatabase.GetAssetPath(filePath);
		StreamReader reader = new StreamReader(pathFile);

		List<string> lines = new List<string>();

		while (reader != null)
		{
			string line = reader.ReadLine();

			if (line == "#")
			{
				break;
			}

			if (reader.EndOfStream)
			{
				break;
			}
			lines.Add(line);
		}

		return lines;

		//Alternative to code above:
		//List<string> lines = new List<string>();
		//string liner = filePath.text.Split('#')[0];
		//string[] rows = liner.Split('\n');
		//lines.AddRange(rows);
		//return lines;
	}
}

