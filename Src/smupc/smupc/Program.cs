using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using Ionic.Zlib;

namespace smupc
{
	internal class Program
	{
		public static void Unpack(string source, string dest)
		{
			FileStream input = File.Open(source, FileMode.Open, FileAccess.Read);
			BinaryReader binaryReader = new BinaryReader(input);
			byte[] value = binaryReader.ReadBytes(4);
			byte[] compressed = binaryReader.ReadBytes((int)(binaryReader.BaseStream.Length - binaryReader.BaseStream.Position));
			byte[] array = ZlibStream.UncompressBuffer(compressed);
			Debug.Assert(BitConverter.ToInt32(value, 0) == array.Length);
			File.WriteAllBytes(dest, array);
		}

		public static void Pack(string source, string dest, bool bigEndian)
		{
			byte[] array = File.ReadAllBytes(source);
			byte[] collection = ZlibStream.CompressBuffer(array);
			byte[] source2 = BitConverter.GetBytes(array.Length);
			if (bigEndian)
			{
				source2 = source2.Reverse().ToArray();
			}
			List<byte> list = source2.ToList();
			list.AddRange(collection);
			File.WriteAllBytes(dest, list.ToArray());
		}

		public static void Pack(bool bigEndian)
		{
			string searchPattern = "*.locale";
			string[] files = Directory.GetFiles(Path.Combine(Directory.GetCurrentDirectory(), "data"), searchPattern, SearchOption.AllDirectories);
			string[] array = files;
			string[] array2 = array;
			foreach (string text in array2)
			{
				string extension = Path.GetExtension(text);
				string str = Path.Combine(Path.GetDirectoryName(text), Path.GetFileNameWithoutExtension(text));
				if (extension == ".locale")
				{
					Pack(text, str + ".pc", bigEndian);
				}
			}
		}

		private static void Main(string[] args)
		{
			string text = "-p";
			if (args.Length >= 1)
			{
				text = args[0];
			}
			string text2 = null;
			string text3 = null;
			if (args.Length == 3)
			{
				text2 = args[1];
				text3 = args[2];
			}
			int num;
			switch (text)
			{
			case "-pb":
				if (text2 != null && text3 != null)
				{
					Pack(text2, text3, bigEndian: true);
				}
				else
				{
					Pack(bigEndian: true);
				}
				return;
			case "-p":
				if (text2 != null && text3 != null)
				{
					Pack(text2, text3, bigEndian: false);
				}
				else
				{
					Pack(bigEndian: false);
				}
				return;
			case "-u":
				num = ((args.Length == 3) ? 1 : 0);
				break;
			default:
				num = 0;
				break;
			}
			if (num != 0)
			{
				Unpack(text2, text3);
			}
		}
	}
}
