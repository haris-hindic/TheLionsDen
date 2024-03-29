﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheLionsDen.Model.Requests;
using TheLionsDen.Model.Responses;
using TheLionsDen.Model.SearchObjects;

namespace TheLionsDen.Services
{
    public interface IUserService : ICRUDService<UserResponse,UserSearchObject, UserInsertRequest, UserUpdateRequest>
    {
        public Task<UserResponse> Login(string username, string password);
        public Task<UserResponse> Register(UserInsertRequest request);
        public Task<UserResponse> CustomerUpdate(int id,UserUpdateRequest request);
    }
}
