using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SimplePcControl.Application.Commands;

namespace SimplePcControl.Application.CommandHandlers
{
    public class ComputerBlockCommandHandler : AsyncRequestHandler<ComputerBlockCommand>
    {
        [DllImport("user32")]
        private static extern void LockWorkStation();

        protected override Task Handle(ComputerBlockCommand request, CancellationToken cancellationToken)
        {
            LockWorkStation();

            return Task.CompletedTask;
        }
    }
}
