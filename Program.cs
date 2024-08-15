using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace proy3parc
{
    internal class Program
    {
        
            private static int[,] ultimaMatriz;

        static void Main()

        {

            //EL BUCLE WHILE PERMITE QUE EL PROGRAMA CONTINUE EJECUTÁNDOSE

            //HASTA QUE SE SELECCIONE LA OPCIÓN 5 QUE ES SALIR

            while (true)

            {

                //MUESTRA EL MENÚ DE OPCIONES A ELEGIR

                Console.WriteLine("\nMenú de Operaciones con Matrices");

                Console.WriteLine("1. Sumar Matrices");

                Console.WriteLine("2. Restar Matrices");

                Console.WriteLine("3. Multiplicar Matrices");

                Console.WriteLine("4. Mostrar Última Matriz Resultante");

                Console.WriteLine("5. Salir");

                Console.WriteLine("----------------------------------------");

                Console.Write("Seleccione una opción: ");

                //LEE LA OPCIÓN ELEGIDA POR EL USUARIO

                string opcion = Console.ReadLine();

                //EJECUTA LA ACCIÓN CORRESPONDIENTE DEPENDIENDO LA OPCIÓN ELEGIDA

                //LLAMADA A LAS FUNCIONES

                switch (opcion)

                {

                    case "1":

                        RealizarOperacion("Suma");

                        break;

                    case "2":

                        RealizarOperacion("Resta");

                        break;

                    case "3":

                        RealizarOperacion("Multiplicación");

                        break;

                    case "4":

                        MostrarUltimaMatriz();

                        break;

                    case "5":

                        Console.WriteLine("¡Hasta luego!");

                        Console.ReadKey();

                        return;

                    //MUESTRA UN MENSAJE EN PANTALLA EN CASO

                    //DE QUE LA OPCIÓN ELEGIDA NO SEA VÁLIDA

                    default:

                        Console.WriteLine("Opción no válida. Intente de nuevo.");

                        break;

                }

            }

        }


        static void RealizarOperacion(string tipoOperacion)

        {

            int filas1, columnas1, filas2, columnas2;

            int[,] matriz1, matriz2;


            // SOLICITA DIMENSIONES DE LA PRIMERA MATRIZ

            Console.WriteLine($"Ingrese las dimensiones de la primera matriz para la {tipoOperacion}:");

            (filas1, columnas1) = SolicitarDimensiones();


            // SOLICITA DIMENSIONES DE LA SEGUNDA MATRIZ

            Console.WriteLine($"Ingrese las dimensiones de la segunda matriz para la {tipoOperacion}:");

            (filas2, columnas2) = SolicitarDimensiones();


            //VERIFICA QUE LAS DIMENSIONES DE LAS MATRICES SEAN CORRECTAS

            if (tipoOperacion == "Suma" || tipoOperacion == "Resta")

            {

                if (filas1 != filas2 || columnas1 != columnas2)

                {

                    Console.WriteLine("Las dimensiones de las matrices deben ser iguales para suma y resta.");

                    return;

                }

            }

            else if (tipoOperacion == "Multiplicación")

            {

                if (columnas1 != filas2)

                {

                    Console.WriteLine("El número de columnas de la primera matriz debe ser igual al número de filas de la segunda matriz para multiplicación.");

                    return;

                }

            }


            //LAMA A SolicitarMatriz PARA OBTENER LOS DATOS DE LAS DOS MATRICES

            Console.WriteLine("Ingrese los elementos de la primera matriz:");

            matriz1 = SolicitarMatriz(filas1, columnas1);


            Console.WriteLine("Ingrese los elementos de la segunda matriz:");

            matriz2 = SolicitarMatriz(filas2, columnas2);


            //REALIZA LA OPERACIÓN, DEPENDE EL TIPO DE OPERACIÓN, LLAMA A LA FUNCIÓN CORRESPONDIENTE

            int[,] resultado = null;

            switch (tipoOperacion)

            {

                case "Suma":

                    resultado = SumarMatrices(matriz1, matriz2);

                    break;

                case "Resta":

                    resultado = RestarMatrices(matriz1, matriz2);

                    break;

                case "Multiplicación":

                    resultado = MultiplicarMatrices(matriz1, matriz2);

                    break;

            }


            //MUESTRA LA MAZTRIZ RESULTANTE

            Console.WriteLine("Resultado:");

            MostrarMatriz(resultado);


            //GUARDA LA MATRIZ RESULTANTE EN UN ARCHIVO DE TEXTO

            string archivo = "matriz_resultado.txt";

            GuardarMatrizEnArchivo(resultado, archivo);


            //GUARDA EL RESULTADO EN ultimaMatriz PARA FUTURAS CONSULTAS

            ultimaMatriz = resultado;

        }

        //SOLICITA AL USUARIO EL NÚMERO DE FILAS Y COLUMNAS DE UNA MATRIZ

        //VALIDA QUE LOS DATOS INGRESADOS SEAN NÚMEROS ENTEROS POSITIVOS 

        //SI EL USUARIO INGRESA DATOS NO VALIDOS, SE SOLICITAN NUEVAMENTE HASTA QUE EL USUARIO INGRESE DATOS CORRECTOS 

        static (int, int) SolicitarDimensiones()

        {

            int filas, columnas;

            while (true)

            {

                Console.Write("Número de filas: ");

                if (int.TryParse(Console.ReadLine(), out filas) && filas > 0)

                {

                    Console.Write("Número de columnas: ");

                    if (int.TryParse(Console.ReadLine(), out columnas) && columnas > 0)

                    {

                        return (filas, columnas);

                    }

                }

                Console.WriteLine("Las dimensiones deben ser números enteros positivos. Intente de nuevo.");

            }

        }

        //SOLICITA AL USUARIO QUE INGRESE LOS ELEMENTOS DE CADA FILA DE UNA MATRIZ

        //VALIDA QUE LA CANTIDAD DE ELEMENTOS EN CADA FILA COINCIDA CON EL NÚMERO DE COLUMNAS Y QUE LOS VALORES INGRESADOS SEAN ENTEROS

        //SI LOS DATOS INGRESADOS NO SON CORRECTOS, VUELVE A PEDIR LA INTRODUCCIÓN DE DATOS

        static int[,] SolicitarMatriz(int filas, int columnas)

        {

            int[,] matriz = new int[filas, columnas];

            for (int i = 0; i < filas; i++)

            {

                while (true)

                {

                    Console.WriteLine($"Ingrese los {columnas} elementos de la fila {i + 1}, separados por espacios:");

                    string[] entrada = Console.ReadLine().Split();

                    if (entrada.Length == columnas)

                    {

                        bool valido = true;

                        for (int j = 0; j < columnas; j++)

                        {

                            if (int.TryParse(entrada[j], out int valor))

                            {

                                matriz[i, j] = valor;

                            }

                            else

                            {

                                Console.WriteLine("Todos los elementos deben ser números enteros. Intente de nuevo.");

                                valido = false;

                                break;

                            }

                        }

                        if (valido) break;

                    }

                    else

                    {

                        Console.WriteLine($"Debe ingresar exactamente {columnas} elementos.");

                    }

                }

            }

            return matriz;

        }

        //SUMA LOS ELEMENTOS CORRESPONDIENTES DE DOS MATRICES Y DEVUELVE LA MATRIZ RESULTANTE

        static int[,] SumarMatrices(int[,] m1, int[,] m2)

        {

            int filas = m1.GetLength(0);

            int columnas = m1.GetLength(1);

            int[,] resultado = new int[filas, columnas];

            for (int i = 0; i < filas; i++)

            {

                for (int j = 0; j < columnas; j++)

                {

                    resultado[i, j] = m1[i, j] + m2[i, j];

                }

            }

            return resultado;

        }

        //RESTA LOS ELEMENTOS CORRESPONDIENTES DE DOS MATRICES Y DEVUELVE LA MATRIZ RESULTANTE

        static int[,] RestarMatrices(int[,] m1, int[,] m2)

        {

            int filas = m1.GetLength(0);

            int columnas = m1.GetLength(1);

            int[,] resultado = new int[filas, columnas];

            for (int i = 0; i < filas; i++)

            {

                for (int j = 0; j < columnas; j++)

                {

                    resultado[i, j] = m1[i, j] - m2[i, j];

                }

            }

            return resultado;

        }

        //MULTIPLICA DOS MATRICES Y DEVUELVE LA MATRIZ RESULTANTE

        //PARA REALIZAR LA MULTIPLICACIÓN DE MATRICES SE DEBE SEGUIR LA REGLA 

        //DE QUE EL NÚMERO DE COLUMNAS DE LA 1° MATRIZ DEBE COINCIDIR CON EL NÚMERO

        //DE FILAS EN LA SEGUNDA

        static int[,] MultiplicarMatrices(int[,] m1, int[,] m2)

        {

            int filas1 = m1.GetLength(0);

            int columnas1 = m1.GetLength(1);

            int filas2 = m2.GetLength(0);

            int columnas2 = m2.GetLength(1);

            int[,] resultado = new int[filas1, columnas2];

            for (int i = 0; i < filas1; i++)

            {

                for (int j = 0; j < columnas2; j++)

                {

                    resultado[i, j] = 0;

                    for (int k = 0; k < columnas1; k++)

                    {

                        resultado[i, j] += m1[i, k] * m2[k, j];

                    }

                }

            }

            return resultado;

        }

        //IMPRIME EN LA CONSOLA UNA MATRIZ, MOSTRANDOLA FILA POR FILA

        static void MostrarMatriz(int[,] matriz)

        {

            for (int i = 0; i < matriz.GetLength(0); i++)

            {

                for (int j = 0; j < matriz.GetLength(1); j++)

                {

                    Console.Write(matriz[i, j] + " ");

                }

                Console.WriteLine();

            }

        }

        //GUARDA LA MATRIZ RESULTANTE EN UN ARCHIVO DE TEXTO, CON CADA ELEMENTO SEPARADO POR ESPACIOS Y CADA FILA EN UNA LÍNEA DIFERENTE

        static void GuardarMatrizEnArchivo(int[,] matriz, string nombreDelArchivo)

        {

            using (StreamWriter archivo = new StreamWriter(nombreDelArchivo))

            {

                for (int i = 0; i < matriz.GetLength(0); i++)

                {

                    for (int j = 0; j < matriz.GetLength(1); j++)

                    {

                        archivo.Write(matriz[i, j] + " ");

                    }

                    archivo.WriteLine();

                }

            }

            Console.WriteLine($"La Matriz Resultante del Calculo Elegido fue almacenada en el archivo {nombreDelArchivo}.");

        }

        //MUESTRA LA ÚLTIMA MATRIZ RESULTANTE QUE SE ENCUENTRA ALMACENADA EN ultimaMatriz, si es que existe

        //SI NO EXISTE NINGUNA, MUESTRA EN PANTALLA UN MENSAJE INDICANDO QUE NO HAY MATRIZ RESULTANTE 

        static void MostrarUltimaMatriz()

        {

            if (ultimaMatriz != null)

            {

                Console.WriteLine("Última Matriz Resultante:");

                MostrarMatriz(ultimaMatriz);

            }

            else

            {

                Console.WriteLine("No hay ninguna matriz resultante para mostrar.");

            }

            Console.ReadKey();

        }


    }
}

    
