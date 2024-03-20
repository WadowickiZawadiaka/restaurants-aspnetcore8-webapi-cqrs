using FluentValidation.TestHelper;
using Restaurants.Application.Restaurants.Commands.CreateRestaurant;

namespace Restaurants.Application.Tests.Restaurants.Commands.CreateRestaurant;

public class CreateRestaurantCommandValidatorTests
{
    [Fact()]
    public void Validator_ForValidCommand_ShouldNotHaveValidationErrors()
    {
        //arrange
        var command = new CreateRestaurantCommand()
        {
            Name = "Test",
            Category = "Croatian",
            ContactEmail = "test@test.com",
            PostalCode = "12-345",
            Description = "Test"
        };

        var validator = new CreateRestaurantCommandValidator();

        //act
        var result = validator.TestValidate(command);

        //assert
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Fact()]
    public void Validator_ForInvalidCommand_ShouldHaveValidationErrors()
    {
        //arrange
        var command = new CreateRestaurantCommand()
        {
            Name = "Te",
            Category = "Cro",
            ContactEmail = "@test.com",
            PostalCode = "12345",
            //Description = "Test"
        };

        var validator = new CreateRestaurantCommandValidator();

        //act
        var result = validator.TestValidate(command);

        //assert
        result.ShouldHaveValidationErrorFor(c => c.Name);
        result.ShouldHaveValidationErrorFor(c => c.Category);
        result.ShouldHaveValidationErrorFor(c => c.ContactEmail);
        result.ShouldHaveValidationErrorFor(c => c.PostalCode);
        result.ShouldHaveValidationErrorFor(c => c.Description);
    }

    [Theory()]
    [InlineData("Italian")]
    [InlineData("Polish")]
    [InlineData("Mexican")]
    [InlineData("Japanese")]
    [InlineData("Croatian")]
    public void Validator_ForValidCategory_ShouldNotHaveValidationErrorsForCategoryProperty(string category)
    {
        //arrange
        var validator = new CreateRestaurantCommandValidator();
        var command = new CreateRestaurantCommand() { Category = category };

        //act
        var result = validator.TestValidate(command);

        //assert
        result.ShouldNotHaveValidationErrorFor(c => c.Category);
    }

    [Theory()]
    [InlineData("420")]
    [InlineData("4-20")]
    [InlineData("111-11")]
    [InlineData("12-3 45")]
    public void Validator_ForInvalidPostalCode_ShouldHaveValidationErrorsForPostalCodeProperty(string postalCode)
    {
        //arrange
        var validator = new CreateRestaurantCommandValidator();
        var command = new CreateRestaurantCommand() { PostalCode = postalCode };

        //act
        var result = validator.TestValidate(command);

        //assert
        result.ShouldHaveValidationErrorFor(c => c.PostalCode);
    }
}
