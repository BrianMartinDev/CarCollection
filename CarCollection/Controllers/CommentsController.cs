using AutoMapper;
using BugTracker.Data;
using CarCollection.Repository.IRepository;
using CarCollection.ViewModels.Comment;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CarCollection.Controllers
    {
    [Route("api/[controller]")]
    [ApiController]
    public class CommentsController : ControllerBase
        {
        private readonly ICommentRepository _commentRepository;
        private readonly IMapper _mapper;

        public CommentsController(ICommentRepository commentRepository, IMapper mapper)
            {
            _commentRepository = commentRepository;
            _mapper = mapper;
            }

        /// <summary>
        /// Api controller that returns an IEnumerable list of comments from a vehicle posting.
        /// </summary>
        /// <remarks>[GET] Endpoint: api/Comments/GetAllCommentsWithVehicleId/{vehicleId}</remarks>
        /// <returns>A IEnumerable list of comment objects from a vehicle posting.</returns>
        [HttpGet("GetAllCommentsWithVehicleId/{vehicleId}")]
        public async Task<ActionResult<IEnumerable<CommentViewModel>>> GetAllCommentsWithVehicleId(int? vehicleId)
            {
            var comments = await _commentRepository.GetAllCommentsWithVehicleId(vehicleId);
            var commentViewModel = _mapper.Map<IEnumerable<CommentViewModel>>(comments);
            return Ok(commentViewModel);
            }

        /// <summary>
        /// Api controller that returns an IEnumerable list of comments.
        /// </summary>
        /// <remarks>[GET] Endpoint: api/Comments</remarks>
        /// <returns>A IEnumerable list of comment objects.</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CommentViewModel>>> GetComment()
            {
            var comments = await _commentRepository.GetAllAsync();
            var commentViewModel = _mapper.Map<IEnumerable<CommentViewModel>>(comments);
            return Ok(commentViewModel);
            }

        /// <summary>
        /// Api controller that a single comment.
        /// </summary>
        /// <remarks>[HttpGet("{id}")] Endpoint: api/Comments/5</remarks>
        /// <param name="id">Id property for Comments Id.</param>
        /// <returns>A single Comments object.</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<CommentViewModel>> GetComment(int? id)
            {

            if (id == null) return BadRequest();

            var comment = await _commentRepository.GetAsync(id);

            if (comment == null) return NotFound();
            var commentViewModel = _mapper.Map<CommentViewModel>(comment);

            return Ok(commentViewModel);
            }

        /// <summary>
        /// Api controller that updates a single comment.
        /// </summary>
        /// <remarks>[HttpPut("{id}")] Endpoint: api/Comment/5</remarks>
        /// <param name="id">Id property for Comment Id.</param>
        /// <param name="UpdateCommentViewModel">User input of a single Comment object.</param>
        /// <returns>The created <see cref="UpdateCommentViewModel"/> for the response.</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutComment(int id, UpdateCommentViewModel updateCommentViewModel)
            {

            if (id != updateCommentViewModel.Id) return BadRequest();

            var comment = await _commentRepository.GetAsync(id);

            if (comment == null) return NotFound();

            if (id != comment.Id) return BadRequest();

            _mapper.Map(updateCommentViewModel, comment);

            if (id != comment.Id) return BadRequest();

            try
                {
                await _commentRepository.UpdateAsync(comment);
                }
            catch (DbUpdateConcurrencyException)
                {
                if (!await _commentRepository.Exist(id))
                    {
                    return NotFound();
                    }
                else
                    {
                    throw;
                    }
                }
            var commentVm = _mapper.Map<GetCommentViewModel>(comment);
            return Ok(commentVm);
            }

        /// <summary>
        /// Api controller that creates a single comment object.
        /// </summary>
        /// <remarks>[HttpPost] Endpoint: api/Comments</remarks>
        /// <param name="vehiclePostId">Id property for Vehicle Id</param>
        /// <param name="CreateCommentViewModel">User input of a single Comment object.</param>
        /// <returns>The created <see cref="CreateCommentViewModel"/> for the response.</returns>
        [HttpPost("{vehiclePostId}")]
        public async Task<ActionResult<CreateCommentViewModel>> PostComment(int vehiclePostId, CreateCommentViewModel createCommentViewModel)
            {

            if (vehiclePostId != createCommentViewModel.VehicleId) return BadRequest();

            var comment = _mapper.Map<Comment>(createCommentViewModel);

            _mapper.Map(createCommentViewModel, comment);
            await _commentRepository.CreateAsync(comment);

            return CreatedAtAction("GetComment", new { id = comment.Id }, createCommentViewModel);
            }

        /// <summary>
        /// Api controller that deletes a single comment
        /// </summary>
        /// <remarks>[HttpDelete("{id}")] Endpoint: HttpPost: api/Comments/{id}</remarks>
        /// <param name="id">Id property for Comment Id.</param>
        /// <returns>After successful execution the response status is 200 OK.</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteComment(int? id)
            {
            if (id == null) return BadRequest();

            var comment = await _commentRepository.GetAsync(id);

            if (comment == null) return NotFound();

            await _commentRepository.DeleteAsync(comment);

            return NoContent();
            }
        }
    }
