using Domain.Graphql.Types.Exceptions;
using Domain.Graphql.Types.Inputs;
using Domain.Graphql.Types.Results;
using Domain.Repositories.Interfaces;
using Domain.Services.Interfaces;
using Model.Entities;

namespace Domain.Services.Implementations;

public class YommentService : IYommentService
{
    private IYommentRepository _yommentRepository;
    private IYeetRepository _yeetRepository;
    private IUserRepository _userRepository;

    public YommentService(IYommentRepository yommentRepository, IUserRepository userRepository, IYeetRepository yeetRepository)
    {
        _yommentRepository = yommentRepository;
        _userRepository = userRepository;
        _yeetRepository = yeetRepository;
    }

    public Task<YommentsResult> Yomments(YommentsInput input) => _yommentRepository.ReadYomments(input);

    public Task<YommentResult> Yomment(YommentInput input) => _yommentRepository.ReadYomment(input);

    public async Task<CreateYommentResult> CreateYomment(CreateYommentInput input)
    {
        if (!await _userRepository.Exists(input.UserId))
            throw new UserNotFoundException(input.UserId);
        
        if (!await _yeetRepository.Exists(input.YeetId))
            throw new YeetNotFoundException(input.YeetId);

        var yomment = new Yomment()
        {
            Body = input.Body,
            UserId = input.UserId,
            YeetId = input.YeetId,
            CreatedAt = DateTime.Now.ToUniversalTime()
        };

        yomment = await  _yommentRepository.CreateAsync(yomment);

        var graphqlYomment = await _yommentRepository.ReadGraphqlYomment(yomment.Id);
        return new CreateYommentResult(graphqlYomment);
    }

    public async Task<DeleteYommentResult> DeleteYomment(DeleteYommentInput input)
    {
        if (!await _yommentRepository.Exists(input.YommentId))
            throw new YommentNotFoundException(input.YommentId);

        await _yommentRepository.DeleteAsync(y => y.Id == input.YommentId);
        return new DeleteYommentResult(input.YommentId);
    }

    public async Task<AddLikeToYommentResult> AddLikeToYomment(AddLikeToYommentInput input)
    {
        var yomment = await _yommentRepository.ReadAsync(input.YommentId);
        if (yomment is null)
            throw new YommentNotFoundException(input.YommentId);
        yomment.Likes += 1;
        await _yommentRepository.UpdateAsync(yomment);
        return new AddLikeToYommentResult(yomment.Likes);
    }
}