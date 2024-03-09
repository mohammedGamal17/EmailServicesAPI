using AmanTaskAPI.Models;
using AmanTaskAPI.Repositories.MessageRepository;
using AmanTaskAPI.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace AmanTaskAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessagesController : ControllerBase
    {
        #region Methods
        private readonly IMessageRepository _massageRepository;
        #endregion

        #region Constructors
        public MessagesController(IMessageRepository massageRepository)
        {
            _massageRepository = massageRepository;
        }
        #endregion

        #region Methods

        #region Get
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Message>>> GetMessages()
        {
            IEnumerable<Message>? messages = await _massageRepository.GetAll();

            if (messages == null || !messages.Any())
                return Ok(new List<Message>());

            #region Mapping
            List<MessageViewModel> messagesViewModel = new List<MessageViewModel>();

            foreach (Message message in messages)
            {
                MessageViewModel messageViewModel = new MessageViewModel()
                {
                    Id = message.Id,
                    Text = message.Text,
                    Subject = message.Subject
                };

                messagesViewModel.Add(messageViewModel);
            }
            #endregion

            return Ok(messagesViewModel);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Message>> GetMessage(int id)
        {

            if (await _massageRepository.GetAll() == null)
                return NotFound("The List is Empty");

            Message message = await _massageRepository.GetById(id);

            if (message == null)
                return NotFound("This Message Not Found");

            #region Mapping
            MessageViewModel messageViewModel = new MessageViewModel()
            {
                Id = message.Id,
                Subject = message.Subject,
                Text = message.Text
            };
            #endregion

            return Ok(messageViewModel);
        }
        #endregion

        #region Add
        [HttpPost]
        public async Task<ActionResult<Message>> PostMessage(MessageViewModel messageViewModel)
        {
            try
            {
                Message message = new Message()
                {
                    Text = messageViewModel.Text,
                    Subject = messageViewModel.Subject
                };
                await _massageRepository.Add(message);
                return CreatedAtAction("GetMessage", new { id = message.Id }, message);

            }
            catch (Exception ex) { return BadRequest(ex); }
        }
        #endregion

        #region Delete
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMessage(int id)
        {
            try
            {
                await _massageRepository.DeleteById(id);
                return Ok("Deleted Success");

            }
            catch (Exception ex) { return BadRequest(ex); };
        }
        #endregion

        #endregion

    }
}
