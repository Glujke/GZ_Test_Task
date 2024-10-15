namespace GZ_Test_Repo.Entity
{
    public class Patient
    {
        public int Id { get; set; }
        public string Surname { get; set; }
        public string Name { get; set; }    
        public string Father { get; set; }
        public int AreaId { get; set; }
        public Area? Area { get; set; }
        public int GenderId { get; set; }
        public Gender? Gender { get; set; }
        public string Address { get; set; }
        public DateTime BurnDate { get; set; }
    }
}
