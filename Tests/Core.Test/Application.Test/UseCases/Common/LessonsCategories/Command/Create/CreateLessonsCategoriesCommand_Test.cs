using Application.UseCases.Common.LessonsCategories.Command.Create;
using Application.UseCases.Membership.Permission.Command.Create;

namespace Application.Test.UseCases.Common.LessonsCategories.Command.Create
{
    public class CreateLessonsCategoriesCommand_Test : UnitTestBase<Common_LessonsCategories, ILessonsCategoriesRepo>
    {
        private readonly CreateLessonsCategoriesCommand _createCommand;
        private readonly CreateLessonsCategoriesCommandHandler _handler;


        public CreateLessonsCategoriesCommand_Test(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
        {

            _createCommand = new CreateLessonsCategoriesCommand
            {
                IsActive = true,
                Name = "Test",
                Title = "test"
            };

            _handler = new CreateLessonsCategoriesCommandHandler(_repoMock.Object, _mapper, _cacheManager.Object);
        }
        [Fact]
        public async Task CreateLessonsCategoriesCommand_NullExcption_ResultTest()
        {
            var exception = await Assert.ThrowsAsync<BadRequestException>(() => _handler.Handle(null, CancellationToken.None));

            _repoMock.Verify(p => p.Insert(It.IsAny<Common_LessonsCategories>()), Times.Never);
            Assert.Equal(string.Format(CommonMessage.NullException, "request"), exception.Message);
        }
        [Fact]
        public async Task CreateLessonsCategoriesCommand_ValidationExcption_ResultTest()
        {
            var validation = new CreateLessonsCategoriesCommandValidator();
            CreateLessonsCategoriesCommand command = new CreateLessonsCategoriesCommand
            {
                Name = "T",
                Title = "test"
            };
            var failur = await validation.ValidateAsync(command);

            
            Assert.False(failur.IsValid);
            Assert.True(failur.Errors.Count > 0);
        }
        [Fact]
        public async Task CreateLessonsCategoriesCommand_SuccessResult_ResultTest()
        {
            _repoMock.Setup(p => p.Insert(It.IsAny<Common_LessonsCategories>()))
                .Returns(Task.FromResult(new Common_LessonsCategories
                {
                    IsActive = true,
                    Name = "Test",
                    Title = "test"
                }
                ));
            CommandResponse<Common_LessonsCategories> result = await _handler.Handle(_createCommand, CancellationToken.None);

            _repoMock.Verify(p => p.Insert(It.IsAny<Common_LessonsCategories>()), Times.Once);
            Assert.True(result.IsSuccess);
        }
    }
}
