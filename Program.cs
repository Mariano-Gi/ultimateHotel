using System;
using System.Collections.Generic;
using System.Threading;
using static ultimateHotel.Program;

namespace ultimateHotel
{

    public class Program
    {
        // Matriz de disponibilidad: 20 habitaciones x 7 días
        static bool[,] disponibilidad = new bool[20, 7];

        public static void Main(string[] args)
        {
            int contador = 0, opcionMenu = 0;
            Huesped[] huespedes = new Huesped[100];
            bool aplicacionActiva = true; // Variable bool


            InicioPrograma();

            while (aplicacionActiva)
            {
                mostrarMenu();

                opcionMenu = intParseo(Console.ReadLine(), false, "Opcion de menu no valida, vuelva a intentarlo: ");

                switch (opcionMenu)
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
                        BuscarHuespedPorNombre(huespedes, contador);
                        Console.WriteLine("\nPresione Enter para volver.");
                        Console.ReadLine();
                        Console.Clear();
                        break;

                    case 8:
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

        static int intParseo(string texto, bool borrarMensaje, string mensaje = "Entrada no valida, intente de nuevo: ")
        {
            int numero = -1;
            bool validez = false;
            do
            {
                if (!string.IsNullOrWhiteSpace(texto) && int.TryParse(texto, out numero))
                {
                    Console.Clear();
                    numero = int.Parse(texto);
                    validez = true;
                }
                else
                {
                    if (borrarMensaje)
                    {
                        Console.Clear();
                    }
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write(mensaje);
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    texto = Console.ReadLine();
                    Console.ResetColor();
                }
            } while (!validez);
            return numero;
        }

        static string stringParseo(string texto, bool borrarMensaje, string mensaje = "Entrada no valida, intente de nuevo: ")
        {
            string palabra = "";
            bool validez = false;
            do
            {
                if (!string.IsNullOrWhiteSpace(texto))
                {
                    Console.Clear();
                    palabra = texto;
                    validez = true;
                }
                else
                {
                    if (borrarMensaje)
                    {
                        Console.Clear();
                    }
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write(mensaje);
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    texto = Console.ReadLine();
                    Console.ResetColor();
                }
            } while (!validez);
            return palabra;
        }

        static void mostrarMenu()
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
            Console.WriteLine("║ 7) Buscar huesped por nombre.                    ║");
            Console.WriteLine("║ 8) Salir de la aplicación.                       ║");
            Console.WriteLine("╚" + bordeHorizontal + "╝");

            Console.ResetColor();
            Console.Write("Ingrese la opcion que desea utilizar: ");
        }
        static void InicioPrograma()
        {

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
        }

        static void BarraCarga()
        {
            int total = 55;
            Console.WriteLine("Cargando...");

            for (int i = 0; i <= total; i++)
            {
                int porcentaje = (i * 100) / total;
                string barra = new string('█', i) + new string('░', total - i);
                Console.Write($"\r[{barra}] {porcentaje}%");
                Thread.Sleep(15);
            }
            Console.WriteLine();
        }

        static void ConsultarPrecios()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("=== PRECIOS DE HABITACIONES ===\n");


            decimal precioEconomica = 35000;
            decimal precioEstandar = 45000;
            decimal precioDeluxe = 50000;

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

            public int dni;
            public string nombre;
            public int numeroHuespedes;
            public int numeroHabitacion;
            public string claseHabitacion;
            public int nochesEstadia;
            public string metodoPago;
            public decimal precioTotal;

            public Huesped(int dni, string nombre, int numeroHuespedes, int numeroHabitacion, string claseHabitacion, int nochesEstadia, string metodoPago, decimal precioTotal)
            {
                this.dni = dni;
                this.nombre = nombre;
                this.numeroHuespedes = numeroHuespedes;
                this.numeroHabitacion = numeroHabitacion;
                this.claseHabitacion = claseHabitacion;
                this.nochesEstadia = nochesEstadia;
                this.metodoPago = metodoPago;
                this.precioTotal = precioTotal;
            }

            public void MostrarInfo()
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"DNI: {dni}");
                Console.WriteLine($"Nombre: {nombre}");
                Console.WriteLine($"Números de huespedes: {numeroHuespedes}");
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
            int dni, cantidadHuespedes, cantidadMayores, cantidadMenores, claseHabitacionNum, nochesEstadia, opcionPagoNum, numeroHabitacion;
            string nombre, claseHabitacion = "", metodoPago = "";
            decimal precioBase = 0, precioTotal;


