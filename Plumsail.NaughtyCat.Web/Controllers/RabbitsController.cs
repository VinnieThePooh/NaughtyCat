using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Microsoft.Extensions.Configuration;
using Plumsail.NaughtyCat.Common.Models;
using Plumsail.NaughtyCat.Core.Services.Abstract;
using Plumsail.NaughtyCat.Domain.Models;
using Plumsail.NaughtyCat.Domain.Models.ListViews;
using Plumsail.NaughtyCat.Domain.WebDto;

namespace Plumsail.NaughtyCat.Web.Controllers
{
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    public class RabbitsController : ControllerBase
    {
        private readonly GenericServiceBase<Rabbit, RabbitDto> _rabbitsService;
        private readonly IConfiguration _configuration;

        public RabbitsController(GenericServiceBase<Rabbit, RabbitDto> rabbitsService, IConfiguration configuration)
        {
            _rabbitsService = rabbitsService ?? throw new ArgumentNullException(nameof(rabbitsService));
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        [HttpGet]
        public async Task<PagingModel<RabbitDto>> GetRabbits([FromQuery] string listModel)
        {
            RabbitListModel model = null;
            if (!string.IsNullOrEmpty(listModel))
            {
                //todo: replace in .net core 3
                model = JsonConvert.DeserializeObject<RabbitListModel>(listModel);
            }

            var pNumber = model?.PageNumber ?? 1;

            var defaultPageSize = _configuration["PaginationSettings:DefaultPageSize"];
            if (!int.TryParse(defaultPageSize, out var pSize))
                pSize = 10;

            var data =  await _rabbitsService.GetByCondition(model?.Filter, pNumber, model?.PageSize ?? pSize);
            return data;
        }

        [HttpPost]
        public async Task<int> AddNewRabbit(RabbitDto rabbit)
        {
            var key = await _rabbitsService.Add(rabbit);
            return key;
        }
    }
}