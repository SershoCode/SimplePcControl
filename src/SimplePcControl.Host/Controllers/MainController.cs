using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SimplePcControl.Application.Commands;
using SimplePcControl.Host.Models;

namespace SimplePcControl.Host.Controllers
{
    /// <summary>
    /// Можно было бы сделать POST запрос на одну конечную точку и брать тип команды из тела, но пока сделано так,
    /// возможно еще изменится на данный вариант.
    /// </summary>
    [ApiController]
    [Route("api")]
    public class MainController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<MainController> _logger;

        public MainController(IMediator mediator, ILogger<MainController> logger)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpGet]
        [Route("monitorson")]
        public Task<ApiResponse> MonitorsOn() => ExecuteCommand(new MonitorsOnCommand());

        [HttpGet]
        [Route("monitorsoff")]
        public Task<ApiResponse> MonitorsOff() => ExecuteCommand(new MonitorsOffCommand());

        [HttpGet]
        [Route("computershutdown")]
        public Task<ApiResponse> ComputerShutdown() => ExecuteCommand(new ComputerShutdownCommand());

        [HttpGet]
        [Route("computerreboot")]
        public Task<ApiResponse> ComputerReboot() => ExecuteCommand(new ComputerRebootCommand());

        [HttpGet]
        [Route("computerlogout")]
        public Task<ApiResponse> ComputerLogout() => ExecuteCommand(new ComputerLogoutCommand());

        [HttpGet]
        [Route("computerblock")]
        public Task<ApiResponse> ComputerBlock() => ExecuteCommand(new ComputerBlockCommand());

        private async Task<ApiResponse> ExecuteCommand(object command, [CallerMemberName] string callerMemberName = default)
        {
            ApiResponse response;

            try
            {
                if (command is IRequest)
                {
                    await _mediator.Send(command);

                    response = new ApiResponse(true);
                }
                else
                {
                    response = new ApiResponse(false, $"Внутренняя ошибка. В метод ExecuteCommand можно передать только объект команды. Источник: {callerMemberName}");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Ошибка при попытке выполнения команды {callerMemberName}");

                response = new ApiResponse(false, ex.Message);
            }

            return response;
        }
    }
}
