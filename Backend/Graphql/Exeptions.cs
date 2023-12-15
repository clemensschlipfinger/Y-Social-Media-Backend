namespace Backend.Graphql; 

public class UsernameAlreadyTakenException : Exception
{
    public UsernameAlreadyTakenException(string username) : base($"Username {username} is already taken")
    {
    }
}

public class PasswordTooShortException : Exception
{
    public PasswordTooShortException() : base("Password is too short")
    {
    }
}