using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ProcesacimientoNubePuntos
{
    class CampoVoxeles
    {
        public CampoVoxeles(NubeDePuntos nube, float ladoVoxel)
        {
            this.Nube = nube;
            this.LadoVoxel = ladoVoxel;
            voxeles.Clear();

        }

        public NubeDePuntos Nube{ get; set; }

        public float LadoVoxel { get; set; }
        public float CentroVoxelInicialX
        {
            get { return Nube.MinX + LadoVoxel / 2; }
        }

        public float CentroVoxelInicialY
        {
            get { return Nube.MinY + LadoVoxel / 2; }
        }

        public float CentroVoxelInicialZ
        {
            get { return Nube.MinZ + LadoVoxel / 2; }
        }

        public List<Voxel> voxeles = new List<Voxel>();

        public void GenerarNube()
        {
            Console.WriteLine("¿Como quieres que se llame la nube de puntos?");
            string nombreFile = Console.ReadLine();
            string path = @"C:\Users\3DCOVIM-Station 2\Documents\Desarrollo\Nubes de puntos\Scripts MatLab\" + nombreFile+ ".txt";
            using (StreamWriter sw = File.CreateText(path))
            {
                sw.WriteLine("X Y Z T1 T2 T3");
                foreach(Voxel voxel in voxeles)
                {
                    float temp1 = 0; //Valor para la media de la temperatura de los puntos de la nube 1 que están en el Voxel
                    float temp2 = 0;
                    float temp3 = 0;
                    int contadorPuntosNube1 = 0; //Cuenta el número de puntos de la nube 1 en el Voxel
                    int contadorPuntosNube2 = 0;
                    int contadorPuntosNube3 = 0;

                    foreach (Punto punto in voxel.Puntos)
                    {
                        if (punto.IndexNube == 0)
                        {
                            temp1 = punto.Temperatura + temp1;
                            contadorPuntosNube1++;
                        }
                        else if(punto.IndexNube == 1)
                        {
                            temp2 = punto.Temperatura + temp2;
                            contadorPuntosNube2++;
                        }
                        else if (punto.IndexNube == 2)
                        {
                            temp3 = punto.Temperatura + temp3;
                            contadorPuntosNube3++;
                        }
                    }
                    temp1 = temp1 / contadorPuntosNube1;
                    temp2 = temp2 / contadorPuntosNube2;
                    temp3 = temp3 / contadorPuntosNube3;


                    sw.WriteLine($"{voxel.Centro.X} {voxel.Centro.Y} {voxel.Centro.Z} {temp1} {temp2} {temp3}");
                }
            }
            Console.WriteLine("Nube creada");
            Console.WriteLine($"La nube tiene {voxeles.Count} puntos.");
        }
    }
}
