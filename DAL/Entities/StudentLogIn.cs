namespace DAL.Entities
{
    public class StudentLogIn
    {
  
        public int ID { get; set; }
        public string EmailID { get; set; }
        public string Password { get; set; }

        public virtual Student Student { get; set; }
    }
}
