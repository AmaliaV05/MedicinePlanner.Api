using MedicinePlanner.BusinessLogic.Interfaces;
using MedicinePlanner.Data.Data;
using MedicinePlanner.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedicinePlanner.BusinessLogic.Services
{
    public class NotificationService : INotificationService
    {
        private readonly ApplicationDbContext _context;

        public NotificationService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<string>> GetNotifications()
        {
            var messages = new List<string>();
            messages.AddRange(await StockNotifications());
            messages.AddRange(await PlanningNotifications());
            messages.AddRange(await DailyPlanningNotifications());
            return messages;
        }

        private async Task<IEnumerable<string>> StockNotifications()
        {
            var messages = new List<string>();
            var medicines = await _context.Medicines
                .Where(m => m.Plannings.Count > 0)
                .Include(m => m.Plannings)
                .ThenInclude(d => d.DailyPlannings.Where(d => d.IntakeTime >= DateTime.Now && d.IntakeTime <= DateTime.Now.AddDays(5)))
                .Include(m => m.Stock)
                .AsSplitQuery()
                .ToListAsync();
            foreach (Medicine medicine in medicines)
            {
                var stock = medicine.Stock.Total;
                foreach (DailyPlanning dailyPlanning in medicine.Plannings.Last().DailyPlannings)
                {
                    stock -= dailyPlanning.Dosage;
                    var numberOfDays = dailyPlanning.IntakeTime.Date.Subtract(DateTime.Now).TotalDays;
                    if (stock <= 0 && numberOfDays >= 1)
                    {
                        messages.Add(string.Format("{0} will be gone in {1} days", medicine.Name, (int)numberOfDays));
                    }
                }
            }
            return messages;
        }

        private async Task<IEnumerable<string>> PlanningNotifications()
        {
            var messages = new List<string>();
            var medicines = await _context.Medicines
                .Where(m => m.Plannings.Count > 0)
                .Include(m => m.Plannings.Where(p => p.StartDate <= DateTime.Now))                
                .AsSplitQuery()
                .ToListAsync();
            foreach (Medicine medicine in medicines)
            {
                if (medicine.Plannings.Last().PauseEndDate.HasValue)
                {
                    var daysLeftUntilNextTreatment = medicine.Plannings.Last().PauseEndDate.Value.Subtract(DateTime.Now).TotalDays;
                    if (daysLeftUntilNextTreatment >= 1 && daysLeftUntilNextTreatment <= 5)
                    {
                        messages.Add(string.Format("{0} has to be retaken in {1} days", medicine.Name, (int)daysLeftUntilNextTreatment));
                    }
                }                
            }
            return messages;
        }

        private async Task<IEnumerable<string>> DailyPlanningNotifications()
        {
            var messages = new List<string>();
            var beforeConsumption = DateTime.Now.AddMinutes(15);            
            var dailyPlannings = await _context.DailyPlannings                
                .Where(d => d.IntakeTime <= beforeConsumption && d.IntakeTime >= DateTime.Now && d.Consumed == false)
                .Include(d => d.Planning)
                .ThenInclude(p => p.Medicine)
                .ToListAsync();
            foreach (DailyPlanning dailyPlanning in dailyPlannings)
            {
                messages.Add(string.Format("{0} must be taken at {1:t}", dailyPlanning.Planning.Medicine.Name, dailyPlanning.IntakeTime.TimeOfDay));
            }
            return messages;
        }
    }
}
