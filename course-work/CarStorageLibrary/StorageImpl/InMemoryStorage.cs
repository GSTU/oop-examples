using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarStorageLibrary.StorageImpl
{
	class InMemoryStorage:AbstractStorage
	{

		private List<Car> cars;

		public InMemoryStorage()
		{
			cars = new List<Car>();
			cars.Add(new Car(1, "dsfs"));
			cars.Add(new Car(4, "kjrwer"));
		}

		protected override List<Car> Load()
		{
			return cars;
		}

		protected override bool Save( List<Car> cars )
		{
			return true;
		}
	}
}
