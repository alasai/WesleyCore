﻿using System;
using WesleyCore.Infrastruction.Core;
using WesleyCore.Infrastructure;
using WesleyCore.User.Domain;

namespace WesleyCore.User.Infrastructure.Repositories
{
    /// <summary>
    /// 用户仓储
    /// </summary>
    public class UserRepository : Repository<Domain.User, Guid, UserContext>, IUserRepository
    {
        /// <summary>
        ///
        /// </summary>
        private readonly UserContext _context;

        /// <summary>
        ///
        /// </summary>
        private readonly ITenantProvider _tenantProvider;

        /// <summary>
        ///
        /// </summary>
        /// <param name="context"></param>
        /// <param name="tenantProvider"></param>
        public UserRepository(UserContext context, ITenantProvider tenantProvider) : base(context, tenantProvider)
        {
            _context = context;
            _tenantProvider = tenantProvider;
        }
    }
}