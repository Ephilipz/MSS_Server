using Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Complaints
{
    public class Complaint
    {
        public int Id { get; }
        public ComplaintStatus Status { get; set; }
        public string ComplaintMessage { get; set; }
        public string ComplaintResponse { get; set; }
    }
}