            if (contador < huespedes.Length)
            {
                Console.WriteLine("=== AGREGAR NUEVO CLIENTE ===\n");


                Console.Write("Ingrese el DNI del huesped: ");
                dni = intParseo(Console.ReadLine(), false, "DNI inválido, intente de nuevo: ");

                Console.Write("Ingrese el nombre del huesped: ");
                nombre = stringParseo(Console.ReadLine(), false, "Nombre inválido, intente de nuevo: ");

                do
                {
                    Console.Write("Ingrese la cantidad de huespedes (máximo 4): ");
                    cantidadHuespedes = intParseo(Console.ReadLine(), false, "Numero inválido, intente de nuevo: ");
                    if (cantidadHuespedes < 1 || cantidadHuespedes > 4)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Cantidad inválida. Máximo 4 personas, minimo 1.");
                        Console.ResetColor();
                    }

                } while (cantidadHuespedes < 1 || cantidadHuespedes > 4);

                do
                {
                    Console.Write("¿Cuántos son mayores de edad?: ");
                    cantidadMayores = intParseo(Console.ReadLine(), false, "Numero inválido, intente de nuevo: ");
                    if (cantidadMayores < 0 || cantidadMayores > cantidadHuespedes)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Cantidad inválida. Ingrese el numero nuevamente");
                        Console.ResetColor();
                    }

                } while (cantidadMayores < 0 || cantidadMayores > cantidadHuespedes);

