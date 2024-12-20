using Lab5.Application.Abstractions.Repositories;
using Lab5.Application.Contracts.Users;
using Lab5.Presentation.Console;

namespace Lab5.Tests.Moq.Scenarios;

public class MockPutMoneyScenario : IScenario
{
    private readonly ICurrentUserService _userService;
    private readonly IUserRepository _userRepository;
    private readonly decimal _amount;

    public MockPutMoneyScenario(ICurrentUserService userService, IUserRepository userRepository, decimal amount)
    {
        _userService = userService;
        _userRepository = userRepository;
        _amount = amount;
    }

    public string Name => "Add money";

    public void Run()
    {
        if (_userService.User is null)
        {
            throw new ArgumentNullException();
        }

        _userRepository.AddMoney(_userService.User.Id, _amount);
    }
}