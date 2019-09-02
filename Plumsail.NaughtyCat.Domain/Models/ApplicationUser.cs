using System;
using Microsoft.AspNetCore.Identity;
using Plumsail.NaughtyCat.Common.Interfaces;

namespace Plumsail.NaughtyCat.Domain.Models
{
    public class ApplicationUser: IdentityUser<int>, IHasKey<int>, IAuditable
    {
        public DateTime? CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
    }
}
