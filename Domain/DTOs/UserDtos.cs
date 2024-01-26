using Model.Entities;

namespace Domain.DTOs;

public record DefaultUserDto(int Id, string Username, string FirstName, string LastName);

public record CountUserDto(int Count, IQueryable<DefaultUserDto> Users);