using DataAccess.Complaint;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataService.Complaint
{
    public class ComplaintDataService : IComplaintDataService
    {
        private IComplaintDataAccess _IComplaintDataAccess;

        public ComplaintDataService(IComplaintDataAccess iComplaintDataAccess)
        {
            _IComplaintDataAccess = iComplaintDataAccess;
        }
        public async Task<Entities.Complaints.Complaint> GetComplaint(int Id)
        {
            return await _IComplaintDataAccess.GetComplaint(Id);
        }

        public async Task<Entities.Complaints.Complaint> PostComplaint(Entities.Complaints.Complaint complaint)
        {
            return await _IComplaintDataAccess.PostComplaint(complaint);
        }
    }
}
