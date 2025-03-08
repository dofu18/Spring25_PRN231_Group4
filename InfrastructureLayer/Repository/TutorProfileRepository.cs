using DomainLayer.Entities;
using InfrastructureLayer.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfrastructureLayer.Repository
{
    public class TutorProfileRepository : GenericRepository<TutorProfile>, ITutorProfileRepository
    {
        private readonly TutoringKidDbContext _tutoringKidDbContext;

        public TutorProfileRepository(TutoringKidDbContext tutoringKidDbContext) : base(tutoringKidDbContext)
        {
            _tutoringKidDbContext = tutoringKidDbContext;
        }
    }
}
