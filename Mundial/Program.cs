using System;
using System.Collections.Generic;
using Dominio;

namespace Mundial
{
    class Program
    {
        static void Main(string[] args)
        {
            bool salir = false;
            Sistema sistema = Sistema.ObtenerInstancia;
            Console.Clear();
            while(!salir)
            {
                
                Console.WriteLine("Bienvenidos al Mundial Qatar2022! ! !");
                Console.WriteLine("");
                Console.WriteLine("Porfavor ingrese una opcion para continuar...");
                Console.WriteLine("");
                Console.WriteLine("###Seccion Periodistas###");
                Console.WriteLine("1-Dar de alta a periodista");
                Console.WriteLine("2-Listar Periodista");
                Console.WriteLine("");
                Console.WriteLine("###Seccion Jugadores###");
                Console.WriteLine("3-Listar partidos de jugador");
                Console.WriteLine("4-Ingresar Monto Referencia para categorias de jugadores");
                Console.WriteLine("5-Listar jugadores expulsados");
                Console.WriteLine("6-Listar jugadores que hayan convertido gol");
                Console.WriteLine("");
                Console.WriteLine("###Seccion Selecciones###");
                Console.WriteLine("7-Mostrar partido con mas goles, Busqueda por seleccion");
                Console.WriteLine("0-Salir");
                Console.WriteLine("");
                Console.WriteLine("Ingrese una opcion: ");
                bool correcto = int.TryParse(Console.ReadLine(), out int opcion);
                if (!correcto)
                {
                    Console.WriteLine("Opcion invalida. Debe ingresar un valor numerico");
                }
                else
                {
                    switch (opcion)
                    {
                        case 1:
                            CrearPeriodista(sistema);
                            break;
                        case 2:
                            MostrarPeriodistas(sistema);
                            break;
                        case 3:
                            ListarPartidosJugador(sistema);
                            break;
                        case 4:
                            CambiarMontoCategoria(sistema);
                            break;
                        case 5:
                            ListarJugadoresExpulsados(sistema);
                            break;
                        case 6:
                            ListarJugadoresGol(sistema);
                            break;
                        case 7:
                            SeleccionPartidoMasGoles(sistema);
                            break;
                        case 0:
                            Console.WriteLine("Saliendo del sistema...");
                            salir = true;
                            break;
                        default:
                            Console.WriteLine("Ingresa una de las opciones validas! ! !");
                            Console.WriteLine("-----------------------------------------");
                            break;
                    }

                }
                
            } 
            
        }

