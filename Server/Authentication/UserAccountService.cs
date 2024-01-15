namespace FestivalApp.Server.Authentication
{
    // UserAccountService class responsible for managing user accounts
    public class UserAccountService
    {
        // List to store user accounts
        private List<UserAccount> _userAccountList;

        // Constructor to initialize the UserAccountService with default user accounts
        public UserAccountService()
        {
            // Initialize the list with default user accounts
            _userAccountList = new List<UserAccount>
            {
                new UserAccount { UserName = "Coordinator", Password = "admin", Role = "Coordinator" },
                new UserAccount { UserName = "Volunteer", Password = "user", Role = "Volunteer" }
            };
        }

        // Method to retrieve a user account based on the username
        public UserAccount? GetUserAccountByUserName(string userName)
        {
            return _userAccountList.FirstOrDefault(x => x.UserName == userName);
        }
    }
}