                do
                {
                    Console.Write("¿Cuántos son menores de edad?: ");
                    cantidadMenores = intParseo(Console.ReadLine(), false, "Numero inválido, intente de nuevo: ");
                    if (cantidadMenores < 0 || cantidadMenores > cantidadHuespedes - cantidadMayores)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Cantidad inválida. Ingrese el numero nuevamente");
                        Console.ResetColor();
                    }

                } while (cantidadMenores < 0 || cantidadMenores > cantidadHuespedes - cantidadMayores);

                do
                {
                    Console.Write("Ingrese el número de habitación (1-20): ");
                    numeroHabitacion = intParseo(Console.ReadLine(), false, "Número inválido, intente de nuevo (1-20): ");
                    if (numeroHabitacion < 1 || numeroHabitacion > 20)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Número de habitación inválido. Ingrese un número entre 1 y 20.");
                        Console.ResetColor();
                    }
                    else
                    {
                        // Verifica si la habitación está disponible para las noches de estadía (máximo 7 noches)
                        bool disponible = true;
                        for (int d = 0; d < 7; d++)
                        {
                            if (disponibilidad[numeroHabitacion - 1, d])
                            {
                                disponible = false;
                                break;
                            }
                        }
                        if (!disponible)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("La habitación está ocupada. Por favor, elija otra.");
                            Console.ResetColor();
                            numeroHabitacion = -1; // Forzar repetición del bucle
                        }
                    }
                } while (numeroHabitacion < 1 || numeroHabitacion > 20);

                do
                {
                    Console.WriteLine("\nTipos de habitación:");
                    Console.WriteLine($"1) Económica $35000");
                    Console.WriteLine($"2) Estándar $45000");
                    Console.WriteLine($"3) Deluxe $50000");
                    Console.Write("Seleccione el tipo de habitacion: ");
                    claseHabitacionNum = intParseo(Console.ReadLine(), false, "Tipo inválido, intente de nuevo: ");
                    if (claseHabitacionNum == 1)
                    {
                        claseHabitacion = "Economica";
                        precioBase = 35000;
                    }
                    else if (claseHabitacionNum == 2)
                    {
                        claseHabitacion = "Estandar";
                        precioBase = 45000;
                    }
                    else if (claseHabitacionNum == 3)
                    {
                        claseHabitacion = "Deluxe";
                        precioBase = 50000;
                    }
                    else if (claseHabitacionNum < 1 || claseHabitacionNum > 3)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Tipo inválido. Ingrese el numero nuevamente");
                        Console.ResetColor();
                    }

                } while (claseHabitacionNum < 1 || claseHabitacionNum > 3);

                do
                {
                    Console.Write("Ingrese las noches de estadía: ");
                    nochesEstadia = intParseo(Console.ReadLine(), true, "Ingrese las noches de estadía: ");
                    if (nochesEstadia < 1)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Noches inválidas. Ingrese el numero nuevamente");
                        Console.ResetColor();
                    }

                } while (nochesEstadia < 1);

                precioTotal = precioBase * nochesEstadia;

                do
                {
                    Console.WriteLine("\nMétodo de pago: ");
                    Console.WriteLine("1) Efectivo");
                    Console.WriteLine("2) Tarjeta de debito");
                    Console.WriteLine("3) Tarjeta de crédito");
                    Console.WriteLine("4) Transferencia");
                    Console.Write("Seleccione el tipo: ");
                    opcionPagoNum = intParseo(Console.ReadLine(), false, "Numero invalido, intente de nuevo: ");
                    if (opcionPagoNum == 1)
                    {
                        metodoPago = "Efectivo";
                    }
                    else if (opcionPagoNum == 2)
                    {
                        metodoPago = "Tarjeta de débito";
                    }
                    else if (opcionPagoNum == 3)
                    {
                        metodoPago = "Tarjeta de crédito";
                    }
                    else if (opcionPagoNum == 4)
                    {
                        metodoPago = "Transferencia";
                    }
                    else if (opcionPagoNum < 1 || opcionPagoNum > 4)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Tipo inválido. Ingrese el numero nuevamente");
                        Console.ResetColor();
                    }

                } while (opcionPagoNum < 1 || opcionPagoNum > 4);


                huespedes[contador] = new Huesped(dni, nombre, cantidadHuespedes, numeroHabitacion, claseHabitacion, nochesEstadia, metodoPago, precioTotal);
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
            int eliminarId;


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
                    if (huespedesCopia[j].dni > huespedesCopia[j + 1].dni)
                    {
                        Huesped temp = huespedesCopia[j];
                        huespedesCopia[j] = huespedesCopia[j + 1];
                        huespedesCopia[j + 1] = temp;
                    }
                }
            }

            Console.WriteLine("=== ELIMINAR CLIENTE ===\n");
            Console.Write("Ingrese el DNI del huésped a eliminar: ");
            eliminarId = intParseo(Console.ReadLine(), false, "DNI inválido, intente de nuevo: ");

            // Búsqueda binaria
            int inicio = 0, fin = contador - 1;
            bool encontrado = false;
            int indice = -1;

            while (inicio <= fin && !encontrado)
            {
                int medio = (inicio + fin) / 2;

                if (huespedesCopia[medio].dni == eliminarId)
                {
                    encontrado = true;
                    indice = medio;
                }
                else if (huespedesCopia[medio].dni < eliminarId)
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
                if (huespedes[i].dni == huespedesCopia[indice].dni)
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
            int idBusqueda, opcionModificar, nuevoNumeroHuespedes, nuevoMayores, nuevoMenores, nuevoNumero, nuevasNoches;


            if (contador == 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("No hay huéspedes registrados.");
                Console.ResetColor();
                return;
            }

            Console.WriteLine("=== MODIFICAR CLIENTE ===\n");
            Console.Write("Ingrese el DNI del huésped a modificar: ");
            idBusqueda = intParseo(Console.ReadLine(), false, "DNI inválido, intente de nuevo: ");

            int indice = -1;
            for (int i = 0; i < contador; i++)
            {
                if (huespedes[i].dni == idBusqueda)
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


            do
            {
                Console.WriteLine("\n¿Qué desea modificar?");
                Console.WriteLine("1) Nombre");
                Console.WriteLine("2) Cantidad de huespedes");
                Console.WriteLine("3) Número de habitación");
                Console.WriteLine("4) Noches de estadía");
                Console.WriteLine("5) Método de pago");
                Console.Write("Seleccione el dato a modificar: ");
                opcionModificar = intParseo(Console.ReadLine(), false, "Opción inválida, intente de nuevo: ");
                switch (opcionModificar)
                {
                    case 1:
                        Console.Write("Ingrese el nuevo nombre: ");
                        huespedes[indice].nombre = stringParseo(Console.ReadLine(), false, "Nombre inválido, intente de nuevo: ");
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("Nombre modificado con éxito.");
                        Console.ResetColor();
                        break;

                    case 2:
                        do
                        {
                            Console.Write("¿Cuántas personas van a hospedarse? (máximo 4): ");
                            nuevoNumeroHuespedes = intParseo(Console.ReadLine(), false, "Cantidad inválida, intente de nuevo: ");
                            if (nuevoNumeroHuespedes < 1 || nuevoNumeroHuespedes > 4)
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("Cantidad inválida. Máximo 4 personas.");
                                Console.ResetColor();
                            }

                        } while (nuevoNumeroHuespedes < 1 || nuevoNumeroHuespedes > 4);

                        do
                        {
                            Console.Write("¿Cuántos son mayores de edad?: ");
                            nuevoMayores = intParseo(Console.ReadLine(), false, "Cantidad inválida, intente de nuevo: ");
                            if (nuevoMayores < 0 || nuevoMayores > nuevoNumeroHuespedes)
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("Cantidad inválida. Ingrese el numero nuevamente");
                                Console.ResetColor();
                            }
                        } while (nuevoMayores < 0 || nuevoMayores > nuevoNumeroHuespedes);

                        do
                        {
                            Console.Write("¿Cuántos son menores de edad?: ");
                            nuevoMenores = intParseo(Console.ReadLine(), false, "Cantidad inválida, intente de nuevo: ");
                            if (nuevoMenores < 0 || nuevoMenores > nuevoNumeroHuespedes - nuevoMayores)
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("Cantidad inválida. Ingrese el numero nuevamente");
                                Console.ResetColor();
                            }
                        } while (nuevoMenores < 0 || nuevoMenores > nuevoNumeroHuespedes - nuevoMayores);

                        huespedes[indice].numeroHuespedes = nuevoNumeroHuespedes;

                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine($"Huéspedes modificados: {nuevoNumeroHuespedes} (Mayores: {nuevoMayores}, Menores: {nuevoMenores})");
                        Console.ResetColor();
                        break;

                    case 3:
                        do
                        {
                            Console.Write("Nuevo número de habitación (1-20): ");
                            nuevoNumero = intParseo(Console.ReadLine(), true, "Número inválido, intente de nuevo (1-20): ");

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

                        } while (nuevoNumero > 0 && nuevoNumero <= 20);
                        break;

                    case 4:
                        do
                        {
                            Console.Write("Nuevas noches: ");
                            nuevasNoches = intParseo(Console.ReadLine(), false, "Número inválido, intente de nuevo: ");
                            if (nuevasNoches > 0)
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

                        } while (nuevasNoches < 1);
                        break;

                    case 5:
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

            } while (opcionModificar < 1 || opcionModificar > 5);

        }


        static void BuscarHuespedPorNombre(Huesped[] huespedes, int contador)
        {
            Console.WriteLine("=== BUSCAR CLIENTE ===\n");
            Console.Write("Ingrese el nombre a buscar: ");
            string nombreBuscar = stringParseo(Console.ReadLine(), false, "Nombre no valido, intente de vuelta: ");

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
                    decimal Economica = 35000;
                    return Economica;
                case "Estandar":
                    decimal Estandar = 45000;
                    return Estandar;
                case "Deluxe":
                    decimal Deluxe = 50000;
                    return Deluxe;
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