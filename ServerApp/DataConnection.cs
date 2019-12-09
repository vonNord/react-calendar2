using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace ServerApp
{
	public class DataConnection
	{
		static string _ConnectionString = @"Data Source = TFN-MACBOOKPRO\SQLEXPRESS;Initial Catalog = Appointment_Mock; Integrated Security = True";


		public static string ConnectionString { get => DataConnection._ConnectionString; set => DataConnection._ConnectionString = value; }


		public static SqlConnection Connection()
		{
			SqlConnection conn = new SqlConnection( ConnectionString );
			if( conn.State == System.Data.ConnectionState.Closed )
			{
				conn.Open();
			}

			return conn;
		}
	}
}