        public static void CrearPeriodista(Sistema sistema)
        {
            Console.Clear();
            try
            {
                Console.WriteLine("###Registro de periodista###");
                Console.WriteLine("");
                Console.WriteLine("Ingrese nombre: ");
                string nombre = Console.ReadLine();
                Console.WriteLine("Ingrese apellido: ");
                string apellido = Console.ReadLine();
                Console.WriteLine("Ingrese email: ");
                string email = Console.ReadLine();
                Console.WriteLine("Ingrese contrasenia: ");
                string password = Console.ReadLine();
                Console.WriteLine("Repita la contrasenia: ");
                string rePassword = Console.ReadLine();
                if (password == rePassword)
                {
                    Periodista periodista = new Periodista(nombre,apellido, email, password);
                    sistema.AltaPeriodista(periodista);
                    Console.WriteLine("Registro exitoso! ! !");
                }else
                {
                    Console.WriteLine("Contrasenias diferentes");
                }

                Console.WriteLine("");
            } catch (Exception e) {
                Console.WriteLine("");
                Console.WriteLine(e.Message);
            }
            Console.WriteLine("Presiona enter para continuar...");
            Console.ReadLine();
            Console.WriteLine("---------------------------------");
            Console.Clear();
            
        }
        public static void MostrarPeriodistas(Sistema sistema)
        {
            Console.Clear();
            Console.WriteLine("###Lista de Periodistas###");
            Console.WriteLine("");
            List<Periodista> periodistas = sistema.ObtenerPeriodistas();
            foreach(Periodista p in periodistas)
            {
                Console.WriteLine(p.ToString());
                Console.WriteLine("---------------------------------");
            }
            Console.WriteLine("");
            Console.WriteLine("Presiona enter para continuar...");
            Console.ReadLine();
            Console.WriteLine("---------------------------------");
            Console.Clear();
        }
        public static void CambiarMontoCategoria(Sistema sistema)
        {
            Console.Clear();
            try
            {
                Console.WriteLine("###Cambio de categoria financiera###");
                Console.WriteLine("");
                Console.WriteLine("Ingresa el nuevo monto: ");
                int monto = int.Parse(Console.ReadLine());           
                sistema.CambiarMontoJugador(monto);
                Console.WriteLine("El monto de categoria se actualizo correctamente");
                Console.WriteLine($"El monto de categoria es: {monto}");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            Console.WriteLine("");
            Console.WriteLine("Presiona enter para continuar...");
            Console.ReadLine();
            Console.WriteLine("---------------------------------");
            Console.Clear();
        }
        public static void ListarPartidosJugador(Sistema sistema)
        {
            try
            {
                Console.Clear();
                Console.WriteLine("###Busqueda de Partidos por Id Jugador###");
                Console.WriteLine("");
                Console.WriteLine("Ingrese el id del jugador");
                Console.WriteLine("");
                int idJugador = int.Parse(Console.ReadLine());
                List<Partido> partidosParticipados = sistema.ObtenerPartidosJugadorPorId(idJugador);
                foreach (Partido p in partidosParticipados)
                {
                    Console.WriteLine(p.ToString());
                }
                Console.WriteLine("");
                Console.WriteLine("Presiona enter para continuar...");
                Console.ReadLine();
                Console.WriteLine("---------------------------------");
            }       
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
            Console.Clear();
        }
        public static void ListarJugadoresExpulsados(Sistema sistema)
        {
            Console.Clear();
            try
            {
                Console.WriteLine("###Jugadores Expulsados###");
                Console.WriteLine("");
                List<Jugador> jugadores = sistema.OrdenarPorValor(sistema.ObtenerJugadoresExpulsados());
                foreach (Jugador j in jugadores)
                {
                    Console.WriteLine(j.ToString());
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
            Console.WriteLine("");
            Console.WriteLine("Presiona enter para continuar...");
            Console.ReadLine();
            Console.WriteLine("---------------------------------");
            Console.Clear();
        }
        public static void ListarJugadoresGol(Sistema sistema)
        {

            try
            {
                Console.Clear();
                Console.WriteLine("###Jugadores que convirtieron gol###");
                Console.WriteLine("");
                Console.WriteLine("Ingrese Id del partido");
                int idPartido = int.Parse(Console.ReadLine());
                List<Jugador> jugadores = sistema.OrdenarPorValor(sistema.ObtenerJugadoresConGolPorIdPartido(idPartido));
                foreach (Jugador j in jugadores)
                {
                    Console.WriteLine(j.ToString());
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
            Console.WriteLine("");
            Console.WriteLine("Presiona enter para continuar...");
            Console.ReadLine();
            Console.WriteLine("---------------------------------");
            Console.Clear();
        }
        public static void SeleccionPartidoMasGoles(Sistema sistema)
        {
            
                Console.Clear();
                Console.WriteLine("###Partido con mas Goles");
                Console.WriteLine("");
            try
            {
                Console.WriteLine("Ingresa el nombre de la seleccion para buscar: ");
                Console.WriteLine("");
                string nombreSeleccion = Console.ReadLine();
                Console.WriteLine("");
                Console.WriteLine(sistema.ObtenerPartidoConMasGoles(nombreSeleccion));
                Console.ReadLine();

                Console.WriteLine("");
                Console.WriteLine("Presiona enter para continuar...");
                Console.ReadLine();
                Console.WriteLine("---------------------------------");
            }          
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
            Console.Clear();
        }
    }
}
