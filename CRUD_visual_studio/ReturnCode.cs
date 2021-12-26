﻿using System;
namespace CRUD_visual_studio
{
    public enum ReturnCode
    {
        CONNECTION_ERROR = 100,
        ACCESS_DENIED_NO_MODE = 100,
        ACCESS_DENIED = 500,
        CREATE_SUCCESS = 101,
        READ_SUCCESS = 201,
        UPDATE_SUCCESS = 301,
        DELETE_SUCCESS = 401,
        QUERY_FAILURE = 601
    }
}
