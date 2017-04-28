using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using System.Linq;
using System.Collections;
using System.Reflection;
using System;

[CustomEditor(typeof(ObjectLinker))]
public class ObjectLinkerEditor : Editor
{
	static string[] methods;
	static string[] ignoreMethods = new string[] { "Start", "Update", "OnClick", "Run" };

	static ObjectLinkerEditor()
	{
		methods =
			typeof(ObjectLinker)
				.GetMethods(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public) // Instance methods, both public and private/protected
				.Where(x => x.DeclaringType == typeof(ObjectLinker)) // Only list methods defined in our own class
				.Where(x => x.GetParameters().Length == 0) // Make sure we only get methods with zero argumenrts
				.Where(x => !ignoreMethods.Any(n => n == x.Name)) // Don't list methods in the ignoreMethods array (so we can exclude Unity specific methods, etc.)
				.Select(x => x.Name)
				.ToArray();
	}

	public override void OnInspectorGUI()
	{
		ObjectLinker obj = target as ObjectLinker;

		if (obj != null)
		{
			int index;

			try
			{
				index = methods
					.Select((v, i) => new { Name = v, Index = i })
					.First(x => x.Name == obj.methodToCall)
					.Index;
			}
			catch
			{
				index = 0;
			}

			obj.methodToCall = methods[EditorGUILayout.Popup(index, methods)];
		}
	}
}