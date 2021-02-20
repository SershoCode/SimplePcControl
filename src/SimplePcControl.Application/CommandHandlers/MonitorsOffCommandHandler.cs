using System;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SimplePcControl.Application.Commands;

namespace SimplePcControl.Application.CommandHandlers
{
    public class MonitorsOffCommandHandler : AsyncRequestHandler<MonitorsOffCommand>
    {
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern int SendMessage(IntPtr hWnd, int wMsg, IntPtr wParam, IntPtr lParam);

        protected override Task Handle(MonitorsOffCommand request, CancellationToken cancellationToken)
        {
            SendMessage(new IntPtr(0xFFFF), 0x0112, (IntPtr)0xF170, (IntPtr)2);

            return Task.CompletedTask;
        }
    }
}
