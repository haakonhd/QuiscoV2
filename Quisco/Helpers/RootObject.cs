﻿using System.Collections.Generic;

namespace Quisco.Helpers
{
    public class RootObject
    {
        public int response_code { get; set; }
        public List<Result> results { get; set; }
    }
}
