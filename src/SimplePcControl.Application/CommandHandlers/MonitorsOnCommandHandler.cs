using System;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SimplePcControl.Application.Commands;

namespace SimplePcControl.Application.CommandHandlers
{
    public class MonitorsOnCommandHandler : AsyncRequestHandler<MonitorsOnCommand>
    {
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern void mouse_event(int dwFlags, int dx, int dy, int dwData, UIntPtr dwExtraInfo);

        protected override Task Handle(MonitorsOnCommand request, CancellationToken cancellationToken)
        {
            mouse_event(0x0001, 0, 1, 0, UIntPtr.Zero);

            return Task.CompletedTask;
        }
    }
}
