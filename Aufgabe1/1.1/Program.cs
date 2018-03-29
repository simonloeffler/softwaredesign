using System;

namespace Aufgabe1
{
    class Program
    {
        static void Main(string[] args)
        {
            try{
                var type = args[0];
                var size = double.Parse(args[1]);
                if(type != null && size != 0)
                    switch(type){
                        case "w":
                            Console.WriteLine(GetCubeInfo(size));
                            break;
                        case "k":
                            Console.WriteLine(GetBallInfo(size));
                            break;
                        case "o":
                            Console.WriteLine(GetOctInfo(size));
                            break;
                    }

            } catch(Exception){
                Console.WriteLine("Eingabe fehlerhaft");
            }

        }

        #region Infos
        public static string GetCubeInfo(double size){
            return "Würfel: A=" + Math.Round(getCubeSurface(size), 2) + " | V=" + Math.Round(getCubeVolume(size), 2);
        }

        public static string GetBallInfo(double size){
            return "Kugel: A=" + Math.Round(getBallSurface(size), 2) + " | V=" + Math.Round(getBallVolume(size), 2);
        }

        public static string GetOctInfo(double size){
            return "Oktaeder: A=" + Math.Round(getOctSurface(size), 2) + " | V=" + Math.Round(getOctVolume(size), 2);
        }

        #endregion

        #region Maths

        public static double getCubeSurface(double size){
            return 6*Math.Pow(size, 2);
        }
        public static double getCubeVolume(double size){
            return Math.Pow(size, 3);
        }

        public static double getBallSurface(double size){
            return Math.PI * Math.Pow(size, 2);
        }
        public static double getBallVolume(double size){
            return (Math.PI * Math.Pow(size, 3))/6;
        }

        public static double getOctSurface(double size){
            return 2*Math.Sqrt(3)*Math.Pow(size, 2);
        }
        public static double getOctVolume(double size){
            return (Math.Sqrt(2)*Math.Pow(size, 3))/3;
        }

        #endregion
    }
}
