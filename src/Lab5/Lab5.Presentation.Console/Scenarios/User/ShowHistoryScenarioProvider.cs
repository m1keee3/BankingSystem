﻿using Lab5.Application.Abstractions.Repositories;
using Lab5.Application.Contracts.Users;
using Lab5.Application.Models.Users;
using System.Diagnostics.CodeAnalysis;

namespace Lab5.Presentation.Console.Scenarios.User;

public class ShowHistoryScenarioProvider : IScenarioProvider
{
    private readonly ICurrentUserService _currentUser;
    private readonly IOperationRepository _operationRepository;

    public ShowHistoryScenarioProvider(
        IOperationRepository operationRepository,
        ICurrentUserService currentUser)
    {
        _operationRepository = operationRepository;
        _currentUser = currentUser;
    }

    public bool TryGetScenario([NotNullWhen(true)] out IScenario? scenario)
    {
        if (_currentUser.User is null || _currentUser.User.Role != UserRole.User)
        {
            scenario = null;
            return false;
        }

        scenario = new ShowHistoryScenario(_operationRepository, _currentUser);
        return true;
    }
}