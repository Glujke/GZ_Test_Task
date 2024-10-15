using GZ_Test_Repo.Context;
using GZ_Test_Repo.Entity;
using Microsoft.EntityFrameworkCore;

namespace GZ_Test_Repo.Repository
{
    public class RepositoryPatient : RepositoryBase<Patient>
    {
        public RepositoryPatient(GZ_DbContext context) : base(context) { }
        public override async Task<IEnumerable<Patient>> GetAllAsync() =>
                  await Task.FromResult<IEnumerable<Patient>>(_context.Patients.Include(p => p.Area)
                                                  .Include(p => p.Gender));


        public override async Task<Patient> GetByIdAsync(int id) =>
               await _context.Patients.Include(d => d.Area).Include(p => p.Gender)
                                              .Where(p => p.Id == id).FirstOrDefaultAsync();

        public override async Task CreateAsync(Patient entity)
        {
            CheckIsValid(entity);
            await base.CreateAsync(entity);
        }

        public override async Task UpdateAsync(Patient entity)
        {
            CheckIsValid(entity);
            await base.UpdateAsync(entity);
        }

        private void CheckIsValid(Patient entity)
        {
            if (entity.AreaId < -1 || !_context.Areas.Any(a => a.Id == entity.AreaId))
                throw new DbUpdateException();
            if (entity.GenderId < -1 || !_context.Genders.Any(g => g.Id == entity.GenderId))
                throw new DbUpdateException();
        }
    }
}
