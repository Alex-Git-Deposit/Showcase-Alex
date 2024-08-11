using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Textadventure
{
    public interface I_Status_Duration
    {
        void reduce_all_effect_durations();
        void increase_all_effect_durations();
    }
}
