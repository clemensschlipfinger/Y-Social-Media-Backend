namespace Backend.Graphql;

public class UserNotFoundException: Exception
{
    public UserNotFoundException(string user) : base($"User {user} not found."){}
    
}

public class InvalidPasswordException: Exception
{
    public InvalidPasswordException() : base($"Wrong password"){}
    
}
