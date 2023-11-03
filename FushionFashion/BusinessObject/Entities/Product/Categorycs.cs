﻿using BusinessObject.Enum.EnumStatus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.Entities.Product
{
    public class Categorycs : BaseEntity
    {
        public string? Name { get; set; }
        public EnumStatus Status { get; set; }
    }
}
