namespace Domain.Mapper;

public interface FromMapper<F, out T>
{
    T mapFrom(F f);
}