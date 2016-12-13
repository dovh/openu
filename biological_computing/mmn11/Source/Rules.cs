using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace ApplicationSpace
{
    public class rule_change_state : Rule
    {
        public override bool Criteria(Cell[,] N)
        {
            return true; 
        }

        public override void Output(Cell[,] N, ref Cell cell)
        {
            Cell center = N[1, 1];
            if (center.State == Cell.state.Select1) cell.State = Cell.state.Select2;
            if (center.State == Cell.state.Select2) cell.State = Cell.state.Select3;
            if (center.State == Cell.state.Select3) cell.State = Cell.state.Select4;
            if (center.State == Cell.state.Select4) cell.State = Cell.state.Move;
            if (center.State == Cell.state.Move)    cell.State = Cell.state.Select1;
        }
    }

    public class rule_stay : Rule
    {
        public override bool Criteria(Cell[,] N)
        {
            return N[1, 1].State != Cell.state.Move;
        }

        public override void Output(Cell[,] N, ref Cell cell)
        {
            Cell center = N[1, 1];
            cell.Copy(center);
            cell.Direction = center.Direction; 
        }
    }

    public class rule_select1 : Rule
    {
        public override bool Criteria(Cell[,] N)
        {
            // detect previous state 
            Cell center = N[1, 1];
            return (center.State == Cell.state.Select1) && !center.IsEmpty();
        }

        public override void Output(Cell[,] N, ref Cell cell)
        {
            Cell center = N[1, 1];

            // check the matches to neighbors 
            int[] Matches = 
                {N[0, 0].Match(center), N[1, 0].Match(center), N[2, 0].Match(center),
                 N[0, 1].Match(center),                        N[2, 1].Match(center), 
                 N[0, 2].Match(center), N[1, 2].Match(center), N[2, 2].Match(center)};
            double maxMatch = Matches.Max();

            if (center.Match() < maxMatch && CA.MatchThreshold < maxMatch)
            {
                // if the match is highier than "what you have", and higher than a threshold 
                //   set direction to him 
                int index = Array.FindIndex(Matches, d => d == maxMatch);
                cell.Direction = Cell.neighbors[index].dir;
            }
        }
    }

    public class rule_select2 : Rule
    {
        public override bool Criteria(Cell[,] N)
        {
            // detect previous state 
            Cell center = N[1, 1];
            return N[1, 1].State == Cell.state.Select2 && !center.IsEmpty(); 
        }

        public override void Output(Cell[,] N, ref Cell cell)
        {
            Cell center = N[1, 1];

            // Check if I want some one who does not want me. 
            bool mismatch = false;
                 if (center.Direction == Cell.direction.UpLeft     && N[0, 0].Direction != Cell.direction.DownRight )  mismatch = true;
            else if (center.Direction == Cell.direction.Up         && N[1, 0].Direction != Cell.direction.Down      )  mismatch = true;
            else if (center.Direction == Cell.direction.UpRight    && N[2, 0].Direction != Cell.direction.DownLeft  )  mismatch = true;
            else if (center.Direction == Cell.direction.Left       && N[0, 1].Direction != Cell.direction.Right     )  mismatch = true;
            else if (center.Direction == Cell.direction.Right      && N[2, 1].Direction != Cell.direction.Left      )  mismatch = true;
            else if (center.Direction == Cell.direction.DownLeft   && N[0, 2].Direction != Cell.direction.UpRight   )  mismatch = true;
            else if (center.Direction == Cell.direction.Down       && N[1, 2].Direction != Cell.direction.Up        )  mismatch = true;
            else if (center.Direction == Cell.direction.DownRight  && N[2, 2].Direction != Cell.direction.UpLeft    )  mismatch = true;

            if (mismatch)
            {
                // in this case, check if some one else who stand next to you like you and still a better choise than the one you have.
                //   select one among all heighbors directed to the center, sorted by round odered.
                     if (N[0, 0].Direction == Cell.direction.DownRight && N[0, 0].Match(center) > center.Match()) cell.Direction = Cell.direction.UpLeft;
                else if (N[1, 0].Direction == Cell.direction.Down      && N[1, 0].Match(center) > center.Match()) cell.Direction = Cell.direction.Up;
                else if (N[2, 0].Direction == Cell.direction.DownLeft  && N[2, 0].Match(center) > center.Match()) cell.Direction = Cell.direction.UpRight;
                else if (N[0, 1].Direction == Cell.direction.Right     && N[0, 1].Match(center) > center.Match()) cell.Direction = Cell.direction.Left;
                else if (N[2, 1].Direction == Cell.direction.Left      && N[2, 1].Match(center) > center.Match()) cell.Direction = Cell.direction.Right;
                else if (N[0, 2].Direction == Cell.direction.UpRight   && N[0, 2].Match(center) > center.Match()) cell.Direction = Cell.direction.DownLeft;
                else if (N[1, 2].Direction == Cell.direction.Up        && N[1, 2].Match(center) > center.Match()) cell.Direction = Cell.direction.Down;
                else if (N[2, 2].Direction == Cell.direction.UpLeft    && N[2, 2].Match(center) > center.Match()) cell.Direction = Cell.direction.DownRight;

            }
        }
    }

    public class rule_select3 : Rule
    {
        public override bool Criteria(Cell[,] N)
        {
            // detect previous state 
            Cell center = N[1, 1];
            return center.State == Cell.state.Select3 && !center.IsEmpty();
        }

        public override void Output(Cell[,] N, ref Cell cell)
        {
            Cell center = N[1, 1];

            // Check if still I want some one who does not want me. 
            //   that is, In stage select2 I did not found some one around me who likes me. 

            bool mismatch = false;
                 if (center.Direction == Cell.direction.UpLeft     && N[0, 0].Direction != Cell.direction.DownRight )  mismatch = true;
            else if (center.Direction == Cell.direction.Up         && N[1, 0].Direction != Cell.direction.Down      )  mismatch = true;
            else if (center.Direction == Cell.direction.UpRight    && N[2, 0].Direction != Cell.direction.DownLeft  )  mismatch = true;
            else if (center.Direction == Cell.direction.Left       && N[0, 1].Direction != Cell.direction.Right     )  mismatch = true;
            else if (center.Direction == Cell.direction.Right      && N[2, 1].Direction != Cell.direction.Left      )  mismatch = true;
            else if (center.Direction == Cell.direction.DownLeft   && N[0, 2].Direction != Cell.direction.UpRight   )  mismatch = true;
            else if (center.Direction == Cell.direction.Down       && N[1, 2].Direction != Cell.direction.Up        )  mismatch = true;
            else if (center.Direction == Cell.direction.DownRight  && N[2, 2].Direction != Cell.direction.UpLeft    )  mismatch = true;

            if (mismatch || center.Direction == Cell.direction.None)
            {
                //  in this case, keep on moving 
                //    radomly select an empty cell to move to (avoid borders).

                HashSet<int> keys = new HashSet<int>();

                while (keys.Count != Cell.neighbors.Length)
                {
                    // get random neighbor 
                    int key = (int)(CA.NextRandom * Cell.neighbors.Length);
                    keys.Add(key);

                    // if neigbor is epmty and not a border, set direction to it. 
                    Cell.neighbor Ni = Cell.neighbors[key];
                    if (N[Ni.x, Ni.y].IsEmpty() && !N[Ni.x, Ni.y].IsBorder())
                    {
                        cell.Direction = Ni.dir;
                        break;
                    }
                };
            }
        }
    }

    public class rule_select4 : Rule
    {
        public override bool Criteria(Cell[,] N)
        {
            // detect previous state 
            Cell center = N[1, 1];
            return center.State == Cell.state.Select4 && center.IsEmpty(); 
        }

        public override void Output(Cell[,] N, ref Cell cell)
        {
            Cell center = N[1, 1];

            // If empty and a neigbors wants to move to me, aprove the match 

                 if (N[0, 0].Direction == Cell.direction.DownRight ) cell.Direction = Cell.direction.UpLeft;
            else if (N[1, 0].Direction == Cell.direction.Down      ) cell.Direction = Cell.direction.Up;
            else if (N[2, 0].Direction == Cell.direction.DownLeft  ) cell.Direction = Cell.direction.UpRight;
            else if (N[0, 1].Direction == Cell.direction.Right     ) cell.Direction = Cell.direction.Left;
            else if (N[2, 1].Direction == Cell.direction.Left      ) cell.Direction = Cell.direction.Right;
            else if (N[0, 2].Direction == Cell.direction.UpRight   ) cell.Direction = Cell.direction.DownLeft;
            else if (N[1, 2].Direction == Cell.direction.Up        ) cell.Direction = Cell.direction.Down;
            else if (N[2, 2].Direction == Cell.direction.UpLeft    ) cell.Direction = Cell.direction.DownRight;
        }
    }

    public class rule_move : Rule
    {
        public override bool Criteria(Cell[,] N)
        {
            // detect previous state 
            return N[1, 1].State == Cell.state.Move;
        }

        public override void Output(Cell[,] N, ref Cell cell)
        {
            Cell center = N[1, 1];

            // find matches, and merge them 
                 if (center.Direction == Cell.direction.UpLeft     && N[0, 0].Direction == Cell.direction.DownRight )  cell.Merge(N[1,1], N[0, 0]);
            else if (center.Direction == Cell.direction.Up         && N[1, 0].Direction == Cell.direction.Down      )  cell.Merge(N[1,1], N[1, 0]);
            else if (center.Direction == Cell.direction.UpRight    && N[2, 0].Direction == Cell.direction.DownLeft  )  cell.Merge(N[1,1], N[2, 0]);
            else if (center.Direction == Cell.direction.Left       && N[0, 1].Direction == Cell.direction.Right     )  cell.Merge(N[1,1], N[0, 1]);
            else if (center.Direction == Cell.direction.Right      && N[2, 1].Direction == Cell.direction.Left      )  cell.Merge(N[1,1], N[2, 1]);
            else if (center.Direction == Cell.direction.DownLeft   && N[0, 2].Direction == Cell.direction.UpRight   )  cell.Merge(N[1,1], N[0, 2]);
            else if (center.Direction == Cell.direction.Down       && N[1, 2].Direction == Cell.direction.Up        )  cell.Merge(N[1,1], N[1, 2]);
            else if (center.Direction == Cell.direction.DownRight  && N[2, 2].Direction == Cell.direction.UpLeft    )  cell.Merge(N[1,1], N[2, 2]);
                 else cell.Copy(center);
        }
    }


}
