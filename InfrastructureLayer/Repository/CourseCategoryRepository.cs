using DomainLayer.Entities;
using InfrastructureLayer.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfrastructureLayer.Repository
{
    public class CourseCategoryRepository : GenericRepository<CourseCategory>, ICourseCategoryRepository
    {
        private readonly TutoringKidDbContext _tutoringKidDbContext;
        public CourseCategoryRepository(TutoringKidDbContext tutoringKidDbContext) : base(tutoringKidDbContext)
        {
            _tutoringKidDbContext = tutoringKidDbContext;
        }
    }
}
