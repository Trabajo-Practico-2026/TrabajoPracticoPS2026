using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrabajoPracticoPS.Application.DTOs;
using TrabajoPracticoPS.Application.Interfaces;
using TrabajoPracticoPS.Application.UseCases.User.Queries;
using TrabajoPracticoPS.Domain.Entities;
using TrabajoPracticoPS.Domain.Exceptions;

namespace TrabajoPracticoPS.Application.UseCases.User.Handlers
{
    public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, UserResponseDto>
    {
        private readonly IUserRepository _repository;
        public GetUserByIdQueryHandler(IUserRepository repository)
        {
            _repository = repository;
        }

        public async Task<UserResponseDto> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            var user = await _repository.GetUserById(request.Id);
            if (user == null) throw new NotFoundException($"No se encontró el usuario con ID {request.Id}.");

            return new UserResponseDto
            {
                Name = user.Name,
                Email = user.Email
            };
        }
    }
}
