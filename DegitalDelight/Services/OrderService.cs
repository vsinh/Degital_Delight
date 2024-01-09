using DegitalDelight.Services.Interfaces;
using MailKit.Security;
using Microsoft.EntityFrameworkCore;
using MimeKit.Text;
using MimeKit;
using DegitalDelight.Data;
using DegitalDelight.Models;
using Microsoft.AspNetCore.Identity;
using Hangfire;
using Microsoft.Build.ObjectModelRemoting;
using static System.Net.Mime.MediaTypeNames;
using System.Security.Claims;

namespace DegitalDelight.Services
{
    public class OrderService : IOrderService
    {
        private IHttpContextAccessor _httpContextAccessor;
        private readonly ApplicationDbContext _context;
        private readonly UserManager<User> _userManager;
        private readonly IEmailService _emailService;
        private readonly IUserService _userService;
        private readonly ICartService _cartService;
        public OrderService(ApplicationDbContext applicationDbContext,
            UserManager<User> userManager,
            IHttpContextAccessor httpContextAccessor,
            IEmailService emailService,
            IUserService userService, ICartService cartService)
        {
            _context = applicationDbContext;
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
            _emailService = emailService;
            _userService = userService;
            _cartService = cartService;
        }
        public async Task CreateOrder(Order order)
        {
            var currentUser = await _userService.GetCurrentUser();

            order.User = currentUser;
            order.Date = DateTime.Now;
            order.IsDeleted = false;
            order.State = "Đã đặt hàng";

            await _context.AddAsync(order);
            var jobSendEmailId = BackgroundJob.Enqueue(() => SendEmailConfirmOrder(order.Id));

            BackgroundJob.ContinueJobWith(jobSendEmailId, () => DeleteRemindOrder(currentUser.Email));

            await _cartService.ClearCart(currentUser.Id);

            await _context.SaveChangesAsync();
        }
        public void SendEmailConfirmOrder(int orderId)
        {
            var email = _userService.GetCurrentUserEmail();
            var order = _context.Order.Include(x => x.User).FirstOrDefault(x => x.Id == orderId);

            _emailService.SendMail(email, "Xác nhận đơn hàng", "Bạn đã đặt hàng, đơn hàng sẽ được đưa vào vận chuyển đến bạn");
        }
        public void DeleteRemindOrder(string email)
        {
            var monitor = JobStorage.Current.GetMonitoringApi();
            var jobsScheduled = monitor.ScheduledJobs(0, int.MaxValue)
            .Where(x => x.Value.Job.Method.Name == "RemindOrder").ToList();
            foreach (var j in jobsScheduled)
            {
                var t = (String)j.Value.Job.Args[0];
                if (t == email)
                {
                    BackgroundJob.Delete(j.Key);
                }
            }
        }
        public async Task<List<Order>> GetAllOrders()
        {
            var orders = await _context.Order.Include(x => x.User).Include(x => x.OrderItems).ThenInclude(x => x.Product).ToListAsync();
            return orders;
        }
    }
}
