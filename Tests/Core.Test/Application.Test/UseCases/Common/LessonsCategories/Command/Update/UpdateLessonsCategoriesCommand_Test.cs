using Application.UseCases.Common.LessonsCategories.Command.Update;
using Application.UseCases.Membership.Permission.Command.Update;

namespace Application.Test.UseCases.Common.LessonsCategories.Command.Update
{
    public class UpdateLessonsCategoriesCommand_Test : UnitTestBase<Common_LessonsCategories, ILessonsCategoriesRepo>
    {
        private readonly UpdateLessonsCategoriesCommand _createCommand;
        private readonly UpdateLessonsCategoriesCommandHandler _handler;


        public UpdateLessonsCategoriesCommand_Test(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
        {

            _createCommand = new UpdateLessonsCategoriesCommand
            {
                IsActive = true,
                Name = "Test",
                Title = "test"
            };

            _handler = new UpdateLessonsCategoriesCommandHandler(_repoMock.Object, _mapper, _cacheManager.Object);
        }
        [Fact]
        public async Task UpdateLessonsCategoriesCommand_NullExcption_ResultTest()
        {
            var exception = await Assert.ThrowsAsync<BadRequestException>(() => _handler.Handle(null, CancellationToken.None));

            _repoMock.Verify(p => p.Insert(It.IsAny<Common_LessonsCategories>()), Times.Never);
            Assert.Equal(string.Format(CommonMessage.NullException, "request"), exception.Message);
        }
        [Fact]
        public async Task UpdateLessonsCategoriesCommand_ValidationExcption_ResultTest()
        {
            var validation = new UpdateLessonsCategoriesCommandValidator();
            UpdateLessonsCategoriesCommand command = new UpdateLessonsCategoriesCommand
            {
                Name = "T",
                Title = "U"
            };
            var failur = await validation.ValidateAsync(command);

            
            Assert.False(failur.IsValid);
            Assert.True(failur.Errors.Count > 0);
        }
        [Fact]
        public async Task UpdateLessonsCategoriesCommand_SuccessResult_ResultTest()
        {
            _repoMock.Setup(p => p.Insert(It.IsAny<Common_LessonsCategories>()))
                .Returns(Task.FromResult(new Common_LessonsCategories
                {
                    IsActive = true,
                    Name = "UpTest",
                    Title = "Uptest"
                }
                ));
            CommandResponse<Common_LessonsCategories> result = await _handler.Handle(_createCommand, CancellationToken.None);

            _repoMock.Verify(p => p.Update(It.IsAny<Common_LessonsCategories>()), Times.Once);
            Assert.True(result.IsSuccess);
        }
    }
}
