using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IngenieProject
{
    public class Control
    {
        // The class has been designed to support just one wall and multiple spiders on the same wall
        // this could be modified adding a class which is a Wall-List<Spider> pair
        private List<Spider> spiders;
        private List<String> outputList;
        private Wall thisWall;

        public Control()
        {
            this.spiders = new List<Spider>();
            this.outputList = new List<String>();
        }

        public Control(Int32 x, Int32 y)
            :this()
        {
            this.thisWall = new Wall(x, y);
        }

        // If any of the inputs are not as expected, this function will raise a FormatException
        // Any input has been made case insensitive, since this function will format it the way it is required
        public void ParseInput(String input)
        {
            String[] instructions = input.Split('\n');

            //If the length of the array is even, it means that the input either ended on \n and the last element is null
            // or the las element is not complete, solo I trim it
            if (instructions.Length % 2 == 0)
            {
                instructions = instructions.Take(instructions.Count() - 1).ToArray();
            }

            String[] wallSize = instructions[0].Split(' ');
            if (wallSize.Length != 2)
                throw new FormatException("Wall size format incorrect");
            this.thisWall = new Wall(Int32.Parse(wallSize[0]), Int32.Parse(wallSize[1]));

            String[] spider;
            String movement;
            for (int i = 1; i < instructions.Length; i += 2)
            {
                spider = instructions[i].Split(' ');
                if (spider.Length != 3)
                {
                    throw new FormatException("Spider format incorrect");
                }
                movement = instructions[i + 1].Replace(" ", "").ToUpper();

                spiders.Add(new Spider(
                    Int32.Parse(spider[0]),
                    Int32.Parse(spider[1]),
                    this.DirectionFormat(spider[2]),
                    movement,
                    this));
            }
        }

        //In case there is an error typing the direction, this function will try to fix it
        // Otherwise will throw a FormatException
        private String DirectionFormat(String direction)
        {
            if (string.IsNullOrEmpty(direction))
            {
                return string.Empty;
            }
            String[] directionOptions = { "right", "left", "up", "down" };

            if (directionOptions.Contains(direction.ToLower()))
            {
                char[] a = direction.ToCharArray();
                a[0] = char.ToUpper(a[0]);
                return new string(a);
            } 
            else
            {
                throw new FormatException("Direction incorrect");
            }
        }

        // Each spider will complete their moves once at the time. It could be modified so every spider will 
        // make one step at the time modifying the movement list inside the spiders and turning it into a stack
        // and it would continue until no spider wants to make any movement.
        // I decided this approach because it was simpler and quicker.
        private void MoveSpiders()
        {
            foreach (Spider s in spiders)
            {
                outputList.Add(s.Move());
            }
        }

        //The only limitation of movement for a Spider is the limits of the wall. If any other limitation is required 
        // such as two spiders can't be on the same position, it should be added here.
        public Boolean CheckBounds(Int32 futureX, Int32 futureY)
        {
            return futureX > thisWall.top &&
                futureX < 0 &&
                futureY > thisWall.right &&
                futureY < 0;
        }

        public String GetOutput()
        {
            StringBuilder output = new StringBuilder();

            this.MoveSpiders();

            foreach (String spiderOutput in this.outputList)
            {
                output.Append(spiderOutput);
            }

            return output.ToString();
        }
    }
}
