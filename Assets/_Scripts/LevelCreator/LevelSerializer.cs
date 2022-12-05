using System.IO;
using System.Linq;
using System.Text;
using UnityEngine;

namespace _Scripts.LevelCreator
{
	public static class LevelSerializer
	{
		public static string SerializeLevel(LevelSO levelSO)
		{
			return JsonUtility.ToJson(levelSO);
		}

		public static void SaveSerializedLevelOnDesktop(string serializedData, string fileName)
		{
			DirectoryInfo info = Directory.CreateDirectory(
				$"{System.Environment.GetFolderPath(System.Environment.SpecialFolder.Desktop)}\\KHPI_Levels");

			if (string.IsNullOrWhiteSpace(fileName) || string.IsNullOrEmpty(fileName))
			{
				fileName = "Level_" + info.GetFiles().Count(f => f.Extension == ".txt");
			}
			
			System.IO.File.WriteAllText(
				$"{System.Environment.GetFolderPath(System.Environment.SpecialFolder.Desktop)}\\KHPI_Levels\\{fileName}.txt", 
				serializedData, 
				Encoding.Default);
		}
	}
}