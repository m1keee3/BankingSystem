using Lab5.Application.Abstractions.Repositories;
using Lab5.Application.Contracts.Users;
using Lab5.Application.Models.Users;
using System.Diagnostics.CodeAnalysis;

namespace Lab5.Presentation.Console.Scenarios.AdminAbilities;

public class AddUserScenarioProvider : IScenarioProvider
{
    private readonly IUserRepository _userRepository;
    private readonly ICurrentUserService _currentUser;

    public AddUserScenarioProvider(
        IUserRepository userRepository,
        ICurrentUserService currentUser)
    {
        _userRepository = userRepository;
        _currentUser = currentUser;
    }

    public bool TryGetScenario([NotNullWhen(true)] out IScenario? scenario)
    {
        if (_currentUser.Role != UserRole.Admin)
        {
            scenario = null;
            return false;
        }

        scenario = new AddUserScenario(_userRepository);
        return true;
    }
}