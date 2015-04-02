using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IngenieProject
{
    public class Spider
    {
        private Orientation orientation;
        private Coordinate location;
        private Control controller;
        public String movPattern { get; set; }

        public Spider()
        {
            orientation = Orientation.Up;
            location = new Coordinate();
            movPattern = "";
        }

        public Spider(Int32 x, Int32 y, String orientation)
        {
            this.location = new Coordinate(x, y);
            Enum.TryParse(orientation, out this.orientation);
        }
        public Spider(Int32 x, Int32 y, String orientation, Control c)
            : this(x, y, orientation)
        {
            this.controller = c;
        }

        public Spider(Int32 x, Int32 y, String orientation, String movement, Control c) 
            : this(x, y, orientation, c)
        {
            this.movPattern = movement;
        }

        public virtual String Move()
        {
            return this.Move(this.movPattern);
        }

        // If an invalid instrucion is passed, it will simply ignore it and continue with the next
        public virtual String Move(String explorePattern)
        {
            foreach (Char instruction in explorePattern)
            {
                switch (instruction)
                {
                    case 'F' :
                        this.Forward();
                        break;
                    case 'L' :
                        this.TurnLeft();
                        break;
                    case 'R' :
                        this.TurnRight();
                        break;
                    default :

                        break;
                }
            }
            return String.Format("{0} {1} {2}\n", this.location.X, this.location.Y, this.orientation.ToString());
        }

        private void Forward()
        {
            switch (this.orientation)
            {
                case Orientation.Up:
                    if (this.controller.CheckBounds(this.location.X, this.location.Y++))
                        this.location.Y++;
                    break;
                case Orientation.Right:
                    if (this.controller.CheckBounds(this.location.X++, this.location.Y)) 
                        this.location.X++;
                    break;
                case Orientation.Down:
                    if (this.controller.CheckBounds(this.location.X, this.location.Y--)) 
                        this.location.Y--;
                    break;
                case Orientation.Left:
                    if (this.controller.CheckBounds(this.location.X--, this.location.Y)) 
                        this.location.X--;
                    break;
                default:
                    break;
            }
        }

        private void TurnLeft()
        {
            if (this.orientation == Orientation.Up)
                this.orientation = Orientation.Left;
            else
                this.orientation--;
        }

        private void TurnRight()
        {
            if (this.orientation == Orientation.Left)
                this.orientation = Orientation.Up;
            else
                this.orientation++;
        }

        public override bool Equals(object obj)
        {
            if (obj is Spider)
            {
                Spider s = (Spider)obj;
                Boolean orientation = this.orientation == s.orientation;
                Boolean position = this.location.X == s.location.X && this.location.Y == s.location.Y;
                Boolean control = this.controller == s.controller;
                return orientation && position && control;
            }
            else
            {
                return base.Equals(obj);
            }
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }

    public enum Orientation
    {
        Up,
        Right,
        Down,
        Left
    }
}
