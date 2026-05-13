using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrabajoPracticoPS.Application.DTOs;

namespace TrabajoPracticoPS.Application.UseCases.User.Queries
{
    public record GetUserByIdQuery(int Id) : IRequest<UserResponseDto>
    {
    }
}
