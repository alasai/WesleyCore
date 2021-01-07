﻿using DotNetCore.CAP;
using WesleyCore.Infrastructure.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Toolbelt.ComponentModel.DataAnnotations;
using WesleyCore.Configuration;
using WesleyCore.Domain.OrderAggregate;

namespace WesleyCore.EntityFrameworkCore
{
    /// <summary>
    /// 实体映射
    /// </summary>
    public class DomainContext : EFContext
    {
        public DomainContext(DbContextOptions options, IMediator mediator, ICapPublisher capBus) : base(options, mediator, capBus)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            ////租户
            var tenantIdPropertyName = "TenantId";
            //软删除
            var deletePropertyName = "IsDeleted";
            foreach (var item in modelBuilder.Model.GetEntityTypes())
            {
                var type = item.ClrType;
                var props = type.GetProperties().Where(c => c.IsDefined(typeof(DecimalPrecisionAttribute), true)).ToArray();
                foreach (var p in props)
                {
                    var precis = p.GetCustomAttribute<DecimalPrecisionAttribute>();
                    modelBuilder.Entity(type).Property(p.Name).HasColumnType($"decimal({precis.Precision},{precis.Scale})");
                }
                //实现IsDeleted==false 过滤
                if (type.GetProperty(deletePropertyName) != null)
                {
                    var parameter = Expression.Parameter(type, "e");
                    var body = Expression.Equal(
                        Expression.Call(typeof(EF), nameof(EF.Property), new[] { typeof(bool) }, parameter, Expression.Constant(deletePropertyName)),
                        Expression.Constant(false));
                    modelBuilder.Entity(type).HasQueryFilter(Expression.Lambda(body, parameter));
                }
                if (type.GetProperty(tenantIdPropertyName) != null)
                {
                    //查询过滤租户
                    var parameter = Expression.Parameter(type, "e");
                    var body = Expression.Equal(
                        Expression.Call(typeof(EF), nameof(EF.Property), new[] { typeof(int) }, parameter, Expression.Constant(tenantIdPropertyName)),
                        Expression.Constant(false));
                    modelBuilder.Entity(type).HasQueryFilter(Expression.Lambda(body, parameter));
                }
            }
            modelBuilder.Entity<Order>().OwnsOne(typeof(Address), "Address");

            base.OnModelCreating(modelBuilder);
            //创建索引
            modelBuilder.BuildIndexesFromAnnotations();
        }

        /// <summary>
        /// 获取当前用户TenantId
        /// </summary>
        /// <returns></returns>
        private Guid GetTenantId()
        {
            return new Guid();
        }

        public virtual DbSet<Order> Order { get; set; }
    }
}