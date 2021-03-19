using BusinessLayer.Bases;
using DAL;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Repositories
{
    public class AppointmentRepository : BaseRepository<Appointment>
    {
        private DbContext EC_DbContext;

        public AppointmentRepository(DbContext EC_DbContext) : base(EC_DbContext)
        {
            this.EC_DbContext = EC_DbContext;
        }
        #region CRUB

        public List<Appointment> GetAllAppointments()
        {
            return GetAll().ToList();
        }


        public Appointment GetAppointmentById(int id)
        {
            return GetFirstOrDefault(l => l.Id == id);
        }

        public bool InsertAppointment(Appointment appointment)
        {
            return Insert(appointment);
        }

        public void UpdateAppointment(Appointment appointment)
        {
            Update(appointment);
        }
        public void DeleteAppointment(int id)
        {
            Delete(id);
        }

        public bool CheckAppointmentExists(Appointment appointment)
        {
            return GetAny(l => l.Id == appointment.Id);
        }

        #endregion
    }
}
