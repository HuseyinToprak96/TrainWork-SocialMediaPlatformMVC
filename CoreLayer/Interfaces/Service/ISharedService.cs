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
        CommentListDto CommentAddDto(CommentAddDto commentAddDto);
        CommentListDto CommentAnswer(CommentAnswerDto commentAnswerDto);
        bool CommentDeleteOrUpdate(CommentDeleteorUpdateDto commentDeleteorUpdateDto);
        bool SharedLike(SharedLikeDto sharedLikeDto);

    }
}
