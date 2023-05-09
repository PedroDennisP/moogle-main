namespace MoogleEngine;
using System;


public class Sugestion
{
	
	public Sugestion()
	{

	}
	
	public static int Leve(string A, string B)
	{
		int[,] Distancia = new int[A.Length+1, B.Length+1];
		int costo = 0;
		for (int i = 0; i <= A.Length;i++) Distancia[i, 0] = i ;
		for (int i = 0; i <= B.Length; i++) Distancia[0, i] = i;
		for(int i=1;i<= A.Length; i++)
        {
			for(int j=1;j<= B.Length; j++)
            {
				if (A[i - 1] == B[j - 1])
				{
					costo = 0;
					
				}

				else 
					costo = 1;

				Distancia[i,j] = Math.Min(Math.Min(Distancia[i,j-1]+1,Distancia[i-1,j]+1),Distancia[i-1,j-1]+costo);

            }
        }
		return Distancia[A.Length, B.Length];
	}
	public static string SugestionResult(List<string> Busqueda,List<string> Palabras)
	{
		string resultado = "";
		
		for(int i = 0; i < Busqueda.Count; i++)
        {
            if (Palabras.Contains(Busqueda[i])){
				resultado += Busqueda[i];
            }
            else
            {
				int p = 0;
				int Mdis = 999999999;
				for (int j = 0; j < Palabras.Count; j++)
                {
					int L = Leve(Palabras[j], Busqueda[i]);
					if (L < Mdis)
                    {
						Mdis = L;
						p = j;
                    }
					
                }
				resultado = resultado +Palabras[p];
				
            }
			if (i != Busqueda.Count - 1) resultado += " ";
        }

		
		return resultado;

	}
}
