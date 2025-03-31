using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NetBy.Business.Servicios;

namespace NetBy.Api.Controllers
{
    [ApiController]
    public abstract class ApplicationControllerBase : ControllerBase
    {
        protected readonly IMapper _mapper;
        protected readonly IUriService _uriService;

        protected ApplicationControllerBase(IMapper mapper,
                                            IUriService uriService)
        {
            this._mapper = mapper;
            this._uriService = uriService;
        }
    }
}
