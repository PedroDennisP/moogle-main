namespace MoogleEngine
{

    public class TF_IDF
    {
        public List<string> Title { get; private set; }
        public List<string>[] TheTexts { get; private set; } 
        public Dictionary<string, int> WordValue { get; private set; }

        public List<double[]> TFIDF { get; private set; }

        int TamFil = 0;


        public List<double> vector_idf { get; private set; }
        public string Ruta { get; private set; }



        public TF_IDF(string ruta)
        {
            this.Title = new List<string>();
            this.Ruta = ruta;
            this.vector_idf = new List<double>();
            this.TFIDF = new List<double[]>();
            this.WordValue = new Dictionary<string, int>();
            string TextTemp = "";
            string[] File = Directory.GetFiles(ruta);
            this.TheTexts = new List<string>[File.Length];
            string word = "";
            int[] contador_de_palabras_por_texto = new int[File.Length];
            int p = -1;
            for(int i = 0; i < File.Length; i++)
            {
                Title.Add(File[i].Substring(ruta.Length+1));
            }
            foreach (string i in File)
            {
                p++;
                TheTexts[p] = new List<string>();
                StreamReader sr = new StreamReader(i);
                string Text = sr.ReadToEnd().ToLower();
                
                TextTemp = Text;
                TextTemp = TextTemp.Replace('.', ' '); TextTemp = TextTemp.Replace(',', ' '); TextTemp = TextTemp.Replace('!', ' '); TextTemp = TextTemp.Replace('?', ' '); TextTemp = TextTemp.Replace('+', ' '); TextTemp = TextTemp.Replace('=', ' '); TextTemp = TextTemp.Replace('-', ' '); TextTemp = TextTemp.Replace('_', ' '); TextTemp = TextTemp.Replace('*', ' '); TextTemp = TextTemp.Replace('^', ' '); TextTemp = TextTemp.Replace('{', ' '); TextTemp = TextTemp.Replace('}', ' '); TextTemp = TextTemp.Replace('(', ' '); TextTemp = TextTemp.Replace(')', ' '); TextTemp = TextTemp.Replace('%', ' '); TextTemp = TextTemp.Replace('$', ' '); TextTemp = TextTemp.Replace('@', ' '); TextTemp = TextTemp.Replace('<', ' '); TextTemp = TextTemp.Replace('>', ' '); TextTemp = TextTemp.Replace('¿', ' '); TextTemp = TextTemp.Replace('¡', ' '); TextTemp = TextTemp.Replace('"', ' '); TextTemp = TextTemp.Replace('♥', ' '); TextTemp = TextTemp.Replace('/', ' '); TextTemp = TextTemp.Replace('&', ' '); TextTemp = TextTemp.Replace('|', ' '); TextTemp = TextTemp.Replace(',', ' '); TextTemp = TextTemp.Replace('!', ' '); TextTemp = TextTemp.Replace('?', ' '); TextTemp = TextTemp.Replace('+', ' '); TextTemp = TextTemp.Replace('=', ' '); TextTemp = TextTemp.Replace('-', ' '); TextTemp = TextTemp.Replace('_', ' '); TextTemp = TextTemp.Replace('*', ' '); TextTemp = TextTemp.Replace('^', ' '); TextTemp = TextTemp.Replace('{', ' '); TextTemp = TextTemp.Replace('}', ' '); TextTemp = TextTemp.Replace('(', ' '); TextTemp = TextTemp.Replace(')', ' '); TextTemp = TextTemp.Replace('%', ' '); TextTemp.Replace('$', ' '); TextTemp = TextTemp.Replace('@', ' '); TextTemp = TextTemp.Replace('<', ' '); TextTemp = TextTemp.Replace('>', ' '); TextTemp = TextTemp.Replace('¿', ' '); TextTemp = TextTemp.Replace('¡', ' '); TextTemp = TextTemp.Replace('"', ' '); TextTemp = TextTemp.Replace('♥', ' '); TextTemp = TextTemp.Replace('/', ' '); TextTemp = TextTemp.Replace('&', ' '); TextTemp.Replace('|', ' '); TextTemp = TextTemp.Replace('[', ' '); TextTemp = TextTemp.Replace(']', ' '); TextTemp = TextTemp.Replace(':', ' '); TextTemp = TextTemp.Replace(';', ' '); TextTemp.Replace('^', ' ');

                for (int j = 0; j < TextTemp.Length; j++)
                {

                    if (TextTemp[j] == ' ' || j == TextTemp.Length - 1)
                    {
                        if (j == TextTemp.Length - 1 && TextTemp[j] != ' ')
                        {
                            word += TextTemp[j];
                        }

                        if (word == "") { continue; }
                        TheTexts[p].Add(word);
                        if (WordValue.ContainsKey(word))
                        {
                            TFIDF[WordValue[word]][p] += 1;
                            contador_de_palabras_por_texto[p]++;
                            word = "";
                        }
                        else
                        {
                            if (WordValue.Count == 0)
                            {
                                WordValue.Add(word, 0);
                            }
                            else
                            {
                                WordValue.Add(word, WordValue.Count);
                            }


                            TFIDF.Add(new double[File.Length]);
                            TFIDF[WordValue[word]][p] = 1;
                            contador_de_palabras_por_texto[p]++;
                            word = "";
                        }
                    }
                    else
                    {
                        word += TextTemp[j];
                    }
                }
                TextTemp = "";
                word = "";

            }

            int count = 0;
            for (int i = 0; i < TFIDF.Count; i++)
            {
                for (int j = 0; j < TFIDF[1].Length; j++)
                {
                    TFIDF[i][j] = TFIDF[i][j] / contador_de_palabras_por_texto[j];
                    if (TFIDF[i][j] != 0)
                    {
                        count++;
                    }
                }
                vector_idf.Add((Math.Log(1 + (TFIDF[i].Length / count))));
              
                
                for (int j = 0; j < TFIDF[1].Length; j++)
                {
                 
                    
                    TFIDF[i][j] = TFIDF[i][j] * vector_idf[i];
                   
                    
                }
                count = 0;
                
            }

        }
    }
}
