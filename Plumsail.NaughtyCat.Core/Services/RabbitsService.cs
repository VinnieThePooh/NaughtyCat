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

            var totalExpression = ExpressionsHelper.TrueExpression;

            var stringProps = new[]
            {
	            nameof(RabbitListModelFilter.Name),
	            nameof(RabbitListModelFilter.Color)
            };


            foreach (var strProp in stringProps)
            {
	            totalExpression = Expression.AndAlso(totalExpression,
		            ExpressionsHelper.Contains<Rabbit, RabbitListModelFilter>(strProp));
            }


            totalExpression = Expression.AndAlso(totalExpression,
	            ExpressionsHelper.IsLessThanOrEqual<Rabbit, RabbitListModelFilter>(
		            nameof(RabbitListModelFilter.UpdateDateTo), nameof(Rabbit.UpdateDate)));

            totalExpression = Expression.AndAlso(totalExpression,
	            ExpressionsHelper.IsGreaterThanOrEqual<Rabbit, RabbitListModelFilter>(
		            nameof(RabbitListModelFilter.UpdateDateFrom), nameof(Rabbit.UpdateDate)));


            totalExpression = Expression.AndAlso(totalExpression,
	            ExpressionsHelper.IsLessThanOrEqual<Rabbit, RabbitListModelFilter>(
		            nameof(RabbitListModelFilter.CreateDateTo), nameof(Rabbit.CreateDate)));

            totalExpression = Expression.AndAlso(totalExpression,
	            ExpressionsHelper.IsGreaterThanOrEqual<Rabbit, RabbitListModelFilter>(
		            nameof(RabbitListModelFilter.CreateDateFrom), nameof(Rabbit.CreateDate)));

            totalExpression = Expression.AndAlso(totalExpression,
	            ExpressionsHelper.Equals<Rabbit, RabbitListModelFilter>(nameof(RabbitListModelFilter.Delicacy)));

            totalExpression = Expression.AndAlso(totalExpression,
	            ExpressionsHelper.Equals<Rabbit, RabbitListModelFilter>(nameof(RabbitListModelFilter.Priority)));

            totalExpression = Expression.AndAlso(totalExpression,
	            ExpressionsHelper.Equals<Rabbit, RabbitListModelFilter>(nameof(RabbitListModelFilter.Age)));

            return ExpressionsHelper.ConvertToLambda<Rabbit>(totalExpression);
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
