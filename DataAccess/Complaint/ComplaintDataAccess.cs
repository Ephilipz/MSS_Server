using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Complaint
{
    public class ComplaintDataAccess : IComplaintDataAccess
    {
        private readonly ApplicationContext _context;
        public ComplaintDataAccess(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<Entities.Complaints.Complaint> GetComplaint(int Id)
        {
            //get the reservation from database
            Entities.Complaints.Complaint Complaint = await _context.Complaints.FindAsync(Id);
            return Complaint;
        }

        public async Task<Entities.Complaints.Complaint> PostComplaint(Entities.Complaints.Complaint complaint)
        {
            await _context.Complaints.AddAsync(complaint);
            return complaint;
        }
    }
}
