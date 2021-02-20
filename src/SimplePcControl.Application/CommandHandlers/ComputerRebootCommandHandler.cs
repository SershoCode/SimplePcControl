using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SimplePcControl.Application.Commands;

namespace SimplePcControl.Application.CommandHandlers
{
    public class ComputerRebootCommandHandler : AsyncRequestHandler<ComputerRebootCommand>
    {
        protected override Task Handle(ComputerRebootCommand request, CancellationToken cancellationToken)
        {
            Process.Start("shutdown", "/r /t 0");

            return Task.CompletedTask;
        }
    }
}
