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
        protected readonly IMapper _mapper;
        protected readonly IHttpContextAccessor _httpCtx;

        public BaseService(IMapper mapper, IHttpContextAccessor httpCtx)
        {
            _mapper = mapper;
            _httpCtx = httpCtx;
        }
    }
}
