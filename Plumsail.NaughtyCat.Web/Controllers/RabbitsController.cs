using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Plumsail.NaughtyCat.Core.Services.Abstract;
using Plumsail.NaughtyCat.Domain.Models;
using Plumsail.NaughtyCat.Domain.Models.ListViews;
using Plumsail.NaughtyCat.Domain.WebDto;

namespace Plumsail.NaughtyCat.Web.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class RabbitsController : ControllerBase
    {
        private readonly GenericServiceBase<Rabbit, RabbitDto> _rabbitsService;

        public RabbitsController(GenericServiceBase<Rabbit, RabbitDto> rabbitsService)
        {
            _rabbitsService = rabbitsService ?? throw new ArgumentNullException(nameof(rabbitsService));
        }

        public Task<List<RabbitDto>> GetRabbits([FromBody] RabbitListView viewModel)
        {
            return _rabbitsService.GetByCondition(viewModel.Filter, viewModel.PageNumber, viewModel.PageSize);
        }
    }
}