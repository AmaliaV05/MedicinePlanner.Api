using AutoMapper;
using MedicinePlanner.BusinessLogic.DTOs;
using MedicinePlanner.BusinessLogic.Exceptions;
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
    public class MedicineService : IMedicineService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public MedicineService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<MedicineDTO> GetMedicine(int idMedicine)
        {
            var medicine = await _context.Medicines
                .Where(m => m.Id == idMedicine)
                .Include(m => m.Stock)
                .FirstOrDefaultAsync();
            if(medicine == null)
            {
                throw new IdNotFoundException(nameof(Medicine), idMedicine);
            }
            return _mapper.Map<MedicineDTO>(medicine);
        }

        public async Task<IEnumerable<MedicineDTO>> GetMedicines()
        {
            return await _context.Medicines
                .Include(m => m.Stock)
                .Select(m => _mapper.Map<MedicineDTO>(m))
                .ToListAsync();
        }

        public async Task<MedicineDTO> UpdateMedicine(int idMedicine, MedicineDTO medicineDTO)
        {
            if (idMedicine != medicineDTO.Id)
            {
                throw new Exception();
            }
            var medicine = await _context.Medicines.FindAsync(medicineDTO.Id);
            if (medicine == null)
            {
                throw new Exception();
            }
            _context.Entry(medicine).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return medicineDTO;
        }

        public async Task<MedicineDTO> AddMedicine(MedicineDTO medicineDTO)
        {
            var medicine = _mapper.Map<Medicine>(medicineDTO);
            _context.Medicines.Add(medicine);
            await _context.SaveChangesAsync();
            _context.Stocks.Add(new Stock
            {
                Total = 0,
                MedicineId = medicine.Id
            });
            await _context.SaveChangesAsync();
            medicine = await _context.Medicines.FirstAsync(m => m.Id == medicine.Id);
            return _mapper.Map<MedicineDTO>(medicine);
        }
    }
}
