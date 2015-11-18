using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarStorageLibrary
{
	abstract class AbstractStorage: IStorage
	{
		protected abstract List<Car> Load();
		protected abstract bool Save(List<Car> cars);

		public Car Get( int id )
		{
			List<Car> cars = Load();
			foreach (Car car in cars)
			{
				if (car.Id == id)
				{
					return car;
				}
			}

			throw new NotImplementedException();
		}

		public List<Car> GetAll()
		{
			return Load();
		}

		public bool Clear()
		{
			throw new NotImplementedException();
		}

		public bool Delete( int id )
		{
			throw new NotImplementedException();
		}

		public bool Update( Car car )
		{
			throw new NotImplementedException();
		}

	}
}
