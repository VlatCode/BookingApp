using HostelBookingSystem.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HostelBookingSystem.Services.Interfaces
{
    public interface IHostelService
    {
        List<HostelDto> GetAllHostels(); 
        HostelDto GetById(int id);
        void AddHostel(AddHostelDto hostel);
    }
}
