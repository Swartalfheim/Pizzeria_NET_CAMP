namespace PizzaProject.Team1.People
{
    public interface ICanLogIn //IdentityUser...
    {
        //дані для автентифікації?
        string UserName { get; } //string Email { get; }
        string PasswordHash { get; }
        
        //У Симуляторі?
        //void Register();
        //void LogIn();
    }

    //public class User : IdentityUser
    //{

    //}
}
