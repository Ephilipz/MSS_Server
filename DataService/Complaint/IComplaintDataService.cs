using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataService.Complaint
{
    public interface IComplaintDataService
    {
        Task<Entities.Complaints.Complaint> PostComplaint(Entities.Complaints.Complaint complaint);
        Task<Entities.Complaints.Complaint> GetComplaint(int Id);
    }
}
