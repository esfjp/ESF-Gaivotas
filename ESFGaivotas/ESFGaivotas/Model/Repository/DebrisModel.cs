using ESFGaivotas.Enumerations;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ESFGaivotas.Model.Repository
{
    public class DebrisModel
    {
        public int Id { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public EDebrisType DebrisType { get; set; }

        public int ReportId { get; set; }
        public ReportModel Report { get; set; }
    }
}
