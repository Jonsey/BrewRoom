﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BrewRoom.Modules.Core.Models;
using FluentNHibernate.Mapping;

namespace BrewRoom.Modules.Core.Data
{
    public sealed class RecipeFermentableMap : ClassMap<RecipeFermentable>
    {
        public RecipeFermentableMap()
        {
            Id(x => x.Id).GeneratedBy.GuidComb().UnsavedValue("00000000-0000-0000-0000-000000000000");

            Map(x => x.Name);
            Map(x => x.Pppg);
            Map(x => x.Description).CustomSqlType("nvarchar(MAX)");

            Component(x => x.Weight);

            References(x => x.Recipe);
        }
    }
}
