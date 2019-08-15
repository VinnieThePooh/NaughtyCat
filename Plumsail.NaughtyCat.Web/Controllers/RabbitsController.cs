using System;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Plumsail.NaughtyCat.Domain.WebDto;
using Plumsail.NaughtyCat.Web.Dto;

namespace Plumsail.NaughtyCat.Web.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class RabbitsController : ControllerBase
    {
        private readonly IMapper _mapper;

        public RabbitsController(IMapper mapper)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }


        public Task<RabbitDto> GetRabbits(int pageNumber, int pageSize, string filterValue)
        {
            return null;
        }
    }
}