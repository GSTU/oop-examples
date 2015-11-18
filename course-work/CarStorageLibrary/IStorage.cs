using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarStorageLibrary
{
	public interface IStorage
	{
		Car Get( int id );
		List<Car> GetAll();
		bool Clear();
		bool Delete( int id );
		bool Update( Car car );

		// 
		// bool Save();
	}
}
