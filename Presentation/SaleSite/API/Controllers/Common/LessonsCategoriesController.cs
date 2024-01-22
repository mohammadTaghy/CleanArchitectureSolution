using Application.Common.Model;
using Application.UseCases;
using Application.UseCases.Common.LessonsCategories.Command.Create;
using Application.UseCases.Common.LessonsCategories.Command.Update;
using Asp.Versioning;
using Common;
using Domain.Entities;
using Infrastructure.Authentication;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{

    public class LessonsCategoriesController : BaseController
    {
        public LessonsCategoriesController(IMediator mediator, ICurrentUserSession currentUserSession) : base(mediator, currentUserSession)
        {
        }
        #region HttpGet
        [HttpGet]
        [ApiVersion("1.0")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [CMSAuthorize]
        public async Task<QueryResponse<List<LessonsCategoriesTreeDto>>> LessonsCategories([FromQuery] LessonsCategoriesAsTreeQuery lessonsCategoriesAsTreeQuery, CancellationToken cancellationToken)
        {
            return await _mediator.Send(lessonsCategoriesAsTreeQuery, cancellationToken);
        }
        [HttpGet]
        [ApiVersion("1.0")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [CMSAuthorize]
        public async Task<QueryResponse<List<LessonsCategoriesTreeDto>>> LessonsCategoriesAsList([FromQuery] LessonsCategoriesAsListQuery lessonsCategoriesAsTreeQuery, CancellationToken cancellationToken)
        {
            return await _mediator.Send(lessonsCategoriesAsTreeQuery, cancellationToken);
        }
        #endregion
        #region HttpPost
        [HttpPost]
        [ApiVersion("1.0")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [CMSAuthorize]
        public async Task<CommandResponse<Common_LessonsCategories>> LessonsCategories(CreateLessonsCategoriesCommand createLessonsCategoriesCommand, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(createLessonsCategoriesCommand, cancellationToken);
            return result;
        }
        #endregion
        #region HttpPut
        [HttpPut]
        [ApiVersion("1.0")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [CMSAuthorize]
        public async Task<CommandResponse<Common_LessonsCategories>> PutLessonsCategories(
            UpdateLessonsCategoriesCommand UpdateEntityCommand, CancellationToken cancellationToken)
        {
            return await _mediator.Send(UpdateEntityCommand,cancellationToken);
        }
        #endregion
        #region HttpDelete

        #endregion

    }
}
