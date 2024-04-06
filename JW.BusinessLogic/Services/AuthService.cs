using System.Linq;
using System.Security.Cryptography;
using System.Text;
using JW.Domain;
using JW.Infrastructure;

namespace JW.BusinessLogic.Services
{
    public class AuthService
    {
        private JewelryStoreContext _dbContext = new JewelryStoreContext();

        // Метод для аутентификации пользователя
        public Customer AuthenticateUser(string username, string password)
        {
            var customer = _dbContext.Customers.FirstOrDefault(c => c.Username == username);
            if (customer != null && IsPasswordValid(password, customer.PasswordHash))
            {
                return customer;
            }
            return null;
        }
        
        // Метод проверки роли пользователя
        public bool CheckUserAuthorization(Customer customer, string roleRequired)
        {
            return customer.Role == roleRequired;
        }

        // Метод для регистрации нового пользователя
        public Customer CreateAccount(string username, string password)
        {
            byte[] hash = CreatePasswordHash(password);

            var newCustomer = new Customer
            {
                Username = username,
                PasswordHash = hash,
                Role = "User"
            };
            
            _dbContext.Customers.Add(newCustomer);
            _dbContext.SaveChanges();
            
            return newCustomer;
        }
        
        // Генерация хеша пароля с использованием SHA256
        private static byte[] CreatePasswordHash(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                return sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
        }

        // Проверка хеша пароля
        private static bool IsPasswordValid(string password, byte[] storedHash)
        {
            using (var sha256 = SHA256.Create())
            {
                var computedHash = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(storedHash);
            }
        }
    }
}