using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarStorageLibrary.StorageImpl;

namespace CarStorageLibrary
{
	public static class StorageFactory
	{
		public static IStorage CreateMemoryStorage()
		{
			return new InMemoryStorage();
		}

		public static IStorage CreateBinarryStorage(String path)
		{
			return new BinarryStorage();
		}

		public static IStorage CreateCsvStorage(String path)
		{
			return new CsvStorage();
		}


	}
}
