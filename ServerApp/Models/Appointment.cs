using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;

namespace ServerApp.Models
{
	public class Appointment
	{
		public Appointment( int id, string type, DateTime startTime, DateTime endTime
							, string contactName = "", string personName = "", string description = "", string location = "" )
		{
			Id = id;
			Type = type;
			StartTime = startTime;
			EndTime = endTime;
			ContactName = contactName;
			PersonName = personName;
			Description = description;
			Location = location;
		}

		public Appointment()
		{
		}

		public int Id { get; set; }

		public string Type { get; set; }

		public DateTime StartTime { get; set; }
		public DateTime EndTime { get; set; }

		public string ContactName { get; set; }
		public string PersonName { get; set; }

		public string Description { get; set; }
		public string Location { get; set; }

		public async Task<List<Appointment>> Appointments()
		{
			using( DataConnection.Connection() )
			{
				string query = "SELECT * FROM Appointment";
				CommandType command = CommandType.Text;
				var rs = await DataConnection.Connection().QueryAsync<Appointment>( query, null, null, null, command );
				return rs.ToList();
			}
		}


		public async Task<Appointment> GetAppointment( int Id )
		{
			using( DataConnection.Connection() )
			{
				string query = "SELECT * FROM Appointment WHERE Id = @Id";
				CommandType command = CommandType.Text;
				var param = new DynamicParameters();
				param.Add( "@Id", Id );

				var rs = await DataConnection.Connection().QueryAsync<Appointment>( query, param, null, null, command );

				return rs.FirstOrDefault();
			}
		}

		public async Task<int> AddAppointment( Appointment appointment )
		{
			using( DataConnection.Connection() )
			{
				var insert = 0;
				string query = "INSERT INTO Appointment VALUES(@Type, @StartTime, @EndTime, @ContactName, @PersonName, @Description, @Location)";
				DynamicParameters param = GetDynParams( appointment );
				CommandType command = CommandType.Text;
				insert = await DataConnection.Connection().ExecuteAsync( query, param, null, null, command );
				return insert;
			}
		}

		private static DynamicParameters GetDynParams( Appointment appointment )
		{
			var param = new DynamicParameters();
			param.Add( "@Type", appointment.Type );
			param.Add( "@StartTime", appointment.StartTime );
			param.Add( "@EndTime", appointment.EndTime );
			param.Add( "@ContactName", appointment.ContactName );
			param.Add( "@PersonName", appointment.PersonName );
			param.Add( "@Description", appointment.Description );
			param.Add( "@Location", appointment.Location );
			return param;
		}

		public async Task<int> UpdateAppointment( Appointment appointment )
		{
			using( DataConnection.Connection() )
			{
				var update = 0;
				string query = "Update Appointment SET Type=@Type, StartTime=@StartTime, EndTime=@EndTime, ContactName=@ContactName, PersonName=@PersonName, Description=@Description, Location=@Location WHERE Id = @Id";
				DynamicParameters param = GetDynParams( appointment );
				param.Add( "@Id", appointment.Id);
				CommandType command = CommandType.Text;
				update = await DataConnection.Connection().ExecuteAsync( query, param, null, null, command );
				return update;
			}
		}

		public async Task<int> DeleteAppointment( int Id )
		{
			using( DataConnection.Connection() )
			{
				var delete = 0;
				string query = "DELETE Appointment WHERE Id = @Id";
				var param = new DynamicParameters();
				param.Add( "@Id", Id );
				CommandType command = CommandType.Text;
				delete = await DataConnection.Connection().ExecuteAsync( query, param, null, null, command );
				return delete;
			}
		}
	}
}
