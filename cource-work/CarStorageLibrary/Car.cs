using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarStorageLibrary
{
	public class Car
	{
		public int Id { get; set; }
		public String Brand { get; set; }

		public Car(int id, String brand) {
			Id = id;
			Brand = brand;
		}

		public override string ToString()
		{
			return String.Format("{0} {1}", Id, Brand);
		}
	}
}
