using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using Plumsail.NaughtyCat.Common.Interfaces;
using Plumsail.NaughtyCat.Core.Services.Abstract;
using Plumsail.NaughtyCat.DataAccess.Providers.Abstract;
using Plumsail.NaughtyCat.Domain.Models;
using Plumsail.NaughtyCat.Domain.WebDto;

namespace Plumsail.NaughtyCat.Core.Services
{
    public class RabbitsService: GenericServiceBase<Rabbit, RabbitDto>, IAuditor
    {
        private readonly IAuditor _auditor;

        public RabbitsService(IGenericProvider<Rabbit, int> dataProvider, IMapper mapper) : base(dataProvider, mapper)
        {
            _auditor = this;
        }

        protected override Expression<Func<Rabbit, bool>> GenerateExpression<RabbitListViewModelFilter>(RabbitListViewModelFilter filter)
        {
            if (filter == null)
                return null;


            return null;
        }

        public override Task<int> Add(RabbitDto dto)
        {
            _auditor.AuditCreateEvent(dto);
            return base.Add(dto);
        }

        public override Task Update(RabbitDto dto)
        {
            _auditor.AuditUpdateEvent(dto);
            return base.Update(dto);
        }

        #region Implementation details

        void IAuditor.AuditUpdateEvent(IAuditable entity)
        {
            entity.UpdateDate = DateTime.Now;
        }

        void IAuditor.AuditCreateEvent(IAuditable entity)
        {
            entity.CreateDate = DateTime.Now;
        }

        #endregion
    }
}
