using BlueberryHomeworkApp.Domain.Exceptions;
using BlueberryHomeworkApp.Infrastructure;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace BlueberryHomeworkApp.Application.Usecases.User.UpdateUserByName;

public class UpdateUserByNameHandler(
    IUnitOfWork unitOfWork
) : IRequestHandler<UpdateUserByNameCommand, IResult<UpdateUserByNameResult>>
{
    public async Task<IResult<UpdateUserByNameResult>> Handle(UpdateUserByNameCommand request,
        CancellationToken cancellationToken)
    {
        // 레포지토리 객체를 취득
        var userRepository = unitOfWork.GetRepository<Domain.Entities.User>();

        // 갱신 대상 취득
        var getResult = await userRepository.GetAsync(request.Id);

        if (getResult.IsError)
        {
            return Result<UpdateUserByNameResult>.Error(getResult.GetError()!);
        }


        var user = getResult.Get();
        // 유저명이 같을 경우
        if (user.Name == request.Name)
        {
            return Result<UpdateUserByNameResult>.Error(
                new ArgumentException("A name with the same value already exists."));
        }

        user.Name = request.Name;
        user.UpdatedAt = DateTime.UtcNow;

        // 등록 처리 실시
        var addResult = await userRepository.UpdateAsync(user);

        // 처리 중 에러가 발생될 경우
        return addResult.IsError
            ? Result<UpdateUserByNameResult>.Error(addResult.GetError()!)
            : Result<UpdateUserByNameResult>.Ok(new UpdateUserByNameResult(user.Id, user.Name));
    }
}