using Lab5.Application.Users;
using Lab5.Tests.Moq;
using Lab5.Tests.Moq.Scenarios;
using Xunit;

namespace Lab5.Tests;

public class AllTests
{
    [Fact]
    public void InsertUser_LoginUser_PutMoney_MoneyShouldBeOnAccount()
    {
        var mockUserRepository = new MockUserRepository();
        mockUserRepository.InsertUser(1, 123);
        var curUser = new CurrentUserManager();
        var userService = new UserService(mockUserRepository, curUser);
        userService.Login(1, 123);

        new MockPutMoneyScenario(curUser, mockUserRepository, 1000).Run();

        Assert.True(mockUserRepository.FindUserById(1) is { Balance: 1000 });
    }

    [Fact]
    public void InsertUser_LoginUser_RemoveMoney_RightMoneyAmountShouldBeOnAccount()
    {
        var mockUserRepository = new MockUserRepository();
        mockUserRepository.InsertUser(1, 123);
        var curUser = new CurrentUserManager();
        var userService = new UserService(mockUserRepository, curUser);
        userService.Login(1, 123);

        new MockPutMoneyScenario(curUser, mockUserRepository, 1000).Run();
        new MockRemoveMoneyScenario(curUser, mockUserRepository, 500).Run();

        Assert.True(mockUserRepository.FindUserById(1) is { Balance: 500 });
    }
}