﻿using CoreLayer.Dtos.GenericDtos;
using CoreLayer.Dtos.Shared;
using CoreLayer.Entities.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLayer.Interfaces.Service
{
    public interface ISharedService:IService<Shared>
    {
        Task<CustomResponseDto<CommentListDto>> CommentAddDto(CommentAddDto commentAddDto);
        Task<CustomResponseDto<CommentListDto>> CommentAnswer(CommentAnswerDto commentAnswerDto);
        Task<CustomResponseDto<bool>> CommentDeleteOrUpdate(CommentDeleteorUpdateDto commentDeleteorUpdateDto);
        Task<CustomResponseDto<bool>> SharedLike(SharedLikeDto sharedLikeDto);
        Task<CustomResponseDto<IEnumerable<SharedListDto>>> HomeSharedList();
        Task<CustomResponseDto<IEnumerable<SharedListDto>>> GetUserShareds(int userId);

    }
}
