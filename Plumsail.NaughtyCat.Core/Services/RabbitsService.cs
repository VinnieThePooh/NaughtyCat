using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using Plumsail.NaughtyCat.Common.Helpers;
using Plumsail.NaughtyCat.Common.Interfaces;
using Plumsail.NaughtyCat.Core.Services.Abstract;
using Plumsail.NaughtyCat.DataAccess.Providers.Abstract;
using Plumsail.NaughtyCat.Domain.Models;
using Plumsail.NaughtyCat.Domain.Models.ListViews;
using Plumsail.NaughtyCat.Domain.WebDto;

namespace Plumsail.NaughtyCat.Core.Services
{
    public class RabbitsService: GenericServiceBase<Rabbit, RabbitDto, RabbitListModelFilter>, IAuditor
    {
        private readonly IAuditor _auditor;

        public RabbitsService(IGenericProvider<Rabbit, int> dataProvider, IMapper mapper) : base(dataProvider, mapper)
        {
            _auditor = this;
        }

        protected override Expression<Func<Rabbit, bool>> GenerateExpression(RabbitListModelFilter filter)
        {
            if (filter == null)
                return null;

            // todo: generate expression manually
            Expression<Func<Rabbit, bool>> fExpression = (r) => (string.IsNullOrEmpty(filter.Name) ||
                                                        string.IsNullOrEmpty(r.Name) && r.Name.Contains(filter.Name, StringComparison.InvariantCultureIgnoreCase)) &&
                                                       (string.IsNullOrEmpty(filter.Color) || string.IsNullOrEmpty(r.Name) &&  r.Color.Contains(filter.Color, StringComparison.InvariantCultureIgnoreCase)) &&
                                                       (filter.Delicacy == null || filter.Delicacy.Equals(r.IdRabbitDelicacy)) &&
                                                       (filter.Priority == null || filter.Priority.Equals(r.IdRabbitPriority)) &&
                                                       (filter.CreateDateFrom == null || r.CreateDate >= filter.CreateDateFrom) &&
                                                       (filter.CreateDateTo == null || r.CreateDate <= filter.CreateDateTo) &&
                                                       (filter.UpdateDateFrom == null || r.UpdateDate >= filter.UpdateDateFrom) &&
                                                       (filter.UpdateDateTo == null || r.UpdateDate <= filter.UpdateDateTo);

            return fExpression;
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
