using Lab5.Application.Contracts.Users;
using Lab5.Application.Models.Users;
using System.Diagnostics.CodeAnalysis;

namespace Lab5.Presentation.Console.Scenarios.User;

public class ShowBalanceScenarioProvider : IScenarioProvider
{
    private readonly ICurrentUserService _currentUser;

    public ShowBalanceScenarioProvider(
        ICurrentUserService currentUser)
    {
        _currentUser = currentUser;
    }

    public bool TryGetScenario([NotNullWhen(true)] out IScenario? scenario)
    {
        if (_currentUser.User is null || _currentUser.User.Role != UserRole.User)
        {
            scenario = null;
            return false;
        }

        scenario = new ShowBalanceScenario(_currentUser);
        return true;
    }
}