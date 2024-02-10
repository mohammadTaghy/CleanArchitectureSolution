using Application.UseCases.Common.LessonsCategories.Command.Update;

namespace Application.Test.UseCases.Common.LessonsCategories.Command.Update
{
    public class UpdateLessonsCategoriesCommand_Test : UnitTestBase<Common_LessonsCategories, ILessonsCategoriesRepo>
    {
        private readonly UpdateLessonsCategoriesCommand _updateCommand;
        private readonly UpdateLessonsCategoriesCommandHandler _handler;

        public UpdateLessonsCategoriesCommand_Test(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
        {

            _updateCommand = new UpdateLessonsCategoriesCommand
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

            _repoMock.Verify(p => p.Update(It.IsAny<Common_LessonsCategories>()), Times.Never);
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
        public async Task Update_LessonsCategoriesCommand_BeingRepetitive_ResultTest()
        {
            _repoMock.Setup(p => p.AnyEntity(It.IsAny<Expression<Func<Common_LessonsCategories, bool>>>()))
                .Returns(Task.FromResult(true));

           var exeption = await Assert.ThrowsAsync<ValidationException>(() => _handler.Handle(_updateCommand, CancellationToken.None));
            _repoMock.Verify(p => p.Update(It.IsAny<Common_LessonsCategories>()), Times.Never);

            Assert.Equal(exeption.Message,string.Format(CommonMessage.ValidationMessage, "The name of the Lessons Categories is repetitive"));
        }

        [Fact]
        public async Task Update_LessonsCategoriesCommand_NotFoundId_ResultTest()
        {
            _repoMock.Setup(p => p.FindAsync(It.IsAny<Expression<Func<Common_LessonsCategories, bool>>>(),CancellationToken.None))
                .Returns(() => Task.FromResult<Common_LessonsCategories>(null));

             _repoMock.Verify(p => p.Update(It.IsAny<Common_LessonsCategories>()), Times.Never);
            var exeption = await Assert.ThrowsAsync<NotFoundException>(() => _handler.Handle(_updateCommand, CancellationToken.None));
            Assert.Equal(exeption.Message, string.Format(CommonMessage.NotFound, "LessonsCategories"));
        }
       
        [Fact]
        public async Task UpdateLessonsCategoriesCommand_SuccessResult_ResultTest()
        {
            _repoMock.Setup(p => p.AnyEntity(It.IsAny<Expression<Func<Common_LessonsCategories, bool>>>()))
                .Returns(Task.FromResult(false));
            _repoMock.Setup(p => p.FindAsync(It.IsAny<Expression<Func<Common_LessonsCategories, bool>>>(),CancellationToken.None))
                .Returns(Task.FromResult(new Common_LessonsCategories
                {
                    IsActive = true,
                    Name = "UpTest",
                    Title = "Uptest",
                    ParentId = 1
                }
                ));

            _repoMock.Setup(p => p.Update(It.IsAny<Common_LessonsCategories>()))
                .Returns(Task.CompletedTask);

           var result = await _handler.Handle(_updateCommand, CancellationToken.None);

            _repoMock.Verify(p => p.Update(It.IsAny<Common_LessonsCategories>()), Times.Once);
        }
    }
}
