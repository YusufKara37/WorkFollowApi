using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using WorkFvApi.DTO.AuthorityDTO;


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








