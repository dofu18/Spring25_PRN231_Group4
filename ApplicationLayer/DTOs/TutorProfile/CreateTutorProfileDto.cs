using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLayer.DTOs.TutorProfile
{
    public class CreateTutorProfileDto
    {
        public string Fullname { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
    }
}
