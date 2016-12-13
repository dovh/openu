using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace ApplicationSpace
{

    // defines rule abstract base class 
    public abstract class Rule
    {
        public abstract bool Criteria(Cell[,] N);
        public abstract void Output(Cell[,] N, ref Cell cell);
    }

    public class Transition_rules
    {
        public void Step(Cell[,] N, ref Cell cell)
        {
            // iterate on all transtion rules
            foreach(Rule tr in m_rules)
            {
                if (tr.Criteria(N))
                    tr.Output(N, ref cell);
            }
        }

        // Transition Rules 
        List<Rule> m_rules = new List<Rule>
        { 
            new rule_change_state(),
            new rule_stay(),

            new rule_select1(),
            new rule_select2(),
            new rule_select3(),
            new rule_select4(),
            new rule_move(),
        };
    }

    
}
