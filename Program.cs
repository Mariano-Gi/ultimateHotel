using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.IO;

namespace hotel
{
    public class Program
    {
        public static void Main(string[] args)
        {


            mostrarMenu();
        }

        static void mostrarMenu()
        {

            int contador = 0;
            Huesped[] huespedes = new Huesped[2];

            Console.ForegroundColor = ConsoleColor.DarkYellow;

            Console.Write(@" 

                   ________                 __                     .___       .__            __         .__   
                  /  _____/  ____   _______/  |_  ___________    __| _/____   |  |__   _____/  |_  ____ |  |  
                 /   \  ____/ __ \ /  ___/\   __\/  _ \_  __ \  / __ |/ __ \  |  |  \ /  _ \   __\/ __ \|  |  
                 \    \_\  \  ___/ \___ \  |  | (  <_> )  | \/ / /_/ \  ___/  |   Y  (  <_> )  | \  ___/|  |__
                  \______  /\___  >____  > |__|  \____/|__|    \____ |\___  > |___|  /\____/|__|  \___  >____/
                         \/     \/     \/                           \/    \/       \/                 \/      

                 ");
            barraCarga();
            Thread.Sleep(1100); //1 segundo aprox

            Console.Clear();


            bool salir = false;
            while (!salir)
            {




                Console.ForegroundColor = ConsoleColor.DarkYellow;
                string bordeHorizontal = new string('═', 50);
                Console.WriteLine("╔" + bordeHorizontal + "╗");
                Console.WriteLine("║                 MENU PRINCIPAL                   ║");
                Console.WriteLine("╠" + bordeHorizontal + "╣");

                Console.WriteLine("║ 1) Consultar precios.                            ║");
                Console.WriteLine("║ 2) Consultar habitaciones disponibles.           ║");
                Console.WriteLine("║ 3) Ingresar un cliente.                          ║");
                Console.WriteLine("║ 4) Eliminar un cliente.                          ║");
                Console.WriteLine("║ 5) Mostrar clientes.                             ║");
                Console.WriteLine("║ 0) Salir de la aplicación.                       ║");
                Console.WriteLine("╚" + bordeHorizontal + "╝");

                Console.ResetColor();
                Console.Write("Ingrese la opcion que desea utilizar: ");

                if (int.TryParse(Console.ReadLine(), out int opcion))
                    switch (opcion)
                    {
                        case 0:
                            Console.Clear();
                            Console.ForegroundColor = ConsoleColor.DarkYellow;
                            Console.WriteLine(@"
     ,-.      .             .         .     .                                           
    (   `     | o           |         |     |                                           
     `-.  ,-: | . ,-. ;-. ,-| ,-.   ,-| ,-. |   ;-. ;-. ,-. ,-: ;-. ,-: ;-.-. ,-:       
    .   ) | | | | |-' | | | | | |   | | |-' |   | | |   | | | | |   | | | | | | |       
     `-'  `-` ' ' `-' ' ' `-' `-'   `-' `-' '   |-' '   `-' `-| '   `-` ' ' ' `-` 
                                                '           `-'                         
    ");
                            Thread.Sleep(500);
                            Console.Clear();

                            Console.WriteLine(@"
     ,-.      .             .         .     .                                           
    (   `     | o           |         |     |                                           
     `-.  ,-: | . ,-. ;-. ,-| ,-.   ,-| ,-. |   ;-. ;-. ,-. ,-: ;-. ,-: ;-.-. ,-:       
    .   ) | | | | |-' | | | | | |   | | |-' |   | | |   | | | | |   | | | | | | |       
     `-'  `-` ' ' `-' ' ' `-' `-'   `-' `-' '   |-' '   `-' `-| '   `-` ' ' ' `-` o 
                                                '           `-'                         
    ");
                            Thread.Sleep(500);
                            Console.Clear();

                            Console.WriteLine(@"
     ,-.      .             .         .     .                                           
    (   `     | o           |         |     |                                           
     `-.  ,-: | . ,-. ;-. ,-| ,-.   ,-| ,-. |   ;-. ;-. ,-. ,-: ;-. ,-: ;-.-. ,-:       
    .   ) | | | | |-' | | | | | |   | | |-' |   | | |   | | | | |   | | | | | | |       
     `-'  `-` ' ' `-' ' ' `-' `-'   `-' `-' '   |-' '   `-' `-| '   `-` ' ' ' `-` o o
                                                '           `-'                         
    ");
                            Thread.Sleep(500);
                            Console.Clear();

                            Console.WriteLine(@"
     ,-.      .             .         .     .                                           
    (   `     | o           |         |     |                                           
     `-.  ,-: | . ,-. ;-. ,-| ,-.   ,-| ,-. |   ;-. ;-. ,-. ,-: ;-. ,-: ;-.-. ,-:       
    .   ) | | | | |-' | | | | | |   | | |-' |   | | |   | | | | |   | | | | | | |       
     `-'  `-` ' ' `-' ' ' `-' `-'   `-' `-' '   |-' '   `-' `-| '   `-` ' ' ' `-` o o o
                                                '           `-'                         
    ");
                            salir = true;
                            break;




                        case 3:
                            Console.Clear();
                            contador = AgregarHuesped(huespedes, contador);
                            Console.WriteLine("");
                            Console.WriteLine("Presione enter o ingrese algo para volver");
                            Console.ReadLine();
                            Console.Clear();
                            break;
                        case 4:
                            Console.Clear();
                            contador = eliminarHuesped(huespedes, contador);
                            Console.WriteLine("");
                            Console.WriteLine("Presione enter o ingrese algo para volver");
                            Console.ReadLine();
                            Console.Clear();
                            break;
                        case 5:
                            Console.Clear();
                            MostrarHuespedes(huespedes, contador);
                            Console.WriteLine("");
                            Console.ForegroundColor = ConsoleColor.DarkYellow;
                            Console.WriteLine("Presione enter o ingrese algo para volver");
                            Console.ReadLine();
                            Console.Clear();
                            break;
                    }
            }
        }

        static void barraCarga()
        {
            int total = 55; //longitud de la barra de carga
            Console.WriteLine("Cargando...");

            for (int i = 0; i <= total; i++)
            {
                int porcentaje = (i * 100) / total; //calcula el porcentaje

                string barra = new string('#', i) + new string('-', total - i); //hace la barra

                Console.Write($"\r[{barra}] {porcentaje}%"); //la muestra

                Thread.Sleep(15); //fakea que carga jaj (menos numero = menos tiempo de carga)
            }
        }

        public struct Huesped
        {
            public int Id;
            public string Nombre;
            public string MetodoPago;
            public int NochesEstadia;

            public Huesped(int id, string nombre, string metodoPago, int noches)
            {
                Id = id;
                Nombre = nombre;
                MetodoPago = metodoPago;
                NochesEstadia = noches;
            }

            public void MostrarInfo()
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"ID: {Id}");
                Console.WriteLine($"Nombre: {Nombre}");
                Console.WriteLine($"Método de pago: {MetodoPago}");
                Console.WriteLine($"Noches de estadía: {NochesEstadia}");
                Console.ResetColor();
                Console.WriteLine("---------------------------");
            }
        }


        static int AgregarHuesped(Huesped[] huespedes, int contador)
        {
            if (contador < huespedes.Length)
            {
                Console.Write("ID: ");
                int id = int.Parse(Console.ReadLine());

                Console.Write("Nombre: ");
                string nombre = Console.ReadLine();

                Console.Write("Método de pago: ");
                string metodoPago = Console.ReadLine();

                Console.Write("Noches de estadía: ");
                int noches = int.Parse(Console.ReadLine());

                huespedes[contador] = new Huesped(id, nombre, metodoPago, noches);
                contador++;

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("");
                Console.WriteLine("Huésped agregado con éxito.\n");
                Console.ForegroundColor = ConsoleColor.DarkYellow;

            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("");
                Console.WriteLine("La lista de huéspedes está llena.\n");
                Console.ForegroundColor = ConsoleColor.DarkYellow;
            }

            return contador; // devuelve el nuevo valor del contador para el limite deeee los hurspedes
        }

        //ffuncion para mostrar todos los huespedes
        static void MostrarHuespedes(Huesped[] huespedes, int contador)
        {
            Console.WriteLine("=== Lista de huéspedes ===");
            if (contador == 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("No hay huéspedes registrados.\n");
                Console.ResetColor();
            }
            else
            {
                for (int i = 0; i < contador; i++)
                {
                    huespedes[i].MostrarInfo();
                }
            }
        }

        //el nombre dice que hace pero bue, la funcion elimina huespedes y si no encuentra nada te avisa
        static int eliminarHuesped(Huesped[] huespedes, int contador)
        {
            Console.WriteLine("Ingrese el ID del huésped a eliminar: ");

            int eliminarId = Convert.ToInt32(Console.ReadLine());
            int indice = -1;

            for (int i = 0; i < contador; i++)
            {
                if (huespedes[i].Id == eliminarId)
                {
                    indice = i;
                    break;
                }
            }

            if (indice == -1)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Huésped no encontrado.");
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                return contador;
            }

            for(int i = indice; i < contador - 1; i++)
            {
                huespedes[i] = huespedes[i + 1];
            }

            contador--;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("");
            Console.WriteLine("Huésped eliminado con éxito.");
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            return contador;

        }




    }
}
