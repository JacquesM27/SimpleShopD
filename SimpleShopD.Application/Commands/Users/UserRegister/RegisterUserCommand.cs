﻿using SimpleShopD.Application.Commands.SharedDto;
using SimpleShopD.Application.Exceptions;
using SimpleShopD.Domain.Policies;
using SimpleShopD.Domain.Repositories;
using SimpleShopD.Domain.Shared.ValueObjects;
using SimpleShopD.Domain.Users;
using SimpleShopD.Shared.Abstractions.Commands;
using DomainVo = SimpleShopD.Domain.Shared.ValueObjects;

namespace SimpleShopD.Application.Commands.Users.UserRegister
{
    internal sealed class RegisterUserCommand : ICommandTResultHandler<RegisterUser, Guid>
    {
        private readonly IUserRepository _userRepository;
        private readonly IEnumerable<ICreateUserPolicy> _createUserPolicies;

        public RegisterUserCommand(IUserRepository userRepository, 
            IEnumerable<ICreateUserPolicy> createUserPolicies)
        {
            _userRepository = userRepository;
            _createUserPolicies = createUserPolicies;
        }

        public async Task<Guid> HandleAsync(RegisterUser command)
        {
            await VerifyPolicies(command);

            if (!await _userRepository.IsTheEmailUniqueAsync(command.Email))
                throw new NonUniqueEmailException(command.Email);

            User user = new(Guid.NewGuid(), MapFullname(command), command.Email, command.Password, command.UserRole);
            command.Addresses.ToList().ForEach(x => user.AddAddress(Guid.NewGuid(), x.Country, x.City, x.ZipCode, x.Street, x.BuildingNumber));

            await _userRepository.AddAsync(user);
            return user.Id;
        }

        private async Task VerifyPolicies(RegisterUser command)
        {
            User? author = null;
            if (command.AuthorId is not null)
                author = await _userRepository.GetAsync(command.AuthorId.Value);

            var policy = _createUserPolicies.Single(x => x.CanBeApplied(command.UserRole));
            if (!policy.CanCreate(author))
                throw new CannotCreateUserPolicyException(command.AuthorId.HasValue ? command.AuthorId!.Value.ToString() : string.Empty);
        }

        private static DomainVo.Fullname MapFullname(RegisterUser command)
            => new(command.Firstname, command.Lastname);
    }
}
