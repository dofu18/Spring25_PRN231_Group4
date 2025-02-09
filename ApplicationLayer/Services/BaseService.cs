using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using InfrastructureLayer;
using Microsoft.AspNetCore.Http;

namespace ApplicationLayer.Services
{
    public abstract class BaseService
    {
        protected readonly TutoringKidDbContext _dbContext;
        protected readonly IMapper _mapper;
        protected readonly IHttpContextAccessor _httpCtx;

        public BaseService(TutoringKidDbContext dbContext, IMapper mapper, IHttpContextAccessor httpCtx)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _httpCtx = httpCtx;
        }
    }
}
