using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarStorageLibrary;

namespace Template
{
	class Program
	{
		static void Main( string[] args )
		{

			
			IStorage storage = StorageFactory.CreateMemoryStorage();

			foreach (Car car in storage.GetAll())
			{
				Console.WriteLine(car);
			}


			Console.ReadKey();

			
		}

	}
}
