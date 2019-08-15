using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
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

        [HttpGet]
        public Task<List<RabbitDto>> GetRabbits(string listModel)
        {
            RabbitListModel model = null;
            if (!string.IsNullOrEmpty(listModel))
            {
                //todo: replace in .net core 3
                model = JsonConvert.DeserializeObject<RabbitListModel>(listModel);
            }
            return _rabbitsService.GetByCondition(model?.Filter, model?.PageNumber, model?.PageSize);
        }
    }
}