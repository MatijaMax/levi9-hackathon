namespace FibaTest
{
    public class FibaTest
    {
        [Fact]
        public void Get_all()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var controller

            // Act
            var result = ((ObjectResult)controller.GetAll(0, 0).Result)?.Value as PagedResult<TourPreferenceDto>;

            // Assert
            result.ShouldNotBeNull();
            result.Results.Count.ShouldBe(3);
            result.TotalCount.ShouldBe(3);
        }

    }
}