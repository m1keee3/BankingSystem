﻿using Lab5.Application.Abstractions.Repositories;
using Lab5.Application.Contracts.Users;
using Spectre.Console;

namespace Lab5.Presentation.Console.Scenarios.User;

public class PutMoneyScenario : IScenario
{
    private readonly ICurrentUserService _userService;
    private readonly IUserRepository _userRepository;

    public PutMoneyScenario(ICurrentUserService userService, IUserRepository userRepository)
    {
        _userService = userService;
        _userRepository = userRepository;
    }

    public string Name => "Add money";

    public void Run()
    {
        decimal amount = AnsiConsole.Ask<decimal>("Enter amount");

        if (_userService.User is null)
        {
            throw new ArgumentNullException();
        }

        if (amount < 0)
        {
            AnsiConsole.WriteLine("Amount can not be below zero");
        }
        else
        {
            _userRepository.AddMoney(_userService.User.Id, amount);
        }

        AnsiConsole.Ask<string>("Ok");
        AnsiConsole.Clear();
    }
}