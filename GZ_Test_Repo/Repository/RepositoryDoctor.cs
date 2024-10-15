using GZ_Test_Repo.Context;
using GZ_Test_Repo.Entity;
using Microsoft.EntityFrameworkCore;

namespace GZ_Test_Repo.Repository
{
    public class RepositoryDoctor : RepositoryBase<Doctor>
    {
        public RepositoryDoctor(GZ_DbContext context) : base(context) { }

        public override async Task<IEnumerable<Doctor>> GetAllAsync() => 
               await Task.FromResult< IEnumerable<Doctor>> (_context.Doctors.Include(d => d.Specialization)
                                                          .Include(d => d.Cabinet).Include(d => d.Area));
        

        public override async Task<Doctor> GetByIdAsync(int id) =>
               await _context.Doctors.Include(d  => d.Specialization).Include(d => d.Cabinet)
                               .Include(d => d.Area).Where(d => d.Id == id).FirstOrDefaultAsync();

        public override async Task CreateAsync(Doctor entity)
        {
            CheckIsValid(entity);
            entity.Specialization = null;
            entity.Area = null;
            entity.Cabinet = null;
            await base.CreateAsync(entity);
        }

        public override async Task UpdateAsync(Doctor entity)
        {
            CheckIsValid(entity);
            entity.Specialization = null;
            entity.Area = null;
            entity.Cabinet = null;
            await base.UpdateAsync(entity);
        }

        private void CheckIsValid(Doctor entity)
        {
            if (entity.AreaId < -1 || !_context.Areas.Any(a => a.Id == entity.AreaId))
                throw new DbUpdateException();
            if (entity.SpecializationId < -1 || !_context.Specializations.Any(s => s.Id == entity.SpecializationId))
                throw new DbUpdateException();
            if (entity.CabinetId < -1 || !_context.Cabinetes.Any(c => c.Id == entity.CabinetId))
                throw new DbUpdateException();
        }
    }
}
