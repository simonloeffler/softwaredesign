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
                double surface = 0;
                double volume = 0;
                string info = "";
                if(type != null && size != 0)
                    switch(type){
                        case "w":
                            surface=getCubeSurface(size);
                            volume=getCubeVolume(size);
                            info = GetCubeInfo(surface, volume);
                            break;
                        case "k":
                            surface=getBallSurface(size);
                            volume=getBallVolume(size);
                            info = GetBallInfo(surface, volume);
                            break;
                        case "o":
                            surface=getOctSurface(size);
                            volume=getOctVolume(size);
                            info = GetOctInfo(surface, volume);
                            break;
                    }
                
                Console.WriteLine(info);

            } catch(Exception){
                Console.WriteLine("Eingabe fehlerhaft");
            }

        }

        public static string GetCubeInfo(double surface, double volume){
            return "Würfel: A=" + Math.Round(surface, 2) + " | V=" + Math.Round(volume, 2);
        }

        public static string GetBallInfo(double surface, double volume){
            return "Kugel: A=" + Math.Round(surface, 2) + " | V=" + Math.Round(volume, 2);
        }

        public static string GetOctInfo(double surface, double volume){
            return "Oktaeder: A=" + Math.Round(surface, 2) + " | V=" + Math.Round(volume, 2);
        }

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
    }
}
