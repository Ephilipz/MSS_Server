using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Complaint
{
    public interface IComplaintDataAccess
    {
        Task<Entities.Complaints.Complaint> PostComplaint(Entities.Complaints.Complaint complaint);
        Task<Entities.Complaints.Complaint> GetComplaint(int Id);
    }
}
