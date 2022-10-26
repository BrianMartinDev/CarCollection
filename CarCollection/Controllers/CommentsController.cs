using AutoMapper;
using BugTracker.Data;
using CarCollection.ViewModels.Comment;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CarCollection.Controllers
    {
    [Route("api/[controller]")]
    [ApiController]
    public class CommentsController : ControllerBase
        {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public CommentsController(ApplicationDbContext context, IMapper mapper)
            {
            _context = context;
            _mapper = mapper;
            }


        /// <summary>
        /// Api controller that returns an IEnumerable list of comments.
        /// </summary>
        /// <remarks>[GET] Endpoint: api/Comments</remarks>
        /// <returns>A IEnumerable list of comment objects.</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CommentViewModel>>> GetComment()
            {
            var comments = await _context.Comment.ToListAsync();
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
        public async Task<ActionResult<Comment>> GetComment(int id)
            {
            var comment = await _context.Comment.FindAsync(id);

            if (comment == null)
                {
                return NotFound();
                }
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

            var comment = await _context.Comment.FindAsync(id);

            if (comment == null) return NotFound();

            if (id != comment.Id) return BadRequest();

            _mapper.Map(updateCommentViewModel, comment);

            if (id != comment.Id) return BadRequest();

            _context.Entry(comment).State = EntityState.Modified;

            try
                {
                await _context.SaveChangesAsync();
                }
            catch (DbUpdateConcurrencyException)
                {
                if (!CommentExists(id))
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
            _context.Comment.Add(comment);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetComment", new { id = comment.Id }, createCommentViewModel);
            }

        /// <summary>
        /// Api controller that deletes a single comment
        /// </summary>
        /// <remarks>[HttpDelete("{id}")] Endpoint: HttpPost: api/Comments/{id}</remarks>
        /// <param name="id">Id property for Comment Id.</param>
        /// <returns>After successful execution the response status is 200 OK.</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteComment(int id)
            {
            var comment = await _context.Comment.FindAsync(id);
            if (comment == null) return NotFound();

            _context.Comment.Remove(comment);
            await _context.SaveChangesAsync();

            return NoContent();
            }

        /// <summary>
        /// Check if Comment exist else return false
        /// </summary>
        /// <param name="id">Id property for Comment Id.</param>
        /// <returns>returns boolean value</returns>
        private bool CommentExists(int id)
            {
            return _context.Comment.Any(e => e.Id == id);
            }
        }
    }
