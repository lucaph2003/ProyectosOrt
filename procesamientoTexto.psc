// retorna el numero si es numero y si no lo es retorna -1
Funcion num <- esnumero(ch)
	num <- -1
	Si (ch='0' O ch='1' O ch='2' O ch='3' O ch='4' O ch='5' O ch='6' O ch='7' O ch='8' O ch='9') Entonces
		num <- ConvertirANumero(ch)
	FinSi
FinFuncion

// carga el texto a procesar en t
Funcion t <- cargartexto()
	t <- 'Este texto contiene el precio de 10 productos que se comercializan en la tienda virtual. Cabe aclarar que un precio se considera valido si esta precedido de un símbolo de $ seguido de una secuencia de números consecutivos hasta encontrar el primer blanco. No se tomará como validos aquellos precios o números que no cumplan dicha condición. Por ejemplo $12bc45 no sería un precio valido ya que la secuencia de números que sigue al símbolo de $ hasta llegar al blanco no son solamente números. En estas condiciones, los productos elegidos en esta primera etapa serian 10 dentro del rubro bebidas para hacer una  prime prueba de procesamiento. Los productos elegidos son:  Coca cola de 1litro $105 Pepsi de 1 litro retornable  $65 cerveza pilsen de 1 litro $120  jugo de naranja 1 litro $150 vino tinto don pascual 1 litro $300  vino blanco  1 litro $300  Fanta 600 $55 Coca 600 $55 jugo de uva 1 litro $155  y jugo de limón $200 '
FinFuncion

// parcea el texto a procesar y carga el array de precios
Funcion cargarprecios(precios,textoaprocesar)
	// parseo para validar el texto cargado en TEXTOAPROCESAR
	largo <- Longitud(textoaprocesar)
	num <- '' //Variable numero donde se guardara temporalmente el numero
	pos <- 1 //Poscicion del array
	Para x<-0 Hasta largo Hacer
		k <- Subcadena(textoaprocesar,x,x)
		sig <- Subcadena(textoaprocesar,x+1,x+1)  //Es el siguiente caracter
		a <- x	
		Si k='$' Y sig<>' ' Entonces      // si hay un $ y el siguiente caracter no esta vacio entonces entra
			existeNumero <- esnumero(sig)	//verifica si existe el numero viendo si la siguiente linea es un numero
			Repetir
				a <- a + 1
				k <- Subcadena(textoaprocesar,a,a)
				Si k<>' ' Entonces
					existeNumero <- esnumero(k)		//verifica si existe el numero viendo si el actual caracter es un numero
					Si existeNumero<>-1 Entonces
						num <- num+k					//Concatenamos los numeros
					SiNo
						num <- 'x'					//En caso de no ser un numero num se iguala a x y se termina el programa en la esta iteracion
					FinSi
				FinSi
			Hasta Que k=' ' O existeNumero=-1 		//Se ejecuta hasta que este vacio o no sea un numero
			Si num<>'x' Entonces					//si num es diferente a x significa que es valido
				precios[pos] <- num					//Insertamoos el numero en el array
				pos <- pos+1							//Sumamos uno a la poscicion del array
			FinSi
			num <- ''								//Igualamos a num de vuelta a vacio
			x <- a									//Igualamos x igual a la variable de trabajo a
		FinSi
	FinPara
FinFuncion

// muestra los precios almacenados en el array de precios
Funcion mostrarprecios(precios)
	// validacion mostrando los numeros recolectados en el array
	Para x<-1 Hasta 10 Hacer
		Escribir '$'+precios[x]      
	FinPara
FinFuncion

// retorna el precio de mayor valor entre tosos los almacenados en el array de precios
Funcion mayorprecio <- obtenermayorprecio(precios)
	mayorprecio <- 0
	Para x<-1 Hasta 10 Hacer
		precio <- ConvertirANumero(precios[x])
		Si precio > mayorprecio Entonces
			mayorprecio <- precio                        //Si el precio es el valor de la iteracion es mayor a la variable entonces la misma se iguala al valor de la iteracion
		FinSi
	FinPara
FinFuncion

// Calcula el promedio de los precios almacenados en el array de precios
Funcion promedio <- calcularpromedio(precios)
	total <- 0
	Para x<-1 Hasta 10 Hacer
		precio <- ConvertirANumero(precios[x])
		total <- total+precio										//Suma todos los valores del array en una variable total
	FinPara
	promedio <- total/10										//Dividiomos la variable total por cantidad de espacios en el array obteniendo asi el promedio
FinFuncion

// la funcion busca la cantidad de veces que aparece el precio de mayor valor
Funcion cantidad = cantidvecesmaximoprecio(precios,mayorprecio)
	cantidad <- 0
	Para x<-1 Hasta 10 Hacer
		precio <- ConvertirANumero(precios[x])
		Si precio=mayorprecio Entonces
			cantidad <- cantidad+1						//Si el valor actual de la iteracion es igual al precio mayor entonces el contador sube uno
		FinSi
	FinPara
FinFuncion

Algoritmo PROCESARTEXTO
	textoaprocesar <- cargartexto
	Dimension precios[10]
	Escribir textoaprocesar
	Escribir '--------------------------------------------------------------------- '
	Escribir 'Los precios encontrados  son :'
	cargarprecios(precios,textoaprocesar)
	mostrarprecios(precios)
	Escribir ' ---------------------------------------------------------------------'
	promedio <- calcularpromedio(precios)
	Escribir 'El promedio es $',promedio
	Escribir ' ---------------------------------------------------------------------'
	mayorprecio <- obtenermayorprecio(precios)
	Escribir 'El precio de mayor valor es : $',mayorprecio
	Escribir ' ---------------------------------------------------------------------'
	cantidad <- cantidvecesmaximoprecio(precios,mayorprecio)
	Escribir 'El mayor precio es $',mayorprecio,' y aparece ',cantidad,' veces'
	Escribir ' ---------------------------------------------------------------------'
FinAlgoritmo
