using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace PROYECTO_PROGRA
{
    internal class Program
    {
        static int tableWidth = Console.WindowWidth;
        static void Main(string[] args)
        {
            string NombreOficial;
            string fecha_de_creacion;
            string telefono;
            string ganancia;
            string direccion;
            string dueño;
            string HOMOCLAVE,RFC;
            int opciones;

            do
            {
                Console.Clear();
                Console.WriteLine("Menu");
                Console.WriteLine("1. INFORMACION DE  EMPRESAS");
                Console.WriteLine("2. LEER LA INFORMACION");
                Console.WriteLine("3. ELIMINAR EMPRESA");
                Console.WriteLine("4. SALIR");
                Console.Write("QUE DESEA HACEER?  ");
                opciones = Int32.Parse(Console.ReadLine());
                switch (opciones)
                {
                    case 1:
                        Console.Clear();
                        Console.Write("Escribe el nombre oficial de la empresa: ");
                        NombreOficial = Console.ReadLine();
                        Console.Write("Escribe la direccion: ");
                        direccion = Console.ReadLine();
                        Console.Write("Escribe  la fecha de creacion (AA/MM/DD): ");
                        fecha_de_creacion = Console.ReadLine();
                        Console.Write("Escribe N de telefono: ");
                        telefono = (Console.ReadLine());
                        Console.Write("Escribe EL DUEÑO DE LA EMPRESA: ");
                        dueño = Console.ReadLine();
                        Console.Write("Escribe la ganancia anual: ");
                        ganancia = Console.ReadLine();
                        Console.Write("Escribe  la homoclave de la empresa: ");
                        HOMOCLAVE = Console.ReadLine();

                        StreamWriter sw = new StreamWriter("..prograproyec.txt", true, Encoding.ASCII);
                        sw.WriteLine(NombreOficial);
                        sw.WriteLine(direccion);
                        sw.WriteLine(fecha_de_creacion + "/");
                        sw.WriteLine(telefono);
                        sw.WriteLine(dueño);
                        sw.WriteLine(ganancia);
                        sw.WriteLine(HOMOCLAVE);
                        sw.Close();
                        Console.ReadKey();
                        break;

                    case 2:

                        StreamReader sr = new StreamReader("..prograproyec.txt");

                        NombreOficial = sr.ReadLine();
                        direccion = sr.ReadLine();
                        fecha_de_creacion = sr.ReadLine();
                        telefono = sr.ReadLine();
                        dueño = sr.ReadLine();
                        ganancia = sr.ReadLine();
                        HOMOCLAVE = sr.ReadLine();

                        PrintLine();
                        PrintRow("NOMBRE", "FECHA", "UBICACION", "CONTACTO", "DUEÑO", "GANANCIA", "HOMOCLAVE","RFC");
                        PrintLine();

                        while (NombreOficial != null)
                        {
                            RFC = "";
                            string[] nombre = NombreOficial.Split();
                            int num = 0;
                            List<string> list = new List<string>();
                            for (int i = 0; i < nombre.Length; i++)
                            {
                                if (nombre[i].Length > 2) 
                                {
                                    list.Add(nombre[i]);
                                    num++;
                                }
                            }
                            switch(num)
                            {
                                case 1:
                                    foreach(string nom in list) 
                                    {
                                        RFC += nom.Substring(0, 3); 
                                    }
                                    break;
                                case 2:
                                    int primero= 0;
                                    int segundo = 0;
                                    foreach (string nom in list)
                                    {
                                        segundo++;
                                        if(segundo==2)
                                        {
                                            RFC += nom.Substring(0, 2);
                                        }
                                    }
                                    foreach(string nom in list)
                                    {
                                        primero++;
                                        if (primero==1)
                                        {
                                            RFC += nom.Substring(0, 1);
                                        }
                                    }
                                    break;
                                default:

                                    int prim = 0;
                                    int seg = 0;
                                    int ter = 0;
                                    foreach (string nom in list)
                                    {
                                        seg++;
                                        if (seg == 2)
                                        {
                                            RFC += nom.Substring(0, 1);
                                        }
                                    }
                                    foreach (string nom in list)
                                    {
                                        ter++;
                                        if (ter == 3)
                                        {
                                            RFC += nom.Substring(0, 1);
                                        }
                                    }
                                    foreach (string nom in list)
                                    {
                                        prim++;
                                        if (prim == 1)
                                        {
                                            RFC += nom.Substring(0, 1);
                                        }
                                    }
                                    break;
                            }
                            string[] arreglo = fecha_de_creacion.Split('/');
                            for (int i = 0; i < arreglo.Length; i++)
                            {
                                RFC += arreglo[i];
                            }
                            RFC = (RFC + HOMOCLAVE).ToUpper();
                            PrintRow(NombreOficial, fecha_de_creacion, direccion, telefono, dueño, ganancia, HOMOCLAVE,RFC);
                            PrintLine();


                            NombreOficial = sr.ReadLine();
                            direccion = sr.ReadLine();
                            fecha_de_creacion = sr.ReadLine();
                            telefono = sr.ReadLine();
                            dueño = sr.ReadLine();
                            ganancia = sr.ReadLine();
                            HOMOCLAVE = sr.ReadLine();

                        }
                        sr.Close();
                        Console.ReadKey();
                        break;
                    case 3:
                        Console.WriteLine("Que empresa desea eliminar");
                        string eliminar = Console.ReadLine();
                        string tempFile = Path.GetTempFileName();
                        StreamReader  lector = new StreamReader("..prograproyec.txt");
                        using (StreamWriter escritor = new StreamWriter(tempFile))
                        {
                            string line;

                            while ((line = lector.ReadLine()) != null)
                            {
                                if (line != eliminar)
                                {
                                    escritor.WriteLine(line);
                                }
                                else
                                {
                                    lector.ReadLine();
                                    lector.ReadLine();
                                    lector.ReadLine();
                                    lector.ReadLine();
                                    lector.ReadLine();
                                    lector.ReadLine();
                                }
                            }
                        }
                        lector.Close();
                        File.Delete("..prograproyec.txt");
                        File.Move(tempFile, "..prograproyec.txt");
                        break;
                    default:
                        break;

                }
            } while (opciones != 4);


            Console.ReadKey();

        }
        static void PrintLine()
        {
            Console.WriteLine(new String('-', tableWidth));
        }
        static void PrintRow(params string[] columns)
        {
            int width = (tableWidth - columns.Length) / columns.Length;
            string row = "|";
            foreach (string column in columns)
            {
                row += Aligncenter(column, width) + "|";
            }
            Console.WriteLine(row);

        }
        static string Aligncenter(string text, int width)
        {
            text = text.Length > width ? text.Substring(0, width - 3) + "..." : text;
            if (string.IsNullOrEmpty(text))
            {
                return new string(' ', width);
            }
            else
            {
                return text.PadRight(width - (width - text.Length) / 2).PadLeft(width);
            }
        }
    }
}




