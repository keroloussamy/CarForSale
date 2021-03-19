using BusinessLayer.Bases;
using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.AppService
{
    public class AppointmentAppService : AppServiceBase
    {
        public List<Appointment> GetAllAppointment()
        {

            return TheUnitOfWork.Appointment.GetAllAppointments();
        }
        public List<Appointment> GetAllAppointmentWhere(string dealerId)
        {

            return TheUnitOfWork.Appointment.GetAllWhere(x => x.DealerId == dealerId);
        }
        public Appointment GetAppointment(int id)
        {
            return TheUnitOfWork.Appointment.GetAppointmentById(id);
        }


        public bool SaveNewAppointment(Appointment appointment)
        {
            bool result = false;
            if (TheUnitOfWork.Appointment.Insert(appointment))
            {
                result = TheUnitOfWork.Commit() > new int();
            }
            return result;
        }


        public bool UpdateAppointment(Appointment appointment)
        {
            TheUnitOfWork.Appointment.Update(appointment);
            TheUnitOfWork.Commit();

            return true;
        }


        public bool DeleteAppointment(int id)
        {
            bool result = false;

            TheUnitOfWork.Appointment.Delete(id);
            result = TheUnitOfWork.Commit() > new int();

            return result;
        }

        public bool CheckAppointmentExists(Appointment appointment)
        {
            return TheUnitOfWork.Appointment.CheckAppointmentExists(appointment);
        }
    }
}
