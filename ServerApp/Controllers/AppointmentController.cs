using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ServerApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ServerApp.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class AppointmentController : ControllerBase
    {
        Appointment appointment = new Appointment();


        // GET: api/Appointment
        [HttpGet]
        public async Task<JsonResult> Appointments()
        {
            var rs = await appointment.Appointments();
            return new JsonResult( rs );
        }

        // GET: api/Appointment/5
        [HttpGet( "{id}", Name = "Get" )]
        public async Task<JsonResult> Get( int id )
        {
            var rs = await appointment.GetAppointment( id );
            if( rs == null )
            {
                return new JsonResult( new Notice( 1, "Get error" ) );
            }
            return new JsonResult( rs );
        }

        // POST: api/Appointment
        [HttpPost]
        public async Task<JsonResult> AddAppointment( [FromBody] Appointment appointmentInsert )
        {
            var rs = await appointment.AddAppointment( appointmentInsert );
            if( rs == 0 )
            {
                return new JsonResult( new Notice( 1, "Insert error" ) );
            }
            else{
                return new JsonResult( new Notice( 0, "Inserted" ) );
            }
        }

        // PUT: api/Appointment/5
        [HttpPut]
        public async Task<JsonResult> UpdateAppointment( [FromBody] Appointment appointmentUpdate)
        {
            var rs = await appointment.UpdateAppointment( appointmentUpdate);
            if( rs == 0 )
            {
                return new JsonResult( new Notice( 1, "Update error" ) );
            }
            else
            {
                return new JsonResult( new Notice( 0, "Updated" ) );
            }
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public async Task<JsonResult> DeleteAppointment( int id )
        {
            var rs = await appointment.DeleteAppointment( id );
            if( rs == 0 )
            {
                return new JsonResult( new Notice( 1, "Delete error" ) );
            }
            else
            {
                return new JsonResult( new Notice( 0, "Deleted" ) );
            }
        }
    }
}
