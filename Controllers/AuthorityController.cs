
using AutoMapper;
using Microsoft.AspNetCore.Mvc;





namespace WorkFvApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorityController : ControllerBase
    {
        private readonly IMapper _mapper;

        private readonly IAuthorityService _authorityService;


        public AuthorityController(IMapper mapper, IAuthorityService authorityService)
        {

            _mapper = mapper;
            _authorityService = authorityService;
        }


    }
}








