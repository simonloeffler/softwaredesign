using System;

namespace L02
{
    class Program
    {
        static void Main(string[] args)
        {

            double d = 15;

            public double getCubeSurface(double d)
            {
                double cubeSurface = d*d*6;
                return cubeSurface;
            }

            public double getCubeVolume(double d)
            {
                double cubeVolume = d*d*d;
                return cubeSurface;
            }

            public double getGlobeSurface(double d)
            {
                double globeSurface = Math.PI*d*d;
                return globeSurface;
            }

            public double getglobeVolume(double d)
            {
                double globeVolume = Math.PI*(d*d*d)/6;
                return globeVolume;
            }

            public double getOctahedronSurface(double d)
            {
                double octahedronSurface = 2*(Math.Sqrt(3))*d*d;
                return octahedronSurface;
            }

            public double getOctahedronVolume(double d)
            {
                double octahedronVolume = (Math.Sqrt(2))*(d*d*d)/3;
                return octahedronVolume;
            }

            public string getCubeInfo(double getCubeSurface, double getCubeVolume){
                string cubeInfo = ("Cube: " + "A:" + getCubeSurface.ToString("#.##") + "|" + "V:" + getCubeVolume.ToString("#.##"));
                Console.WriteLine(cubeInfo);
            }

            public string getGlobeInfo(double getGlobeSurface, double getGlobeVolume){
                string globeInfo = ("Cube: " + "A:" + getGlobeSurface.ToString("#.##") + "|" + "V:" + getGlobeVolume.ToString("#.##"));
                Console.WriteLine(globeInfo);
            }

            public string getOctahedronInfo(double getOctahedronSurface, double getOctahedronVolume){
                string octahedronInfo = ("Octahedron: " + "A:" + getOctahedronSurface.ToString("#.##") + "|" + "V:" + getOctahedronVolume.ToString("#.##"));
                Console.WriteLine(octahedronInfo)
            }

            if (args[0] == "w")
            {
                getCubeInfo;
            };
            if (args[0] == "k")
            {
                getGlobeInfo;
            };
            if (args[0] == "o")
            {
                getOctahedronInfo;
            }
            else if (args[0] == "a,b,c,d,e,f,h,i,j,l,m,n,p,q,r,s,t,u,v,x,y,z,")
            {
            Console.WriteLine("Falsche Eingabe");
            }
        }
    }
}
