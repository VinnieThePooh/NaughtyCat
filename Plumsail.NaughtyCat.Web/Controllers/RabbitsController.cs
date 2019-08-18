using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
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

        public RabbitsController(GenericServiceBase<Rabbit, RabbitDto> rabbitsService)
        {
            _rabbitsService = rabbitsService ?? throw new ArgumentNullException(nameof(rabbitsService));
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

            //todo: set default pagesize in config
            int pSize = model?.PageSize ?? 10;
            var data =  await _rabbitsService.GetByCondition(model?.Filter, pNumber, pSize);
            return data;
        }
    }
}