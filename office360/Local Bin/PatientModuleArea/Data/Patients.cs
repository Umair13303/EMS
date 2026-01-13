using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace office360.Areas.PatientModuleArea.Data
{
    public class Patients
    {
        public class PatientsModuleSession
        {
            public string AppointmentCode { get; set; }
            public string PatientName { get; set; }
            public string StartDate { get; set; }
            public string EndDate { get; set; }

        }
        public class PatientsSearchParameters
        {
            public string StartDate { get; set; }
            public string EndDate { get; set; }
        }
    }
}