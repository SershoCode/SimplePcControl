using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SimplePcControl.Application.Commands;

namespace SimplePcControl.Application.CommandHandlers
{
    public class ComputerShutdownCommandHandler : AsyncRequestHandler<ComputerShutdownCommand>
    {
        protected override Task Handle(ComputerShutdownCommand request, CancellationToken cancellationToken)
        {
            Process.Start("shutdown", "/s /t 0");

            return Task.CompletedTask;
        }
    }
}
