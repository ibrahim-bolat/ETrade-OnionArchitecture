﻿using ETrade.Domain.Enums;

namespace ETrade.Application.Wrappers.Abstract;

public interface IResult
    {
        public ResultStatus ResultStatus { get; }
        public string Message { get; }
        public Exception Exception { get; }
    }
