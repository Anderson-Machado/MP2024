using System.Collections.ObjectModel;

namespace MP.Tests.UnitTest.Application.Models
{
    public class ServiceResultTests
    {
        private readonly Fixture _fixture = new();

        #region CreateSuccess Method

        [Fact]
        public void CreateSucess_method_should_create_success_object_without_notification()
        {
            // arrange
            CustomResult value = new();

            // act
            ServiceResult<CustomResult> result = ServiceResult<CustomResult>.CreateSuccess(value);

            // assert
            result.Value.Should().NotBeNull();
            result.IsValid.Should().BeTrue();
            result.ResultType.Should().Be(ServiceResultTypes.Success);
            result.Notifications.Should().BeEmpty();
        }

        [Fact]
        public void CreateSucess_method_must_throw_exception_when_receiving_null_object()
        {
            // arrange

            // act
            Func<ServiceResult<CustomResult>> act = () => ServiceResult<CustomResult>.CreateSuccess(default!);

            // assert
            act.Should().ThrowExactly<ArgumentNullException>();
        }

        #endregion

        #region CreateNotFound Method

        [Fact]
        public void CreateNotFound_method_should_create_notfound_object()
        {
            // arrange

            // act
            ServiceResult<CustomResult> result = ServiceResult<CustomResult>.CreateNotFound();

            // assert
            result.Value.Should().BeNull();
            result.IsValid.Should().BeTrue();
            result.ResultType.Should().Be(ServiceResultTypes.NotFound);
            result.Notifications.Should().BeEmpty();
        }

        #endregion

        #region CreateUnprocessable Method

        [Fact]
        public void CreateUnprocessable_method_should_create_unprocessable_object()
        {
            // arrange

            // act
            ServiceResult<CustomResult> result = ServiceResult<CustomResult>.CreateUnprocessable();

            // assert
            result.Value.Should().BeNull();
            result.IsValid.Should().BeTrue();
            result.ResultType.Should().Be(ServiceResultTypes.Unprocessable);
            result.Notifications.Should().BeEmpty();
        }

        #endregion

        #region CreateWithErrors Method

        [Fact]
        public void CreateWithErrors_method_should_create_error_object_with_notifications()
        {
            // arrange
            IReadOnlyCollection<Notification> notifications = new ReadOnlyCollection<Notification>(_fixture.CreateMany<Notification>(2).ToList());

            // act
            ServiceResult<CustomResult> result = ServiceResult<CustomResult>.CreateWithErrors(notifications);

            // assert
            result.Value.Should().BeNull();
            result.IsValid.Should().BeFalse();
            result.ResultType.Should().Be(ServiceResultTypes.Error);
            result.Notifications.Should().HaveCount(2);
        }

        [Fact]
        public void CreateWithErrors_method_must_throw_exception_when_receiving_null_object()
        {
            // arrange

            // act
            Func<ServiceResult<CustomResult>> act = () => ServiceResult<CustomResult>.CreateWithErrors(default!);

            // assert
            act.Should().ThrowExactly<ArgumentNullException>();
        }

        #endregion

        #region CreateWithError Method

        [Fact]
        public void CreateWithError_method_should_create_error_object_with_notification()
        {
            // arrange
            Notification notification = _fixture.Create<Notification>();

            // act
            ServiceResult<CustomResult> result = ServiceResult<CustomResult>.CreateWithError(notification);

            // assert
            result.Value.Should().BeNull();
            result.IsValid.Should().BeFalse();
            result.ResultType.Should().Be(ServiceResultTypes.Error);
            result.Notifications.Should().HaveCount(1);
        }

        [Fact]
        public void CreateWithError_method_must_throw_exception_when_receiving_null_object()
        {
            // arrange

            // act
            Func<ServiceResult<CustomResult>> act = () => ServiceResult<CustomResult>.CreateWithError(default!);

            // assert
            act.Should().ThrowExactly<ArgumentNullException>();
        }

        #endregion
    }

    internal class CustomResult { }
}