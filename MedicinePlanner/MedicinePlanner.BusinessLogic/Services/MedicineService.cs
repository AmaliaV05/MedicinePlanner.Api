using MedicinePlanner.BusinessLogic.Interfaces;
using MedicinePlanner.Data.Data;
using MedicinePlanner.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MedicinePlanner.BusinessLogic.Services
{
    public class MedicineService : IMedicineService
    {
        private readonly ApplicationDbContext _context;

        public MedicineService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Medicine> GetMedicine(int id)
        {
            var medicine = await _context.Medicines.FindAsync(id);
            if(medicine == null)
            {
                throw new Exception();
            }
            return await _context.Medicines.FindAsync(id);
        }

        public async Task<IEnumerable<Medicine>> GetMedicines()
        {
            return await _context.Medicines.ToListAsync();
        }

        public async Task<Medicine> UpdateMedicine(int idMedicine, Medicine medicine)
        {
            if (idMedicine != medicine.Id)
            {
                throw new Exception();
            }
            var med = await _context.Medicines.FindAsync(medicine.Id);
            if (med == null)
            {
                throw new Exception();
            }
            _context.Entry(med).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return med;
        }

        public async Task<Medicine> AddMedicine(Medicine medicine)
        {
            await _context.Medicines.AddAsync(medicine);
            await _context.SaveChangesAsync();
            return medicine;
        }
    }
}
