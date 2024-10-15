namespace GZ_Test_Repo.Entity
{
    public class Doctor
    {
        public int Id { get; set; }
        public string Surname { get; set; }
        public string Name { get; set; }
        public string Father { get; set; }
        public int CabinetId { get; set; }
        public Cabinet? Cabinet { get; set; }
        public int SpecializationId { get; set; }
        public Specialization? Specialization { get; set; }
        public int AreaId{ get; set; }
        public Area? Area { get; set; }
    }
}
