using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SimplePcControl.Application.Commands;

namespace SimplePcControl.Application.CommandHandlers
{
    public class ComputerLogoutCommandHandler : AsyncRequestHandler<ComputerLogoutCommand>
    {
        [DllImport("user32")]
        private static extern bool ExitWindowsEx(uint uFlags, uint dwReason);

        protected override Task Handle(ComputerLogoutCommand request, CancellationToken cancellationToken)
        {
            ExitWindowsEx(0, 0);

            return Task.CompletedTask;
        }
    }
}
