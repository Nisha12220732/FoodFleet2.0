using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UserService.Controllers;
using UserService.Data;
using UserService.Models;
using Xunit;

namespace UserService.Tests;

public class UserControllerTests
{
    [Fact]
    public void Register_ShouldAddUser()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<UserDbContext>()
            .UseInMemoryDatabase("TestDatabase")
            .Options;

        using var context = new UserDbContext(options);
        var controller = new UserController(context);

        var user = new User
        {
            Name = "Nisha",
            Email = "nisha@test.com",
            Password = "12345"
        };

        // Act
        controller.Register(user);

        // Assert
        Assert.Single(context.Users);
        Assert.Equal("Nisha", context.Users.First().Name);
    }

    [Fact]
    public void GetById_ShouldReturnUser()
    {
        var options = new DbContextOptionsBuilder<UserDbContext>()
            .UseInMemoryDatabase("GetByIdTest")
            .Options;

        using var context = new UserDbContext(options);

        context.Users.Add(new User
        {
            UserId = 1,
            Name = "Nisha",
            Email = "nisha@test.com",
            Password = "12345"
        });

        context.SaveChanges();

        var controller = new UserController(context);

        var result = controller.GetById(1);

        var okResult = Assert.IsType<OkObjectResult>(result);
        var user = Assert.IsType<User>(okResult.Value);

        Assert.Equal("Nisha", user.Name);
    }


    [Fact]
    public void GetById_ShouldReturnNotFound_WhenUserDoesNotExist()
    {
        var options = new DbContextOptionsBuilder<UserDbContext>()
            .UseInMemoryDatabase("GetMissingUserTest")
            .Options;

        using var context = new UserDbContext(options);

        var controller = new UserController(context);

        var result = controller.GetById(99);

        Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public void Delete_ShouldRemoveUser()
    {
        var options = new DbContextOptionsBuilder<UserDbContext>()
            .UseInMemoryDatabase("DeleteTest")
            .Options;

        using var context = new UserDbContext(options);

        context.Users.Add(new User
        {
            UserId = 1,
            Name = "Nisha",
            Email = "nisha@test.com",
            Password = "12345"
        });

        context.SaveChanges();

        var controller = new UserController(context);

        controller.Delete(1);

        Assert.Empty(context.Users);
    }


}