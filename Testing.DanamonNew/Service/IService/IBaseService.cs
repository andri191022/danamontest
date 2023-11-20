﻿using Testing.DanamonNew.Models;

namespace Testing.DanamonNew.Service.IService
{
    public interface IBaseService
    {
        Task<ResponseDto?> SendAsync(RequestDto requestDto, bool withBearer = true);

        Task<ResponseDto?> postAsync(RequestDto requestDto);
    }
}
