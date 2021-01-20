﻿using Grpc.Core;
using MediatR;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Ocelot.JwtAuthorize;
using System;
using System.Security.Claims;
using System.Threading.Tasks;
using WesleyCore.User.Application.Queries.Login;
using WesleyCore.User.Proto;
using WesleyUntity;

namespace WesleyCore.User.GrpcService
{
    /// <summary>
    ///
    /// </summary>
    public class LoginService : ILoginService.ILoginServiceBase
    {
        /// <summary>
        /// 读取appsetting.json配置
        /// </summary>
        private IConfiguration Configuration { get; set; }

        /// <summary>
        /// 令牌
        /// </summary>
        public ITokenBuilder TokenBuilder { get; }

        /// <summary>
        /// 中介
        /// </summary>
        private IMediator Mediator { get; set; }

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="mediator"></param>
        /// <param name="configuration"></param>
        /// <param name="tokenBuilder"></param>
        public LoginService(IMediator mediator, IConfiguration configuration, ITokenBuilder tokenBuilder)
        {
            Configuration = configuration;
            TokenBuilder = tokenBuilder;
            Mediator = mediator;
        }

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="input"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public override async Task<LoginResult> Login(LoginForm input, ServerCallContext context)
        {
            var hash = Configuration["Customization:PwdKey"];

            var user = await Mediator.Send(new LoginDto()
            {
                IpAddress = input.IpAddress,
                Password = EncryptUtil.AESEncrypt(input.Password, hash),
                PhoneNumber = input.PhoneNumber
            });
            var expired = DateTime.Now.AddMinutes(120);

            var claims = new Claim[] {
                        new Claim(ClaimTypes.Name, user.UserName),
                        new Claim(ClaimTypes.Sid ,user.UserId.ToString()),
                        new Claim ("Ip",input.IpAddress),
                        new Claim("TenantId",user.TenantId.ToString()),//租户
                        new Claim(ClaimTypes.MobilePhone,user.PhoneNumber)
                    };
            return new LoginResult() { Token = JsonConvert.SerializeObject(TokenBuilder.BuildJwtToken(claims, DateTime.UtcNow, expired)) };
        }
    }
}