namespace CollegeApp.Data
{
    public class RolePrivilege
    {
        public int Id { get; set; }
        public string RolePrivilegeName { get; set; }


        public string Password { get; set; }

        public string PasswordSalt { get; set; }

        public int UserType { get; set; }


        public bool IsActive { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime ModifiedDate { get; set; }

    }
}
