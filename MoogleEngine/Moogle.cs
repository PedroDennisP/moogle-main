//Pedro Dennis Perez Marquez C121
namespace MoogleEngine;


public static class Moogle
{
    public static TF_IDF Datos = new TF_IDF(@"../Content");
    public static SearchResult Query(string pepe)
    {
        // Modifique este método para responder a la búsqueda
       
        List<string> PalabrasTotales = new List<string>();
        foreach (string key in Datos.WordValue.Keys) PalabrasTotales.Add(key);
        string query = "";
        for (int i = 0; i < pepe.Length; i++)
        {
            query += pepe[i];
        }
        query = query.ToLower();
        List<string> QueryPalabras = new List<string>();
        bool[] Obligado=new bool[Datos.WordValue.Count];
        double[] presencia = new double[Datos.WordValue.Count()];
        for (int i = 0; i < presencia.Length; i++) presencia[i] = 1;
        double[] relevancia = new double[Datos.TheTexts.Length];
        for (int i = 0; i < relevancia.Length; i++) relevancia[i] = 1;

        double[] Query_Vec = new double[Datos.WordValue.Count()];
        string TempWord1 = "";
        string TempWord2 = "";
        bool ban = false;
        List<char> signos = new List<char>();
        for (int i = 0; i < query.Length; i++)
        {
        A:
            if ((query[i] == '*' || query[i] == '^' || query[i] == '!' || query[i] == '~') && TempWord1 == "")
            {
                signos.Add(query[i]);
                continue;
            }

            else if (query[i] == ' ' || ban || i==query.Length-1)
            {
                if(i == query.Length - 1)TempWord1+=query[i];
                //if(Datos.WordValue.ContainsKey(TempWord1))TempWord1 = Sugestion.SugestionResult(new List<string> { TempWord1 }, PalabrasTotales);
                //Console.WriteLine(TempWord1 + "<------ESTO");
                if (Datos.WordValue.ContainsKey(TempWord1))
                {
                    
                    Query_Vec[Datos.WordValue[TempWord1]]+=1;
                    
                    for (int j = 0; j < signos.Count; j++)
                    {
                        if (signos[j] == '*')
                        {
                            Console.WriteLine("ES LAPINGA");
                            presencia[Datos.WordValue[TempWord1]] *= 100;
                        }
                        if (signos[j] == '!')
                        {
                            presencia[Datos.WordValue[TempWord1]] *=-9;
                        }
                        if (signos[j] == '^')
                        {
                            Obligado[Datos.WordValue[TempWord1]] = true;
                        }
                        if (signos[j] == '~')
                        {
                            for (int k = 0; k < Datos.TheTexts.Length; k++)
                            {
                                int HValue = -1;
                                for (int t = 0; t < Datos.TheTexts[k].Count; t++)
                                {
                                    if (Datos.TheTexts[k][t] == TempWord1 || Datos.TheTexts[k][t] == TempWord2)
                                    {
                                        HValue = 1;
                                    }
                                    if (HValue != -1)
                                    {
                                        HValue++;
                                        if (Datos.TheTexts[k][t] == TempWord1 || Datos.TheTexts[k][t] == TempWord2) break;
                                    }


                                }
                                if (HValue == 1) HValue = -1; else HValue--;
                                relevancia[k] = 1/HValue;
                                HValue = -1;
                            }
                        }
                    }
                }

                signos.Clear();
                if (TempWord1 != "") {TempWord2 = TempWord1;QueryPalabras.Add(TempWord1);}
                TempWord1 = "";
                ban = false;
                continue;
            }
            else
            {
                if (query[i] == '@' || query[i] == '(' || query[i] == ')' || query[i] == '{' || query[i] == '}' || query[i] == '[' || query[i] == ']' || query[i] == '/' || query[i] == '-' || query[i] == '+' || query[i] == '=' || query[i] == '&' || query[i] == '%' || query[i] == '#' || query[i] == '#' || query[i] == '"' || query[i] == ':' || query[i] == '?')
                {
                    ban = true;
                    goto A;
                }
                if (query[i] == '/' || query[i] == '|' || query[i] == '<' || query[i] == '>' || query[i] == '.' || query[i] == ',' || query[i] == ';' || query[i] == '¿' || query[i] == '¡' || query[i] == '!' || query[i] == '*' || query[i] == '^' || query[i] == '~')
                {
                    ban = true;
                    goto A;
                }
                else
                {
                    TempWord1 += query[i];
                }




            }





        }
        for(int u = 0; u < Query_Vec.Length; u++)
        {
            Query_Vec[u] = Query_Vec[u] / QueryPalabras.Count();
        }
       
        List<double> TempQuery = Utilidades.MultV(Query_Vec, Datos.vector_idf);
        List<double> FQuery = Utilidades.MultV(presencia, TempQuery);
        List<double> ScoreVec = Utilidades.MultM(Datos.TFIDF, FQuery);
       
        for(int g=0; g < Obligado.Length; g++)
        {
            if (Obligado[g])
            {
                for(int t = 0; t < Datos.TheTexts.Length; t++)
                {
                   
                    if(Datos.TFIDF[g][t] == 0)
                    {
                        relevancia[t] = -9;
                    }
                }
            }
        }
        List<double> FScore = Utilidades.MultV(relevancia, ScoreVec);
        SearchItem[] items = new SearchItem[0];
        for (int h = 0; h < Datos.TheTexts.Length; h++)
        {

            
            if (FScore[h] <= 0) continue;
            int pos = 0;
            double maxRE = 0;
            for (int t = 0; t < Datos.TheTexts[h].Count; t++)
            {
                string Palabra=Datos.TheTexts[h][t];
                
                
                if(FQuery[Datos.WordValue[Palabra]] >= maxRE){
                    maxRE = FQuery[Datos.WordValue[Palabra]];
                    pos = t;
                }
            }
            int left = 0;
            int right = Datos.TheTexts[h].Count - 1;
            if(pos - 20 > 0)
                left = pos - 20;
            if (pos + 20 < Datos.TheTexts[h].Count)
                right = pos + 20;
            String MySnipet = "";
            for (int H = left; H <= right; H++) MySnipet += Datos.TheTexts[h][H] + " ";
            SearchItem pipol = new SearchItem(Datos.Title[h], MySnipet, (float)(FScore[h]));
            Array.Resize(ref items, items.Length+1);
            items[items.Length-1] = pipol;
            
        }
        
        
        
        return new SearchResult(Ordenar.Sort(items), Sugestion.SugestionResult(QueryPalabras,PalabrasTotales));
    }
}

    

