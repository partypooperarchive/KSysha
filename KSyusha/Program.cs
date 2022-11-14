/*
 * Created by SharpDevelop.
 * User: User
 * Date: 02.05.2022
 * Time: 22:52
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.IO;

namespace KSyusha
{
	class Program
	{
		const string assembly_name = "Assembly-CSharp.dll";
		
		public static void Main(string[] args)
		{
			string dll_path = null;
			string output_file = null;
			
			#if DEBUG
			dll_path = @"Z:\Il2CppDumper\OUT-2.8.0-OS\DummyDll";
			output_file = @"Z:\output.ksy";
			#else
			if (args.Length < 2)
			{
				Usage();
				return;
			}
			
			dll_path = args[0];
			output_file = args[1];
			#endif
			
			var parser = new AssemblyParser(Path.Combine(dll_path, assembly_name));
			
			parser.Parse();
			
			parser.Dump(output_file);
			
			#if DEBUG
			Console.Write("Press any key to continue . . . ");
			Console.ReadKey(true);
			#endif
		}
		
		public static void Usage() {
			var param_string = "\t{0,-15} {1}";
			
			var usage = string.Join(
				Environment.NewLine,
				"KS dumper tool",
				"",
				"Usage:",
				string.Format("\t{0} input_dir output_file", AppDomain.CurrentDomain.FriendlyName),
				"",
				"Parameters:",
				string.Format(param_string, "input_dir", "Directory where Assembly-CSharp.dll is located"),
				string.Format(param_string, "output_file", "Path to the output file (beware of overwriting!)"),
				""
			);
			Console.WriteLine(usage);
		}
	}
}