using System;

namespace LegacyApp
{
    public class UserService
    {
        public bool AddUser(string firstName, string lastName, string email, DateTime dateOfBirth, int clientId)
        {
            var userValidator = new UserValidator();

            if (!userValidator.ValidateUser(firstName, lastName, email, dateOfBirth))
            {
                return false;
            }
            
            var clientRepository = new ClientRepository();
            var client = clientRepository.GetById(clientId);

            var user = new User
            {
                Client = client,
                DateOfBirth = dateOfBirth,
                EmailAddress = email,
                FirstName = firstName,
                LastName = lastName
            };
            SetCreditLimit(user, client);
            if (user.HasCreditLimit && user.CreditLimit < 500)
            {
                return false;
            }

            UserDataAccess.AddUser(user);
            return true;
        }

        private void SetCreditLimit(User user,Client client)
        {
            var userCreditService = new UserCreditService();
            var creditLimit = userCreditService.GetCreditLimit(user.LastName, user.DateOfBirth);
            switch (client.Type)
            {
                case "VeryImportantClient":
                    user.HasCreditLimit = false;
                    break;
                case "ImportantClient":
                    creditLimit *= 2;
                    user.CreditLimit = creditLimit;
                    break;
                default:
                    user.HasCreditLimit = true;
                    user.CreditLimit = creditLimit;
                    break;
            }
        }
    }
}
