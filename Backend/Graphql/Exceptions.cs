namespace Backend.Graphql;

public class UserNotFoundException: Exception
{
    public UserNotFoundException(string user) : base($"User {user} not found."){}
    
}

public class InvalidPasswordException: Exception
{
    public InvalidPasswordException() : base($"Wrong password"){}
    
}

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
