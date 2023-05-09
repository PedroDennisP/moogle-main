

namespace MoogleEngine
{
    class Ordenar
    {
        
        public static SearchItem[] Sort(SearchItem[] items)
        {
            
            SearchItem[] OItems = new SearchItem[items.Length];
            for (int i = 0; i < OItems.Length; ++i) OItems[i] = new SearchItem(items[i]);

           
            for (int i = 0; i < OItems.Length; ++i)
            {
                for (int j = i; j > 0 && OItems[j].Score > OItems[j - 1].Score; --j)
                {
                    SearchItem c = new SearchItem(OItems[j]);
                    OItems[j] = OItems[j - 1];
                    OItems[j - 1] = c;
                }
            }

            return OItems;
        }
    }
}
