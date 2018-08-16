using Infrastructure.DI;
using Park.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Park.Rep
{
    [Scoped]
    public class ExitRep : ParkRepository<ExitEntity>
    {
    }
}
