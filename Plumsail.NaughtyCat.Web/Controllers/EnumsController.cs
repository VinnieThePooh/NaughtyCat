using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Plumsail.NaughtyCat.Common.Extensions;
using Plumsail.NaughtyCat.Domain.Enums;
using Plumsail.NaughtyCat.Domain.WebDto;

namespace Plumsail.NaughtyCat.Web.Controllers
{

    // todo:  add several enums getting within one response
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class EnumsController : ControllerBase
    {

        [HttpGet, Route("priorities")]
        public List<EnumItemDto> GetPriorities() => GetEnumValues<PriorityEnum>();


        [HttpGet, Route("delicacies")]
        public List<EnumItemDto> GetDelicacies() => GetEnumValues<DelicacyEnum>();


        #region Implementation details

        private List<EnumItemDto> GetEnumValues<TEnum>() where TEnum : struct, IConvertible
        {
            var list = new List<EnumItemDto>();

            foreach (var val in Enum.GetValues(typeof(TEnum)).OfType<TEnum>())
            {
                list.Add(new EnumItemDto
                {
                    Value = val.ToInt32(CultureInfo.InvariantCulture),
                    Description = val.GetEnumDescription()
                });
            }

            return list;
        }

        #endregion
    }
}