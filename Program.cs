namespace ProyectoHotel
{
    using System;
    using System.Collections.Generic;
    using System.Threading;

    public class Program
    {
        // Matriz de disponibilidad: 20 habitaciones x 7 días
        static bool[,] disponibilidad = new bool[20, 7];

        public static void Main(string[] args)
        {
            MostrarMenu();
        }

        static void MostrarMenu()
        {
            int contador = 0;
            Huesped[] huespedes = new Huesped[100];
            char continuar = 's'; // Variable char para control
            bool aplicacionActiva = true; // Variable bool

            Console.ForegroundColor = ConsoleColor.DarkYellow;

            Console.Write(@"
                   ________                 __                     .___       .__            __         .__   
                  /  _____/  ____   _______/  |_  ___________    __| _/____   |  |__   _____/  |_  ____ |  |  
                 /   \  ____/ __ \ /  ___/\   __\/  _ \_  __ \  / __ |/ __ \  |  |  \ /  _ \   __\/ __ \|  |  
                 \    \_\  \  ___/ \___ \  |  | (  <_> )  | \/ / /_/ \  ___/  |   Y  (  <_> )  | \  ___/|  |__
                  \______  /\___  >____  > |__|  \____/|__|    \____ |\___  > |___|  /\____/|__|  \___  >____/
                         \/     \/     \/                           \/    \/       \/                 \/      

                 ");
            BarraCarga();
            Thread.Sleep(1100);

            Console.Clear();

            while (aplicacionActiva)
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
                Console.WriteLine("║ 6) Modificar cliente.                            ║");
                Console.WriteLine("║ 7) Salir de la aplicación.                       ║");
                Console.WriteLine("╚" + bordeHorizontal + "╝");

                Console.ResetColor();
                Console.Write("Ingrese la opcion que desea utilizar: ");

                if (int.TryParse(Console.ReadLine(), out int opcion))
                {
                    switch (opcion)
                    {
                        case 1:
                            Console.Clear();
                            ConsultarPrecios();
                            Console.WriteLine("\nPresione Enter para volver.");
                            Console.ReadLine();
                            Console.Clear();
                            break;

                        case 2:
                            Console.Clear();
                            MostrarDisponibilidad();
                            Console.WriteLine("\nPresione Enter para volver.");
                            Console.ReadLine();
                            Console.Clear();
                            break;

                        case 3:
                            Console.Clear();
                            contador = AgregarHuesped(huespedes, contador);
                            Console.WriteLine("\nPresione Enter para volver.");
                            Console.ReadLine();
                            Console.Clear();
                            break;

                        case 4:
                            Console.Clear();
                            contador = EliminarHuesped(huespedes, contador);
                            Console.WriteLine("\nPresione Enter para volver.");
                            Console.ReadLine();
                            Console.Clear();
                            break;

                        case 5:
                            Console.Clear();
                            MostrarHuespedes(huespedes, contador);
                            Console.WriteLine("\nPresione Enter para volver.");
                            Console.ReadLine();
                            Console.Clear();
                            break;

                        case 6:
                            Console.Clear();
                            ModificarHuesped(huespedes, contador);
                            Console.WriteLine("\nPresione Enter para volver.");
                            Console.ReadLine();
                            Console.Clear();
                            break;

                        case 7:
                            Console.Clear();
                            aplicacionActiva = false;
                            MostrarAnimacionSalida();
                            break;

                        default:
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Opción inválida.");
                            Console.ResetColor();
                            Thread.Sleep(1500);
                            Console.Clear();
                            break;
                    }
                }
            }
        }

        static void BarraCarga()
        {
            int total = 55;
            Console.WriteLine("Cargando...");

            for (int i = 0; i <= total; i++)
            {
                int porcentaje = (i * 100) / total;
                string barra = new string('#', i) + new string('-', total - i);
                Console.Write($"\r[{barra}] {porcentaje}%");
                Thread.Sleep(15);
            }
            Console.WriteLine();
        }

        static void ConsultarPrecios()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("=== PRECIOS DE HABITACIONES ===\n");


            decimal precioEconomica = decimal.Parse("35000");
            decimal precioEstandar = decimal.Parse("45000");
            decimal precioDeluxe = decimal.Parse("50000");

            Console.WriteLine($"1. Económica: ${precioEconomica} por día");
            Console.WriteLine("   Incluye: Estadía y servicio a la habitación\n");
            Console.WriteLine($"2. Estándar: ${precioEstandar} por día");
            Console.WriteLine("   Incluye: Estadía, servicio a la habitación y 50% descuento en desayuno\n");
            Console.WriteLine($"3. Deluxe: ${precioDeluxe} por día");
            Console.WriteLine("   Incluye: Estadía, servicio a la habitación y desayuno\n");
            Console.ResetColor();
        }

        static void MostrarDisponibilidad()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("=== DISPONIBILIDAD SEMANAL ===");
            Console.WriteLine("(Habitaciones 1 a 20, días 1 a 7)\n");

            for (int i = 0; i < 20; i++)
            {
                Console.Write($"Habitación {i + 1}: ");
                for (int j = 0; j < 7; j++)
                {
                    Console.Write(disponibilidad[i, j] ? "[Ocupada] " : "[Libre] ");
                }
                Console.WriteLine();
            }
            Console.ResetColor();
        }

        public struct Huesped
        {
            // camelCase correcto para las propiedades
            public int id;
            public string nombre;
            public int numeroHabitacion;
            public string claseHabitacion;
            public int nochesEstadia;
            public string metodoPago;
            public decimal precioTotal;

            public Huesped(int id, string nombre, int numeroHabitacion, string claseHabitacion, int nochesEstadia, string metodoPago, decimal precioTotal)
            {
                this.id = id;
                this.nombre = nombre;
                this.numeroHabitacion = numeroHabitacion;
                this.claseHabitacion = claseHabitacion;
                this.nochesEstadia = nochesEstadia;
                this.metodoPago = metodoPago;
                this.precioTotal = precioTotal;
            }

            public void MostrarInfo()
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"ID: {id}");
                Console.WriteLine($"Nombre: {nombre}");
                Console.WriteLine($"Habitación: {numeroHabitacion}");
                Console.WriteLine($"Clase de habitación: {claseHabitacion}");
                Console.WriteLine($"Noches de estadía: {nochesEstadia}");
                Console.WriteLine($"Método de pago: {metodoPago}");
                Console.WriteLine($"Precio Total: ${precioTotal}");
                Console.ResetColor();
                Console.WriteLine("---------------------------");
            }
        }

        static int AgregarHuesped(Huesped[] huespedes, int contador)
        {
            if (contador < huespedes.Length)
            {
                Console.WriteLine("=== AGREGAR NUEVO CLIENTE ===\n");

                Console.Write("ID: ");
                if (!int.TryParse(Console.ReadLine(), out int id))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("ID inválido.");
                    Console.ResetColor();
                    return contador;
                }

                Console.Write("Nombre: ");
                string nombre = Console.ReadLine();

                Console.Write("Número de habitación (1-20): ");
                if (!int.TryParse(Console.ReadLine(), out int numeroHabitacion) || numeroHabitacion < 1 || numeroHabitacion > 20)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Número de habitación inválido.");
                    Console.ResetColor();
                    return contador;
                }

                Console.WriteLine("\nTipos de habitación:");
                Console.WriteLine("1. Económica ($35000)");
                Console.WriteLine("2. Estándar ($45000)");
                Console.WriteLine("3. Deluxe ($50000)");
                Console.Write("Seleccione el tipo: ");

                string claseHabitacion = "";
                decimal precioBase = 0;

                if (int.TryParse(Console.ReadLine(), out int tipoHab))
                {
                    switch (tipoHab)
                    {
                        case 1:
                            claseHabitacion = "Economica";
                            precioBase = decimal.Parse("35000");
                            break;
                        case 2:
                            claseHabitacion = "Estandar";
                            precioBase = decimal.Parse("45000");
                            break;
                        case 3:
                            claseHabitacion = "Deluxe";
                            precioBase = decimal.Parse("50000");
                            break;
                        default:
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Tipo inválido.");
                            Console.ResetColor();
                            return contador;
                    }
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Entrada inválida.");
                    Console.ResetColor();
                    return contador;
                }

                Console.Write("Noches de estadía: ");
                if (!int.TryParse(Console.ReadLine(), out int nochesEstadia) || nochesEstadia < 1)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Noches inválidas.");
                    Console.ResetColor();
                    return contador;
                }

                Console.Write("Método de pago: ");
                string metodoPago = Console.ReadLine();

                decimal precioTotal = precioBase * nochesEstadia;

                huespedes[contador] = new Huesped(id, nombre, numeroHabitacion, claseHabitacion, nochesEstadia, metodoPago, precioTotal);
                contador++;

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"\nHuésped agregado con éxito.");
                Console.WriteLine($"Precio total: ${precioTotal}\n");
                Console.ForegroundColor = ConsoleColor.DarkYellow;

                if (numeroHabitacion >= 1 && numeroHabitacion <= 20)
                {
                    for (int d = 0; d < nochesEstadia && d < 7; d++)
                    {
                        disponibilidad[numeroHabitacion - 1, d] = true;
                    }
                }
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nLa lista de huéspedes está llena.\n");
                Console.ForegroundColor = ConsoleColor.DarkYellow;
            }

            return contador;
        }

        static int EliminarHuesped(Huesped[] huespedes, int contador)
        {
            if (contador == 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("No hay huéspedes registrados.");
                Console.ResetColor();
                return contador;
            }

            Huesped[] huespedesCopia = (Huesped[])huespedes.Clone();

            // Ordenamiento por burbujeo usando el ID 
            for (int i = 0; i < contador - 1; i++)
            {
                for (int j = 0; j < contador - i - 1; j++)
                {
                    if (huespedesCopia[j].id > huespedesCopia[j + 1].id)
                    {
                        Huesped temp = huespedesCopia[j];
                        huespedesCopia[j] = huespedesCopia[j + 1];
                        huespedesCopia[j + 1] = temp;
                    }
                }
            }

            Console.WriteLine("=== ELIMINAR CLIENTE ===\n");
            Console.Write("Ingrese el ID del huésped a eliminar: ");
            if (!int.TryParse(Console.ReadLine(), out int eliminarId))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("ID inválido.");
                Console.ResetColor();
                return contador;
            }

            // Búsqueda binaria
            int inicio = 0, fin = contador - 1;
            bool encontrado = false;
            int indice = -1;

            while (inicio <= fin && !encontrado)
            {
                int medio = (inicio + fin) / 2;

                if (huespedesCopia[medio].id == eliminarId)
                {
                    encontrado = true;
                    indice = medio;
                }
                else if (huespedesCopia[medio].id < eliminarId)
                {
                    inicio = medio + 1;
                }
                else
                {
                    fin = medio - 1;
                }
            }

            if (!encontrado)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Huésped no encontrado.");
                Console.ResetColor();
                return contador;
            }

            // Buscar índice real en arreglo original
            int indiceReal = -1;
            for (int i = 0; i < contador; i++)
            {
                if (huespedes[i].id == huespedesCopia[indice].id)
                {
                    indiceReal = i;
                    break;
                }
            }


            // Eliminar desplazando
            for (int i = indiceReal; i < contador - 1; i++)
            {
                huespedes[i] = huespedes[i + 1];
            }

            contador--;

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\nHuésped eliminado con éxito.");
            Console.ForegroundColor = ConsoleColor.DarkYellow;

            return contador;
        }

        static void MostrarHuespedes(Huesped[] huespedes, int contador)
        {
            Console.WriteLine("=== LISTA DE HUÉSPEDES (ORDENADA POR NOMBRE) ===\n");

            if (contador == 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("No hay huéspedes registrados.\n");
                Console.ResetColor();
                return;
            }

            Huesped[] huespedesOrdenados = new Huesped[contador];
            for (int i = 0; i < contador; i++)
            {
                huespedesOrdenados[i] = huespedes[i];
            }

            // Ordenamiento burbuja por nombre
            for (int i = 0; i < contador - 1; i++)
            {
                for (int j = 0; j < contador - i - 1; j++)
                {
                    if (string.Compare(huespedesOrdenados[j].nombre, huespedesOrdenados[j + 1].nombre, StringComparison.OrdinalIgnoreCase) > 0)
                    {
                        Huesped temp = huespedesOrdenados[j];
                        huespedesOrdenados[j] = huespedesOrdenados[j + 1];
                        huespedesOrdenados[j + 1] = temp;
                    }
                }
            }

            for (int i = 0; i < contador; i++)
            {
                huespedesOrdenados[i].MostrarInfo();
            }
        }

        static void ModificarHuesped(Huesped[] huespedes, int contador)
        {
            if (contador == 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("No hay huéspedes registrados.");
                Console.ResetColor();
                return;
            }

            Console.WriteLine("=== MODIFICAR CLIENTE ===\n");
            Console.Write("Ingrese el ID del huésped a modificar: ");

            if (!int.TryParse(Console.ReadLine(), out int idBusqueda))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("ID inválido.");
                Console.ResetColor();
                return;
            }

            int indice = -1;
            for (int i = 0; i < contador; i++)
            {
                if (huespedes[i].id == idBusqueda)
                {
                    indice = i;
                    break;
                }
            }

            if (indice == -1)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Huésped no encontrado.");
                Console.ResetColor();
                return;
            }

            Console.WriteLine("\n¿Qué desea modificar?");
            Console.WriteLine("1. Nombre");
            Console.WriteLine("2. Número de habitación");
            Console.WriteLine("3. Noches de estadía");
            Console.WriteLine("4. Método de pago");
            Console.Write("Seleccione: ");

            if (int.TryParse(Console.ReadLine(), out int opcionModificar))
            {
                switch (opcionModificar)
                {
                    case 1:
                        Console.Write("Nuevo nombre: ");
                        huespedes[indice].nombre = Console.ReadLine();
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("Nombre modificado con éxito.");
                        Console.ResetColor();
                        break;

                    case 2:
                        Console.Write("Nuevo número de habitación (1-20): ");
                        if (int.TryParse(Console.ReadLine(), out int nuevoNumero) && nuevoNumero > 0 && nuevoNumero <= 20)
                        {
                            bool disponible = true;
                            //verifica si la nueva habitacion esta disponible para las noches de estadia del huesped
                            for (int i = 0; i < huespedes[indice].nochesEstadia && i < 7; i++)
                            {
                                if (disponibilidad[nuevoNumero - 1, i])
                                {
                                    disponible = false;
                                    break;
                                }
                            }
                            //si no esta disponible, muestra un mensaje y sale
                            if (!disponible)
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("La habitación esta ocupada.");
                                Console.ResetColor();
                                break;
                            }

                            //Loopea por la cnatidad de días que el huesped se va a quedar y libera la habitación anterior
                            //(pero no mas de 7 veces porque, obviamente, solo hay 7 dias en la semana)
                            //Como la matriz es bool, se pone en false para resetear los valores a libre
                            for (int i = 0; i < huespedes[indice].nochesEstadia && i < 7; i++)
                            {
                                disponibilidad[huespedes[indice].numeroHabitacion - 1, i] = false;
                            }

                            //Actualiza el numeero de habitación
                            huespedes[indice].numeroHabitacion = nuevoNumero;

                            //loopea de vuelta por la cantidad de días que el huesped se va a quedar y ocupa la nueva habitación
                            for (int i = 0; i < huespedes[indice].nochesEstadia && i < 7; i++)
                            {
                                disponibilidad[nuevoNumero - 1, i] = true;
                            }
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine("Habitación modificada con éxito.");
                            Console.ResetColor();
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Número inválido.");
                            Console.ResetColor();
                        }
                        break;

                    case 3:
                        Console.Write("Nuevas noches: ");
                        if (int.TryParse(Console.ReadLine(), out int nuevasNoches) && nuevasNoches > 0)
                        {
                            huespedes[indice].nochesEstadia = nuevasNoches;
                            decimal precioUnitario = ObtenerPrecioHabitacion(huespedes[indice].claseHabitacion);
                            huespedes[indice].precioTotal = precioUnitario * nuevasNoches;
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine($"Noches modificadas con éxito. Nuevo precio: ${huespedes[indice].precioTotal}");
                            Console.ResetColor();
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Número inválido.");
                            Console.ResetColor();
                        }
                        break;

                    case 4:
                        Console.Write("Nuevo método de pago: ");
                        huespedes[indice].metodoPago = Console.ReadLine();
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("Método de pago modificado con éxito.");
                        Console.ResetColor();
                        break;

                    default:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Opción inválida.");
                        Console.ResetColor();
                        break;
                }
            }
        }

        static void BuscarHuespedPorNombre(Huesped[] huespedes, int contador)
        {
            Console.WriteLine("=== BUSCAR CLIENTE ===\n");
            Console.Write("Ingrese el nombre a buscar: ");
            string nombreBuscar = Console.ReadLine();

            bool encontroAlguien = false;

            // Usar while para buscar en el arreglo
            int i = 0;
            while (i < contador)
            {
                if (huespedes[i].nombre.ToLower().Contains(nombreBuscar.ToLower()))
                {
                    if (!encontroAlguien)
                    {
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.WriteLine($"\nResultados encontrados:\n");
                        encontroAlguien = true;
                    }
                    huespedes[i].MostrarInfo();
                }
                i++;
            }

            if (!encontroAlguien)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("No se encontraron resultados.");
                Console.ResetColor();
            }
            else
            {
                Console.ResetColor();
            }
        }

        static decimal ObtenerPrecioHabitacion(string claseHabitacion)
        {
            switch (claseHabitacion)
            {
                case "Economica":
                    return decimal.Parse("35000");
                case "Estandar":
                    return decimal.Parse("45000");
                case "Deluxe":
                    return decimal.Parse("50000");
                default:
                    return 0m;
            }
        }

        static void MostrarAnimacionSalida()
        {
            string[] animaciones = new[] { "o", "o o", "o o o" };

            for (int i = 0; i < animaciones.Length; i++)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine(@"
     ,-.      .             .         .     .                                           
    (   `     | o           |         |     |                                           
     `-.  ,-: | . ,-. ;-. ,-| ,-.   ,-| ,-. |   ;-. ;-. ,-. ,-: ;-. ,-: ;-.-. ,-:       
    .   ) | | | | |-' | | | | | |   | | |-' |   | | |   | | | | |   | | | | | | |       
     `-'  `-` ' ' `-' ' ' `-' `-'   `-' `-' '   |-' '   `-' `-| '   `-` ' ' ' `-` " + animaciones[i] + @"
                                                '           `-'                         
    ");
                Thread.Sleep(500);
            }
            Console.Clear();
        }
    }
}
