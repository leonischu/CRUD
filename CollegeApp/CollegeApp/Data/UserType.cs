namespace CollegeApp.Data
{
    public class UserType
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        //One User Type Will have multiple user 

        public virtual ICollection<User> Users { get; set; }

    }
}
