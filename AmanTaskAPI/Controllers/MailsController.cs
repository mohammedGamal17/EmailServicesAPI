using AmanTaskAPI.Models;
using AmanTaskAPI.Repositories.MailRepository;
using AmanTaskAPI.Repositories.MessageRepository;
using AmanTaskAPI.Repositories.ReceiverRepository;
using AmanTaskAPI.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mail;

namespace AmanTaskAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MailsController : ControllerBase
    {
        #region Fileds
        private readonly IMailRepository _context;
        private readonly IReceiverRepository _receiverRepository;
        private readonly IMessageRepository _messageRepository;
        #endregion

        #region Constructors
        public MailsController(IMailRepository context, IReceiverRepository receiverRepository, IMessageRepository messageRepository)
        {
            _context = context;
            _receiverRepository = receiverRepository;
            _messageRepository = messageRepository;
        }
        #endregion

        #region Methods

        #region Get
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Mail>>> GetMail()
        {
            IEnumerable<Mail>? mails = await _context.GetAll();

            if (mails == null)
                return Ok(new List<Mail>());

            return Ok(mails);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Mail>> GetMail(int id)
        {
            if (await _context.GetAll() == null)
                return Ok(new List<Mail>());


            Mail? mail = await _context.GetById(id);

            if (mail == null)
                return NotFound("This Mail Not Found");


            return Ok(mail);
        }
        #endregion

        #region Add
        [HttpPost]
        public async Task<ActionResult<Mail>> PostMail(List<RecevierViewModel> receiversViewModel, int messageId)
        {

            List<int> receversId = new List<int>();
            Message message = await _messageRepository.GetById(messageId);

            #region Add Receviers First
            foreach (RecevierViewModel recevierViewModel in receiversViewModel)
            {

                int receverId = _receiverRepository.GetReceiverIdByEmail(recevierViewModel.Email);

                if (receverId == -1)
                {
                    Models.Receiver receiver = new() { Email = recevierViewModel.Email };
                    await _receiverRepository.Add(receiver);
                    receverId = _receiverRepository.GetReceiverIdByEmail(recevierViewModel.Email);
                }

                receversId.Add(receverId);
            }
            #endregion

            #region Send Mail and Save into DB
            foreach (int receverId in receversId)
            {
                try
                {
                    #region Mail
                    ConfigSmtpClient().Send(CreateEmailItem(message, receverId));
                    #endregion

                    #region Save Into Db
                    Mail mail = new Mail() { MessageId = messageId, ReceiverId = receverId };
                    await _context.Add(mail);
                    #endregion

                }
                catch (SmtpException) { return StatusCode(500, "Failed to send email"); }
                catch (Exception ex) { return StatusCode(500, "Failed to Save email into DB"); }
            }
            #endregion

            return Ok();
        }

        #endregion

        #region Mail
        private MailMessage CreateEmailItem(Message message, int receverId)
        {
            MailMessage mailMessage = new MailMessage();
            mailMessage.From = new MailAddress("mohammed_gamal14@hotmail.com");
            mailMessage.Subject = message.Subject;
            mailMessage.Body = message.Text;
            mailMessage.IsBodyHtml = true;
            mailMessage.To.Add(new MailAddress(_receiverRepository.GetReceiverEmailById(receverId)));

            return mailMessage;
        }

        private static SmtpClient ConfigSmtpClient()
        {
            SmtpClient client = new SmtpClient("smtp.office365.com", 587); //Outlook smtp    
            System.Net.NetworkCredential basicCredential1 = new
            System.Net.NetworkCredential(userName: "mohammed_gamal14@hotmail.com", password: "MoGemmy!7");

            client.EnableSsl = true;
            client.UseDefaultCredentials = false;
            client.Credentials = basicCredential1;

            return client;
        }
        #endregion

        #endregion
    }
}
