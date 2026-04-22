using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace TrabajoPracticoPS.Application.UseCases.User.Commands
{
    public record CreateUserCommand(string UserName, string Email, string Password) : IRequest<int>
    {
    }
}
