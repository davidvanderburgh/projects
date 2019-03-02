using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using WordPlay.Classes;

// Here is a random grid of letters that is 10x10.
// Picking a starting location and continually moving to one adjacent square (including diagonals and potentially 
//  reusing the same letter twice in one word if you can get back to it), what's the longest word you can spell?

// A E S K L N A O P O
// E N F D E K L I E L
// A N A C U K T T T G
// I O N D O H A I M L
// T N O S S T L I L O
// E B U E D D I C O O
// S G I O A D I A M N
// L S D V M Y K E E E
// R E A B O L M A L N
// Y S C S A S R Y E O

//TODO: description of WordFind



//write code enjoyably advance tech industry excite ideas



namespace Wordplay
{
    class Program
    {
        static void Main(string[] args)
        {
            UserInterface userInterface = new UserInterface();
            userInterface.Run();
        }
    }
}
