using System;
using System.Collections.Generic;

namespace ESFGaivotas.Model.Repository
{
    public class ReportModel
    {
        // Report data
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int Version { get; set; }
        public double Weight { get; set; }

        // User data
        public int UserId { get; set; }
        public UserModel User { get; set; }

        // Debris data
        public ICollection<DebrisModel> Debris { get; set; }
    }
}
