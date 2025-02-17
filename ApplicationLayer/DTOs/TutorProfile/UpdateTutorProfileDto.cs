using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLayer.DTOs.TutorProfile
{
    public class UpdateTutorProfileDto
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public string Meta { get; set; }
    }
}
